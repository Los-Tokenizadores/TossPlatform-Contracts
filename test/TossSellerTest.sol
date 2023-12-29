// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { ITossSellErc721 } from "../src/Interfaces/ITossSellErc721.sol";
import "./BaseTest.sol";

contract TossSellerTest is BaseTest {
    function test_initialization() public {
        uint256 amount = 10 ether;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossSellerV1 seller = DeployWithProxyUtil.tossSellerV1(IERC20(address(erc20)));
        assertEq(address(seller.getErc20()), address(erc20));
    }

    function test_buyErc721() public {
        uint256 amount = 10 ether;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossSellerV1 seller = DeployWithProxyUtil.tossSellerV1(IERC20(address(erc20)));
        assertEq(address(seller.getErc20()), address(erc20));

        TossErc721GeneV1 erc721 = DeployWithProxyUtil.tossErc721GeneV1("Erc721 Test", "E721T");

        erc721.grantRole(erc721.MINTER_ROLE(), address(seller));
        seller.setErc721Sell(erc721, 1 ether, 1);

        uint256[] memory genes = new uint256[](1);
        genes[0] = 0;
        erc721.addGenes(genes);
        erc20.approve(address(seller), 1 ether);
        seller.buyErc721(erc721, 1);
    }

    function test_buyErc721Permit() public {
        uint256 amount = 10 ether;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossSellerV1 seller = DeployWithProxyUtil.tossSellerV1(IERC20(address(erc20)));
        assertEq(address(seller.getErc20()), address(erc20));

        TossErc721GeneV1 erc721 = DeployWithProxyUtil.tossErc721GeneV1("Erc721 Test", "E721T");

        erc721.grantRole(erc721.MINTER_ROLE(), address(seller));
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
        uint256 amount = 10 ether;
        TossErc20V1 erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", amount);
        TossSellerV1 seller = DeployWithProxyUtil.tossSellerV1(IERC20(address(erc20)));
        assertEq(address(seller.getErc20()), address(erc20));

        TossErc721MarketV1 erc721 = DeployWithProxyUtil.tossErc721MarketV1("Erc721 Test", "E721T");
        vm.expectRevert(abi.encodeWithSelector(TossUnsupportedInterface.selector, "ITossSellErc721"));
        seller.setErc721Sell(ITossSellErc721(address(erc721)), 1 ether, 1);
    }
}
