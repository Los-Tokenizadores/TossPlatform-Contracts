// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

interface ITossSellErc721 {
    function sellErc721(address _owner, uint8 _amount) external;
}
