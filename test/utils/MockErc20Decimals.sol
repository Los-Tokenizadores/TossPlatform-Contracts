// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { ERC20 } from "@openzeppelin/contracts/token/ERC20/ERC20.sol";

contract MockErc20Decimals is ERC20 {
    function test() public { }

    uint8 private _decimals;

    constructor(uint8 decimals_) ERC20("Mock", "MK") {
        _decimals = decimals_;
    }

    function decimals() public view virtual override returns (uint8) {
        return _decimals;
    }
}
