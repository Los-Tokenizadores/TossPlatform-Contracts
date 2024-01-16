// SPDX-License-Identifier: MIT
pragma solidity 0.8.23;

import { IERC721 } from "@openzeppelin/contracts/token/ERC721/IERC721.sol";
import { IERC165 } from "@openzeppelin/contracts/utils/introspection/IERC165.sol";
import { PausableUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/PausableUpgradeable.sol";
import { AccessControlUpgradeable } from "@openzeppelin/contracts-upgradeable/access/AccessControlUpgradeable.sol";
import { ReentrancyGuardUpgradeable } from "@openzeppelin/contracts-upgradeable/utils/ReentrancyGuardUpgradeable.sol";
import { IERC20Permit } from "@openzeppelin/contracts/token/ERC20/extensions/IERC20Permit.sol";
import { SafeERC20, IERC20 } from "@openzeppelin/contracts/token/ERC20/Utils/SafeERC20.sol";
import { Strings } from "@openzeppelin/contracts/utils/Strings.sol";
import { TossUUPSUpgradeable } from "./TossUUPSUpgradeable.sol";
import { TossWhitelistClient } from "./TossWhitelistClient.sol";
import { TossErc721MarketV1 } from "../TossErc721MarketV1.sol";
import { TossUpgradeableProxy } from "../TossUpgradeableProxy.sol";
import "../Interfaces/TossErrors.sol";

abstract contract TossInvestBase is TossWhitelistClient, PausableUpgradeable, AccessControlUpgradeable, ReentrancyGuardUpgradeable, TossUUPSUpgradeable {
    /// @custom:storage-location erc7201:tossplatform.storage.TossInvestBase
    struct TossInvestBaseStorage {
        string erc721BaseUri;
        IERC20 erc20;
        TossErc721MarketV1 erc721Implementation;
        address tossProjectAddress;
        address platformAddress;
        uint16 platformCut;
        ProjectInfo[] projects;
        mapping(uint256 => address[]) projectInvestors;
    }

    // keccak256(abi.encode(uint256(keccak256("tossplatform.storage.TossInvestBase")) - 1)) & ~bytes32(uint256(0xff))
    bytes32 private constant TossInvestBaseStorageLocation = 0xaf67e798606a091e8f7739ae10917cbc695392bad453bb419f72cbcae668ae00;

    function _getTossInvestBaseStorage() internal pure returns (TossInvestBaseStorage storage $) {
        assembly {
            $.slot := TossInvestBaseStorageLocation
        }
    }

    using SafeERC20 for IERC20;

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

    uint256 private constant PLATFORM_CUT_PRECISION = 10_000;
    uint256 private constant GAS_EXTRA_RETURN = 100_000;
    uint256 private constant GAS_EXTRA_MINT = 500_000;
    bytes32 public constant PAUSER_ROLE = keccak256("PAUSER_ROLE");
    bytes32 public constant PROJECT_ROLE = keccak256("PROJECT_ROLE");
    bytes32 public constant UPGRADER_ROLE = keccak256("UPGRADER_ROLE");

    event ProjectInvested(uint256 indexed projectId, address indexed account, uint16 indexed amount);
    event ProjectAdded(uint256 indexed projectId);
    event ProjectConfirmed(uint256 indexed projectId, address indexed account);
    event ProjectErc721Created(uint256 indexed projectId, address indexed erc721Address, address indexed implementationAddress);
    event ProjectFinished(uint256 indexed projectId);

    error TossInvestProjectNotFoundByErc721(address erc721);
    error TossInvestInvalidErc721Implementation(TossErc721MarketV1 implementation);
    error TossInvestProjectNotExist(uint256 projectId);
    error TossInvestProjectTargetIsGreaterThanMax(uint32 targetAmount, uint32 maxAmount);
    error TossInvestProjectStartAtLessThanCurrentDate(uint64 startAt, uint256 currentDate);
    error TossInvestProjectStartAtGreaterThanFinishAt(uint64 startAt, uint64 finishAt);
    error TossInvestProjectIsConfirmed();
    error TossInvestProjectIsNotConfirmed();
    error TossInvestProjectNotStarted();
    error TossInvestProjectNotFinished();
    error TossInvestProjectIsFinished();
    error TossInvestProjectFullInvested();
    error TossInvestNotProjectOwner(uint256 projectId);
    error TossInvestAlreadyAllInvestmentReturned();
    error TossInvestAlreadyAllErc721Minted();

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
    ) internal onlyInitializing {
        __Pausable_init();
        __AccessControl_init();
        __ReentrancyGuard_init();
        __TossUUPSUpgradeable_init();
        __TossInvestBase_init_unchained(erc20_, erc721Implementation_, platformAddress_, platformCut_, erc721baseUri_);
    }

    function __TossInvestBase_init_unchained(
        IERC20 erc20_,
        TossErc721MarketV1 erc721Implementation_,
        address platformAddress_,
        uint16 platformCut_,
        string memory erc721baseUri_
    ) internal onlyInitializing {
        if (address(erc20_) == address(0)) {
            revert TossAddressIsZero("erc20");
        }
        if (address(erc721Implementation_) == address(0)) {
            revert TossAddressIsZero("erc721Implementation");
        }
        if (platformAddress_ == address(0)) {
            revert TossAddressIsZero("platformAddress");
        }
        if (platformCut_ > PLATFORM_CUT_PRECISION) {
            revert TossCutOutOfRange(platformCut_);
        }

        _grantRole(DEFAULT_ADMIN_ROLE, msg.sender);
        _grantRole(UPGRADER_ROLE, msg.sender);
        _grantRole(PAUSER_ROLE, msg.sender);
        _grantRole(PROJECT_ROLE, msg.sender);

        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        $.tossProjectAddress = msg.sender;
        $.erc20 = erc20_;
        $.erc721Implementation = erc721Implementation_;
        $.erc721BaseUri = erc721baseUri_;
        $.platformAddress = platformAddress_;
        $.platformCut = platformCut_;
    }

    function _authorizeUpgrade(address newImplementation) internal override onlyRole(UPGRADER_ROLE) { }

    function pause() external onlyRole(PAUSER_ROLE) {
        _pause();
    }

    function unpause() external onlyRole(PAUSER_ROLE) {
        _unpause();
    }

    function setWhitelist(address newAddress) external override onlyRole(DEFAULT_ADMIN_ROLE) {
        _setWhitelist(newAddress);
    }

    function getErc721Implementation() external view onlyRole(DEFAULT_ADMIN_ROLE) returns (address implementation) {
        return address(_getTossInvestBaseStorage().erc721Implementation);
    }

    function setErc721Implementation(TossErc721MarketV1 newImplementation) external nonReentrant onlyRole(DEFAULT_ADMIN_ROLE) {
        if (!IERC165(address(newImplementation)).supportsInterface(type(IERC721).interfaceId)) {
            revert TossInvestInvalidErc721Implementation(newImplementation);
        }
        _getTossInvestBaseStorage().erc721Implementation = newImplementation;
    }

    function getErc20() external view returns (IERC20 erc20) {
        return _getTossInvestBaseStorage().erc20;
    }

    function getErc721BaseUri() external view returns (string memory erc721BaseUri) {
        return _getTossInvestBaseStorage().erc721BaseUri;
    }

    function setErc721BaseUri(string memory erc721BaseUri_) external onlyRole(DEFAULT_ADMIN_ROLE) {
        _getTossInvestBaseStorage().erc721BaseUri = erc721BaseUri_;
    }

    function projectAmount() external view returns (uint256 amount) {
        return _getTossInvestBaseStorage().projects.length;
    }

    function countUniqueAddresses(address[] memory addresses) private pure returns (uint256) {
        address[] memory uniqueAddresses = new address[](addresses.length);
        uint256 uniqueCount = 0;

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
        for (uint256 i; i < count;) {
            if (arr[i] == addr) {
                return true;
            }
            unchecked {
                ++i;
            }
        }
        return false;
    }

    function getProjectInternal(uint256 projectId) internal view returns (ProjectInfo memory info, uint256 invested, uint256 inversors) {
        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        if (projectId >= $.projects.length) {
            revert TossInvestProjectNotExist(projectId);
        }
        info = $.projects[projectId];
        invested = $.projectInvestors[projectId].length;
        inversors = countUniqueAddresses($.projectInvestors[projectId]);
    }

    function getProject(uint256 projectId) external view returns (ProjectInfo memory info, uint256 invested, uint256 inversors) {
        return getProjectInternal(projectId);
    }

    function getProjectInvestor(uint256 projectId, uint256 index) external view returns (address investorAddress) {
        return _getTossInvestBaseStorage().projectInvestors[projectId][index];
    }

    function getProjectByErc721Address(address erc721Address) external view returns (uint256 projectId, ProjectInfo memory info, uint256 invested, uint256 inversors) {
        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        if (erc721Address == address(0)) {
            revert TossAddressIsZero("erc721");
        }

        for (uint256 i; i < $.projects.length;) {
            if ($.projects[i].erc721Address == erc721Address) {
                (info, invested, inversors) = getProjectInternal(i);
                return (i, info, invested, inversors);
            }

            unchecked {
                ++i;
            }
        }

        revert TossInvestProjectNotFoundByErc721(erc721Address);
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
    ) external whenNotPaused onlyRole(PROJECT_ROLE) {
        if (targetAmount == 0) {
            revert TossValueIsZero("target amount");
        }
        if (targetAmount > maxAmount) {
            revert TossInvestProjectTargetIsGreaterThanMax(targetAmount, maxAmount);
        }
        if (price == 0) {
            revert TossValueIsZero("price");
        }
        if (startAt < block.timestamp) {
            revert TossInvestProjectStartAtLessThanCurrentDate(startAt, block.timestamp);
        }
        if (startAt > finishAt) {
            revert TossInvestProjectStartAtGreaterThanFinishAt(startAt, finishAt);
        }
        if (projectWallet == address(0)) {
            revert TossAddressIsZero("project wallet");
        }

        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        uint256 projectId = $.projects.length;
        $.projects.push(
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
    ) external whenNotPaused onlyRole(PROJECT_ROLE) {
        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        if (projectId >= $.projects.length) {
            revert TossInvestProjectNotExist(projectId);
        }

        ProjectInfo storage projectInfo = $.projects[projectId];
        if (projectInfo.confirmed) {
            revert TossInvestProjectIsConfirmed();
        }
        if (targetAmount == 0) {
            revert TossValueIsZero("target amount");
        }
        if (targetAmount > maxAmount) {
            revert TossInvestProjectTargetIsGreaterThanMax(targetAmount, maxAmount);
        }
        if (price == 0) {
            revert TossValueIsZero("price");
        }
        if (startAt < block.timestamp) {
            revert TossInvestProjectStartAtLessThanCurrentDate(startAt, block.timestamp);
        }
        if (startAt > finishAt) {
            revert TossInvestProjectStartAtGreaterThanFinishAt(startAt, finishAt);
        }
        if (projectWallet == address(0)) {
            revert TossAddressIsZero("project wallet");
        }
        projectInfo.name = name;
        projectInfo.symbol = symbol;
        projectInfo.targetAmount = targetAmount;
        projectInfo.maxAmount = maxAmount;
        projectInfo.price = price;
        projectInfo.startAt = startAt;
        projectInfo.finishAt = finishAt;
        projectInfo.projectWallet = projectWallet;
    }

    function confirm(uint256 projectId) external whenNotPaused {
        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        if (projectId >= $.projects.length) {
            revert TossInvestProjectNotExist(projectId);
        }

        ProjectInfo storage projectInfo = $.projects[projectId];
        if (projectInfo.confirmed) {
            revert TossInvestProjectIsConfirmed();
        }
        if (msg.sender != projectInfo.projectWallet) {
            revert TossInvestNotProjectOwner(projectId);
        }

        projectInfo.confirmed = true;

        emit ProjectConfirmed(projectId, msg.sender);
    }

    function invest(uint256 projectId, uint16 amount) external {
        investInternal(projectId, amount);
    }

    function investWithPermit(uint256 projectId, uint16 investAmount, uint256 amount, uint256 deadline, uint8 v, bytes32 r, bytes32 s) external {
        IERC20Permit(address(_getTossInvestBaseStorage().erc20)).permit(msg.sender, address(this), amount, deadline, v, r, s);
        investInternal(projectId, investAmount);
    }

    function investInternal(uint256 projectId, uint16 amount) private nonReentrant whenNotPaused isInWhitelist(msg.sender) {
        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        if (projectId >= $.projects.length) {
            revert TossInvestProjectNotExist(projectId);
        }
        ProjectInfo storage projectInfo = $.projects[projectId];
        if (!projectInfo.confirmed) {
            revert TossInvestProjectIsNotConfirmed();
        }
        if (projectInfo.startAt > block.timestamp) {
            revert TossInvestProjectNotStarted();
        }
        if (block.timestamp > projectInfo.finishAt) {
            revert TossInvestProjectIsFinished();
        }

        address[] storage investors = $.projectInvestors[projectId];
        uint256 investorAmount = investors.length;
        if (investorAmount + amount > projectInfo.maxAmount) {
            revert TossInvestProjectFullInvested();
        }

        for (uint256 i = 0; i < amount;) {
            investors.push(msg.sender);
            unchecked {
                ++i;
            }
        }

        emit ProjectInvested(projectId, msg.sender, amount);

        $.erc20.safeTransferFrom(msg.sender, address(this), amount * projectInfo.price);
    }

    function finish(uint256 projectId) external nonReentrant whenNotPaused {
        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        ProjectInfo storage projectInfo = $.projects[projectId];
        if (!projectInfo.confirmed) {
            revert TossInvestProjectIsNotConfirmed();
        }

        address[] storage investors = $.projectInvestors[projectId];
        if (investors.length < projectInfo.maxAmount && block.timestamp < projectInfo.finishAt) {
            revert TossInvestProjectNotFinished();
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

    function finishMintErc721(uint256 projectId, ProjectInfo storage projectInfo, address[] memory investors) private {
        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        uint32 lastIndex = projectInfo.lastIndex;
        uint256 length = investors.length;
        if (length <= lastIndex) {
            revert TossInvestAlreadyAllErc721Minted();
        }

        TossErc721MarketV1 erc721;
        if (projectInfo.erc721Address == address(0)) {
            TossUpgradeableProxy proxy =
                new TossUpgradeableProxy(address($.erc721Implementation), abi.encodeCall(TossErc721MarketV1.__TossErc721MarketV1_init, (projectInfo.name, projectInfo.symbol)));
            projectInfo.erc721Address = address(proxy);
            erc721 = TossErc721MarketV1(address(proxy));
            if (bytes($.erc721BaseUri).length > 0) {
                erc721.setBaseUri(string.concat($.erc721BaseUri, Strings.toString(projectId), "/"));
            }
            erc721.grantRole(erc721.DEFAULT_ADMIN_ROLE(), $.tossProjectAddress);
            erc721.grantRole(erc721.UPGRADER_ROLE(), $.tossProjectAddress);
            uint256 total = projectInfo.price * length;
            uint256 platformAmount = total * $.platformCut / PLATFORM_CUT_PRECISION;
            projectInfo.mintedAt = uint64(block.timestamp);
            emit ProjectErc721Created(projectId, projectInfo.erc721Address, address($.erc721Implementation));

            $.erc20.safeTransfer(projectInfo.projectWallet, total - platformAmount);
            $.erc20.safeTransfer($.platformAddress, platformAmount);
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

    function finishReturnInvestment(ProjectInfo storage projectInfo, address[] memory investors) private {
        TossInvestBaseStorage storage $ = _getTossInvestBaseStorage();
        uint32 lastIndex = projectInfo.lastIndex;
        uint256 length = investors.length;
        if (length <= lastIndex) {
            revert TossInvestAlreadyAllInvestmentReturned();
        }

        uint256 price = projectInfo.price;
        uint32 i = lastIndex;
        address lastInvestor = investors[i];
        uint256 acumulated = 0;
        for (; i < length;) {
            if (lastInvestor == investors[i]) {
                ++acumulated;
            } else {
                $.erc20.safeTransfer(lastInvestor, acumulated * price);
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
        projectInfo.lastIndex = i;
        $.erc20.safeTransfer(lastInvestor, acumulated * price);
    }
}
