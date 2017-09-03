degatech.page.fboDetailsControllerFactory = function ($scope,
    $rootScope,
    $timeout,
    $uibModal, $log,
    $baseController,
    $usersService,
    $fboPriceMarginsService,
    $aircraftExclusionsService,
    $customerDetailsService,
    $aircraftsService,
    $fboDetailCustomFieldsService,
    $vendorsService,
    $vendorExclusionsService) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$fboPriceMarginsService = $fboPriceMarginsService;
    vm.$aircraftExclusionsService = $aircraftExclusionsService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.$aircraftsService = $aircraftsService;
    vm.$fboDetailCustomFieldsService = $fboDetailCustomFieldsService;
    vm.$vendorsService = $vendorsService;
    vm.$vendorExclusionsService = $vendorExclusionsService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;
    vm.notify = vm.$usersService.getNotifier($scope);

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.leftFBO = _leftFBO;
    vm.rightFBO = _rightFBO;
    vm.onMarginChange = _onMarginChange;
    vm.editNote = _editNote;
    vm.saveNote = _saveNote;
    vm.cancelNote = _cancelNote;
    vm.addExclusion = _addExclusion;
    vm.saveExclusion = _saveExclusion;
    vm.editExclusion = _editExclusion;
    vm.deleteExclusion = _deleteExclusion;
    vm.changeVendorExclusion = _changeVendorExclusion;
    vm.onCompanySelected = _onCompanySelected;
    vm.onTailNumberSelected = _onTailNumberSelected;
    vm.editSupply = _editSupply;
    vm.cancelSupply = _cancelSupply;
    vm.addCustomField = _addCustomField;
    vm.editFields = _editFields;
    vm.saveFields = _saveFields;
    vm.cancelFields = _cancelFields;
    vm.deleteFields = _deleteFields;
    vm.exclude = _exclude;

    //PUBLIC FILTERS//////////////////////////////////////////////
    vm.vendorExclusionFilter = _vendorExclusionFilter;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.aircraftExclusions = [];
    vm.displayedExclusions = [];
    vm.IsDisabled = false;

    _init();

    //PRIVATE METHODS//////////////////////////////////////////////
    function _init() {
        console.log("Getting companies...");
        vm.$customerDetailsService.getCustomersByAdminClient($usersService.user.ClientID, _onGetCompaniesSuccess, _onError);
        console.log("Getting FBO Details...");
        vm.$fboPriceMarginsService.getFBODetails($fboPriceMarginsService.icaoFBO, _onGetFBODetailsSuccess, _onError);
    }

    function _leftFBO(fboName) {
        var index = vm.fboNames.indexOf(fboName);
        var leftFBO = vm.fboNames[index - 1];
        $fboPriceMarginsService.icaoFBO = vm.activeFBOs.find(function (fbo) { return fbo.FBO == leftFBO });
        vm.$fboPriceMarginsService.getFBODetails($fboPriceMarginsService.icaoFBO, _onGetFBODetailsSuccess, _onError);
        vm.IsDisabled = false;
    }

    function _rightFBO(fboName) {
        var index = vm.fboNames.indexOf(fboName);
        var rightFBO = vm.fboNames[index + 1];
        $fboPriceMarginsService.icaoFBO = vm.activeFBOs.find(function (fbo) { return fbo.FBO == rightFBO });
        vm.$fboPriceMarginsService.getFBODetails($fboPriceMarginsService.icaoFBO, _onGetFBODetailsSuccess, _onError);
        vm.IsDisabled = false;
    }

    function _onMarginChange(margin) {
        vm.fboDetails.Margin = margin;
        _updateFBODetails(vm.fboDetails);
    }

    function _editSupply() {
        vm.IsSupplyEditable = !vm.IsSupplyEditable;
    }

    function _cancelSupply(supply) {
        vm.fboDetails.AcukwikFBO.HandlerFuelSupply = vm.savedFBODetails.AcukwikFBO.HandlerFuelSupply;
        if (vm.IsSupplyEditable) vm.IsSupplyEditable = !vm.IsSupplyEditable;
    }

    function _editNote() {
        vm.IsNoteEditable = !vm.IsNoteEditable;
    }

    function _cancelNote(note) {
        vm.fboDetails.Note = vm.savedFBODetails.Note;
        if (vm.IsNoteEditable) vm.IsNoteEditable = !vm.IsNoteEditable;
    }

    function _saveNote(note) {
        vm.savedFBODetails.Note = note;
        _updateFBODetails(vm.fboDetails);
    }

    function _updateFBODetails(fboMargin) {
        vm.IsUpdating = true;
        console.log("Updating FBO margin...");
        if (fboMargin.Id == 0) {
            fboMargin.AdminClientID = $usersService.user.ClientID;
            vm.savedFBOMargin = fboMargin;
            vm.$fboPriceMarginsService.addFBOPriceMargin(fboMargin, _onSaveFBOMarginSuccess, _onError);
        }
        else vm.$fboPriceMarginsService.updateFBOPriceMargin(fboMargin.Id, fboMargin, _onSaveFBOMarginSuccess, _onError);
    }

    function _addExclusion() {
        if (_needsNewExclusion(vm.aircraftExclusions)) {
            vm.aircraftExclusions.unshift({
                AdminClientID: $usersService.user.ClientID,
                ICAO: $fboPriceMarginsService.icaoFBO.ICAO,
                IsReadyToSave: false,
                FBO: $fboPriceMarginsService.icaoFBO.FBO,
                Client: {}
            });
            vm.IsDisabled = true;
        }
    }

    function _needsNewExclusion(exclusions) {
        var filteredExclusions = exclusions.filter(function (a) { return a.Id });
        if (filteredExclusions.length == exclusions.length) return true;
        else return false;
    }

    function _onCompanySelected(exclusion) {
        console.log("Getting tail numbers...");
        vm.updatingExclusion = true;
        if (exclusion.Client.Name == 'APPLY TO ALL') {
            exclusion.Client.CustClientID = 0;
            exclusion.TailNumberList = ['APPLY TO ALL']
            exclusion.aircrafts = [{ TailNumber: 'APPLY TO ALL' }];
            vm.updatingExclusion = false;
        } else {
            exclusion.CustClientID = exclusion.Client.CustClientID;
            vm.$aircraftsService.getRemainingAircrafts(exclusion, _onGetAircraftsSuccess, _onError);
        }
        exclusion.IsReadyToSave = !exclusion.IsReadyToSave;
    }

    function _onTailNumberSelected(exclusion) {
        if (exclusion.TailNumberList.length > 0) {
            for (var i = 0; i < exclusion.TailNumberList.length; i++) {
                if (exclusion.TailNumberList[i] == 'APPLY TO ALL') {
                    exclusion.TailNumberList = ['APPLY TO ALL'];
                    break;
                }
            }
        }
    }

    function _exclude(exclusion) {
        if (exclusion.Id > 0 && !exclusion.IsEditable)
            _saveExclusion(exclusion);
    }

    function _saveExclusion(exclusion) {
        console.log("Saving aircraft...");
        vm.savedExclusion = exclusion;
        vm.savedExclusion.TailNumberList = exclusion.TailNumberList || ['APPLY TO ALL'];
        vm.savingExclusion = true;
        vm.IsUpdating = true;
        if (vm.savedExclusion.Id > 0)
            vm.$aircraftExclusionsService.updateAircraftExclusion(vm.savedExclusion.Id, vm.savedExclusion, _onSaveAircraftExclusionSuccess, _onError);
        else
            vm.$aircraftExclusionsService.addAircraftExclusion(vm.savedExclusion, _onSaveAircraftExclusionSuccess, _onError);
    }

    function _editExclusion(exclusion) {
        exclusion.IsEditable = true;
        vm.IsDisabled = true;
    }

    function _deleteExclusion(exclusion) {
        console.log("Deleting aircraft exclusion...");
        vm.deletedExclusion = exclusion;
        if (exclusion.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = ("Are you sure you want to DELETE this exclusion?");
                    $scope.confirm = function () {
                        $uibModalInstance.close(vm.$aircraftExclusionsService.deleteAircraftExclusion(exclusion.Id, _onDeleteExclusionSuccess, _onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }
                }
            });
        } else {
            vm.aircrafts = null;
            var indexOfExclusion = vm.aircraftExclusions.indexOf(exclusion);
            vm.aircraftExclusions.splice(indexOfExclusion, 1);
        }
        vm.IsDisabled = false;
    }

    function _changeVendorExclusion(exclusion) {
        if (exclusion.IsVendorExcluded) {
            exclusion.FBOID = vm.fboDetails.Id;
            vm.savedExclusion = exclusion;
            vm.$vendorExclusionsService.addFBODetailExclusion(exclusion, _onSaveVendorExclusionSucess, _onError);
        }
        else vm.$vendorExclusionsService.deleteFBODetailExclusion(exclusion.Id, _onDeleteExclusionSuccess, _onError);
    }

    function _addCustomField(type) {
        vm.fboDetails.CustomFields.push({
            AdminClientID: $usersService.user.ClientID,
            FBO: $fboPriceMarginsService.icaoFBO.FBO,
            FieldType: type,
            ICAO: $fboPriceMarginsService.icaoFBO.ICAO,
            IsEditable: true
        });
    }

    //function _addCustomField() {
    //    var modalInstance = $uibModal.open({
    //        animation: true,
    //        ariaLabelledBy: 'modal-title',
    //        ariaDescribedBy: 'modal-body',
    //        templateUrl: '/Partials/Admin/ModalForms/AddCustomField.html',
    //        controller: 'addFBOCustomFieldModalController',
    //        controllerAs: 'modal',
    //        resolve: {
    //            items: function () {
    //                return {
    //                    fboData: $fboPriceMarginsService.icaoFBO,
    //                    customFieldsService: vm.$fboDetailCustomFieldsService,
    //                    userService: vm.$usersService,
    //                    currentCustomFields: vm.fboDetails.CustomFields
    //                }
    //            }
    //        }
    //    });
    //}

    function _editFields(field) {
        field.IsEditable = true;
    }

    function _saveFields(customField) {
        //customFields.AdminClientID = vm.items.userService.user.ClientID;
        //customFields.FBO = vm.items.fboData.FBO;
        //customFields.ICAO = vm.items.fboData.ICAO;
        //vm.customFields = angular.copy(customFields);
        vm.savedCustomField = customField;
        if (vm.savedCustomField.Id > 0)
            vm.$fboDetailCustomFieldsService.updateFBODetailCustomField(vm.savedCustomField.Id, vm.savedCustomField, _onSaveCustomFieldsSuccess, _onError);
        else vm.$fboDetailCustomFieldsService.addFBODetailCustomField(vm.savedCustomField, _onSaveCustomFieldsSuccess, _onError);
    }

    function _cancelFields(field) {
        field.IsEditable = false;
    }

    function _deleteFields(field) {
        if (field.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = ("Are you sure you want to DELETE this field?");
                    $scope.confirm = function () {
                        vm.deletedField = field;
                        $uibModalInstance.close(vm.$fboDetailCustomFieldsService.deleteFBODetailCustomField(field, _onDeleteCustomFieldSuccess, _onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }
                }
            });
        }
        else vm.fboDetails.CustomFields.pop();
    }

    //PRIVATE FILTERS//////////////////////////////////////////////
    function _vendorExclusionFilter(vendor) {
        if (vendor.Vendor.IsDeactivated) return false;
        if (!vm.IsExcludedOnly) return true;
        if (vendor.Vendor.IsExcluded || vendor.IsVendorExcluded) return true;
        return false;
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetCompaniesSuccess(data) {
        vm.notify(function () {
            vm.companies = JSON.parse(data.Item);
            vm.companies.unshift({ Name: 'APPLY TO ALL' });
            console.log(vm.companies);
        });
        //console.log("Getting Aircraft Exclusion List...");
        //vm.$aircraftExclusionsService.getAircraftExclusionsByICAOFBOAndAdminClientID($fboPriceMarginsService.icaoFBO, _onGetExclusionListSuccess, _onError);
    }

    function _onGetExclusionListSuccess(data) {
        vm.notify(function () {
            vm.aircraftExclusions = data.Items;
            angular.forEach(vm.aircraftExclusions, function (exclusion) {
                if (exclusion.CustClientID == 0) exclusion.Client.Name = 'APPLY TO ALL';
                exclusion.aircrafts.unshift({
                    AdminClientID: $usersService.user.ClientID,
                    CustClientID: vm.CustClientID,
                    AircraftID: 0,
                    ICAO: exclusion.ICAO,
                    FBO: exclusion.FBO,
                    TailNumber: 'APPLY TO ALL'
                });
            });
            console.log(vm.aircraftExclusions);
        });
        //console.log("Getting Custom Fields...");
        //vm.$fboDetailCustomFieldsService.getCustomFields($fboPriceMarginsService.icaoFBO, _onGetCustomFieldsSuccess, _onError);
        //console.log("Getting FBO Details...");
        //vm.$fboPriceMarginsService.getFBODetails($fboPriceMarginsService.icaoFBO, _onGetFBODetailsSuccess, _onError);
    }

    //function _onGetCustomFieldsSuccess(data) {
    //    vm.notify(function () {
    //        vm.customFields = data.Items;
    //        console.log(vm.customFields);
    //    });
    //    console.log("Getting FBO Details...");
    //    vm.$fboPriceMarginsService.getFBODetails($fboPriceMarginsService.icaoFBO, _onGetFBODetailsSuccess, $baseController.onError);
    //}

    function _onGetFBODetailsSuccess(data) {
        vm.notify(function () {
            vm.fboDetails = data.Item[0];
            vm.savedFBODetails = angular.copy(vm.fboDetails);
            vm.savedCustomFields = angular.copy(vm.fboDetails.CustomFields);
            console.log(vm.fboDetails);
            console.log("Getting FBO Price Margins...");
            vm.$fboPriceMarginsService.getFBOPriceMarginsByAdminClientAndICAO($usersService.user.ClientID, $fboPriceMarginsService.icaoFBO, _onGetFBOMarginsSuccess, _onError);
        });
    }

    function _onGetFBOMarginsSuccess(data) {
        vm.notify(function () {
            vm.activeFBOs = data.Items.filter(function (fbo) { return !fbo.IsOmitted; });
            vm.activeFBOs.sort(function (a, b) {
                var x = a.FBO;
                var y = b.FBO;
                if (x < y) { return -1; }
                if (x > y) { return 1; }
                return 0;
            });
            console.log("FBOs: ", vm.activeFBOs);
            vm.fboNames = vm.activeFBOs.map(function (fbo) { return fbo.FBO });
            var index = vm.fboNames.indexOf(vm.fboDetails.FBO);
            console.log(index);
            if (vm.fboNames.length > 1) {
                if (index == 0) {
                    vm.HideLeft = true;
                    vm.HideRight = false;
                } else if (index == (vm.fboNames.length - 1)) {
                    vm.HideLeft = false;
                    vm.HideRight = true;
                } else {
                    vm.HideLeft = false;
                    vm.HideRight = false;
                }
            } else {
                vm.HideLeft = true;
                vm.HideRight = true;
            }
            console.log("Getting Vendor Exclusions...");
            vm.$vendorExclusionsService.getFBODetailExclusions(vm.fboDetails.Id, vm.fboDetails.AdminClientID, _onGetVendorsSuccess, _onError);
            console.log("Getting Aircraft Exclusion List...");
            vm.$aircraftExclusionsService.getAircraftExclusionsByICAOFBOAndAdminClientID($fboPriceMarginsService.icaoFBO, _onGetExclusionListSuccess, _onError);
        });
    }

    function _onGetVendorsSuccess(data) {
        vm.notify(function () {
            vm.vendors = data.Items;
            console.log(vm.vendors);
        });
    }

    function _onSaveFBOMarginSuccess(data) {
        vm.notify(function () {
            if (data.Item) vm.savedFBOMargin.Id = data.Item;
            vm.IsNoteEditable = !vm.IsNoteEditable;
            vm.IsUpdating = false;
            console.log("FBO margin saved!");
        });
    }

    function _onGetAircraftsSuccess(data, exclusion) {
        vm.updatingExclusion = false;
        vm.notify(function () {
            exclusion.aircrafts = data.Items;
            exclusion.aircrafts.unshift({
                AdminClientID: $usersService.user.ClientID,
                CustClientID: vm.CustClientID,
                AircraftID: 0,
                ICAO: exclusion.ICAO,
                FBO: exclusion.FBO,
                TailNumber: 'APPLY TO ALL'
            });
        });
    }

    function _onSaveAircraftExclusionSuccess(data) {
        vm.notify(function () {
            $timeout(function () {
                if (data.Item) if (vm.savedExclusion) vm.savedExclusion.Id = data.Item;
                vm.savingExclusion = false;
                vm.IsUpdating = false;
                vm.IsDisabled = false;
                vm.savedExclusion.IsEditable = false;
            });
        });
    }

    function _onSaveVendorExclusionSucess(data) {
        vm.notify(function () {
            if (data.Item) {
                vm.savedExclusion.Id = data.Item;
                console.log("Vendor Exclusion saved!");
            }
        });
    }

    function _onDeleteExclusionSuccess() {
        vm.notify(function () {
            if (vm.deletedExclusion) {
                vm.aircrafts = null;
                var index = vm.aircraftExclusions.indexOf(vm.deletedExclusion);
                if (index > -1) vm.aircraftExclusions.splice(index, 1);
            }
            else console.log("Vendor Exclusion deleted!");
        });
    }

    function _onSaveCustomFieldsSuccess(data) {
        vm.notify(function () {
            console.log("Custom field saved!");
            if (data.Item) vm.savedCustomField.Id = data.Item;
            vm.savedCustomField.IsEditable = false;
        });
    }

    function _onDeleteCustomFieldSuccess() {
        vm.notify(function () {
            var index = vm.fboDetails.CustomFields.indexOf(vm.deletedField);
            if (index > -1) vm.fboDetails.CustomFields.splice(index, 1);
        });
    }

    //error handler//
    function _onError() {
        vm.notify(function () {
            vm.savingExclusion = false;
            vm.updatingExclusion = false;
        });
    }
}
degatech.ng.addController(degatech.ng.app.module,
    "fboDetailsController",
    [
        '$scope',
        '$rootScope',
        '$timeout',
        '$uibModal', '$log',
        '$baseController',
        "$usersService",
        "$fboPriceMarginsService",
        "$aircraftExclusionsService",
        "$customerDetailsService",
        "$aircraftsService",
        "$fboDetailCustomFieldsService",
        "$vendorsService",
        "$vendorExclusionsService"
    ],
    degatech.page.fboDetailsControllerFactory);

