// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossErc721GeneUniqueTest is BaseTest {
    TossErc721GeneUniqueV1 erc721;
    string constant name = "Erc 721 Gene";
    string constant symbol = "Erc 721 Symbol";

    function setUp() public override {
        super.setUp();
        erc721 = DeployWithProxyUtil.tossErc721GeneUniqueV1(name, symbol);
    }

    function test_initializationNameAndSymbol() public {
        assertEq(erc721.name(), name);
        assertEq(erc721.symbol(), symbol);
    }

    function test_upgrade() public {
        TossErc721GeneUniqueV1 erc721Init = new TossErc721GeneUniqueV1();
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

    function test_addGenesUnique(uint8 amount) public {
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

    function test_addGenesIgnoreRepited(uint8 amount, uint8 extraAmount) public {
        vm.assume(amount > 0);
        vm.assume(extraAmount > 0);
        erc721.grantRole(erc721.MINTER_ROLE(), owner);

        uint256[] memory genes = addGenesIncremental(amount, 0);
        uint256[] memory genesContract = erc721.getRangeGene();
        assertEq(genesContract.length, genes.length);
        assertEq(erc721.getRangeGeneLength(), genes.length);

        uint256 totalAmount = uint256(amount) + extraAmount;
        uint256[] memory genesTotal = addGenesIncremental(totalAmount, 0);
        genesContract = erc721.getRangeGene();
        assertEq(genesContract.length, genesTotal.length);
        assertEq(erc721.getRangeGeneLength(), genesTotal.length);

        for (uint256 i = 0; i < totalAmount; i++) {
            assertEq(genesTotal[i], genesContract[i]);
        }
    }

    function addGenesIncremental(uint256 amount, uint256 baseValue) private returns (uint256[] memory) {
        uint256[] memory genes = new uint256[](amount);
        for (uint256 i = 0; i < amount; i++) {
            genes[i] = baseValue + i;
        }
        erc721.addGenes(genes);
        return genes;
    }

    function test_addGenesUseAndTryToReAdd(uint8 amount) public {
        vm.assume(amount > 2);
        erc721.grantRole(erc721.MINTER_ROLE(), owner);
        uint256[] memory genes = addGenesIncremental(amount, 0);
        uint256[] memory genesContract = erc721.getRangeGene();
        assertEq(genesContract.length, genes.length);
        assertEq(erc721.getRangeGeneLength(), genes.length);
        erc721.sellErc721(owner, 3);
        genesContract = erc721.getRangeGene();
        assertEq(genesContract.length, amount - 3);

        addGenesIncremental(amount, 0);
        genesContract = erc721.getRangeGene();
        assertEq(genesContract.length, amount - 3);
        assertEq(erc721.getRangeGeneLength(), amount - 3);

        uint256 gene1 = erc721.getErc721Gene(0);
        uint256 gene2 = erc721.getErc721Gene(1);
        uint256 gene3 = erc721.getErc721Gene(2);
        for (uint256 i = 0; i < genesContract.length; i++) {
            assertNotEq(genesContract[i], gene1);
            assertNotEq(genesContract[i], gene2);
            assertNotEq(genesContract[i], gene3);
        }
    }
}
