// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "forge-std/Base.sol";
import { IERC20Permit } from "@openzeppelin/contracts/token/ERC20/extensions/IERC20Permit.sol";

contract Util is CommonBase {
    function test() public { }
   
    function sign(uint256 privateKey, bytes32 digest) external pure returns (uint8 v, bytes32 r, bytes32 s) {
        return vm.sign(privateKey, digest);
    }
}

library SigUtils {
    function test() public { }

    /// @dev keccak256("Permit(address owner,address spender,uint256 value,uint256 nonce,uint256 deadline)");
    bytes32 public constant PERMIT_TYPEHASH = 0x6e71edae12b1b97f4d1f60370fef10105fa2faae0126114a169c64845d6126c9;

    struct Permit {
        address owner;
        address spender;
        uint256 value;
        uint256 nonce;
        uint256 deadline;
        uint8 v;
        bytes32 r;
        bytes32 s;
    }

    function signPermit(address owner, uint256 ownerPrivateKey, address spender, uint256 value, uint256 deadline, IERC20Permit erc20) internal returns (Permit memory permit) {
        permit = Permit({ owner: owner, spender: spender, value: value, nonce: erc20.nonces(owner), deadline: deadline, v: 0, r: 0, s: 0 });
        bytes32 digest = getTypedDataHash(permit, erc20.DOMAIN_SEPARATOR());
        Util util = new Util();
        (permit.v, permit.r, permit.s) = util.sign(ownerPrivateKey, digest);
    }

    /// @dev Computes the hash of a permit
    /// @param _permit The approval to execute on-chain
    /// @return The encoded permit
    function getStructHash(Permit memory _permit) private pure returns (bytes32) {
        return keccak256(abi.encode(PERMIT_TYPEHASH, _permit.owner, _permit.spender, _permit.value, _permit.nonce, _permit.deadline));
    }

    /// @notice Computes the hash of a fully encoded EIP-712 message for the domain
    /// @param _permit The approval to execute on-chain
    /// @return The digest to sign and use to recover the signer
    function getTypedDataHash(Permit memory _permit, bytes32 domainSeparator) private pure returns (bytes32) {
        return keccak256(abi.encodePacked("\x19\x01", domainSeparator, getStructHash(_permit)));
    }
}
