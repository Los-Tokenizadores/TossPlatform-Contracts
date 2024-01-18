// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossErc721GeneTest is BaseTest {
    TossErc721GeneV1 erc721;
    string constant name = "Erc 721 Gene";
    string constant symbol = "Erc 721 Symbol";

    function setUp() public override {
        super.setUp();
        erc721 = DeployWithProxyUtil.tossErc721GeneV1(name, symbol);
    }

    function test_initializationNameAndSymbol() public {
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }

    function test_upgrade() public {
        TossErc721GeneV1 erc721Init = new TossErc721GeneV1();
        assertNotEq(erc721.getImplementation(), address(erc721Init));
        erc721.upgradeToAndCall(address(erc721Init), "");
        assertEq(erc721.getImplementation(), address(erc721Init));
    }

    function test_sellErc721() public {
        erc721.grantRole(erc721.MINTER_ROLE(), owner);
        uint256[] memory genes = new uint256[](1);
        uint256 gene1 = 333_333;
        genes[0] = gene1;
        erc721.addGenes(genes);
        erc721.sellErc721(owner, 1);
        assertEq(erc721.getErc721Gene(0), gene1);
        assertEq(erc721.ownerOf(0), owner);
    }

    function test_sellErc721NotEnoughGenesRevert(uint8 amount) public {
        vm.assume(amount > 0);
        erc721.grantRole(erc721.MINTER_ROLE(), owner);
        if (amount > 1) {
            uint256[] memory genes = new uint256[](amount - 1);
            for (uint8 i = 0; i < amount - 1; i++) {
                genes[i] = uint256(i);
            }
            erc721.addGenes(genes);
        }
        vm.expectRevert(abi.encodeWithSelector(TossErc721GeneBase.TossErc721GeneNotEnoughGenes.selector, erc721.getRangeGeneLength(), amount));
        erc721.sellErc721(owner, amount);
    }

    function test_addGenes(uint8 amount) public {
        erc721.grantRole(erc721.MINTER_ROLE(), owner);
        uint256[] memory genes = new uint256[](amount);
        for (uint8 i = 0; i < amount; i++) {
            genes[i] = uint256(i);
        }
        erc721.addGenes(genes);
        uint256[] memory genesContract = erc721.getRangeGene();
        assertEq(genesContract.length, amount);
        assertEq(erc721.getRangeGeneLength(), amount);

        for (uint8 i = 0; i < amount; i++) {
            assertEq(genes[i], genesContract[i]);
        }
    }
}
