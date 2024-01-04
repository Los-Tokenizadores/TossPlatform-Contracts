// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { ITossSellErc721 } from "../Interfaces/ITossSellErc721.sol";
import { TossErc721MarketBase } from "./TossErc721MarketBase.sol";
import { IERC165 } from "@openzeppelin/contracts/utils/introspection/IERC165.sol";

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

    event Created(address indexed account, uint256 indexed tokenId, uint256 indexed gene);

    error TossErc721GeneNotEnoughGenes(uint256 totalGenes, uint8 amount);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossErc721GeneBase_init(string memory name_, string memory symbol_) public onlyInitializing {
        __TossErc721MarketBase_init(name_, symbol_);
        __TossErc721GeneBase_init_unchained();
    }

    function __TossErc721GeneBase_init_unchained() public onlyInitializing { }

    function supportsInterface(bytes4 interfaceId) public view virtual override(IERC165, TossErc721MarketBase) returns (bool) {
        return interfaceId == type(ITossSellErc721).interfaceId || super.supportsInterface(interfaceId);
    }

    function getRangeGeneLength() external view returns (uint256 genesLength) {
        return _getTossErc721GeneBaseStorage().rangeOfGene.length;
    }

    function getRangeGene() external view onlyRole(MINTER_ROLE) returns (uint256[] memory genes) {
        return _getTossErc721GeneBaseStorage().rangeOfGene;
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

    function getErc721Gene(uint256 tokenId) external view returns (uint256 gene) {
        _requireOwned(tokenId);
        return _getTossErc721GeneBaseStorage().erc721Genes[tokenId];
    }

    function sellErc721(address _owner, uint8 _amount) external nonReentrant onlyRole(MINTER_ROLE) {
        TossErc721GeneBaseStorage storage $ = _getTossErc721GeneBaseStorage();
        if (_amount > $.rangeOfGene.length) {
            revert TossErc721GeneNotEnoughGenes($.rangeOfGene.length, _amount);
        }
        for (uint8 i; i < _amount;) {
            uint256 index = _randomGeneIndex($);
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

    function _randomGeneIndex(TossErc721GeneBaseStorage storage $) private returns (uint256) {
        uint256 randomN = uint256(blockhash(block.number));
        uint256 index = uint256(keccak256(abi.encodePacked(randomN, $.nonce, block.prevrandao))) % $.rangeOfGene.length;
        $.nonce++;
        assert(index < $.rangeOfGene.length);
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
