// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

import "./TossWhitelistBase.sol";

abstract contract TossWhitelistClient {
    address internal whitelistAddress;

    function isInWhitelist(address user) view internal returns (bool) {
        if(whitelistAddress == address(0)){
            return true;
        }
        return TossWhitelistBase(whitelistAddress).isInWhitelist(user);
    }

    /**
     * @dev This empty reserved space is put in place to allow future versions to add new
     * variables without shifting down storage in the inheritance chain.
     * See https://docs.openzeppelin.com/contracts/4.x/upgradeable#storage_gaps
     */
    uint256[50] private __gap;
}