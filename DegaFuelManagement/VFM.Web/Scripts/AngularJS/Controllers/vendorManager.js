degatech.page.vendorManagerControllerFactory = function ($scope,
    $rootScope,
    $uibModal,
    $baseController,
    $usersService,
    $vendorsService) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$vendorsService = $vendorsService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;
    vm.notify = vm.$usersService.getNotifier($scope);

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.addVendor = _addVendor;
    vm.editVendor = _editVendor;
    vm.saveVendor = _saveVendor;
    vm.deleteVendor = _deleteVendor;
    vm.showAll = _showAll;
    vm.showDeactivated = _showDeactivated;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.RecordsPerPage = [10, 20, 50, 100, 200];
    vm.VendorRecordsPerPage = 20;
    vm.filter = {
        DeactivatedStatus: '',
        DeactivatedLabel: 'Show All'
    };

    //PUBLIC FILTERS//////////////////////////////////////////////
    vm.vendorFilter = _vendorFilter;

    _init();

    //PRIVATE METHODS//////////////////////////////////////////////
    function _init() {
        storage.SetSessionItem("LastActiveSection", "VENDORS");
        console.log("Getting Vendors...");
        vm.$vendorsService.getVendorsByAdminClient($usersService.user.ClientID, _onGetVendorsSuccess, _onError);
    }

    function _addVendor() {
        if (_needsEmptyVendor(vm.vendors)) {
            vm.vendors.unshift({
                AdminClientID: $usersService.user.ClientID,
                IsDeactivated: true,
                IsEditable: true,
                IsExcluded: false,
                Name: '',
                Note: ''
            });
        }
    }

    function _needsEmptyVendor(vendors) {
        var filteredVendors = vendors.filter(function (a) { return a.Id });
        if (filteredVendors.length == vendors.length) return true;
        else return false;
    }

    function _editVendor(vendor) {
        vendor.IsEditable = !vendor.IsEditable;
        vm.editedVendor = angular.copy(vendor);
    }

    function _saveVendor(vendor) {
        vm.savedVendor = vendor;
        if (vendor.Id > 0) vm.$vendorsService.updateVendor(vendor.Id, vendor, _onSaveVendorSuccess, _onError);
        else vm.$vendorsService.addVendor(vendor, _onSaveVendorSuccess, _onError);
    }

    function _deleteVendor(vendor) {
        if (vendor.Id > 0) {
            if (confirm("Are you sure you want to delete this vendor?")) {
                vm.deletedVendor = vendor;
                vm.$vendorsService.deleteVendor(vendor.Id, _onDeleteVendorSuccess, _onError);
            }
        }
        else vm.vendors.shift();
    }

    function _showAll() {
        vm.IsDeactivatedOnly = false;
    }

    function _showDeactivated() {
        vm.IsDeactivatedOnly = true;
    }

    //PRIVATE FILTERS//////////////////////////////////////////////
    function _vendorFilter(vendor) {
        if (vendor.IsDeactivated) return true;
        if (!vm.IsDeactivatedOnly) return true;
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetVendorsSuccess(data) {
        vm.notify(function () {
            vm.vendors = data.Items;
            console.log(vm.vendors);
        });
    }

    function _onSaveVendorSuccess(data) {
        vm.notify(function () {
            vm.savedVendor.IsEditable = false;
            if (data.Item) vm.savedVendor.Id = data.Item;
            //vm.vendors.sort(function (a, b) {
            //    var x = a.Name;
            //    var y = b.Name;
            //    if (x < y) { return -1; }
            //    if (x > y) { return 1; }
            //    return 0;
            //});
        });
    }

    function _onDeleteVendorSuccess() {
        vm.notify(function () {
            var index = vm.vendors.indexOf(vm.deletedVendor);
            if (index > -1) vm.vendors.splice(index, 1);
        });
    }

    function _onError() {
        vm.notify(function () {
            vm.error = "An error has occurred!";
            console.log(vm.error);
        });
    }

}

degatech.ng.addController(degatech.ng.app.module,
    "vendorManagerController",
    [
        '$scope',
        '$rootScope',
        '$uibModal',
        '$baseController',
        "$usersService",
        "$vendorsService"
    ],
    degatech.page.vendorManagerControllerFactory);