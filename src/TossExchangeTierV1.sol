// SPDX-License-Identifier: MIT
pragma solidity ^0.8.19;

import "./Bases/TossExchangeBase.sol";

contract TossExchangeTierV1 is TossExchangeBase {
    bytes32 public constant YEAR_ROLE = keccak256("YEAR_ROLE");

    uint64 public currentYear;

    struct Balance {
        uint128 consume;
        uint8 tier;
        uint64 lastTransactionYear;
    }

    mapping(address => Balance) private userToBalance;
    uint256[50] public tiers;

    event YearChanged(uint64 year);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeTierV1_init(
        IERC20 externalErc20_,
        uint256 externalMinAmount_,
        TossErc20Base internalErc20_,
        uint256 internalMinAmount_,
        uint64 rate_,
        uint64 year
    ) public initializer {
        __TossExchangeBase_init(externalErc20_, externalMinAmount_, internalErc20_, internalMinAmount_, rate_);

        _grantRole(YEAR_ROLE, msg.sender);

        currentYear = year;
    }

    function setYear(uint64 year) external virtual onlyRole(YEAR_ROLE) {
        require(currentYear != year, "is the same year");
        currentYear = year;
        emit YearChanged(year);
    }

    function setTierLimit(uint8 tier, uint128 limit) external virtual onlyRole(DEFAULT_ADMIN_ROLE) {
        require(tier > 0, "tier 0 needs to be limited to 0");
        tiers[tier] = limit;
    }

    function getUserTier(address user) external view returns (uint8) {
        return userToBalance[user].tier;
    }

    function getUserBalance(address user) external view returns (uint256) {
        Balance memory userBalance = userToBalance[user];
        if (userBalance.lastTransactionYear == currentYear) {
            return userBalance.consume;
        }
        return 0;
    }

    function setUserTier(address user, uint8 tier) external onlyRole(DEFAULT_ADMIN_ROLE) {
        userToBalance[user].tier = tier;
    }

    function hasLimit(uint256 toConsume, uint128 consume, uint256 limit) private pure returns (bool) {
        return consume + toConsume <= limit;
    }

    function convertToInternal(uint128 amount) public virtual override {
        Balance storage userBalance = userToBalance[msg.sender];
        if (userBalance.lastTransactionYear < currentYear) {
            userBalance.lastTransactionYear = currentYear;
            userBalance.consume = 0;
        }
        require(hasLimit(amount, userBalance.consume, tiers[userBalance.tier]), "user reach anual limit");
        userBalance.consume = userBalance.consume + amount;
        super.convertToInternal(amount);
    }
}