////////////////////////////MODAL - ADD CUSTOM FIELD///////////////////////////////////

degatech.services.addCustomFieldModalFactory = function ($baseService) {
    var aServiceObject = degatech.services.fboDetailCustomFields;
    var newService = $baseService.merge(true, {}, aServiceObject, $baseService);
    return newService;
}

degatech.ng.addService(degatech.ng.app.module, "$customFieldModalService", ["$baseService"], degatech.services.addCustomFieldModalFactory);

degatech.page.addFBOCustomFieldModalControllerFactory = function ($scope, $uibModalInstance, $customFieldModalService, items, $uibModal) {

    var vm = this;

    vm.$scope = $scope;
    vm.modalInstance = $uibModalInstance;
    vm.$customFieldModalService = $customFieldModalService;
    vm.items = items;

    vm.notify = vm.$customFieldModalService.getNotifier($scope);

    vm.cancel = _cancel;
    vm.saveFields = _saveFields;

    vm.items = items;

    _init();

    function _init() {
        console.log(vm.items);
    }

    function _cancel() {
        $uibModalInstance.close();
    }

    function _saveFields(customFields) {
        customFields.AdminClientID = vm.items.userService.user.ClientID;
        customFields.FBO = vm.items.fboData.FBO;
        customFields.ICAO = vm.items.fboData.ICAO;
        vm.customFields = customFields;
        vm.$customFieldModalService.addFBODetailCustomField(vm.customFields, _onSaveCustomFieldsSuccess, _onError);
    }

    function _onSaveCustomFieldsSuccess(data) {
        console.log("Custom field saved!");
        vm.notify(function () {
            vm.customFields.Id = data.Item;
            $uibModalInstance.close(vm.items.currentCustomFields.push(vm.customFields));
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
            , "addFBOCustomFieldModalController"
            , ['$scope',
                '$uibModalInstance',
                "$customFieldModalService",
                'items',
                '$uibModal'
            ]
            , degatech.page.addFBOCustomFieldModalControllerFactory);