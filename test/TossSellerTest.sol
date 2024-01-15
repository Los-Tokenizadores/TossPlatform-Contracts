// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { ITossSellErc721 } from "../src/Interfaces/ITossSellErc721.sol";
import { IAccessControl } from "@openzeppelin/contracts/access/IAccessControl.sol";
import "./BaseTest.sol";

contract TossSellerTest is BaseTest {
    TossErc20V1 erc20;
    TossSellerV1 seller;
    TossErc721GeneV1 erc721;
    uint256 mintAmount = 10 ether;

    function setUp() public override {
        super.setUp();
        erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", mintAmount);
        seller = DeployWithProxyUtil.tossSellerV1(IERC20(address(erc20)));
        erc721 = DeployWithProxyUtil.tossErc721GeneV1("Erc721 Test", "E721T");
        erc721.grantRole(erc721.MINTER_ROLE(), address(seller));
    }

    function test_initialization() public {
        assertEq(address(seller.getErc20()), address(erc20));
    }

    function test_buyErc721() public {
        seller.setErc721Sell(erc721, 1 ether, 1);

        uint256[] memory genes = new uint256[](1);
        genes[0] = 0;
        erc721.addGenes(genes);
        erc20.approve(address(seller), 1 ether);
        seller.buyErc721(erc721, 1);
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

    function test_setErc721SellFailWithUnsupported() public {
        TossErc721MarketV1 erc721Market = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");
        vm.expectRevert(abi.encodeWithSelector(TossUnsupportedInterface.selector, "ITossSellErc721"));
        seller.setErc721Sell(ITossSellErc721(address(erc721Market)), 1 ether, 1);
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

    function test_convertToErc20(uint32 amount) public {
        amount = uint32(bound(amount, seller.getConvertToErc20MinAmount(), uint32(mintAmount * seller.getConvertToErc20Rate() / 1 ether)));
        erc20.transfer(address(seller), mintAmount);
        seller.convertToErc20(alice, amount);
        uint256 erc20Amount = uint256(amount) * 1 ether * (seller.CUT_PRECISION() - seller.getConvertToErc20Cut()) / seller.getConvertToErc20Rate() / seller.CUT_PRECISION();
        assertEq(erc20.balanceOf(alice), erc20Amount);
    }
}
