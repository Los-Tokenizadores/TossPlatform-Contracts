// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import "./TossUUPSUpgradeable.sol";
import "./TossErc20Base.sol";
import "../Interfaces/ITossExchange.sol";
import "./TossWhitelistClient.sol";

abstract contract TossExchangeBase is ITossExchange, TossWhitelistClient, AccessControlUpgradeable, TossUUPSUpgradeable {
    using SafeERC20 for IERC20;

    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");
    
    uint256 public externalMinAmount;
    uint256 public internalMinAmount;

    address public externalErc20Address;
    address public internalErc20Address;
    
    uint64 public rate;

    event ConvertedToInternal(address indexed account, uint256 externalAmount, uint256 internalAmount);
    event ConvertedToExternal(address indexed account, uint256 externalAmount, uint256 internalAmount);
    
    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeBase_init(address externalErc20Address_, uint256 externalMinAmount_, address internalErc20Address_, uint256 internalMinAmount_, uint64 rate_) initializer public {
        require(externalErc20Address_ != address(0), "external erc20 address is empty");
        require(internalErc20Address_ != address(0), "internal erc20 address is empty");
        require(externalErc20Address_ != internalErc20Address_, "external and internal erc20 are the same");
        require(rate_ > 0, "rate needs to be greater than 0");
        require(externalMinAmount_ > 0, "external min needs to be greater than 0");
        require(internalMinAmount_ > 0, "internal min needs to be greater than 0");

        __AccessControl_init();
        __TossUUPSUpgradeable_init();

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        
        externalErc20Address = externalErc20Address_;
        internalErc20Address = internalErc20Address_;
        
        externalMinAmount = externalMinAmount_;
        internalMinAmount = internalMinAmount_;

        rate = rate_;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) {}

    function getWhitelist() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return whitelistAddress;
    } 

    function setWhitelist(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        whitelistAddress = newAddress;
    } 

    function setExternalMin(uint256 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        require(value > 0, "min needs to be greater than 0");
        externalMinAmount = value;
    }

    function setInternalMin(uint256 value) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        require(value > 0, "min needs to be greater than 0");
        internalMinAmount = value;
    }

    function convertToInternal(uint128 externalAmount) public virtual {
        _convertToInternal(externalAmount, externalAmount * rate / 10000);
    }

    function convertToExternal(uint128 internalAmount) public virtual { 
        _convertToExternal(internalAmount * 10000 / rate, internalAmount);
    } 

    function _convertToInternal(uint128 externalAmount, uint128 internalAmount) internal virtual {
        require(isInWhitelist(msg.sender), "wallet not in whitelist");
        require(internalAmount >= internalMinAmount, "amount need greater than the minimum amount");
        require(IERC20(externalErc20Address).balanceOf(msg.sender) >= externalAmount, "insufficient balance");

        IERC20(externalErc20Address).safeTransferFrom(msg.sender, address(this), externalAmount);
        TossErc20Base(internalErc20Address).mint(msg.sender, internalAmount);

        emit ConvertedToInternal(msg.sender, externalAmount, internalAmount);
    }

    function _convertToExternal(uint128 externalAmount, uint128 internalAmount) internal virtual {
        require(isInWhitelist(msg.sender), "wallet not in whitelist");
        require(externalAmount >= externalMinAmount, "amount need greater than the minimum amount");
        require(IERC20(internalErc20Address).balanceOf(msg.sender) >= internalAmount, "insufficient balance");
        
        TossErc20Base(internalErc20Address).burnFrom(msg.sender, internalAmount);
        IERC20(externalErc20Address).safeTransfer(msg.sender, externalAmount);

        emit ConvertedToExternal(msg.sender, externalAmount, internalAmount);
    }

    /**
     * @dev This empty reserved space is put in place to allow future versions to add new
     * variables without shifting down storage in the inheritance chain.
     * See https://docs.openzeppelin.com/contracts/4.x/upgradeable#storage_gaps
     */
    uint256[50] private __gap;
}