// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import { IERC721 } from "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import { IERC165 } from "@openzeppelin/contracts/utils/introspection/IERC165.sol";
import { PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { IERC20Permit } from "@openzeppelin/contracts/token/ERC20/extensions/IERC20Permit.sol";
import { SafeERC20, IERC20 } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { Strings } from "@openzeppelin/contracts/utils/Strings.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";
import { TossErc721MarketV1 } from "../TossErc721MarketV1.sol";
import { TossUpgradeableProxy } from "../TossUpgradeableProxy.sol";

abstract contract TossInvestBase is TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, TossUUPSUpgradeable {
    using SafeERC20 for IERC20;

    error AddressNotExist();

    uint256 constant GAS_EXTRA_RETURN = 100_000;
    uint256 constant GAS_EXTRA_MINT = 500_000;
    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant PROJECT_ROLE = keccak256("PROJECT_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    struct ProjectInfo {
        uint128 price;
        uint32 targetAmount;
        uint32 maxAmount;
        uint64 startAt;
        uint64 finishAt;
        uint64 mintedAt;
        uint32 lastIndex;
        address projectWallet;
        bool confirmed;
        address erc721Address;
        string name;
        string symbol;
    }

    IERC20 public erc20;
    ProjectInfo[] private projects;
    mapping(uint256 => address[]) public projectInvestors;

    string public erc721baseUri;
    TossErc721MarketV1 private erc721Implementation;
    address private tossProjectAddress;

    address private platformAddress;
    uint16 private platformCut;

    event ProjectInvested(uint256 indexed projectId, address indexed account, uint16 amount);
    event ProjectAdded(uint256 indexed projectId);
    event ProjectConfirmed(uint256 indexed projectId, address indexed account);
    event ProjectErc721Created(uint256 indexed projectId, address indexed erc721Address, address implementationAddress);
    event ProjectFinished(uint256 indexed projectId);

    /// @custom:oz-upgrades-unsafe-allow constructor
    constructor() {
        _disableInitializers();
    }

    function __TossInvestBase_init(
        IERC20 erc20_,
        TossErc721MarketV1 erc721Implementation_,
        address platformAddress_,
        uint16 platformCut_,
        string memory erc721baseUri_
    ) public initializer {
        require(address(erc20_) != address(0));
        require(address(erc721Implementation_) != address(0));
        require(platformAddress_ != address(0), "platform address invalid");
        require(platformCut_ <= 10_000, "platform cut required between 0 and 10000");

        __Pausable_init();
        __AccessControl_init();
        __TossUUPSUpgradeable_init();

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(PROJECT_ROLE, msg.sender);

        tossProjectAddress = msg.sender;
        erc20 = erc20_;
        erc721Implementation = erc721Implementation_;
        erc721baseUri = erc721baseUri_;
        platformAddress = platformAddress_;
        platformCut = platformCut_;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function getWhitelist() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return whitelistAddress;
    }

    function setWhitelist(address newAddress) external onlyRole(DEFAULT_ADMIN_ROLE) {
        whitelistAddress = newAddress;
    }

    function getErc721Implementation() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address) {
        return address(erc721Implementation);
    }

    function setErc721Implementation(TossErc721MarketV1 newImplementation) external onlyRole(DEFAULT_ADMIN_ROLE) {
        require(IERC165(address(newImplementation)).supportsInterface(type(IERC721).interfaceId), "Contract does not support IERC721");
        erc721Implementation = newImplementation;
    }

    function setErc721BaseUri(string memory erc721BaseUri_) external onlyRole(DEFAULT_ADMIN_ROLE) {
        erc721baseUri = erc721BaseUri_;
    }

    function projectAmount() external view returns (uint256) {
        return projects.length;
    }

    function getProjectInternal(uint256 projectId) internal view returns (ProjectInfo memory info, uint256 invested, uint256 inversors) {
        require(projectId < projects.length, "project not exist");
        info = projects[projectId];
        invested = projectInvestors[projectId].length;
        inversors = countUniqueAddresses(projectInvestors[projectId]);
    }

    function getProject(uint256 projectId) external view returns (ProjectInfo memory info, uint256 invested, uint256 inversors) {
        return getProjectInternal(projectId);
    }

    function getProjectByErc721Address(address erc721Address) external view returns (uint256 projectId, ProjectInfo memory info, uint256 invested, uint256 inversors) {
        require(erc721Address != address(0), "address needs to be not 0x0");
        for (uint256 i; i < projects.length;) {
            if (projects[i].erc721Address == erc721Address) {
                (info, invested, inversors) = getProjectInternal(i);
                return (i, info, invested, inversors);
            }

            unchecked {
                ++i;
            }
        }

        revert AddressNotExist();
    }

    function countUniqueAddresses(address[] memory addresses) private pure returns (uint256) {
        address[] memory uniqueAddresses = new address[](addresses.length);
        uint256 uniqueCount;

        for (uint256 i; i < addresses.length;) {
            if (!isAddressInArray(addresses[i], uniqueAddresses, uniqueCount)) {
                uniqueAddresses[uniqueCount] = addresses[i];
                ++uniqueCount;
            }

            unchecked {
                ++i;
            }
        }

        return uniqueCount;
    }

    function isAddressInArray(address addr, address[] memory arr, uint256 count) private pure returns (bool) {
        for (uint256 i = 0; i < count; i++) {
            if (arr[i] == addr) {
                return true;
            }
        }
        return false;
    }

    function addProject(
        string memory name,
        string memory symbol,
        uint32 targetAmount,
        uint32 maxAmount,
        uint128 price,
        uint64 startAt,
        uint64 finishAt,
        address projectWallet
    ) external onlyRole(PROJECT_ROLE) {
        require(targetAmount > 0, "targetAmount needs to be greater than 0");
        require(targetAmount <= maxAmount, "max amount needs to be greater or equal to targetAmount");
        require(price > 0, "prices needs to be greater than 0");
        require(startAt > block.timestamp, "start at needs to be greater than current date");
        require(finishAt > block.timestamp, "finish at needs to be greater than current date");
        require(startAt < finishAt, "finish at needs to be greater than startAt");
        require(projectWallet != address(0), "project wallet invalid");

        uint256 projectId = projects.length;
        projects.push(
            ProjectInfo({
                name: name,
                symbol: symbol,
                targetAmount: targetAmount,
                maxAmount: maxAmount,
                price: price,
                startAt: startAt,
                finishAt: finishAt,
                mintedAt: 0,
                erc721Address: address(0),
                lastIndex: 0,
                projectWallet: projectWallet,
                confirmed: false
            })
        );

        emit ProjectAdded(projectId);
    }

    function changeProject(
        uint256 projectId,
        string memory name,
        string memory symbol,
        uint32 targetAmount,
        uint32 maxAmount,
        uint128 price,
        uint64 startAt,
        uint64 finishAt,
        address projectWallet
    ) external onlyRole(PROJECT_ROLE) {
        require(projectId < projects.length, "project not exist");
        ProjectInfo storage projectInfo = projects[projectId];
        require(!projectInfo.confirmed, "project already confirmed");

        require(targetAmount > 0, "targetAmount needs to be greater than 0");
        require(targetAmount <= maxAmount, "max amount needs to be greater or equal to targetAmount");
        require(price > 0, "prices needs to be greater than 0");
        require(startAt > block.timestamp, "finish at needs to be greater than current date");
        require(finishAt > block.timestamp, "finish at needs to be greater than current date");
        require(startAt < finishAt, "finish at needs to be greater than startAt");
        require(projectWallet != address(0), "project wallet invalid");

        projectInfo.name = name;
        projectInfo.symbol = symbol;
        projectInfo.targetAmount = targetAmount;
        projectInfo.maxAmount = maxAmount;
        projectInfo.price = price;
        projectInfo.startAt = startAt;
        projectInfo.finishAt = finishAt;
        projectInfo.projectWallet = projectWallet;
    }

    function confirm(uint256 projectId) external {
        require(projectId < projects.length, "project not exist");
        ProjectInfo storage projectInfo = projects[projectId];
        require(!projectInfo.confirmed, "project already confirmed");
        require(msg.sender == projectInfo.projectWallet, "not project owner");

        projectInfo.confirmed = true;

        emit ProjectConfirmed(projectId, msg.sender);
    }

    function invest(uint256 projectId, uint16 amount) external isInWhitelist(msg.sender) {
        investInternal(projectId, amount);
    }

    function investWithPermit(uint256 projectId, uint16 investAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) external isInWhitelist(msg.sender) {
        IERC20Permit(address(erc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        investInternal(projectId, investAmount);
    }

    function investInternal(uint256 projectId, uint16 amount) private {
        require(projectId < projects.length, "project not exist");
        ProjectInfo storage projectInfo = projects[projectId];
        require(projectInfo.confirmed, "project not confirmed");
        require(projectInfo.startAt <= block.timestamp, "project hasn't started");
        require(block.timestamp <= projectInfo.finishAt, "project has ended");

        address[] storage investors = projectInvestors[projectId];
        uint256 investorAmount = investors.length;
        require(investorAmount + amount <= projectInfo.maxAmount, "project already full of investors");

        erc20.safeTransferFrom(msg.sender, address(this), amount * projectInfo.price);

        for (uint256 i = 0; i < amount;) {
            investors.push(msg.sender);
            unchecked {
                ++i;
            }
        }

        emit ProjectInvested(projectId, msg.sender, amount);
    }

    function finish(uint256 projectId) external {
        ProjectInfo storage projectInfo = projects[projectId];
        require(projectInfo.confirmed, "project not confirmed");

        address[] storage investors = projectInvestors[projectId];
        if (investors.length < projectInfo.maxAmount) {
            require(block.timestamp >= projectInfo.finishAt, "project is not ended");
        }

        uint256 length = investors.length;
        if (projectInfo.targetAmount <= length) {
            finishMintErc721(projectId, projectInfo, investors);
        } else {
            finishReturnInvestment(projectInfo, investors);
        }

        if (projectInfo.lastIndex == length) {
            emit ProjectFinished(projectId);
        }
    }

    function finishReturnInvestment(ProjectInfo storage projectInfo, address[] storage investors) private {
        uint32 lastIndex = projectInfo.lastIndex;
        uint256 length = investors.length;
        require(length > lastIndex, "project already return all investments");
        uint256 price = projectInfo.price;

        uint32 i = lastIndex;
        address lastInvestor = investors[i];
        uint256 acumulated = 0;
        for (; i < length;) {
            if (lastInvestor == investors[i]) {
                ++acumulated;
            } else {
                erc20.safeTransfer(lastInvestor, acumulated * price);
                lastInvestor = investors[i];
                acumulated = 1;
            }
            unchecked {
                ++i;
            }
            if (gasleft() < GAS_EXTRA_RETURN) {
                break;
            }
        }
        erc20.safeTransfer(lastInvestor, acumulated * price);
        projectInfo.lastIndex = i;
    }

    function finishMintErc721(uint256 projectId, ProjectInfo storage projectInfo, address[] storage investors) private {
        uint32 lastIndex = projectInfo.lastIndex;
        uint256 length = investors.length;
        require(length > lastIndex, "project already fully minted");

        TossErc721MarketV1 erc721;
        if (projectInfo.erc721Address == address(0)) {
            TossUpgradeableProxy proxy =
                new TossUpgradeableProxy(address(erc721Implementation), abi.encodeCall(TossErc721MarketV1.__TossErc721MarketV1_init, (projectInfo.name, projectInfo.symbol)));
            projectInfo.erc721Address = address(proxy);
            erc721 = TossErc721MarketV1(address(proxy));
            if (bytes(erc721baseUri).length > 0) {
                erc721.setBaseUri(string.concat(erc721baseUri, Strings.toString(projectId), "/"));
            }
            erc721.grantRole(erc721.DEFAULT_ADMIN_ROLE(), tossProjectAddress);
            erc721.grantRole(erc721.UPGRADER_ROLE(), tossProjectAddress);
            uint256 total = projectInfo.price * length;
            uint256 platformAmount = total * platformCut / 10_000;
            erc20.safeTransfer(projectInfo.projectWallet, total - platformAmount);
            erc20.safeTransfer(platformAddress, platformAmount);
            projectInfo.mintedAt = uint64(block.timestamp);
            emit ProjectErc721Created(projectId, projectInfo.erc721Address, address(erc721Implementation));
        } else {
            erc721 = TossErc721MarketV1(projectInfo.erc721Address);
        }

        uint32 i = lastIndex;
        for (; i < length;) {
            erc721.safeMint(investors[i], i);
            unchecked {
                ++i;
            }
            if (gasleft() < GAS_EXTRA_MINT) {
                break;
            }
        }
        projectInfo.lastIndex = i;
    }
}
