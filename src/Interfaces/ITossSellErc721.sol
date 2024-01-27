// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { IERC165 } from "@openzeppelin/contracts/utils/introspection/IERC165.sol";

interface ITossSellErc721 is IERC165 {
    function sellErc721(address _owner, uint8 _amount) external;
}
