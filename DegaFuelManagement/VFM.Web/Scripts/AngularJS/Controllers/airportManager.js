degatech.page.airportManagerControllerFactory = function ($scope,
    $rootScope,
    $uibModal,
    $baseController,
    $usersService,
    $acukwikAirportsService,
    $airportPriceMarginsService,
    $fboPriceMarginsService,
    Notification,
    //SmartTable,
    $timeout) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$acukwikAirportsService = $acukwikAirportsService;
    vm.$airportPriceMarginsService = $airportPriceMarginsService;
    vm.$fboPriceMarginsService = $fboPriceMarginsService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;
    vm.notify = vm.$usersService.getNotifier($scope);

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.onToggleFBOMargins = _onToggleFBOMargins;
    vm.addFBOMargin = _addFBOMargin;
    vm.saveFBO = _saveFBO;
    vm.deleteFBOMargin = _deleteFBOMargin;
    vm.changeFBOMargin = _changeFBOMargin;
    vm.getFBODetails = _getFBODetails;
    vm.exportAirports = _exportAirports;
    vm.clearFilters = _clearFilters;
    vm.viewAirports = _viewAirports;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.airportsAndMargins = null;
    vm.displayedAirportsAndMargins = [];
    vm.currentAirport = null;
    vm.savedFBOMargin = null;
    vm.fboDetails = null;
    vm.test = 'test';
    vm.RecordsPerPage = [10, 20, 50, 100, 200];
    vm.AirportRecordsPerPage = 20;
    vm.isAirportsReady = false;

    _init();

    $scope.$on('filteredRows', function (event, data) {
        vm.filteredRows = data;
    });

    //PRIVATE METHODS//////////////////////////////////////////////
    function _init() {
        storage.SetSessionItem("LastActiveSection", "AIRPORTS");
        console.log("Getting Airports...");
        if ($acukwikAirportsService.airports) {
            vm.airportsAndMargins = $acukwikAirportsService.airports;
            _viewAirports();
        }
        else vm.$acukwikAirportsService.getAirportsAndMarginsByAdminClient($usersService.user.ClientID, _onGetAirportsAndMarginsSuccess, vm.onError);
    }

    function _clearFilters() {
        for (var property in vm.filter) {
            vm.filter[property] = '';
        }
    }

    function _onToggleFBOMargins(airport) {
        airport.showingFBOMargins = !airport.showingFBOMargins;
        if (airport.showingFBOMargins) {
            console.log("Getting FBO Price Margins...");
            vm.$fboPriceMarginsService.getFBOPriceMarginsByAdminClientAndICAO($usersService.user.ClientID, airport, _onGetFBOMarginsSuccess, vm.onError);
        }
    }

    function _addFBOMargin(FBOMargins) {
        FBOMargins.push({
            AdminClientID: $usersService.user.ClientID,
            ICAO: airport.ICAO,
            IsEditable: true,
            IsOmitted: false
        });
    }

    function _saveFBO(fboMargin, airport) {
        vm.savingFBOMargin = true;
        fboMargin.AdminClientID = $usersService.user.ClientID;
        fboMargin.ICAO = airport.ICAO;
        if (fboMargin.IsEditable == null) fboMargin.IsEditable = true;
        if (!fboMargin.IsOmitted) fboMargin.IsOmitted = false;
        vm.copiedFBOMargin = fboMargin;
        vm.currentFBOs = airport.FBOMargins;
        if (!vm.savedFBOMargin.Id || vm.savedFBOMargin.Id === 0)
            vm.$fboPriceMarginsService.addFBOPriceMargin(vm.savedFBOMargin, _onSaveFBOMarginSuccess, vm.onError);
        else vm.$fboPriceMarginsService.updateFBOPriceMargin(vm.savedFBOMargin.Id, vm.savedFBOMargin, _onSaveFBOMarginSuccess, vm.onError);
    }

    function _changeFBOMargin(fboMargin, airport) {
        vm.savedFBOMargin = angular.copy(fboMargin);
        if (fboMargin.ICAO == null) airport.FBOMargins.push(vm.savedFBOMargin);
        if (fboMargin.IsEditable || fboMargin.IsEditable == null) fboMargin.FBO = '';
        var omittedMargins = [];
        for (i = 0; i < airport.FBOMargins.length; i++) {
            if (airport.FBOMargins[i].IsOmitted) omittedMargins.push(airport.FBOMargins[i]);
        }
        if (omittedMargins.length == airport.FBOMargins.length) airport.IsInactive = true;
        else airport.IsInactive = false;
        if (!airport.AdminClientID) airport.AdminClientID = $usersService.user.ClientID;
        vm.$airportPriceMarginsService.addAirportPriceMargin(airport, _onAddAirportMarginSuccess, vm.onError);
        _saveFBO(vm.savedFBOMargin, airport);
    }

    function _deleteFBOMargin(fboMargin, airport) {
        vm.deletedMargin = fboMargin;
        vm.currentAirport = airport;
        if (fboMargin.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = "Are you sure you want to DELETE this FBO?";
                    $scope.confirm = function () {
                        $uibModalInstance.close(vm.$fboPriceMarginsService.deleteFBOPriceMargin(fboMargin, _onDeleteFBOMarginSuccess, vm.onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }
                }
            });
        }
    }

    function _getFBODetails(fbo, icao) {
        $fboPriceMarginsService.icaoFBO = {
            FBO: fbo,
            ICAO: icao,
            AdminClientID: $usersService.user.ClientID
        };
        if (vm.filter) $acukwikAirportsService.filter = vm.filter;
        vm.$rootScope.ApplicationState.ActiveSection = 'FBO DETAILS';
    }

    function _exportAirports() {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/ExportCSV.html',
            controller: 'exportAirportsModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        exportData: {
                            ClientID: vm.$usersService.user.ClientID,
                            ClientName: vm.$usersService.user.Client.Name,
                            ListOfIDs: (vm.filteredRows.length == 1 ? vm.filteredRows[0].Airport_ID.toString() : (vm.filteredRows.map(function (a) { return a.Airport_ID })).toString())
                        }
                    }
                }
            }
        });
    }

    function _onExportSuccess(data) {
        console.log(data.Item);
    }

    function _viewAirports() {
        $timeout(function () {
            vm.isAirportsReady = true;
            if ($acukwikAirportsService.filter) vm.filter = $acukwikAirportsService.filter;
            $rootScope.triggerFilters();
        });
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetAirportsAndMarginsSuccess(data) {
        vm.notify(function () {
            vm.airportsAndMargins = $acukwikAirportsService.airports = JSON.parse(data.Item);
            console.log("AIRPORTS: ", vm.airportsAndMargins);
            _viewAirports();
        });

        console.log("1st group of AIRPORTS: ", vm.airportsAndMargins);
    }

    function _onAddAirportMarginSuccess(data) {
        console.log("Airport Margin Updated!");
        vm.notify(function () {
        });
    }

    function _onGetFBOMarginsSuccess(data, airport) {
        vm.notify(function () {
            airport.FBOMargins = data.Items;
            console.log("FBO PRICE MARGINS: ", airport.FBOMargins);
        });
    }

    function _onSaveFBOMarginSuccess(data) {
        vm.notify(function () {
            vm.savingFBOMargin = false;
            if (data.Item && (!vm.savedFBOMargin.Id || vm.savedFBOMargin.Id === 0)) vm.savedFBOMargin.Id = data.Item;
            vm.copiedFBOMargin = null;
        });
        console.log("FBO Updated!");
    }

    function _onDeleteFBOMarginSuccess() {
        vm.notify(function () {
            var index = vm.currentAirport.FBOMargins.indexOf(vm.deletedMargin);
            if (index > -1) vm.currentAirport.FBOMargins.splice(index, 1);
            var omittedMargins = [];
            for (i = 0; i < vm.currentAirport.FBOMargins.length; i++) {
                if (vm.currentAirport.FBOMargins[i].IsOmitted) omittedMargins.push(vm.currentAirport.FBOMargins[i]);
            }
            if (omittedMargins.length == vm.currentAirport.FBOMargins.length) {
                vm.currentAirport.IsInactive = true;
                vm.$airportPriceMarginsService.addAirportPriceMargin(vm.currentAirport, _onAddAirportMarginSuccess, vm.onError);
            }
        });
    }

    function _onError() {
        vm.error = "An error has occurred!";
        console.log(vm.error);
        Notification.error({
            model: this,
            scope: $scope,
            //templateUrl: '/Partials/Common/Notifications/Login.html',
            message: vm.error,
            delay: 3000,
            closeOnClick: false
        });
    }
}

