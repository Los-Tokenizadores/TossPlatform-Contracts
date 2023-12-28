// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { ITossWhitelist } from "../Interfaces/ITossWhitelist.sol";

abstract contract TossWhitelistClient {
    /// @custom:storage-location erc7201:tossplatform.storage.TossWhitelistClient
    struct TossWhitelistClientStorage {
        address whitelistAddress;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossWhitelistClient")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossWhitelistClientStorageLocation = 0xe32c7d464d01bf4699c6877240dec52b29adf1b2f9e414a502cf561c22d22d00;

    error TossWhitelistNotInWhitelist(address address_);

    function _getTossWhitelistClientStorage() private pure returns (TossWhitelistClientStorage storage $) {
        assembly {
            $.slot := TossWhitelistClientStorageLocation
        }
    }

    modifier isInWhitelist(address user) {
        TossWhitelistClientStorage storage $ = _getTossWhitelistClientStorage();
        if (user != address(0) && $.whitelistAddress != address(0) && !ITossWhitelist($.whitelistAddress).isInWhitelist(user)) {
            revert TossWhitelistNotInWhitelist(user);
        }
        _;
    }

    function getWhitelist() external view returns (address whitelistAddress) {
        return _getTossWhitelistClientStorage().whitelistAddress;
    }

    function setWhitelist(address newAddress) external virtual;

    function _setWhitelist(address newAddress) internal {
        _getTossWhitelistClientStorage().whitelistAddress = newAddress;
    }
}
