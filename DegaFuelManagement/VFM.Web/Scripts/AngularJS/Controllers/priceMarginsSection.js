degatech.page.priceMarginsControllerFactory = function ($scope,
    $baseController,
    $uibModal,
    $usersService,
    $priceMarginsService,
    $priceMarginTiersService,
    Notification,
    $siteSettingsService) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$priceMarginsService = $priceMarginsService;
    vm.$priceMarginTiersService = $priceMarginTiersService;
    vm.$siteSettingsService = $siteSettingsService;

    vm.$scope = $scope;
    vm.notify = vm.$usersService.getNotifier($scope);

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.addPricing = _addPricing;
    vm.editPricing = _editPricing;
    vm.savePricing = _savePricing;
    vm.deletePricing = _deletePricing;
    vm.onToggleMarginTiers = _onToggleMarginTiers;
    vm.addTier = _addTier;
    vm.saveTier = _saveTier;
    vm.editTier = _editTier;
    vm.deleteTier = _deleteTier;
    vm.distributeAll = _distributeAll;
    vm.distributeOnly = _distributeOnly;
    vm.addNote = _addNote;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.savedMargin = null;
    vm.deletedPricing = null;
    vm.priceMargins = null;
    vm.siteSettings = {
        ShowMarginVolumeTiers: true
    };

    _init();

    //PRIVATE METHODS//////////////////////////////////////////////
    function _init() {
        storage.SetSessionItem("LastActiveSection", "PRICE MARGINS");
        console.log("Getting Price Margins...");
        vm.$priceMarginsService.getPriceMarginsByAdminClient($usersService.user.ClientID, _onGetPriceMarginsSuccess, _onError);
        vm.$siteSettingsService.getSettings(_onGetSettingsSuccess, _onError);
    }

    function _addPricing() {
        vm.priceMargins.unshift({
            AdminClientID: $usersService.user.ClientID,
            IsAutoSaveable: true,
            Note: " ",
            Id: 0,
            Name: '',
            SortingName: ''
        });
    }

    function _editPricing(margin) {
        margin.IsEditing = true;
    }

    function _savePricing(margin) {
        console.log("Saving margin...");
        //vm.savingPricing = true;
        margin.IsEditing = false;
        vm.savedMargin = margin;
        if (margin.Id > 0) {
            vm.$priceMarginsService.updatePriceMargin(margin.Id, margin, _onSavePriceMarginSuccess, _onError);
        } else {
            vm.$priceMarginsService.addPriceMargin(margin, _onSavePriceMarginSuccess, _onError);
        }
    }

    function _deletePricing(margin) {
        vm.deletedPricing = margin;
        if (margin.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = "Are you sure you want to DELETE this margin?";
                    $scope.confirm = function () {
                        $uibModalInstance.close(vm.$priceMarginsService.deletePriceMargin(margin.Id, _onDeletePriceMarginSuccess, _onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }
                }
            });
        } else vm.priceMargins.shift();
    }

    function _onToggleMarginTiers(margin) {
        margin.showingMarginTiers = !margin.showingMarginTiers;
        if (margin.showingMarginTiers) {
            console.log("Getting Margin Tiers...");
            vm.$priceMarginTiersService.getPriceMarginTiersByMarginID(margin, _onGetMarginTiersSuccess, _onError);
        }
    }

    function _addTier(tier, margin) {
        if (!margin.PriceMarginTiers) {
            Notification.warning({
                model: this,
                scope: $scope,
                //templateUrl: '/Partials/Common/Notifications/Login.html',
                message: "Please add a margin before adding its tiers.",
                delay: 3000,
                closeOnClick: false
            });
        }
        console.log("Saving tier...");
        vm.savedTier = angular.copy(tier);
        vm.copiedTier = tier;
        vm.updatedMargin = margin;
        if (!vm.savedTier.PriceMarginID) vm.savedTier.PriceMarginID = vm.updatedMargin.Id;
        if (vm.savedTier.MaxGallon == null || vm.savedTier.MaxGallon == '') vm.savedTier.MaxGallon = 99999;
        if (vm.updatedMargin.PriceMarginTiers.length > 0) {

            var tiersBelowNewTier = vm.updatedMargin.PriceMarginTiers.filter(function (t) { return t.MinGallon < vm.savedTier.MinGallon });
            var tiersAboveNewTier = vm.updatedMargin.PriceMarginTiers.filter(function (t) {
                return t.MinGallon >= vm.savedTier.MaxGallon;
            });
            if (!tiersAboveNewTier || tiersAboveNewTier.length == 0) {
                var tiersAboveNewTier = vm.updatedMargin.PriceMarginTiers.filter(function (t) {
                    return t.MaxGallon > vm.savedTier.MaxGallon;
                });
            }

            if (tiersBelowNewTier.length > 0) var minGallonBelow = Math.max.apply(Math, tiersBelowNewTier.map(function (t) { return t.MinGallon }));
            if (tiersAboveNewTier.length > 0) var minGallonAbove = Math.min.apply(Math, tiersAboveNewTier.map(function (t) { return t.MinGallon }));
            if (minGallonBelow) var closestTierBelow = vm.updatedMargin.PriceMarginTiers.find(function (t) { return t.MinGallon == minGallonBelow });
            if (minGallonAbove) var closestTierAbove = vm.updatedMargin.PriceMarginTiers.find(function (t) { return t.MinGallon == minGallonAbove });
            if (closestTierBelow) closestTierBelow.MaxGallon = vm.savedTier.MinGallon - 1;
            if (closestTierBelow != closestTierAbove)
                if (closestTierAbove) closestTierAbove.MinGallon = vm.savedTier.MaxGallon + 1;
            if (!vm.savedTier.Id) {
                if (closestTierAbove) {
                    var index = vm.updatedMargin.PriceMarginTiers.indexOf(closestTierAbove);
                    if (index > -1) vm.updatedMargin.PriceMarginTiers.splice(index, 0, vm.savedTier);
                }
                else vm.updatedMargin.PriceMarginTiers.push(vm.savedTier);
            }
            vm.$priceMarginTiersService.addListOfTiers(vm.updatedMargin.PriceMarginTiers, _onSaveTierSuccess, _onError);
            console.log(vm.updatedMargin.PriceMarginTiers);
        } else {
            if (vm.savedTier.Id > 0) vm.$priceMarginTiersService.updatePriceMarginTier(vm.savedTier.Id, vm.savedTier, _onSaveTierSuccess, _onError);
            else vm.$priceMarginTiersService.addPriceMarginTier(vm.savedTier, _onSaveTierSuccess, _onError);
        }
    }

    function _saveTier(tier, margin) {
        tier.IsStillEditing = false;
        _addTier(tier, margin);
    }

    function _editTier(tier) {
        tier.IsEditing = true;
    }

    function _deleteTier(tier, margin) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE this tier?";
                $scope.confirm = function () {
                    console.log("Deleting tier...");
                    vm.deletedTier = tier;
                    vm.updatedMargin = margin;

                    var tiersBelowDeleted = vm.updatedMargin.PriceMarginTiers.filter(function (t) { return t.MaxGallon < vm.deletedTier.MinGallon });
                    var tiersAboveDeleted = vm.updatedMargin.PriceMarginTiers.filter(function (t) { return t.MinGallon >= vm.deletedTier.MaxGallon });
                    if (tiersBelowDeleted.length > 0) var maxGallonBelow = Math.max.apply(Math, tiersBelowDeleted.map(function (t) { return t.MaxGallon }));
                    if (tiersAboveDeleted.length > 0) var minGallonAbove = Math.min.apply(Math, tiersAboveDeleted.map(function (t) { return t.MinGallon }));
                    if (maxGallonBelow) var closestTierBelow = vm.updatedMargin.PriceMarginTiers.find(function (t) { return t.MaxGallon == maxGallonBelow });
                    if (minGallonAbove) var closestTierAbove = vm.updatedMargin.PriceMarginTiers.find(function (t) { return t.MinGallon == minGallonAbove });

                    if (closestTierBelow && closestTierAbove) closestTierBelow.MaxGallon = closestTierAbove.MinGallon - 1;
                    else if (closestTierAbove) closestTierAbove.MinGallon = 1;
                    else if (closestTierBelow) closestTierBelow.MaxGallon = 99999;

                    var index = vm.updatedMargin.PriceMarginTiers.indexOf(vm.deletedTier);
                    if (index > -1) vm.updatedMargin.PriceMarginTiers.splice(index, 1);
                    $uibModalInstance.close(vm.$priceMarginTiersService.deletePriceMarginTiers(vm.deletedTier.Id, _onDeleteTierSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _addNote(margin) {
        console.log("Saving note...");
        vm.$priceMarginsService.updatePriceMargin(margin.Id, margin, _onSavePriceMarginSuccess, _onError);
    }

    function _distributeAll() {
        vm.$priceMarginsService.distributeAllPriceMargins($usersService.user.ClientID, _onDistributeSuccess, _onError);
    }

    function _distributeOnly(margin) {
        vm.$priceMarginsService.distributePriceMargin($usersService.user.ClientID, margin.Id, _onDistributeSuccess, _onError);
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetPriceMarginsSuccess(data) {
        vm.notify(function () {
            vm.priceMargins = $priceMarginsService.margins = data.Items;
            console.log("PRICE MARGINS: ", vm.priceMargins);
        });
    }

    function _onGetSettingsSuccess(data) {
        vm.notify(function () {
            vm.siteSettings.ShowMarginVolumeTiers = data.Item.ShowMarginVolumeTiers;
        });
    }

    function _onSavePriceMarginSuccess(data) {
        console.log("Margin saved!");
        vm.notify(function () {
            vm.savingPricing = false;
            if (data.Item) {
                Notification.success({
                    model: this,
                    scope: $scope,
                    //templateUrl: '/Partials/Common/Notifications/Login.html',
                    message: "Margin Saved <br /> <br />",
                    delay: 3000,
                    closeOnClick: false
                });
                vm.savedMargin.Id = data.Item;
                vm.savedMargin.DateAdded = new Date();
                vm.savedMargin.PriceMarginTiers = [];
                var tier = {
                    MaxGallon: 99999,
                    MinGallon: 1,
                    IsEditing: true,
                    IsStillEditing: true
                };
                _addTier(tier, vm.savedMargin);
            }
            else Notification.success({
                model: this,
                scope: $scope,
                //templateUrl: '/Partials/Common/Notifications/Login.html',
                message: "Margin Updated <br /> <br />",
                delay: 3000,
                closeOnClick: false
            });
        });
    }

    function _onDeletePriceMarginSuccess() {
        vm.notify(function () {
            var index = vm.priceMargins.indexOf(vm.deletedPricing);
            if (index > -1) vm.priceMargins.splice(index, 1);
        });
    }

    function _onGetMarginTiersSuccess(data, margin) {
        vm.notify(function () {
            margin.PriceMarginTiers = data.Items;
            console.log("PRICE MARGIN TIERS: ", margin.PriceMarginTiers);
        });
    }

    function _onSaveTierSuccess(data) {
        console.log("Tiers updated!");
        vm.notify(function () {
            if (vm.copiedTier) {
                if (!vm.copiedTier.IsStillEditing) vm.copiedTier.IsEditing = false;
                if (!vm.copiedTier.Id) vm.copiedTier.Margin = 0;
                vm.copiedTier = null;
            }
            if (data.Item && vm.savedTier) {
                vm.savedTier.Id = data.Item;
                if (vm.updatedMargin.PriceMarginTiers.length == 0) vm.updatedMargin.PriceMarginTiers.push(vm.savedTier);
                if (!vm.savedTier.IsStillEditing) vm.savedTier.IsEditing = false;
                vm.savedTier = null;
            }
            if (vm.updatedMargin.PriceMarginTiers.length == 0) return;
            var highestTierMax = Math.max.apply(Math, vm.updatedMargin.PriceMarginTiers.map(function (t) { return t.MaxGallon }));
            if (highestTierMax < 99999) vm.updatedMargin.newTier.MinGallon = highestTierMax + 1;
            else vm.updatedMargin.newTier.MinGallon = '';
            vm.updatedMargin.newTier.MaxGallon = '';
        });
    }

    function _onDeleteTierSuccess() {
        console.log("Tier deleted!");
        console.log("Updating margin tiers...");
        vm.$priceMarginTiersService.addListOfTiers(vm.updatedMargin.PriceMarginTiers, _onSaveTierSuccess, _onError);
    }

    function _onDistributeSuccess() {
        console.log("Margin(s) distributed!");
        Notification.success({
            model: this,
            scope: $scope,
            //templateUrl: '/Partials/Common/Notifications/Login.html',
            message: "Margin(s) distributed!",
            delay: 3000,
            closeOnClick: false
        });
    }

    function _onError() {
        vm.notify(function () {
            vm.savingTier = false;
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }

}
degatech.ng.addController(degatech.ng.app.module,
    "priceMarginsController",
    [
        '$scope',
        '$baseController',
        '$uibModal',
        "$usersService",
        "$priceMarginsService",
        "$priceMarginTiersService",
        "Notification",
        "$siteSettingsService"
    ],
    degatech.page.priceMarginsControllerFactory);