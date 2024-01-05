// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { IERC20 } from "@openzeppelin/contracts/token/ERC20/IERC20.sol";
import { TossErc20Base } from "./Bases/TossErc20Base.sol";
import { TossExchangeBase } from "./Bases/TossExchangeBase.sol";

contract TossExchangeTierV1 is TossExchangeBase {
    bytes32 public constant YEAR_ROLE = keccak256("YEAR_ROLE");

    struct Balance {
        uint128 consume;
        uint8 tier;
        uint64 lastTransactionYear;
    }

    uint64 public currentYear;
    mapping(address => Balance) private userToBalance;
    uint256[50] public tiers;

    event YearChanged(uint64 indexed year);

    error TossExchangeTierYearLimitReach();
    error TossExchangeTierYearIsTheSame();
    error TossExchangeTierTier0CantChange();

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossExchangeTierV1_init(
        IERC20 externalErc20_,
        uint128 depositMinAmount_,
        TossErc20Base internalErc20_,
        uint128 withdrawMinAmount_,
        uint64 year
    ) public initializer {
        __TossExchangeBase_init(externalErc20_, depositMinAmount_, internalErc20_, withdrawMinAmount_);

        _grantRole(YEAR_ROLE, msg.sender);

        currentYear = year;

        emit YearChanged(year);
    }

    function setYear(uint64 year) external onlyRole(YEAR_ROLE) {
        if (currentYear == year) {
            revert TossExchangeTierYearIsTheSame();
        }
        currentYear = year;
        emit YearChanged(year);
    }

    function setTierLimit(uint8 tier, uint128 limit) external onlyRole(DEFAULT_ADMIN_ROLE) {
        if (tier == 0) {
            revert TossExchangeTierTier0CantChange();
        }
        tiers[tier] = limit;
    }

    function getUserTier(address user) external view returns (uint8 tier) {
        return userToBalance[user].tier;
    }

    function getUserBalance(address user) external view returns (uint256 balance) {
        Balance memory userBalance = userToBalance[user];
        if (userBalance.lastTransactionYear == currentYear) {
            return userBalance.consume;
        }
        return 0;
    }

    function setUserTier(address user, uint8 tier) external onlyRole(DEFAULT_ADMIN_ROLE) {
        userToBalance[user].tier = tier;
    }

    function limitReach(uint256 toConsume, uint128 consume, uint256 limit) private pure returns (bool) {
        return consume + toConsume > limit;
    }

    function deposit(uint128 externalAmount) external override {
        consumeLimit(externalAmount);
        depositInternal(externalAmount);
    }

    function depositWithPermit(uint128 externalAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) public override {
        consumeLimit(externalAmount);
        super.depositWithPermit(externalAmount, amount, deadline, v, r, s);
    }

    function consumeLimit(uint128 amount) private {
        Balance storage userBalance = userToBalance[msg.sender];
        if (userBalance.lastTransactionYear < currentYear) {
            userBalance.lastTransactionYear = currentYear;
            userBalance.consume = 0;
        }
        if (limitReach(amount, userBalance.consume, tiers[userBalance.tier])) {
            revert TossExchangeTierYearLimitReach();
        }
        userBalance.consume = userBalance.consume + amount;
    }
}
