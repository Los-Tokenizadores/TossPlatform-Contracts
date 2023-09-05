// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

import "@openzeppelin/contracts-upgradeable/utils/CountersUpgradeable.sol";
import "../Interfaces/ITossSellErc721.sol";
import "./TossErc721MarketBase.sol";

abstract contract TossErc721GeneBase is ITossSellErc721, TossErc721MarketBase {
    using CountersUpgradeable for CountersUpgradeable.Counter;

    CountersUpgradeable.Counter private _tokenIdCounter;
    uint256 public nonce;
    uint256[] internal rangeOfGene;

    mapping(uint256 => uint256) private erc721Genes;

    event Created(address indexed account, uint256 indexed tokenId, uint256 indexed gene);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721GeneBase_init(string memory name_, string memory symbol_) public initializer {
        __TossErc721MarketBase_init(name_, symbol_);
    }

    function getRangeGeneLength() external view returns (uint256) {
        return rangeOfGene.length;
    }

    function getRangeGene() external view onlyRole(MINTER_ROLE) returns (uint256[] memory) {
        return rangeOfGene;
    }

    function addGenes(uint256[] memory genes) external virtual onlyRole(MINTER_ROLE) {
        for (uint256 i; i < genes.length;) {
            rangeOfGene.push(genes[i]);
            unchecked {
                ++i;
            }
        }
    }

    function getErc721Data(uint256 _id) public view returns (uint256) {
        require(_exists(_id), "Token not exist");
        return erc721Genes[_id];
    }

    function sellErc721(address _owner, uint8 _amount) external override onlyRole(MINTER_ROLE) {
        require(rangeOfGene.length >= _amount, "Insufficient genes to mint");
        for (uint8 i; i < _amount;) {
            uint256 index = _randomGeneIndex();
            require(index < rangeOfGene.length, "index of gene out of range");
            uint256 gene = rangeOfGene[index];

            uint256 id = _tokenIdCounter.current();
            _tokenIdCounter.increment();
            _safeMint(_owner, id);

            erc721Genes[id] = gene;
            _remove(index);

            emit Created(_owner, id, gene);
            unchecked {
                ++i;
            }
        }
    }

    function _burn(uint256 tokenId) internal override {
        super._burn(tokenId);
        delete erc721Genes[tokenId];
    }

    function _randomGeneIndex() private returns (uint256) {
        uint256 randomN = uint256(blockhash(block.number));
        uint256 index = uint256(keccak256(abi.encodePacked(randomN, nonce, block.prevrandao))) % rangeOfGene.length;
        nonce++;
        return index;
    }

    function _remove(uint256 _index) private {
        if (_index >= rangeOfGene.length) {
            return;
        }
        rangeOfGene[_index] = rangeOfGene[rangeOfGene.length - 1];
        rangeOfGene.pop();
    }

    /**
     * @dev This empty reserved space is put in place to allow future versions to add new
     * variables without shifting down storage in the inheritance chain.
     * See https://docs.openzeppelin.com/contracts/4.x/upgradeable#storage_gaps
     */
    uint256[50] private __gap;
}
