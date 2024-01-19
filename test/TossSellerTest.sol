// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { ITossSellErc721 } from "../src/Interfaces/ITossSellErc721.sol";
import { IAccessControl } from "@openzeppelin/contracts/access/IAccessControl.sol";
import "./BaseTest.sol";

contract TossSellerTest is BaseTest {
    TossErc20V1 erc20;
    TossSellerV1 seller;
    TossErc721GeneV1 erc721;
    uint256 mintAmount = 100 ether;

    function setUp() public override {
        super.setUp();
        erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", mintAmount);
        seller = DeployWithProxyUtil.tossSellerV1(IERC20(address(erc20)));
        erc721 = DeployWithProxyUtil.tossErc721GeneV1("Erc721 Test", "E721T");
        erc721.grantRole(erc721.MINTER_ROLE(), address(seller));
    }

    function test_upgrade() public {
        TossSellerV1 sellerInit = new TossSellerV1();
        assertNotEq(seller.getImplementation(), address(sellerInit));
        seller.upgradeToAndCall(address(sellerInit), "");
        assertEq(seller.getImplementation(), address(sellerInit));
    }

    function test_initialization() public {
        assertEq(address(seller.getErc20()), address(erc20));
    }

    function test_initializationWithAddressZeroRevert() public {
        address sellerImp = seller.getImplementation();
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc20"));
        new TossUpgradeableProxy(sellerImp, abi.encodeCall(TossSellerV1.__TossSellerV1_init, IERC20(address(0))));
    }

    function test_withdrawBalance(uint256 amount) public {
        amount = bound(amount, 1, mintAmount);
        seller.setErc20BankAddress(alice);
        erc20.transfer(address(seller), amount);
        seller.withdrawBalance(amount);
        assertEq(erc20.balanceOf(alice), amount);
    }

    function test_buyErc721(uint8 amount) public {
        amount = uint8(bound(amount, 1, seller.SELL_MAX_LIMIT()));
        seller.setErc721Sell(erc721, 1 ether, amount);

        uint256[] memory genes = new uint256[](amount);
        erc721.addGenes(genes);
        uint256 price = 1 ether * uint256(amount);
        erc20.approve(address(seller), price);
        seller.buyErc721(erc721, amount);
        assertEq(erc20.balanceOf(address(seller)), price);
        assertEq(erc721.balanceOf(owner), amount);
    }

    function test_buyErc721Whitelist(uint8 amount) public {
        seller.setWhitelist(address(whitelist));
        amount = uint8(bound(amount, 1, seller.SELL_MAX_LIMIT()));
        seller.setErc721Sell(erc721, 1 ether, amount);

        uint256[] memory genes = new uint256[](amount);
        erc721.addGenes(genes);
        uint256 price = 1 ether * uint256(amount);
        erc20.approve(address(seller), price);
        vm.expectRevert(abi.encodeWithSelector(TossWhitelistClient.TossWhitelistNotInWhitelist.selector, owner));
        seller.buyErc721(erc721, amount);

        whitelist.set(owner, true);
        seller.buyErc721(erc721, amount);
        assertEq(erc20.balanceOf(address(seller)), price);
        assertEq(erc721.balanceOf(owner), amount);
    }

    function test_buyErc721WhenPausedRevertUnpauseWork() public {
        seller.setErc721Sell(erc721, 1 ether, 1);

        uint256[] memory genes = new uint256[](1);
        genes[0] = 0;
        erc721.addGenes(genes);
        erc20.approve(address(seller), 1 ether);
        seller.pause();
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        seller.buyErc721(erc721, 1);
        seller.unpause();
        seller.buyErc721(erc721, 1);
        assertEq(erc721.balanceOf(owner), 1);
    }

    function test_buyErc721AmountZeroRevert() public {
        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "amount"));
        seller.buyErc721(erc721, 0);
    }

    function test_buyErc721AddressZeroRevert() public {
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc721"));
        seller.buyErc721(ITossSellErc721(address(0)), 1);
    }

    function test_buyErc721NotInSellRevert() public {
        vm.expectRevert(abi.encodeWithSelector(TossSellerBase.TossSellerNotOnSell.selector, address(1)));
        seller.buyErc721(ITossSellErc721(address(1)), 1);
    }

    function test_buyErc721AmountExceededRevert(uint8 maxAmount, uint8 randomExtra) public {
        uint8 max = seller.SELL_MAX_LIMIT();
        maxAmount = uint8(bound(maxAmount, 1, max));
        randomExtra = uint8(bound(randomExtra, 0, type(uint8).max - maxAmount - 1));
        uint8 amount = maxAmount + 1 + randomExtra;
        seller.setErc721Sell(erc721, 1 ether, maxAmount);
        vm.expectRevert(abi.encodeWithSelector(TossSellerBase.TossSellerBuyAmountGreatherThanMax.selector, address(erc721), amount, maxAmount));
        seller.buyErc721(erc721, amount);
    }

    function test_buyErc721Permit() public {
        uint256 price = 1 ether;
        seller.setErc721Sell(erc721, uint128(price), 1);

        uint256[] memory genes = new uint256[](1);
        genes[0] = 0;
        erc721.addGenes(genes);

        SigUtils.Permit memory permit = SigUtils.Permit({ owner: owner, spender: address(seller), value: price * 20, nonce: erc20.nonces(owner), deadline: 1 days });
        SigUtils sigUtils = new SigUtils(erc20.DOMAIN_SEPARATOR());
        bytes32 digest = sigUtils.getTypedDataHash(permit);
        (uint8 v, bytes32 r, bytes32 s) = vm.sign(ownerPrivateKey, digest);

        seller.buyErc721WithPermit(erc721, 1, permit.value, permit.deadline, v, r, s);
    }

    function test_setErc721Sell() public {
        seller.setErc721Sell(erc721, 2 ether, 3);
        TossSellerBase.Erc721SellInfo memory info = seller.getErc721Sells(erc721);
        assertEq(info.price, 2 ether);
        assertEq(info.maxAmount, 3);
    }

    function test_setErc721SellInvalidContractRevert() public {
        TossErc721MarketV1 erc721Market = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");
        vm.expectRevert(abi.encodeWithSelector(TossUnsupportedInterface.selector, "ITossSellErc721"));
        seller.setErc721Sell(ITossSellErc721(address(erc721Market)), 1 ether, 1);
    }

    function test_setErc721SellAddressZeroRevert() public {
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc721"));
        seller.setErc721Sell(ITossSellErc721(address(0)), 1 ether, 1);
    }

    function test_setErc721SellMaxAmountZeroRevert() public {
        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "maxAmount"));
        seller.setErc721Sell(erc721, 1 ether, 0);
    }

    function test_setErc721SellMaxAmountExceededRevert(uint8 maxAmount) public {
        uint8 limit = seller.SELL_MAX_LIMIT();
        maxAmount = uint8(bound(maxAmount, limit + 1, type(uint8).max));
        vm.expectRevert(abi.encodeWithSelector(TossSellerBase.TossSellerBuyMaxAmountExceeded.selector, maxAmount, limit));
        seller.setErc721Sell(erc721, 1 ether, maxAmount);
    }

    function test_setBankWithEmptyAddressRevert() public {
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "bank"));
        seller.setErc20BankAddress(address(0));
    }

    function test_setBank() public {
        seller.setErc20BankAddress(alice);
        assertEq(alice, seller.getErc20BankAddress());
    }

    function test_setBankFailNotAdmin() public {
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, seller.DEFAULT_ADMIN_ROLE()));
        seller.setErc20BankAddress(alice);
    }

    function test_convertToOffchain(uint256 amount) public {
        amount = bound(amount, seller.getConvertToOffchainMinAmount(), mintAmount);
        erc20.approve(address(seller), amount);
        seller.convertToOffchain(amount);
        assertEq(erc20.balanceOf(owner), mintAmount - amount);
    }

    function test_convertToOffchainLessThanMinRevert(uint256 amount) public {
        uint256 min = seller.getConvertToOffchainMinAmount();
        amount = bound(amount, 0, min - 1);
        erc20.approve(address(seller), amount);
        vm.expectRevert(abi.encodeWithSelector(TossSellerBase.TossSellConvertOffchainAmountLessThanMin.selector, amount, min));
        seller.convertToOffchain(amount);
        assertEq(erc20.balanceOf(owner), mintAmount);
    }

    function test_convertToOffchainPermit(uint256 amount) public {
        amount = bound(amount, seller.getConvertToOffchainMinAmount(), mintAmount);

        SigUtils.Permit memory permit = SigUtils.Permit({ owner: owner, spender: address(seller), value: amount * 20, nonce: erc20.nonces(owner), deadline: 1 days });
        SigUtils sigUtils = new SigUtils(erc20.DOMAIN_SEPARATOR());
        bytes32 digest = sigUtils.getTypedDataHash(permit);
        (uint8 v, bytes32 r, bytes32 s) = vm.sign(ownerPrivateKey, digest);

        seller.convertToOffchainWithPermit(amount, permit.value, permit.deadline, v, r, s);
        assertEq(erc20.balanceOf(owner), mintAmount - amount);
    }

    function test_convertToOffchainWhenPausedRevertUnpauseWork(uint256 amount) public {
        amount = bound(amount, seller.getConvertToOffchainMinAmount(), mintAmount);
        erc20.approve(address(seller), amount);
        seller.pause();
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        seller.convertToOffchain(amount);
        assertEq(erc20.balanceOf(owner), mintAmount);
        seller.unpause();
        seller.convertToOffchain(amount);
        assertEq(erc20.balanceOf(owner), mintAmount - amount);
    }

    function test_convertToErc20(uint32 amount) public {
        amount = uint32(bound(amount, seller.getConvertToErc20MinAmount(), uint32(mintAmount * seller.getConvertToErc20Rate() / 1 ether)));
        erc20.transfer(address(seller), mintAmount);
        seller.convertToErc20(alice, amount);
        uint256 erc20Amount = convertToErc20Amount(amount, seller.CUT_PRECISION(), seller.getConvertToErc20Rate(), seller.getConvertToErc20Cut());
        assertEq(erc20.balanceOf(alice), erc20Amount);
    }

    function test_convertToErc20WhenPausedRevertUnpauseWork(uint32 amount) public {
        amount = uint32(bound(amount, seller.getConvertToErc20MinAmount(), uint32(mintAmount * seller.getConvertToErc20Rate() / 1 ether)));
        erc20.transfer(address(seller), mintAmount);
        seller.pause();
        vm.expectRevert(abi.encodeWithSelector(PausableUpgradeable.EnforcedPause.selector));
        seller.convertToErc20(alice, amount);
        assertEq(erc20.balanceOf(alice), 0);
        seller.unpause();
        seller.convertToErc20(alice, amount);
        uint256 erc20Amount = convertToErc20Amount(amount, seller.CUT_PRECISION(), seller.getConvertToErc20Rate(), seller.getConvertToErc20Cut());
        assertEq(erc20.balanceOf(alice), erc20Amount);
    }

    function test_convertToErc20WithoutRoleRevert() public {
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, seller.CONVERT_ROLE()));
        seller.convertToErc20(alice, 1);
    }

    function test_convertToErc20AddressZeroRevert() public {
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "user"));
        seller.convertToErc20(address(0), 1);
    }

    function test_convertToErc20LessThanMinRevert(uint32 amount) public {
        uint32 minAmount = seller.getConvertToErc20MinAmount();
        amount = uint32(bound(amount, 0, minAmount - 1));
        vm.expectRevert(abi.encodeWithSelector(TossSellerBase.TossSellConvertErc20AmountLessThanMin.selector, amount, minAmount));
        seller.convertToErc20(alice, amount);
    }

    function convertToErc20Amount(uint32 amount, uint16 cutPrecision, uint32 convertToErc20Rate, uint16 convertToErc20Cut) private pure returns (uint256) {
        return uint256(amount) * 1 ether * (cutPrecision - convertToErc20Cut) / convertToErc20Rate / cutPrecision;
    }

    function test_setConvertToOffchainRate(uint32 newRate) public {
        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "rate"));
        seller.setConvertToOffchainRate(0);

        newRate = uint32(bound(newRate, 1, type(uint32).max));
        seller.setConvertToOffchainRate(newRate);
        assertEq(seller.getConvertToOffchainRate(), newRate);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, seller.CONVERT_ROLE()));
        seller.setConvertToOffchainRate(0);

        assertEq(seller.getConvertToOffchainRate(), newRate);
    }

    function test_setConvertToOffchainMinAmount(uint256 minAmount) public {
        vm.expectRevert(abi.encodeWithSelector(TossValueIsLessThanOneEther.selector, "min amount"));
        seller.setConvertToOffchainMinAmount(0);

        minAmount = bound(minAmount, 1 ether, type(uint256).max);
        seller.setConvertToOffchainMinAmount(minAmount);
        assertEq(seller.getConvertToOffchainMinAmount(), minAmount);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, seller.CONVERT_ROLE()));
        seller.setConvertToOffchainMinAmount(1 ether - 1);

        assertEq(seller.getConvertToOffchainMinAmount(), minAmount);
    }

    function test_setConvertToOffchainCut(uint16 newCut, uint16 newMaxCut) public {
        uint16 maxCut = seller.CUT_PRECISION();
        newMaxCut = uint16(bound(newCut, maxCut + 1, type(uint16).max));
        vm.expectRevert(abi.encodeWithSelector(TossCutOutOfRange.selector, newMaxCut));
        seller.setConvertToOffchainCut(newMaxCut);

        newCut = uint16(bound(newCut, 0, maxCut));
        seller.setConvertToOffchainCut(newCut);
        assertEq(seller.getConvertToOffchainCut(), newCut);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, seller.CONVERT_ROLE()));
        seller.setConvertToOffchainCut(0);

        assertEq(seller.getConvertToOffchainCut(), newCut);
    }

    function test_setConvertToErc20Rate(uint32 newRate) public {
        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "rate"));
        seller.setConvertToErc20Rate(0);

        newRate = uint32(bound(newRate, 1, type(uint32).max));
        seller.setConvertToErc20Rate(newRate);
        assertEq(seller.getConvertToErc20Rate(), newRate);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, seller.CONVERT_ROLE()));
        seller.setConvertToErc20Rate(1);

        assertEq(seller.getConvertToErc20Rate(), newRate);
    }

    function test_setConvertToErc20MinAmount(uint32 minAmount) public {
        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "min amount"));
        seller.setConvertToErc20MinAmount(0);

        minAmount = uint32(bound(minAmount, 1, type(uint32).max));
        seller.setConvertToErc20MinAmount(minAmount);
        assertEq(seller.getConvertToErc20MinAmount(), minAmount);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, seller.CONVERT_ROLE()));
        seller.setConvertToErc20MinAmount(1);

        assertEq(seller.getConvertToErc20MinAmount(), minAmount);
    }

    function test_setConvertToErc20Cut(uint16 newCut, uint16 newMaxCut) public {
        uint16 maxCut = seller.CUT_PRECISION();
        newMaxCut = uint16(bound(newCut, maxCut + 1, type(uint16).max));
        vm.expectRevert(abi.encodeWithSelector(TossCutOutOfRange.selector, newMaxCut));
        seller.setConvertToErc20Cut(newMaxCut);

        newCut = uint16(bound(newCut, 0, maxCut));
        seller.setConvertToErc20Cut(newCut);
        assertEq(seller.getConvertToErc20Cut(), newCut);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, seller.CONVERT_ROLE()));
        seller.setConvertToErc20Cut(0);

        assertEq(seller.getConvertToErc20Cut(), newCut);
    }
}
