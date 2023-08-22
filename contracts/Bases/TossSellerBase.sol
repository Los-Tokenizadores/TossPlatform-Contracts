// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

import "@openzeppelin/contracts-upgradeable/security/PausableUpgradeable.sol";
import "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import "./TossUUPSUpgradeable.sol";
import "../Interfaces/ITossSellErc721.sol";
import "./TossWhitelistClient.sol";

abstract contract TossSellerBase is TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, TossUUPSUpgradeable {
    using SafeERC20 for IERC20;
    
    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant CONVERT_ROLE = keccak256("CONVERT_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    uint32 public convertToOffchainRate;
    uint256 public convertToOffchainMinAmount;
    uint16 public convertToOffchainCut;

    uint32 public convertToErc20Rate;
    uint32 public convertToErc20MinAmount;
    uint16 public convertToErc20Cut;

    address public erc20Address;
    address private erc20BankAddress;

    struct Erc721SellInfo {
        uint128 price;
        uint8 maxAmount;
    }

    mapping(address => Erc721SellInfo) public erc721Sells;

    event ConvertToOffchain(address indexed account, uint256 erc20Amount, uint32 offchainAmount);
    event ConvertToErc20(address indexed account, uint256 erc20Amount, uint32 offchainAmount);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }
    
    function __TossSellerBase_init(address erc20Address_) initializer public {
        __Pausable_init();
        __AccessControl_init();
        __TossUUPSUpgradeable_init();

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(CONVERT_ROLE, msg.sender);

        erc20Address = erc20Address_;
        erc20BankAddress = msg.sender;

        convertToOffchainRate = 100;
        convertToOffchainMinAmount = 1 * 10 ** 18;
        convertToOffchainCut = 0;

        convertToErc20Rate = 500;
        convertToErc20MinAmount = 500;
        convertToErc20Cut = 500;
    }

    function _authorizeUpgrade(address newImplementation) internal onlyRole(UPGRADER_ROLE) override {}

    function getWhitelist() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return whitelistAddress;
    } 

    function setWhitelist(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        whitelistAddress = newAddress;
    } 

    function getErc20BankAddress() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return erc20BankAddress;
    } 

    function setErc20BankAddress(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(newAddress != address(0x0));
        erc20BankAddress = newAddress;
    } 

    function withdrawBalance(uint256 amount) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(amount <= IERC20(erc20Address).balanceOf(address(this)), "insufficient balance");
        IERC20(erc20Address).safeTransfer(erc20BankAddress, amount);
    }
    
    function buyErc721(address address_, uint8 amount_) external {
        require(isInWhitelist(msg.sender), "wallet not in whitelist");
        require(address_ != address(0), "erc721 address invalid");  
        require(amount_ > 0, "create amount needs to be greater than 0");
        
        Erc721SellInfo memory erc721Sell = erc721Sells[address_];
        require(erc721Sell.price > 0, "erc721 not in sell in this seller");
        require(amount_ <= erc721Sell.maxAmount, "create amount needs to be less than assigned limit");
        
        uint256 price = uint256(erc721Sell.price * amount_);
        uint256 balance = IERC20(erc20Address).balanceOf(msg.sender);
        require(balance >= price, "insufficient balance to transaction");
        
        IERC20(erc20Address).safeTransferFrom(msg.sender, address(this), price);
     
        ITossSellErc721(address_).sellErc721(msg.sender, amount_);
    }
    
    function setErc721Sell(address address_, uint128 price, uint8 maxAmount) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(address_ != address(0), "invalid erc721 address");
        require(maxAmount > 0, "maxAmount needs to be greater than 0");
        require(maxAmount <= 40, "maxAmount needs to be less or equals than 40");
        erc721Sells[address_] = Erc721SellInfo({
            price: price,
            maxAmount: maxAmount 
        });
    }

    function convertToOffchain(uint256 erc20Amount) external {
        require(isInWhitelist(msg.sender), "wallet not in whitelist");
        uint256 balance = IERC20(erc20Address).balanceOf(msg.sender);
        require(balance >= erc20Amount, "insufficient balance");
        require(erc20Amount >= convertToOffchainMinAmount,"less than the minimum conversion value");
        
        IERC20(erc20Address).safeTransferFrom(msg.sender, address(this), erc20Amount);
        
        uint256 offchainAmountBig = erc20Amount * convertToOffchainRate * (10000 - convertToOffchainCut) / 10000;
        uint8 decimals = 18;
        uint32 offchainAmount = uint32(offchainAmountBig / (10 ** decimals));

        emit ConvertToOffchain(msg.sender, erc20Amount, offchainAmount);
    }    

    function convertToErc20(address user, uint32 offchainAmount) external onlyRole(CONVERT_ROLE) {
        require(isInWhitelist(user), "wallet not in whitelist");
        require(user != address(0));
        require(offchainAmount >= convertToErc20MinAmount, "less than the minimum conversion value");

        uint8 decimals = 18;
        uint256 erc20Amount = offchainAmount * (10 ** decimals) / convertToErc20Rate * (10000 - convertToErc20Cut) / 10000;

        uint256 balance = IERC20(erc20Address).balanceOf(address(this));
        require(balance >= erc20Amount, "insufficient balance");
        IERC20(erc20Address).safeTransfer(user, erc20Amount);

        emit ConvertToErc20(user, erc20Amount, offchainAmount);
    }

    function setConvertToOffchainRate(uint32 newRate) external onlyRole(CONVERT_ROLE) {
        require(newRate >= 1);
        convertToOffchainRate = newRate;
    }

    function setConvertToOffchainMinAmount(uint256 newAmount) external onlyRole(CONVERT_ROLE) {
        uint8 decimals = 18;
        require(newAmount >= 1 * 10 ** decimals);
        convertToOffchainMinAmount = newAmount;
    }

    function setConvertToOffchainCut(uint16 newPercent) external onlyRole(CONVERT_ROLE) {
        require(newPercent >= 0 && newPercent <= 10000);
        convertToOffchainCut = newPercent;
    }

    function setConvertToErc20Rate(uint32 newRate) external onlyRole(CONVERT_ROLE) {
        require(newRate >= 1);
        convertToErc20Rate = newRate;
    }

    function setConvertToErc20MinAmount(uint32 newAmount) external onlyRole(CONVERT_ROLE) {
        require(newAmount >= 1);
        convertToErc20MinAmount = newAmount;
    }

    function setConvertToErc20Cut(uint16 newPercent) external onlyRole(CONVERT_ROLE) {
        require(newPercent >= 0 && newPercent <= 10000);
        convertToErc20Cut = newPercent;
    }

    /**
     * @dev This empty reserved space is put in place to allow future versions to add new
     * variables without shifting down storage in the inheritance chain.
     * See https://docs.openzeppelin.com/contracts/4.x/upgradeable#storage_gaps
     */
    uint256[50] private __gap;
}