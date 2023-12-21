// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { ITossSellErc721 } from "../Interfaces/ITossSellErc721.sol";
import { TossErc721MarketBase } from "./TossErc721MarketBase.sol";

abstract contract TossErc721GeneBase is ITossSellErc721, TossErc721MarketBase {
    /// @custom:storage-location erc7201:tossplatform.storage.TossErc721GeneBase
    struct TossErc721GeneBaseStorage {
        uint256 nextTokenId;
        uint256 nonce;
        uint256[] rangeOfGene;
        mapping(uint256 => uint256) erc721Genes;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossErc721GeneBase")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossErc721GeneBaseStorageLocation = 0x02d2c01e312ef780b9d48fd6b2f6eed71f728c32e979ed76e14af9eb8bdf8e00;

    function _getTossErc721GeneBaseStorage() internal pure returns (TossErc721GeneBaseStorage storage $) {
        assembly {
            $.slot := TossErc721GeneBaseStorageLocation
        }
    }
    //TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();

    event Created(address indexed account, uint256 indexed tokenId, uint256 indexed gene);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721GeneBase_init(string memory name_, string memory symbol_) public onlyInitializing {
        __TossErc721MarketBase_init(name_, symbol_);
        __TossErc721GeneBase_init_unchained();
    }

    function __TossErc721GeneBase_init_unchained() public onlyInitializing { }

    function getRangeGeneLength() external view returns (uint256) {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();
        return $.rangeOfGene.length;
    }

    function getRangeGene() external view onlyRole(MINTER_ROLE) returns (uint256[] memory) {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();
        return $.rangeOfGene;
    }

    function addGenes(uint256[] memory genes) external virtual onlyRole(MINTER_ROLE) {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();
        for (uint256 i; i < genes.length;) {
            $.rangeOfGene.push(genes[i]);
            unchecked {
                ++i;
            }
        }
    }

    function getErc721Data(uint256 tokenId) public view returns (uint256) {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();
        require(_ownerOf(tokenId) != address(0), "Token not exist");
        return $.erc721Genes[tokenId];
    }

    function sellErc721(address _owner, uint8 _amount) external onlyRole(MINTER_ROLE) {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();
        require($.rangeOfGene.length >= _amount, "Insufficient genes to mint");
        for (uint8 i; i < _amount;) {
            uint256 index = _randomGeneIndex();
            require(index < $.rangeOfGene.length, "index of gene out of range");
            uint256 gene = $.rangeOfGene[index];

            uint256 tokenId = $.nextTokenId++;
            _safeMint(_owner, tokenId);

            $.erc721Genes[tokenId] = gene;
            _remove(index);

            emit Created(_owner, tokenId, gene);
            unchecked {
                ++i;
            }
        }
    }

    function _randomGeneIndex() private returns (uint256) {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();
        uint256 randomN = uint256(blockhash(block.number));
        uint256 index = uint256(keccak256(abi.encodePacked(randomN, $.nonce, block.prevrandao))) % $.rangeOfGene.length;
        $.nonce++;
        return index;
    }

    function _remove(uint256 _index) private {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();
        if (_index >= $.rangeOfGene.length) {
            return;
        }
        $.rangeOfGene[_index] = $.rangeOfGene[$.rangeOfGene.length - 1];
        $.rangeOfGene.pop();
    }
}