degatech.ng.addController(degatech.ng.app.module,
    "airportManagerController",
    [
        '$scope',
        '$rootScope',
        '$uibModal',
        '$baseController',
        "$usersService",
        "$acukwikAirportsService",
        "$airportPriceMarginsService",
        "$fboPriceMarginsService",
        "Notification",
        //"SmartTable",
        "$timeout"
    ],
    degatech.page.airportManagerControllerFactory);

////////////////////////////MODAL - EXPORT CSV///////////////////////////////////

degatech.services.exportAirportsModalFactory = function ($baseService) {
    var aServiceObject = degatech.services.airportPriceMargins;
    var newService = $baseService.merge(true, {}, aServiceObject, $baseService);
    return newService;
}

degatech.ng.addService(degatech.ng.app.module, "$exportAirportsModalService", ["$baseService"], degatech.services.exportAirportsModalFactory);

degatech.page.exportAirportsModalControllerFactory = function ($scope, $uibModalInstance, $exportAirportsModalService, items, $uibModal) {

    var vm = this;

    vm.$scope = $scope;
    vm.modalInstance = $uibModalInstance;
    vm.$exportAirportsModalService = $exportAirportsModalService;
    vm.items = items;

    vm.notify = vm.$exportAirportsModalService.getNotifier($scope);

    vm.cancel = _cancel;

    vm.items = items;

    _init();

    function _init() {
        console.log(vm.items);
        console.log("Exporting Airports...");
        vm.$exportAirportsModalService.exportAirports(vm.items.exportData, _onExportSuccess, _onError);
    }

    function _cancel() {
        $uibModalInstance.close();
    }

    function _onExportSuccess(data) {
        console.log("Airports exported!")
        vm.notify(function () {
            vm.downloadLink = data;
        });
    }

    function _onError() {
        vm.notify(function () {
            vm.error = "An error has occurred!";
            console.log(vm.error);
        });
    }
}

degatech.ng.addController(degatech.ng.app.module
            , "exportAirportsModalController"
            , ['$scope',
                '$uibModalInstance',
                "$exportAirportsModalService",
                'items',
                '$uibModal'
            ]
            , degatech.page.exportAirportsModalControllerFactory);

