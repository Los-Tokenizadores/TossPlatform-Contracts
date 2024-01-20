// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import { ERC20 } from "@openzeppelin/contracts/token/ERC20/ERC20.sol";

contract MockErc20TransferCommision is ERC20 {
    function test() public { }

    uint256 public commision;

    constructor(uint256 amount, uint256 commision_) ERC20("Mock", "MK") {
        _mint(msg.sender, amount);
        commision = commision_;
    }

    function _update(address from, address to, uint256 value) internal override {
        if (value > commision) {
            value = value - commision;
            super._update(from, address(this), commision);
        }
        super._update(from, to, value);
    }
}
