// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossErc721GeneUniqueDevTest is BaseTest {
    TossErc721GeneUniqueDevV1 erc721;
    string constant name = "Erc 721 Gene";
    string constant symbol = "Erc 721 Symbol";

    function setUp() public override {
        super.setUp();
        erc721 = DeployWithProxyUtil.tossErc721GeneUniqueDevV1(name, symbol);
    }

    function test_upgrade() public {
        TossErc721GeneUniqueDevV1 erc721Init = new TossErc721GeneUniqueDevV1();
        assertNotEq(erc721.getImplementation(), address(erc721Init));
        erc721.upgradeToAndCall(address(erc721Init), "");
        assertEq(erc721.getImplementation(), address(erc721Init));
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, erc721.UPGRADER_ROLE()));
        erc721.upgradeToAndCall(address(erc721Init), "");
    }

    function test_sellErc721AndAdminTransfer() public {
        erc721.grantRole(erc721.MINTER_ROLE(), owner);
        uint256[] memory genes = new uint256[](1);
        uint256 gene1 = 333_333;
        genes[0] = gene1;
        erc721.addGenes(genes);
        erc721.sellErc721(owner, 1);
        assertEq(erc721.getErc721Gene(0), gene1);
        assertEq(erc721.ownerOf(0), owner);

        erc721.adminTransfer(alice, 0);
        assertEq(erc721.ownerOf(0), alice);
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, erc721.DEFAULT_ADMIN_ROLE()));
        erc721.adminTransfer(owner, 0);
    }
}
