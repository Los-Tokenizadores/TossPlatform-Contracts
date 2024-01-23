// SPDX-License-Identifier: UNLICENSED
pragma solidity ^0.8.20;

import "./BaseTest.sol";

contract TossInvestTest is BaseTest {
    uint256 constant GAS_BLOCK = 30_000_000;

    TossErc20V1 erc20;
    TossErc721MarketV1 erc721Implementation;
    TossInvestV1 invest;
    uint256 mintAmount = 100 ether;
    address bank = vm.addr(0x9999);
    string uri = "http://baseuri";
    uint16 platformCut = 1000;

    function setUp() public override {
        super.setUp();
        erc20 = DeployWithProxyUtil.tossErc20V1("Erc20 Test", "E20T", mintAmount);
        erc721Implementation = new TossErc721MarketV1();
        invest = DeployWithProxyUtil.tossInvestV1(IERC20(address(erc20)), erc721Implementation, bank, platformCut, uri);
    }

    function test_upgrade() public {
        TossInvestV1 investInit = new TossInvestV1();
        assertNotEq(invest.getImplementation(), address(investInit));
        invest.upgradeToAndCall(address(investInit), "");
        assertEq(invest.getImplementation(), address(investInit));
        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, invest.UPGRADER_ROLE()));
        invest.upgradeToAndCall(address(investInit), "");
    }

    function test_initialization() public {
        TossInvestV1 investInit = DeployWithProxyUtil.tossInvestV1(IERC20(address(erc20)), erc721Implementation, bank, 1000, uri);

        assertEq(address(investInit.getErc20()), address(erc20));
        assertEq(address(investInit.getErc721Implementation()), address(erc721Implementation));
        assertEq(investInit.getErc721BaseUri(), uri);
    }

    function test_initializationRevert() public {
        address investImp = invest.getImplementation();
        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc20"));
        new TossUpgradeableProxy(investImp, abi.encodeCall(TossInvestV1.__TossInvestV1_init, (IERC20(address(0)), erc721Implementation, bank, 1000, uri)));

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc721Implementation"));
        new TossUpgradeableProxy(investImp, abi.encodeCall(TossInvestV1.__TossInvestV1_init, (IERC20(address(erc20)), TossErc721MarketV1(address(0)), bank, 1000, uri)));

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "platformAddress"));
        new TossUpgradeableProxy(investImp, abi.encodeCall(TossInvestV1.__TossInvestV1_init, (IERC20(address(erc20)), erc721Implementation, address(0), 1000, uri)));
    }

    function test_initializationCutRevert(uint16 cut) public {
        address investImp = invest.getImplementation();
        cut = uint16(bound(cut, invest.CUT_PRECISION() + 1, type(uint16).max));

        vm.expectRevert(abi.encodeWithSelector(TossCutOutOfRange.selector, cut));
        new TossUpgradeableProxy(investImp, abi.encodeCall(TossInvestV1.__TossInvestV1_init, (IERC20(address(erc20)), erc721Implementation, bank, cut, uri)));
    }

    function test_setErc721Implementation() public {
        TossErc721MarketV1 invalidImp = TossErc721MarketV1(address(new TossSellerV1()));

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestInvalidErc721Implementation.selector, invalidImp));
        invest.setErc721Implementation(invalidImp);

        TossErc721MarketV1 erc721Imp = new TossErc721MarketV1();
        invest.setErc721Implementation(erc721Imp);
        assertEq(invest.getErc721Implementation(), address(erc721Imp));

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, invest.DEFAULT_ADMIN_ROLE()));
        invest.setErc721Implementation(invalidImp);

        vm.startPrank(owner);
        assertEq(invest.getErc721Implementation(), address(erc721Imp));
    }

    function test_setErc721BaseUri(string memory newUri) public {
        invest.setErc721BaseUri(newUri);

        assertEq(invest.getErc721BaseUri(), newUri);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, invest.DEFAULT_ADMIN_ROLE()));
        invest.setErc721BaseUri("value that is not save");

        vm.startPrank(owner);
        assertEq(invest.getErc721BaseUri(), newUri);
    }

    function test_addProject(
        string memory name,
        string memory symbol,
        uint32 targetAmount,
        uint32 maxAmount,
        uint128 price,
        uint64 startAt,
        uint64 finishAt,
        address projectWallet
    ) public {
        vm.assume(targetAmount > 0);
        vm.assume(maxAmount >= targetAmount);
        vm.assume(price > 0);
        vm.assume(startAt > block.timestamp);
        vm.assume(finishAt > startAt);
        vm.assume(projectWallet > address(0));

        assertEq(invest.projectAmount(), 0);

        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);
        (TossInvestBase.ProjectInfo memory project, uint256 invested, uint256 inversors) = invest.getProject(0);

        assertEq(invest.projectAmount(), 1);
        assertEq(invested, 0);
        assertEq(inversors, 0);

        assertEq(name, project.name);
        assertEq(symbol, project.symbol);
        assertEq(targetAmount, project.targetAmount);
        assertEq(maxAmount, project.maxAmount);
        assertEq(price, project.price);
        assertEq(startAt, project.startAt);
        assertEq(finishAt, project.finishAt);
        assertEq(projectWallet, project.projectWallet);
    }

    function test_addProjectRevert() public {
        vm.warp(100 days);
        string memory name = "name";
        string memory symbol = "symbol";
        uint32 targetAmount = 10;
        uint32 maxAmount = 20;
        uint128 price = 1;
        uint64 startAt = uint64(block.timestamp);
        uint64 finishAt = uint64(block.timestamp + 1 minutes);
        address projectWallet = address(1);

        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "target amount"));
        invest.addProject(name, symbol, 0, maxAmount, price, startAt, finishAt, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectTargetIsGreaterThanMax.selector, targetAmount, targetAmount - 1));
        invest.addProject(name, symbol, targetAmount, targetAmount - 1, price, startAt, finishAt, projectWallet);

        uint64 startAtInvalid = uint64(block.timestamp - 1 days);
        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectStartAtLessThanCurrentDate.selector, startAtInvalid, block.timestamp));
        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAtInvalid, finishAt, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "price"));
        invest.addProject(name, symbol, targetAmount, maxAmount, 0, startAt, finishAt, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectStartAtGreaterThanFinishAt.selector, startAt, startAt - 1));
        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, startAt - 1, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "project wallet"));
        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, address(0));

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, invest.PROJECT_ROLE()));
        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);
    }

    function test_changeProject(
        string memory name,
        string memory symbol,
        uint32 targetAmount,
        uint32 maxAmount,
        uint128 price,
        uint64 startAt,
        uint64 finishAt,
        address projectWallet
    ) public {
        vm.assume(targetAmount > 0);
        vm.assume(maxAmount >= targetAmount);
        vm.assume(price > 0);
        vm.assume(startAt > block.timestamp);
        vm.assume(finishAt > startAt);
        vm.assume(projectWallet > address(0));

        invest.addProject("", "", 1, 1, 1, uint64(block.timestamp), uint64(block.timestamp), address(1));
        invest.changeProject(0, name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);

        (TossInvestBase.ProjectInfo memory project, uint256 invested, uint256 inversors) = invest.getProject(0);

        assertEq(invested, 0);
        assertEq(inversors, 0);

        assertEq(name, project.name);
        assertEq(symbol, project.symbol);
        assertEq(targetAmount, project.targetAmount);
        assertEq(maxAmount, project.maxAmount);
        assertEq(price, project.price);
        assertEq(startAt, project.startAt);
        assertEq(finishAt, project.finishAt);
        assertEq(projectWallet, project.projectWallet);
    }

    function test_changeProjectRevert() public {
        vm.warp(100 days);
        string memory name = "name";
        string memory symbol = "symbol";
        uint32 targetAmount = 10;
        uint32 maxAmount = 20;
        uint128 price = 1;
        uint64 startAt = uint64(block.timestamp);
        uint64 finishAt = uint64(block.timestamp + 1 days);
        address projectWallet = address(1);

        invest.addProject("", "", 1, 1, 1, uint64(block.timestamp), uint64(block.timestamp + 1 minutes), alice);

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectNotExist.selector, 3));
        invest.changeProject(3, name, symbol, 0, maxAmount, price, startAt, finishAt, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "target amount"));
        invest.changeProject(0, name, symbol, 0, maxAmount, price, startAt, finishAt, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectTargetIsGreaterThanMax.selector, targetAmount, targetAmount - 1));
        invest.changeProject(0, name, symbol, targetAmount, targetAmount - 1, price, startAt, finishAt, projectWallet);

        uint64 startAtInvalid = uint64(block.timestamp - 1 days);
        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectStartAtLessThanCurrentDate.selector, startAtInvalid, block.timestamp));
        invest.changeProject(0, name, symbol, targetAmount, maxAmount, price, startAtInvalid, finishAt, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "price"));
        invest.changeProject(0, name, symbol, targetAmount, maxAmount, 0, startAt, finishAt, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectStartAtGreaterThanFinishAt.selector, startAt, startAt - 1));
        invest.changeProject(0, name, symbol, targetAmount, maxAmount, price, startAt, startAt - 1, projectWallet);

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "project wallet"));
        invest.changeProject(0, name, symbol, targetAmount, maxAmount, price, startAt, finishAt, address(0));

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(IAccessControl.AccessControlUnauthorizedAccount.selector, alice, invest.PROJECT_ROLE()));
        invest.changeProject(0, name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);

        invest.confirm(0);

        vm.startPrank(owner);
        vm.expectRevert(TossInvestBase.TossInvestProjectIsConfirmed.selector);
        invest.changeProject(0, name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);
    }

    function test_getProjectRevert() public {
        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectNotExist.selector, 5));
        invest.getProject(5);
    }

    function test_addProjectAndConfirmRevert(
        string memory name,
        string memory symbol,
        uint32 targetAmount,
        uint32 maxAmount,
        uint128 price,
        uint64 startAt,
        uint64 finishAt,
        address projectWallet
    ) public {
        vm.assume(targetAmount > 0);
        vm.assume(maxAmount >= targetAmount);
        vm.assume(price > 0);
        vm.assume(startAt > block.timestamp);
        vm.assume(finishAt > startAt);
        vm.assume(projectWallet > address(0));

        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);

        vm.startPrank(alice);
        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestNotProjectOwner.selector, 0));
        invest.confirm(0);

        vm.startPrank(projectWallet);

        vm.warp(finishAt);
        vm.expectRevert(TossInvestBase.TossInvestProjectAlreadyFinished.selector);
        invest.confirm(0);

        vm.warp(startAt);
        invest.confirm(0);

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectNotExist.selector, 1));
        invest.confirm(1);

        vm.startPrank(projectWallet);
        vm.expectRevert(TossInvestBase.TossInvestProjectIsConfirmed.selector);
        invest.confirm(0);
    }

    function test_invest(
        string memory name,
        string memory symbol,
        uint32 targetAmount,
        uint32 maxAmount,
        uint128 price,
        uint64 startAt,
        uint64 finishAt,
        address projectWallet
    ) public {
        targetAmount = uint32(bound(targetAmount, 1, 100_000));
        maxAmount = uint32(bound(maxAmount, targetAmount + 12, targetAmount + 10_000));
        price = uint128(bound(price, 1, mintAmount / 100));
        vm.assume(startAt > block.timestamp);
        vm.assume(finishAt > startAt);
        vm.assume(projectWallet > address(0));

        erc20.transfer(alice, price * 2);

        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);
        vm.startPrank(projectWallet);
        invest.confirm(0);

        vm.warp(startAt);

        vm.startPrank(owner);
        erc20.approve(address(invest), 10 * price);
        uint16 amount = uint16(bound(maxAmount, 1, 10));
        invest.invest(0, amount);

        (, uint256 invested, uint256 inversors) = invest.getProject(0);

        assertEq(inversors, 1);
        assertEq(invested, amount);

        vm.startPrank(alice);
        SigUtils.Permit memory permit = SigUtils.signPermit(alice, alicePrivateKey, address(invest), 2 * price, block.timestamp + 1 days, erc20);
        invest.investWithPermit(0, 2, permit.value, permit.deadline, permit.v, permit.r, permit.s);

        (, invested, inversors) = invest.getProject(0);

        assertEq(inversors, 2);
        assertEq(invested, amount + 2);
    }

    function test_investRevert(
        string memory name,
        string memory symbol,
        uint32 targetAmount,
        uint32 maxAmount,
        uint128 price,
        uint64 startAt,
        uint64 finishAt,
        address projectWallet
    ) public {
        targetAmount = uint32(bound(targetAmount, 1, 100_000));
        maxAmount = uint32(bound(maxAmount, targetAmount + 12, targetAmount + 10_000));
        price = uint128(bound(price, 1, mintAmount / 100));
        vm.assume(startAt > block.timestamp);
        vm.assume(finishAt > startAt);
        vm.assume(projectWallet > address(0));

        erc20.transfer(alice, price * 2);
        erc20.approve(address(invest), 10 * price);

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectNotExist.selector, 0));
        invest.invest(0, 1);

        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);

        vm.expectRevert(TossInvestBase.TossInvestProjectIsNotConfirmed.selector);
        invest.invest(0, 1);

        vm.startPrank(projectWallet);
        invest.confirm(0);

        vm.expectRevert(TossInvestBase.TossInvestProjectNotStarted.selector);
        invest.invest(0, 1);

        vm.warp(startAt);

        vm.startPrank(owner);
        vm.expectRevert(abi.encodeWithSelector(TossValueIsZero.selector, "amount"));
        invest.invest(0, 0);

        (, uint256 invested, uint256 inversors) = invest.getProject(0);

        assertEq(inversors, 0);
        assertEq(invested, 0);

        vm.warp(uint256(finishAt) + 1);

        vm.expectRevert(TossInvestBase.TossInvestProjectIsFinished.selector);
        invest.invest(0, 1);
    }

    function test_investFullRevert(string memory name, string memory symbol, uint32 targetAmount, uint128 price, uint64 startAt, uint64 finishAt, address projectWallet) public {
        targetAmount = uint32(bound(targetAmount, 1, 100));
        price = uint128(bound(price, 1, mintAmount / targetAmount / 3));
        vm.assume(startAt > block.timestamp);
        vm.assume(finishAt > startAt);
        vm.assume(projectWallet > address(0));

        erc20.approve(address(invest), mintAmount);

        invest.addProject(name, symbol, targetAmount, targetAmount, price, startAt, finishAt, projectWallet);

        vm.startPrank(projectWallet);
        invest.confirm(0);

        vm.warp(startAt);

        vm.startPrank(owner);
        invest.invest(0, uint16(targetAmount));

        (, uint256 invested, uint256 inversors) = invest.getProject(0);

        assertEq(inversors, 1);
        assertEq(invested, targetAmount);

        vm.expectRevert(TossInvestBase.TossInvestProjectFullInvested.selector);
        invest.invest(0, 1);

        vm.expectRevert(TossInvestBase.TossInvestProjectFullInvested.selector);
        invest.invest(0, 100);
    }

    function test_finishMintErc721() public {
        string memory name = "test name";
        string memory symbol = "TN";
        uint32 targetAmount = 10;
        uint32 maxAmount = 15;
        uint128 price = 1 ether;
        uint64 startAt = 1 days;
        uint64 finishAt = 2 days;
        address projectWallet = address(1234);

        erc20.transfer(alice, price * 2);

        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);
        vm.startPrank(projectWallet);
        invest.confirm(0);
        vm.warp(startAt);

        vm.startPrank(owner);
        uint16 amount = 10;
        erc20.approve(address(invest), amount * price);
        invest.invest(0, amount);

        vm.startPrank(alice);
        SigUtils.Permit memory permit = SigUtils.signPermit(alice, alicePrivateKey, address(invest), 2 * price, block.timestamp + 1 days, erc20);
        invest.investWithPermit(0, 2, permit.value, permit.deadline, permit.v, permit.r, permit.s);

        (, uint256 invested, uint256 inversors) = invest.getProject(0);

        assertEq(inversors, 2);
        assertEq(invested, amount + 2);

        vm.warp(uint256(finishAt) + 1);

        //invest.finish{ gas: 1_500_000 }(0);
        invest.finish(0);

        TossInvestBase.ProjectInfo memory projectInfo;
        (projectInfo, invested, inversors) = invest.getProject(0);
        (, projectInfo, invested, inversors) = invest.getProjectByErc721Address(projectInfo.erc721Address);
        assertEq(projectInfo.mintedAt, block.timestamp);
        assertNotEq(projectInfo.erc721Address, address(0));
        TossErc721MarketV1 erc721 = TossErc721MarketV1(projectInfo.erc721Address);
        assertEq(erc721.balanceOf(owner), amount);
        assertEq(erc721.balanceOf(alice), 2);

        uint256 totalAmount = (amount + 2) * price;
        uint256 platformCutAmount = totalAmount * platformCut / invest.CUT_PRECISION();
        assertEq(erc20.balanceOf(projectWallet), totalAmount - platformCutAmount);
        assertEq(erc20.balanceOf(bank), platformCutAmount);

        vm.expectRevert(TossInvestBase.TossInvestAlreadyAllErc721Minted.selector);
        invest.finish(0);

        vm.expectRevert(abi.encodeWithSelector(TossAddressIsZero.selector, "erc721"));
        invest.getProjectByErc721Address(address(0));

        vm.expectRevert(abi.encodeWithSelector(TossInvestBase.TossInvestProjectNotFoundByErc721.selector, address(1)));
        invest.getProjectByErc721Address(address(1));

        assertEq(invest.getProjectInvestor(0, 0), owner);
    }

    function test_finishReturn() public {
        string memory name = "test name";
        string memory symbol = "TN";
        uint32 targetAmount = 13;
        uint32 maxAmount = 15;
        uint128 price = 1 ether;
        uint64 startAt = 1 days;
        uint64 finishAt = 2 days;
        address projectWallet = address(1234);

        erc20.transfer(alice, price * 2);
        uint256 ownerInitialBalance = erc20.balanceOf(owner);
        uint256 aliceInitialBalance = erc20.balanceOf(alice);

        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);
        vm.startPrank(projectWallet);
        invest.confirm(0);
        vm.warp(startAt);

        vm.startPrank(owner);
        uint16 amount = 10;
        erc20.approve(address(invest), amount * price);
        invest.invest(0, amount);

        vm.startPrank(alice);
        SigUtils.Permit memory permit = SigUtils.signPermit(alice, alicePrivateKey, address(invest), 2 * price, block.timestamp + 1 days, erc20);
        invest.investWithPermit(0, 2, permit.value, permit.deadline, permit.v, permit.r, permit.s);

        (, uint256 invested, uint256 inversors) = invest.getProject(0);

        assertEq(inversors, 2);
        assertEq(invested, amount + 2);

        vm.warp(uint256(finishAt) + 1);

        uint256 totalAmount = (amount + 2) * price;
        assertEq(erc20.balanceOf(address(invest)), totalAmount);
        //invest.finish{ gas: 1_500_000 }(0);
        invest.finish(0);

        TossInvestBase.ProjectInfo memory projectInfo;
        (projectInfo, invested, inversors) = invest.getProject(0);
        assertEq(projectInfo.mintedAt, 0);
        assertEq(projectInfo.erc721Address, address(0));

        assertEq(erc20.balanceOf(projectWallet), 0);
        assertEq(erc20.balanceOf(bank), 0);

        assertEq(erc20.balanceOf(owner), ownerInitialBalance);
        assertEq(erc20.balanceOf(alice), aliceInitialBalance);

        vm.expectRevert(TossInvestBase.TossInvestAlreadyAllInvestmentReturned.selector);
        invest.finish(0);
    }

    function test_finishRevert() public {
        string memory name = "test name";
        string memory symbol = "TN";
        uint32 targetAmount = 13;
        uint32 maxAmount = 15;
        uint128 price = 1 ether;
        uint64 startAt = 1 days;
        uint64 finishAt = 2 days;
        address projectWallet = address(1234);
        erc20.approve(address(invest), mintAmount);

        invest.addProject(name, symbol, targetAmount, maxAmount, price, startAt, finishAt, projectWallet);
        vm.warp(startAt);
        vm.expectRevert(TossInvestBase.TossInvestProjectIsNotConfirmed.selector);
        invest.finish(0);

        vm.startPrank(projectWallet);
        invest.confirm(0);

        vm.expectRevert(TossInvestBase.TossInvestProjectNotFinished.selector);
        invest.finish(0);
    }
}
