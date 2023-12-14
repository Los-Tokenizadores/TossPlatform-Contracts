// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { ITossWhitelist } from "../Interfaces/ITossWhitelist.sol";

abstract contract TossWhitelistClient {
    error NotInWhitelist();

    address internal whitelistAddress;

    modifier isInWhitelist(address user) {
        if (user != address(0) && whitelistAddress != address(0) && !ITossWhitelist(whitelistAddress).isInWhitelist(user)) {
            revert NotInWhitelist();
        }
        _;
    }
}
