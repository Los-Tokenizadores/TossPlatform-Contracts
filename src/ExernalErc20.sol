// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { ERC20Permit, ERC20 } from "@openzeppelin/contracts/token/ERC20/extensions/ERC20Permit.sol";

contract ExternalErc20 is ERC20Permit {
    constructor() ERC20Permit("") ERC20("", "") { }
}
