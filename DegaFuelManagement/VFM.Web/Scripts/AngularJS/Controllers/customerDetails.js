degatech.page.customerDetailsControllerFactory = function ($scope,
            $rootScope,
            $baseController,
            $uibModal,
            $timeout,
            $usersService,
            $customerDetailsService,
            $contactsService,
            $customerNotesService,
            $registrationService,
            $aircraftsService,
            $aircraftDataService,
            $priceMarginsService,
            $customerPriceMarginsService,
            $customerAccountingInfoService,
            $airportsService,
            $fuelOrdersService,
            $fuelOrderPricingsService,
            $fuelOrderNotesService,
            $distributionService,
            $customerDetailsCustomFieldsService,
            $supplierFuelsPricesService,
            $vendorExclusionsService,
            $schedulingUploadsService,
            $integrationsService,
            Notification) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.$contactsService = $contactsService;
    vm.$customerNotesService = $customerNotesService;
    vm.$registrationService = $registrationService;
    vm.$aircraftsService = $aircraftsService;
    vm.$aircraftDataService = $aircraftDataService;
    vm.$priceMarginsService = $priceMarginsService;
    vm.$customerPriceMarginsService = $customerPriceMarginsService;
    vm.$customerAccountingInfoService = $customerAccountingInfoService;
    vm.$airportsService = $airportsService;
    vm.$fuelOrdersService = $fuelOrdersService;
    vm.$fuelOrderPricingsService = $fuelOrderPricingsService;
    vm.$fuelOrderNotesService = $fuelOrderNotesService;
    vm.$distributionService = $distributionService;
    vm.$customerDetailsCustomFieldsService = $customerDetailsCustomFieldsService;
    vm.$supplierFuelsPricesService = $supplierFuelsPricesService;
    vm.$vendorExclusionsService = $vendorExclusionsService;
    vm.$schedulingUploadsService = $schedulingUploadsService;
    vm.$integrationsService = $integrationsService;
    vm.$uibModal = $uibModal;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.notify = vm.$customerDetailsService.getNotifier($scope);
    vm.leftCompany = _leftCompany;
    vm.rightCompany = _rightCompany;
    vm.back = _back;
    vm.viewContact = _viewContact;
    vm.addContact = _addContact;
    vm.saveContact = _saveContact;
    //vm.deleteContact = _deleteContact;
    //vm.addUser = _addUser;
    //vm.saveUser = _saveUser;
    //vm.deleteUser = _deleteUser;
    vm.editName = _editName;
    vm.cancelName = _cancelName;
    vm.saveName = _saveName;
    vm.editAddress = _editAddress;
    vm.cancelAddress = _cancelAddress;
    vm.saveAddress = _saveAddress;
    vm.deleteCustomer = _deleteCustomer;
    //vm.addNote = _addNote;
    vm.editNote = _editNote;
    vm.cancelNote = _cancelNote;
    vm.saveNote = _saveNote;
    vm.editPhone = _editPhone;
    vm.cancelPhone = _cancelPhone;
    vm.savePhone = _savePhone;
    vm.editEmail = _editEmail;
    vm.cancelEmail = _cancelEmail;
    vm.saveEmail = _saveEmail;
    vm.updateCustomerDetails = _updateCustomerDetails;
    vm.onIsActiveChanged = _onIsActiveChanged;
    //vm.deleteNote = _deleteNote;
    vm.addAircraft = _addAircraft;
    vm.saveAircraft = _saveAircraft;
    vm.editAircraft = _editAircraft;
    vm.deleteThisAircraft = _deleteThisAircraft;
    vm.deleteAllAircrafts = _deleteAllAircrafts;
    vm.onChangeMargin = _onChangeMargin;
    vm.saveAccounting = _saveAccounting;
    vm.saveRegistration = _saveRegistration;
    vm.onToggleAcctNumbers = _onToggleAcctNumbers;
    vm.onMakeModelChange = _onMakeModelChange;
    vm.changeAircraft = _changeAircraft;
    vm.aircraftChanged = _aircraftChanged;
    vm.icaoChanged = _icaoChanged;
    vm.addLeg = _addLeg;
    vm.removeLeg = _removeLeg;
    vm.openPopup = _openPopup;
    vm.dispatchFuel = _dispatchFuel;
    vm.dispatchEmails = _dispatchEmails;
    vm.distribute = _distribute;
    vm.addCustomField = _addCustomField;
    vm.editFields = _editFields;
    vm.saveFields = _saveFields;
    vm.cancelFields = _cancelFields;
    vm.deleteFields = _deleteFields;
    vm.changeVendorExclusion = _changeVendorExclusion;
    vm.vendorEditable = _vendorEditable;
    vm.onDistributeChanged = _onDistributeChanged;
    vm.checkUsername = _checkUsername;
    vm.onFuelOrderQuoteChanged = _onFuelOrderQuoteChanged;
    //vm.onChangeDate = _onChangeDate;


    //PUBLIC FILTERS//////////////////////////////////////////////
    vm.aircraftFilter = _aircraftFilter;
    vm.fuelPriceFilter = _fuelPriceFilter;
    vm.fuelPriceForAllVendorsFilter = _fuelPriceForAllVendorsFilter;
    vm.vendorExclusionFilter = _vendorExclusionFilter;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.test = "This is a test";
    vm.showFormErrors = false;
    vm.customer = null;
    vm.savedContact = null;
    vm.deletedContact = null;
    vm.savedUser = null;
    vm.deletedUser = null;
    vm.savedNote = null;
    vm.deletedNote = null;
    vm.aircraftData = null;
    vm.aircrafts = null;
    vm.savedAircraft = null;
    vm.deletedAircraft = null;
    vm.CustPriceMargin = null;
    vm.fuelOrder = { FuelingDateTime: new Date().toDateString(), QuotedVolume: 1 };
    vm.dateStorage = { FuelOrderServiceDate: '' };
    vm.dateFormat = "MM/dd/yyyy";
    vm.contactTypes = ["Primary", "Secondary", "Billing"];
    vm.creditCardFee = ["No", "Margin", "Invoice"];
    vm.paymentTerms = ["Credit Card", "Net 10", "Net 14", "Net 15", "Net 20", "Net 25", "Net 30", "Net 45", "Net 60"];
    vm.billToSetup = ["No", "Partial", "Full"];
    vm.schedulingSystem = ['FOS', 'BART', 'FltPlan.com'];
    vm.certificateType = ["PART 91 Corporate", "PART 91 Personal/Non Business", "PART 135 Charter Carrier",
                            "PART 135 Management (Airline)", "PART 135 Management (Non-Airline)", "PART 121 Scheduled Carrier"];
    vm.flightSchedules = [{ 'Id': 1, 'Name': 'BART', 'name': 'BART' }, { 'Id': 2, 'Name': 'Flex Jet', 'name': 'FlexJet' },
                            { 'Id': 3, 'Name': 'Flight Options', 'name': 'FlightOptions' }, { 'Id': 4, 'Name': 'FOS', 'name': 'FOS' }];
    vm.selectedCompanyOptions = {
        selectedPriceMargin: { Name: '--Select One--', Id: 0 }
    };

    render();

    //PRIVATE METHODS//////////////////////////////////////////////
    function render() {
        if ($usersService.user.Client.ClientType == 1) {
            vm.$aircraftDataService.getListOfAircraftData(_onGetAircraftDataSuccess, _onError);
            //console.log("GETTING CUSTOMER...");
            //vm.$customerDetailsService.getCustomer($customerDetailsService.CustClientID, _onGetInfoSuccess, _onError);
            console.log("GETTING PRICE MARGINS...");
            vm.$priceMarginsService.getPriceMarginsByAdminClient($usersService.user.ClientID, _onGetPriceMarginsSuccess, _onError);
        } else {
            vm.$aircraftsService.getAircraftsByClientID($usersService.user.ClientID, _onGetAircraftsSuccess, _onError);
            vm.$customerDetailsService.getCustomer($usersService.user.ClientID, _onGetInfoSuccess, _onError);
        }
        vm.loadingAirports = true;
        vm.$airportsService.getListOfAirports(_onGetAirportSuccess, _onError);
        angular.forEach(vm.flightSchedules, function (schedule) {
            schedule.dropZoneConfig = _getDropzoneConfig(schedule);
            _getUploadStatus(schedule);
        });
    }

    function _getDropzoneConfig(schedule) {
        return {
            uploadMultiple: false,
            previewsContainer: "#Dropzone-Preview-Placeholder",
            maxFileSize: 4,
            url: "/api/files/scheduling/" + schedule.name + "/" + schedule.Id + "/" + $customerDetailsService.CustClientID,
            acceptedFiles: '.xlsx,.xls,.csv',
            addRemoveLinks: true,
            accept: function (file, done) {
                if (file.size > 4000000) {
                    Notification.warning({
                        model: this,
                        scope: $scope,
                        message: "File size exceeded.",
                        delay: 3000,
                        closeOnClick: true
                    });
                }
                else if (file.type == 'application/vnd.ms-excel' || file.type == 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' || file.type == '') {
                    $timeout(function () {
                        schedule.IsComplete = false;
                        schedule.IsUploading = true;
                        schedule.ValidPricingPercentage = 50;
                        schedule.UploadState = 'Processing';
                    });
                    var container = $(file.previewElement).parent();
                    $(file.previewElement).hide();

                    done();
                } else {
                    Notification.error({
                        model: this,
                        scope: $scope,
                        message: "That type of file is currently not supported.",
                        delay: 3000,
                        closeOnClick: true
                    });
                }
            },
            success: function (data) {
                var res = JSON.parse(data.xhr.responseText);
                console.log(res.Item);
                if (res.Item == "Upload failed.") {
                    $timeout(function () {
                        schedule.IsComplete = true;
                        schedule.IsUploading = false;
                        schedule.ValidPricingPercentage = 0;
                        schedule.UploadState = "Failed";
                    });
                } else {
                    $timeout(function () {
                        schedule.IsComplete = true;
                        schedule.IsUploading = false;
                        schedule.ValidPricingPercentage = 100;
                        schedule.UploadState = "Completed";
                        schedule.ImportDate = moment().format('MM/DD/YYYY');
                        $uibModal.open({
                            ariaLabelledBy: 'modal-title',
                            ariaDescribedBy: 'modal-body',
                            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                            controller: function ($scope, $uibModalInstance) {
                                $scope.message = "Are you sure you want to dispatch fuel for each location in this spreadsheet?";
                                $scope.confirm = function () {
                                    vm.$schedulingUploadsService.generateFuelOrders(schedule, $customerDetailsService.CustClientID, _onGetSuccess, _onError);
                                    $uibModalInstance.close();
                                }
                                $scope.cancel = function () {
                                    $uibModalInstance.dismiss();
                                }
                            }
                        });
                    });
                }
            },
            init: function () {

                this.on("processing", function (file) {
                    this.options.url = "/api/files/scheduling/" + schedule.name + "/" + schedule.Id + "/" + $customerDetailsService.CustClientID;
                });
            }
        };
    }

    function _getUploadStatus(schedule) {
        console.log('Getting upload status...');
        vm.$schedulingUploadsService.getUploadData(schedule, $customerDetailsService.CustClientID, _onGetSuccess, _onError);
    }

    function _leftCompany(clientId) {
        var index = $customerDetailsService.CustClientIDs.indexOf(clientId);
        var leftCustClientID = $customerDetailsService.CustClientIDs[index - 1];
        vm.$customerDetailsService.getCustomer(leftCustClientID, _onGetInfoSuccess, _onError);
    }

    function _rightCompany(clientId) {
        var index = $customerDetailsService.CustClientIDs.indexOf(clientId);
        var rightCustClientID = $customerDetailsService.CustClientIDs[index + 1];
        vm.$customerDetailsService.getCustomer(rightCustClientID, _onGetInfoSuccess, _onError);
    }

    function _back() {
        vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMERS';
        vm.$rootScope.ApplicationState.SubSection = "COMPANIES";
    }

    function _viewContact(contactId) {
        $contactsService.ContactID = contactId;
        vm.$rootScope.ApplicationState.ActiveSection = 'CONTACT INFO';
    }

    function _addContact() {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/AddNewContact.html',
            controller: 'addContactWizardModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        userService: vm.$usersService,
                        CustClientID: vm.customer.CustClientID,
                        Contacts: vm.customer.Contacts
                    }
                }
            }
        });
    }

    function _onFuelOrderQuoteChanged() {
        vm.fuelOrder.selectedVolumeDiscount = null;
        $(vm.fuelOrder.pricesForAllVendors).each(function () {
            if (this.Vendor == vm.fuelOrder.Quote.Vendor && this.FBOName == vm.fuelOrder.Quote.FBOName && ReplaceAll('Jet', '', this.Product) == ReplaceAll('Jet', '', vm.fuelOrder.Quote.Product) && this.PriceTierMin > vm.fuelOrder.Quote.PriceTierMin) {
                if (vm.fuelOrder.selectedVolumeDiscount == null || this.PriceTierMin < vm.fuelOrder.selectedVolumeDiscount.PriceTierMin) {
                    vm.fuelOrder.selectedVolumeDiscount = this;
                }
            }
        });
    }

    function _onDistributeChanged(contact) {
        console.log("Updating contact...");
        vm.savedContact = contact;
        if (contact.Id > 0) vm.$contactsService.updateContact(contact.Id, contact, _onSaveContactSuccess, _onError);
        else vm.$contactsService.addContact(contact, _onSaveContactSuccess, _onError);
    }

    function _saveContact(contact) {
        vm.savingContact = true;
        if (contact.ContactType == "Primary") contact.Distribute = true;
        else contact.Distribute = false;
        vm.savedContact = contact;
        if (contact.Id > 0) vm.$contactsService.updateContact(contact.Id, contact, _onSaveContactSuccess, _onError);
        else vm.$contactsService.addContact(contact, _onSaveContactSuccess, _onError);
    }

    function _editName() {
        vm.IsNameEditable = !vm.IsNameEditable;
    }

    function _cancelName(name) {
        vm.customer.Name = vm.updatedDetails.Name;
        vm.IsNameEditable = !vm.IsNameEditable;
    }

    function _saveName(name) {
        vm.customer.Name = name;
        _updateCustomerDetails(vm.customer);
        vm.IsNameEditable = !vm.IsNameEditable;
    }

    function _editAddress() {
        vm.IsAddressEditable = !vm.IsAddressEditable;
    }

    function _cancelAddress() {
        vm.customer.Address1 = vm.updatedDetails.Address1;
        vm.customer.Address2 = vm.updatedDetails.Address2;
        vm.customer.City = vm.updatedDetails.City;
        vm.customer.State = vm.updatedDetails.State;
        vm.customer.ZipCode = vm.updatedDetails.ZipCode;
        vm.customer.Country = vm.updatedDetails.Country;
        vm.IsAddressEditable = !vm.IsAddressEditable;
    }

    function _saveAddress(address) {
        vm.customer.Address1 = address.Address1;
        vm.customer.Address2 = address.Address2;
        vm.customer.City = address.City;
        vm.customer.State = address.State;
        vm.customer.ZipCode = address.ZipCode;
        vm.customer.Country = address.Country;
        _updateCustomerDetails(vm.customer);
    }

    function _deleteCustomer(customer) {
        if (customer.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = ("Are you sure you want to DELETE this company?");
                    $scope.confirm = function () {
                        $uibModalInstance.close(vm.$customerDetailsService.deleteCustomer(customer.CustClientID, _onDeleteCustomerSuccess, _onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }
                }
            });
        }
    }

    function _onIsActiveChanged() {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                var status = vm.customer.IsActive ? 'ACTIVATE' : 'DEACTIVATE'
                $scope.message = "Are you sure you want to " + status + " this company?";
                $scope.confirm = function () {
                    var updatedUser = {
                        Id: $usersService.user.Id,
                        RegistrationID: $usersService.user.RegistrationID,
                        ClientID: vm.customer.CustClientID,
                        IsActive: vm.customer.IsActive
                    };
                    $uibModalInstance.close(vm.$usersService.updateUser(updatedUser.Id, updatedUser, _onUpdateUserSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss(vm.customer.IsActive = !vm.customer.IsActive);
                }
            }
        });
    }

    function _editNote() {
        vm.IsNoteEditable = !vm.IsNoteEditable;
    }

    function _cancelNote(note) {
        vm.customer.Note = vm.updatedDetails.Note;
        vm.IsNoteEditable = !vm.IsNoteEditable;
    }

    function _saveNote(note) {
        vm.customer.Note = note;
        _updateCustomerDetails(vm.customer);
    }

    function _editPhone() {
        vm.IsPhoneEditable = !vm.IsPhoneEditable;
    }

    function _cancelPhone(phone) {
        vm.customer.Phone = vm.updatedDetails.Phone;
        vm.IsPhoneEditable = !vm.IsPhoneEditable;
    }

    function _savePhone(phone) {
        vm.customer.Phone = phone;
        _updateCustomerDetails(vm.customer);
    }

    function _editEmail() {
        vm.IsEmailEditable = !vm.IsEmailEditable;
    }

    function _cancelEmail(email) {
        vm.customer.Email = vm.updatedDetails.Email;
        vm.IsEmailEditable = !vm.IsEmailEditable;
    }

    function _saveEmail(email) {
        vm.customer.Email = email;
        _updateCustomerDetails(vm.customer);
    }

    function _updateCustomerDetails(details) {
        vm.IsUpdating = true;
        vm.updatedDetails = angular.copy(details);
        vm.$customerDetailsService.updateCustomer(vm.updatedDetails.Id, details, _onUpdateDetailsSuccess, _onError);
    }

    function _addAircraft() {
        if (_needsEmptyAircraft(vm.customer.Aircrafts)) {
            vm.customer.Aircrafts.unshift({
                AdminClientID: vm.customer.AdminClientID,
                CustClientID: vm.customer.CustClientID,
                TailNumber: '',
                IsMarginEnabled: true
            });
            vm.IsComplete = false;
        }
    }

    function _needsEmptyAircraft(aircrafts) {
        var filteredVendors = aircrafts.filter(function (a) { return a.Id });
        if (filteredVendors.length == aircrafts.length) return true;
        else return false;
    }

    function _onMakeModelChange(aircraft) {
        angular.forEach(vm.aircraftData, function (value) {
            if (value.AircraftID == aircraft.MakeModelID) aircraft.MakeAndModel = value;
        });
        _changeAircraft(aircraft);
    }

    function _changeAircraft(aircraft) {
        if (aircraft.TempTailNumber && aircraft.MakeModelID) vm.IsComplete = true;
    }

    function _editAircraft(aircraft) {
        aircraft.IsEditingAircraft = !aircraft.IsEditingAircraft;
    }

    function _saveAircraft(aircraft) {
        aircraft.IsEditingAircraft = false;
        vm.savedAircraft = aircraft;
        if (!aircraft.Id || aircraft.Id == 0)
            aircraft.TailNumber = aircraft.TempTailNumber;
        if (aircraft.Id > 0) vm.$aircraftsService.updateAircraft(aircraft.Id, aircraft, _onSaveAircraftSuccess, _onError);
        else {
            if (_hasUsedTailNumber(aircraft)) {
                Notification.warning({
                    model: this,
                    scope: $scope,
                    message: "Please choose a different Tail #",
                    delay: 3000,
                    closeOnClick: false
                });
            }
            else vm.$aircraftsService.addAircraft(aircraft, _onSaveAircraftSuccess, _onError);
        }
    }

    function _hasUsedTailNumber(aircraft) {
        var currentAircrafts = vm.customer.Aircrafts.filter(function (a) { return a.Id });
        var usedTailNumber = currentAircrafts.find(function (a) { return a.TailNumber == aircraft.TailNumber });
        if (usedTailNumber) return true;
        else return false
    }

    function _deleteThisAircraft(aircraft) {
        if (aircraft.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = ("Are you sure you want to DELETE this aircraft?");
                    $scope.confirm = function () {
                        vm.deletedAircraft = [];
                        vm.deletedAircraft.push(aircraft);
                        $uibModalInstance.close(vm.$aircraftsService.deleteAircraft(vm.deletedAircraft, _onDeleteAircraftSuccess, _onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }
                }
            });
        } else {
            vm.customer.Aircrafts.splice(vm.customer.Aircrafts.indexOf(aircraft), 1);
        }
    }

    function _deleteAllAircrafts(aircrafts) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = ("Are you sure you want to DELETE ALL the aircrafts?");
                $scope.confirm = function () {
                    vm.deletedAircrafts = [];
                    angular.forEach(aircrafts, function (value) {
                        if (value.Id > 0) vm.deletedAircrafts.push(value);
                        else {
                            var index = aircrafts.indexOf(value);
                            if (index > -1) vm.customer.Aircrafts.splice(index, 1);
                        }
                    });
                    $uibModalInstance.close(vm.$aircraftsService.deleteAircraft(vm.deletedAircrafts, _onDeleteAircraftSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _onToggleAcctNumbers(aircraft) {
        aircraft.showingAcctNumbers = !aircraft.showingAcctNumbers;
    }

    function _onChangeMargin(margin) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = ("Are you sure you want to UPDATE this companies margin setting?");
                $scope.confirm = function () {
                    vm.customer.CustomerPriceMargin.PriceMarginID = margin.Id;
                    vm.selectedCompanyOptions.selectedPriceMargin = margin;
                    var custPriceMargin = {
                        CustClientID: vm.customer.CustClientID,
                        PriceMarginID: margin.Id
                    }
                    $uibModalInstance.close(vm.$customerPriceMarginsService.addCustomerPriceMargin(custPriceMargin, _onChangeMarginSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _checkUsername() {
        vm.IsUpdating = true;
        vm.$registrationService.checkUsername(vm.customer.Registration, _onCheckUsernameSuccess, _onError);
    }

    function _onCheckUsernameSuccess(data) {
        vm.notify(function () {
            if (!data.IsDuplicateUsername) _saveRegistration();
            else {
                $uibModal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: '/Partials/Admin/ModalForms/Confirmations/Error.html',
                    controller: function ($scope, $uibModalInstance) {
                        $scope.title = "Username is already being used!";
                        $scope.message = "Please enter a new username.";
                        $scope.ok = function () {
                            $uibModalInstance.close();
                        }
                    }
                });
                vm.IsUpdating = false;
            }
        });
    }

    function _saveRegistration() {
        vm.IsUpdating = true;
        vm.$registrationService.updateRegistration(vm.customer.Registration.Id, vm.customer.Registration, _onUpdateRegistrationSuccess, _onError);
    }

    function _saveAccounting(accountingInfo) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = accountingInfo.IsFuelerLinxCustomer ? "Enabling FuelerLinx for this customer will ENABLE price distribution web services. \r Remember to DISABLE price distribution emails for applicable contacts." :
                    "Disabling FuelerLinx for this customer will DISABLE price distribution web services. Remember to ENABLE price distribution emails for applicable contacts.";
                $scope.confirm = function () {
                    $scope.isConfirming = true;
                    accountingInfo.AdminClientID = vm.customer.AdminClientID;
                    accountingInfo.CustClientID = vm.customer.CustClientID;
                    vm.accountingInfo = accountingInfo;
                    if (vm.accountingInfo.IsFuelerLinxCustomer) {
                        $(vm.customer.Contacts).each(function () {
                            if (this.Distribute) {
                                this.Distribute = false;
                                _onDistributeChanged(this);
                            }
                        });
                        if (vm.customer.Integration.AccountNumber == '00000000-0000-0000-0000-000000000000') {
                            vm.customer.Integration.ClientID = vm.accountingInfo.CustClientID;
                            vm.$integrationsService.addAccountNumber(vm.customer.Integration, _onAddAccountSuccess, _onError);
                        } else {
                            _close();
                        }
                    } else {
                        _close();
                        $(vm.customer.Contacts).each(function () {
                            if (!this.Distribute) {
                                this.Distribute = true;
                                _onDistributeChanged(this);
                            }
                        });
                    }
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss(vm.customer.CustomerAccountingInfo.IsFuelerLinxCustomer = !vm.customer.CustomerAccountingInfo.IsFuelerLinxCustomer);
                }

                function _onAddAccountSuccess(data) {
                    vm.notify(function () {
                        vm.customer.Integration = data.Item;
                        _close();
                    });
                }

                function _close() {
                    vm.isConfirming = $scope.isConfirming;
                    $uibModalInstance.close(vm.$customerAccountingInfoService.saveAccountingInfo(vm.accountingInfo, _onSaveAccountingSuccess, _onError));
                }
            }
        });

    }

    function _openPopup() {
        vm.popupOpened = true;
    }

    function _aircraftChanged() {
        vm.showOrderForm = true;
        vm.showButtons = true;
        if (!_isFuelOrderValidForQuote()) return;
        else _icaoChanged(vm.fuelOrder);
    }

    function _icaoChanged(fuelOrder) {
        console.log("Getting prices...");
        if (!_isFuelOrderValidForQuote())
            return;
        vm.gettingPrices = true;
        vm.gettingSupplierPrices = true;
        var clientIDs = {
            AdminClientID: ($usersService.user.Client.ClientType == 1) ? vm.customer.AdminClientID : $usersService.user.CustomerDetail.AdminClientID,
            CustClientID: ($usersService.user.Client.ClientType == 1) ? vm.customer.CustClientID : $usersService.user.ClientID
        };
        vm.$fuelOrderPricingsService.getQuoteForLocation(clientIDs.AdminClientID, clientIDs.CustClientID, fuelOrder.ICAO, fuelOrder.Aircraft.TailNumber, _onGetPricesSuccess, _onError);
        vm.$fuelOrderPricingsService.getQuoteForLocationForAllVendors(clientIDs.AdminClientID, clientIDs.CustClientID, fuelOrder.ICAO, fuelOrder.Aircraft.TailNumber, _onGetPricesForAllVendorsSuccess, _onError);
        //Supplier prices will be added via the fuel order save procedure in SQL
        //vm.$supplierFuelsPricesService.getSupplierFuelsPricesByICAO(vm.customer.AdminClientID, fuelOrder.ICAO, _onGetSupplierFuelsPricesByICAOSuccess, _onError);
    }

    function _isFuelOrderValidForQuote() {
        if (vm.fuelOrder.Aircraft == null || vm.fuelOrder.Aircraft.TailNumber == '' || vm.fuelOrder.ICAO == '' || vm.fuelOrder.ICAO == null)
            return false;
        return true;
    }

    function _addLeg() {
        vm.Legs.push({});
    }

    function _removeLeg() {
        vm.Legs.pop({});
    }

    function _saveFuelOrder(fuelOrder) {
        console.log("Saving Fuel Order...", fuelOrder);
        vm.savingFuelOrder = true;

        fuelOrder.AdminClientID = ($usersService.user.Client.ClientType == 1) ? vm.customer.AdminClientID : $usersService.user.CustomerDetail.AdminClientID;
        fuelOrder.AircraftID = vm.fuelOrder.Aircraft.Id;
        fuelOrder.TailNumber = vm.fuelOrder.Aircraft.TailNumber;
        fuelOrder.CustClientID = ($usersService.user.Client.ClientType == 1) ? vm.customer.CustClientID : $usersService.user.ClientID;
        fuelOrder.OrderedByUserID = $usersService.user.Id;
        fuelOrder.FBO = fuelOrder.Quote.FBOName;
        if ($usersService.user.Client.ClientType == 1) fuelOrder.AdminNotes = fuelOrder.Quote.Notes;
        else fuelOrder.CustNotes = fuelOrder.Quote.Notes;
        fuelOrder.SupplierID = fuelOrder.Quote.SupplierID;
        fuelOrder.DateRequested = new Date().toDateString();
        fuelOrder.QuotedPPG = fuelOrder.Quote.Price;
        fuelOrder.WholesaleQuoted = fuelOrder.Quote.SupplierTotalWithTax;
        fuelOrder.FuelingDateTime = moment(fuelOrder.FuelingDateString).format("MM-DD-YYYY") + " " + fuelOrder.FuelingTimeString;
        fuelOrder.Vendor = fuelOrder.Quote.Vendor;
        fuelOrder.Product = fuelOrder.Quote.Product;
        fuelOrder.AdminStatus = 0;
        fuelOrder.CustStatus = 0;
        fuelOrder.InvoiceStatus = 7;
        fuelOrder.IsArchived = false;
        fuelOrder.IsDirectlyEntered = false;
        fuelOrder.InvoicedPPG = 0;
        fuelOrder.InvoicedVolume = 0;
        fuelOrder.InvoiceStatus = 7;
        fuelOrder.RequestedBy = $usersService.user.Registration.Username;
        vm.priceMarginID = fuelOrder.Quote.PriceMarginID;
        vm.$fuelOrdersService.addFuelOrder(fuelOrder, _onSaveFuelOrderSuccess, _onError);
    }

    function _dispatchFuel(fuelOrder) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = ("Are you sure you want to DISPATCH this fuel order?");
                $scope.confirm = function () {
                    $uibModalInstance.close(_saveFuelOrder(fuelOrder));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _distribute() {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = ("Are you sure you want to DISTRIBUTE pricing to this company?");
                $scope.confirm = function () {
                    $uibModalInstance.close(
                        vm.$distributionService.distributeCompany(vm.customer.AdminClientID, vm.customer.CustClientID, _onDistributeSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _addCustomField(type) {
        // test
        vm.IsEditingCustomField = true;
        if (_needsEmptyField(vm.customer.CustomFields)) {
            vm.customer.CustomFields.push({
                AdminClientID: vm.customer.AdminClientID,
                CustClientID: vm.customer.CustClientID,
                FieldType: type,
                IsEditable: true
            });
        }
    }

    function _needsEmptyField(fields) {
        var filteredFields = fields.filter(function (f) { return f.Id });
        if (filteredFields.length == fields.length) return true;
        else return false;
    }

    function _editFields(field) {
        field.IsEditable = true;
    }

    function _saveFields(customField) {
        vm.IsEditingCustomField = false;
        vm.savedCustomField = customField;
        if (vm.savedCustomField.Id > 0)
            vm.$customerDetailsCustomFieldsService.updateCustomField(vm.savedCustomField.Id, vm.savedCustomField, _onSaveCustomFieldsSuccess, _onError);
        else vm.$customerDetailsCustomFieldsService.addCustomField(vm.savedCustomField, _onSaveCustomFieldsSuccess, _onError);
    }

    function _cancelFields(field) {
        field.IsEditable = false;
    }

    function _deleteFields(field) {
        vm.IsEditingCustomField = false;
        if (field.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = "Are you sure you want to DELETE this field?";
                    $scope.confirm = function () {
                        vm.deletedField = field
                        $uibModalInstance.close(vm.$customerDetailsCustomFieldsService.deleteCustomField(field, _onDeleteCustomFieldSuccess, _onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }
                }
            });
        }
        else vm.customer.CustomFields.pop();
    }

    function _saveFuelOrderCustomerPricings(fuelOrderId) {

        //Save all customer pricings associated with the fuel order
        var pricings = [];
        angular.forEach(vm.fuelOrder.pricesForAllVendors, function (price) {
            var pricing = angular.copy(price);
            pricing.FuelOrderID = fuelOrderId;
            pricings.push(pricing);
        });
        vm.$fuelOrderPricingsService.addCustomerPricings(pricings, _onSaveCustomerPricingsSuccess, _onError);
    }

    function _saveFuelOrderSupplierPricings(fuelOrderId) {
        //Supplier fuel pricing is now saved within the insert procedure of a new fuel order
    }

    function _dispatchEmails(fuelOrderId) {
        vm.$fuelOrdersService.dispatchEmails(fuelOrderId, onDispatchEmailsSuccess, _onError);
    }

    function _resetFuelOrder() {
        vm.fuelOrder = { FuelingDateTime: new Date().toDateString(), QuotedVolume: 1 };
        vm.Legs = [];
    }



    function _vendorEditable(vendor) {
        console.log('test');
        vendor.isVendorExclusionEditable = !vendor.isVendorExclusionEditable;
    }



    function _changeVendorExclusion(vendor) {
        console.log("Changing exclusion...");
        if (vendor.IsVendorExcluded) {
            if (vendor.Id > 0) {
                console.log("Updating exclusion...");
                if (vendor.TailNumberList[0] == '' || vendor.TailNumberList.length == 0) return;
                vm.$vendorExclusionsService.updateCustomerDetailExclusion(vendor.Id, vendor, _onSaveVendorExclusionSuccess, _onError);
            } else {
                console.log("Adding exclusion...");
                vendor.CustClientID = vm.customer.CustClientID;
                vm.savedExclusion = vendor;
                //if (vm.customer.Aircrafts.length > 0) {
                //    vm.savedExclusion.TailNumberList.pop();
                //    vm.savedExclusion.TailNumberList = vm.customer.Aircrafts.map(function (a) { return a.TailNumber });
                //}
                vm.$vendorExclusionsService.addCustomerDetailExclusion(vendor, _onSaveVendorExclusionSuccess, _onError);
            }
        }
        else {
            console.log("Deleting exclusion...");
            vm.deletedExclusion = vendor;
            vm.$vendorExclusionsService.deleteCustomerDetailExclusion(vendor.Id, _onDeleteExclusionSuccess, _onError);
        }
    }

    //PRIVATE FILTERS///////////////////////////////////////////////
    function _aircraftFilter(aircraft) {
        if (!aircraft.IsMarginEnabled) return false;
        return true;
    }

    function _fuelPriceFilter(priceOption) {
        if (!_isFuelOrderValidForQuote())
            return false;
        if (vm.fuelOrder.Aircraft.TailNumber != priceOption.TailNumber)
            return false;
        if ((($usersService.user.Client.ClientType == 1) ? vm.customer.AdminClientID : $usersService.user.CustomerDetail.AdminClientID) != priceOption.AdminClientID)
            return false;
        //Verify the current quoted volume falls within the price tier's gallon range
        if ((vm.fuelOrder.QuotedVolume < priceOption.PriceTierMin)
            || ((vm.fuelOrder.QuotedVolume > priceOption.PriceTierMax)
                && priceOption.PriceTierMax > 0))
            return false;
        //Set a default fuel option if one isn't selected
        //if (!vm.fuelOrder.Quote)
        //    vm.fuelOrder.Quote = priceOption;
        return true;
    }

    function _fuelPriceForAllVendorsFilter(priceOption) {
        if (!_isFuelOrderValidForQuote())
            return false;
        if (vm.fuelOrder.Aircraft.TailNumber != priceOption.TailNumber)
            return false;
        if (vm.customer.AdminClientID != priceOption.AdminClientID)
            return false;
        return true;
    }

    function _vendorExclusionFilter(vendor) {
        if (vendor.Vendor.IsDeactivated) return false;
        if (!vm.IsExcludedOnly) return true;
        if (vendor.Vendor.IsExcluded || vendor.IsVendorExcluded) return true;
        return false;
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetInfoSuccess(data) {
        vm.notify(function () {
            vm.customer = data.Item;
            vm.updatedDetails = angular.copy(data.Item);
            console.log("CUSTOMER: ", vm.customer);
            vm.customer.Contacts.sort(function (a, b) {
                var x = a.LastName;
                var y = b.LastName;
                if (x < y) { return -1; }
                if (x > y) { return 1; }
                return 0;
            });
            if (!$customerDetailsService.CustClientIDs) {
                console.log("Getting Customers...");
                vm.$customerDetailsService.getCustomersByAdminClient($usersService.user.ClientID, _onGetCustomersSuccess, _onError);
            }
            else {
                console.log("ORDERED CUSTOMER IDs: ", $customerDetailsService.CustClientIDs);
                _renderButtons();
            }
            //Pick the default margin to display
            $(vm.priceMargins).each(function () {
                if (this.Id == vm.customer.CustomerPriceMargin.PriceMarginID)
                    vm.selectedCompanyOptions.selectedPriceMargin = this;
            });
            vm.$vendorExclusionsService.getCustomerDetailExclusions(vm.customer.CustClientID, vm.customer.AdminClientID, _onGetVendorsSuccess, _onError);
        });
    }

    function _renderButtons() {
        var index = $customerDetailsService.CustClientIDs.indexOf(vm.customer.CustClientID);
        console.log(index);
        if (index == 0) vm.HideLeft = true;
        else if ($customerDetailsService.CustClientIDs.length > 0 && index == ($customerDetailsService.CustClientIDs.length - 1)) vm.HideRight = true;
        else {
            vm.HideLeft = false;
            vm.HideRight = false;
        }
    }

    function _onGetCustomersSuccess(data) {
        vm.notify(function () {
            vm.customers = $customerDetailsService.companies = JSON.parse(data.Item);
            vm.orderedCustomers = vm.customers.sort(function (a, b) {
                var x = a.Name;
                var y = b.Name;
                if (x < y) { return -1; }
                if (x > y) { return 1; }
                return 0;
            });
            console.log("ORDERED CUSTOMERS: ", vm.orderedCustomers);
            $customerDetailsService.CustClientIDs = vm.orderedCustomers.map(function (customer) { return customer.CustClientID });
            console.log("ORDERED CUSTOMER IDs: ", $customerDetailsService.CustClientIDs);
            _renderButtons();
        });
    }

    function _onDistributeSuccess(data) {
        vm.notify(function () {
            Notification.success({
                model: this,
                scope: $scope,
                message: "Company Distributed!<br /> <br />",
                delay: 3000,
                closeOnClick: false
            });
        });
    }

    function _onSaveContactSuccess(data) {
        vm.notify(function () {
            console.log("Contact updated!");
            vm.savingContact = false;
            if (data.Item) {
                vm.savedContact.Id = data.Item;
                vm.savedContact.DateAdded = new Date();
            }
            $contactsService.contacts = null;
        });
    }

    function _onDeleteContactSuccess() {
        vm.notify(function () {
            var index = vm.customer.Contacts.indexOf(vm.deletedContact);
            if (index > -1) vm.customer.Contacts.splice(index, 1);
            $contactsService.contacts = null;
        });
    }

    function _onDeleteCustomerSuccess() {
        vm.notify(function () {
            $customerDetailsService.companies = null;
            vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMERS';
            vm.$rootScope.ApplicationState.SubSection = "COMPANIES";
        });
    }

    function _onSaveRegSuccess(result) {
        console.log("Registration Saved!");
        vm.notify(function () {
            vm.savedUser.Registration.Id = result.Item;
            vm.savedUser.Registration.DateAdded = new Date();
        });
        var user = {
            RegistrationID: result.Item,
            Registration: {
                Id: result.Item
            },
            ClientID: vm.customer.CustClientID,
            IsActive: false
        };
        vm.$usersService.addUser(user, _onSaveUserSuccess, vm.Error);
    }

    function _onSaveUserSuccess(data) {
        vm.notify(function () {
            vm.savingUser = false;
            if (data.Item) vm.savedUser.Id = data.Item;
        });
    }

    function _onUpdateUserSuccess(data) {
        console.log("User Updated!");
        if (vm.customer.IsActive) {
            Notification.success({
                model: this,
                scope: $scope,
                message: 'You have ENABLED price distribution for this company',
                delay: 3000,
                closeOnClick: false
            });
        }
        else {
            Notification.warning({
                model: this,
                scope: $scope,
                message: 'You have DISABLED price distribution for this company',
                delay: 3000,
                closeOnClick: false
            });
        }
        _updateCustomerDetails(vm.customer);
    }

    function _onDeleteUserSuccess() {
        vm.notify(function () {
            var index = vm.customer.Users.indexOf(vm.deletedUser);
            if (index > -1) vm.customer.Users.splice(index, 1);
        });
    }

    function _onSaveNoteSuccess(data) {
        vm.notify(function () {
            vm.savingNote = false;
            if (data.Item) vm.savedNote.Id = data.Item;
        });
    }

    function _onDeleteNoteSuccess() {
        vm.notify(function () {
            var index = vm.customer.CustomerNotes.indexOf(vm.deletedNote);
            if (index > -1) vm.customer.CustomerNotes.splice(index, 1);
        });
    }

    function _onGetAircraftsSuccess(data) {
        vm.notify(function () {
            vm.aircrafts = data.Items;
            console.log("AIRCRAFTS: ", vm.aircrafts);
        });
    }

    function _onGetAircraftDataSuccess(data) {
        vm.notify(function () {
            vm.aircraftData = JSON.parse(data.Item);
            console.log("AIRCRAFT DATA: ", vm.aircraftData);
        });
    }

    function _onSaveAircraftSuccess(data) {
        vm.notify(function () {
            if (data.Item) vm.savedAircraft.Id = data.Item;
            $customerDetailsService.companies = null;
        });
    }

    function _onDeleteAircraftSuccess() {
        vm.notify(function () {
            if (vm.deletedAircraft) {
                angular.forEach(vm.deletedAircraft, function (value) {
                    var index = vm.customer.Aircrafts.indexOf(value);
                    if (index > -1) vm.customer.Aircrafts.splice(index, 1);
                });
            }
            if (vm.deletedAircrafts) {
                angular.forEach(vm.deletedAircrafts, function (value) {
                    var index = vm.customer.Aircrafts.indexOf(value);
                    if (index > -1) vm.customer.Aircrafts.splice(index, 1);
                });
            }
            $customerDetailsService.companies = null;
        });
    }

    function _onGetPriceMarginsSuccess(data) {
        vm.notify(function () {
            vm.priceMargins = data.Items;
            console.log("PRICE MARGINS: ", vm.priceMargins);
            console.log("GETTING CUSTOMER...");
            vm.$customerDetailsService.getCustomer($customerDetailsService.CustClientID, _onGetInfoSuccess, _onError);
        });
    }

    function _onChangeMarginSuccess(data) {
        vm.notify(function () {
            console.log("MARGIN CHANGED!");
            _aircraftChanged();
            $customerDetailsService.companies = null;
            $scope.$emit('updateCompanies', $usersService);
        });
    }

    function _onGetAccountingSuccess(data) {
        vm.notify(function () {
            if (data.Item) {
                vm.accounting = data.Item;
                console.log("ACCOUNTING INFO: ", vm.accounting);
            }
            else _onError();
        });
    }

    function _onSaveAccountingSuccess(data) {
        vm.notify(function () {
            vm.isConfirming = !vm.isConfirming;
            console.log("Accounting Info Updated!");
        });
    }

    function _onUpdateRegistrationSuccess(data) {
        vm.notify(function () {
            if (!data.IsDuplicateUsername) {
                vm.customer.User.RegistrationID = data.Item;
                vm.customer.Registration.Id = data.Item;
                vm.$usersService.updateUser(vm.customer.User.Id, vm.customer.User, _onUpdateUserRegistrationSuccess, _onError);
            }
            vm.IsUpdating = false;
        });
    }

    function _onUpdateUserRegistrationSuccess(data) {
        vm.notify(function () {
            vm.IsUpdating = false;
            console.log("Registration Updated!");
        });
    }

    function _onUpdateDetailsSuccess(data) {
        vm.notify(function () {
            vm.IsPhoneEditable = false;
            vm.IsEmailEditable = false;
            vm.IsNoteEditable = false;
            vm.IsAddressEditable = false;
            vm.IsUpdating = false;
            $customerDetailsService.companies = null;
        });
        console.log("Company Details Updated!");
    }

    function _onGetAirportSuccess(data) {
        vm.notify(function () {
            vm.airports = JSON.parse(data.Item);
            vm.loadingAirports = false;
            console.log("AIRPORTS: ", vm.airports);
        });
    }

    function _onGetPricesSuccess(data, leg) {
        vm.notify(function () {
            vm.gettingPrices = false;
            vm.showOrderForm = true;
            vm.fuelOrder.prices = data.Items;
            angular.forEach(vm.fuelOrder.prices, function (price) {
                if (price.Product == '') price.Product = 'Jet';
            });
            //leg.prices = data.Item;
            console.log("PRICES: ", vm.fuelOrder.prices);
            //vm.fuelOrder.Quote = vm.fuelOrder.prices[0];
        });
    }

    function _onGetPricesForAllVendorsSuccess(data, leg) {
        vm.notify(function () {
            vm.gettingPricesForAllVendors = false;
            vm.fuelOrder.pricesForAllVendors = data.Items;
            //leg.prices = data.Item;
            console.log("VOLUME DISCOUNTS: ", vm.fuelOrder.pricesForAllVendors);
            //vm.fuelOrder.Quote = vm.fuelOrder.prices[0];
        });
    }

    function _onGetSupplierFuelsPricesByICAOSuccess(data) {
        vm.notify(function () {
            vm.gettingSupplierPrices = false;
            vm.fuelOrder.supplierPrices = data.Items;
            console.log("SUPPLIER PRICES", vm.fuelOrder.supplierPrices);
        });
    }

    function _onGetVendorsSuccess(data) {
        vm.notify(function () {
            vm.vendors = data.Items;
            //angular.forEach(vm.vendors, function (vendor) {
            //    if (vendor.TailNumbers) vendor.TailNumbers = JSON.parse(vendor.TailNumbers);
            //});
            console.log("VENDORS", vm.vendors);
        });
    }

    function _onSaveVendorExclusionSuccess(data) {
        vm.notify(function () {
            if (data.Item) {
                vm.savedExclusion.Id = data.Item;
                if (vm.customer.Aircrafts.length > 0) {
                    if (vm.savedExclusion.TailNumberList.length == 1 && vm.savedExclusion.TailNumberList[0] == '') {
                        vm.savedExclusion.TailNumberList.pop();
                        vm.savedExclusion.TailNumberList = vm.customer.Aircrafts.map(function (a) { return a.TailNumber });
                        _changeVendorExclusion(vm.savedExclusion);
                    }
                }
            }
            console.log("Vendor Exclusion saved!");
        });
    }

    function _onDeleteExclusionSuccess() {
        vm.notify(function () {
            vm.deletedExclusion.Id = 0;
            console.log("Vendor Exclusion deleted!");
        });
    }

    function _onSaveSupplierPricingsSuccess(data) {
        console.log("Fuel Order Supplier Pricings saved!");
    }

    function _onSaveCustomerPricingsSuccess(data) {
        console.log("Fuel Order Customer Pricings saved!");
    }

    function onDispatchEmailsSuccess(data) {
        console.log("Email dispatched!");
    }

    function _onSaveFuelOrderSuccess(data) {
        vm.notify(function () {
            _saveFuelOrderCustomerPricings(data.Item);
            _saveFuelOrderSupplierPricings(data.Item);
            _dispatchEmails(data.Item);
            if (vm.fuelOrder.Note) {
                var note = {
                    ClientID: $usersService.user.ClientID,
                    FuelOrderID: data.Item,
                    Note: vm.fuelOrder.Note,
                    AddedByUserID: $usersService.user.Id
                };
                vm.$fuelOrderNotesService.addFuelOrderNote(note, _onSaveFuelOrderNoteSuccess, _onError);
            }
            else _displaySuccessMessage();
        });
    }

    function _onSaveFuelOrderNoteSuccess(data) {
        vm.notify(function () {
            _displaySuccessMessage();
        });
    }

    function _displaySuccessMessage() {
        var msg = "Fuel order saved! <br /> <br />";
        console.log(msg);
        Notification.success({
            model: this,
            scope: $scope,
            message: msg,
            delay: 3000,
            closeOnClick: false
        });
        vm.savingFuelOrder = false;
        vm.showOrderForm = true;
        _resetFuelOrder();
        console.log("Updating total...");
        $scope.$emit('updateTotal', $usersService.dashboard);
        $fuelOrdersService.fuelOrders = null;
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
            var index = vm.customer.CustomFields.indexOf(vm.deletedField);
            if (index > -1) vm.customer.CustomFields.splice(index, 1);
        });
    }

    function _onGetSuccess(data, scheduling) {
        vm.notify(function () {
            if (data.Item.DateUploaded !== "0001-01-01T00:00:00") {
                scheduling.UploadState = 'Completed';
                scheduling.IsComplete = true;
                scheduling.ImportDate = moment(data.Item.DateUploaded).format('MM/DD/YYYY');
            }
        });
    }

    function _onError(error) {
        vm.notify(function () {
            vm.savingContact = false;
            vm.savingUser = false;
            vm.savingFuelOrder = false;
            vm.savingPricing = false;
            vm.gettingPrices = false;
            vm.IsUpdating = false;
            //vm.updatingAircraft = false;
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }
}

degatech.ng.addController(degatech.ng.app.module,
    "customerDetailsController",
    ['$scope',
    "$rootScope",
    '$baseController',
    '$uibModal',
    '$timeout',
    "$usersService",
    "$customerDetailsService",
    "$contactsService",
    "$customerNotesService",
    "$registrationService",
    "$aircraftsService",
    "$aircraftDataService",
    "$priceMarginsService",
    "$customerPriceMarginsService",
    "$customerAccountingInfoService",
    "$airportsService",
    "$fuelOrdersService",
    "$fuelOrderPricingsService",
    '$fuelOrderNotesService',
    "$distributionService",
    "$customerDetailsCustomFieldsService",
    "$supplierFuelsPricesService",
    "$vendorExclusionsService",
    "$schedulingUploadsService",
    "$integrationsService",
    "Notification"],
    degatech.page.customerDetailsControllerFactory);

////////////////////////////MODAL - ADD CONTACT///////////////////////////////////

degatech.services.addContactModalFactory = function ($baseService) {
    var aServiceObject = degatech.services.contacts;
    var newService = $baseService.merge(true, {}, aServiceObject, $baseService);
    return newService;
}

degatech.services.customerDetailsFactory = function ($baseService) {
    var serviceObject = degatech.services.customerDetails;
    var newService = $baseService.merge(true, {}, serviceObject, $baseService);
    return newService;
}

degatech.services.contactNotesFactory = function ($baseService) {
    var serviceObject = degatech.services.contactNotes;
    var newService = $baseService.merge(true, {}, serviceObject, $baseService);
    return newService;
}

degatech.ng.addService(degatech.ng.app.module, "$contactModalService", ["$baseService"], degatech.services.addContactModalFactory);

degatech.ng.addService(degatech.ng.app.module, "$customerDetailsService", ["$baseService"], degatech.services.customerDetailsFactory);

degatech.ng.addService(degatech.ng.app.module, "$contactNotesService", ["$baseService"], degatech.services.contactNotesFactory);

degatech.page.addContactWizardModalControllerFactory = function ($scope, $rootScope, $uibModalInstance, $contactModalService, items, $uibModal,
    $customerDetailsService,
    $contactNotesService,
    Notification) {

    var vm = this;

    vm.$scope = $scope;
    vm.$rootScope = $rootScope;
    vm.modalInstance = $uibModalInstance;
    vm.$contactModalService = $contactModalService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.$contactNotesService = $contactNotesService;
    vm.items = items;

    vm.notify = vm.$contactModalService.getNotifier($scope);

    vm.cancel = _cancel;
    vm.save = _save;
    vm.changeType = _changeType;
    vm.saveAndNew = _saveAndNew;
    vm.saveContact = _saveContact;
    vm.NewContact = {
        Contact: {
            Distribute: (vm.items.Contacts.length == 0) ? true : false,
            ContactType: (vm.items.Contacts.length == 0) ? 'Primary' : ''
        }
    };

    vm.items = items;

    _init();

    function _init() {
        console.log(vm.items);
    }

    function _cancel() {
        $uibModalInstance.close();
    }

    function _changeType() {
        if (vm.IsPrimary) {
            vm.NewContact.Contact.Distribute = true;
            vm.NewContact.Contact.ContactType = 'Primary';
        }
        else vm.NewContact.Contact.ContactType = '';
    }

    function _save(newContact) {
        if (vm.contactForm.$valid) {
            vm.showFormErrors = true;
            newContact.IsRenewable = false;
            _saveContact(newContact);
        }
    }

    function _saveAndNew(newContact) {
        if (vm.contactForm.$valid) {
            vm.showFormErrors = true;
            newContact.IsRenewable = true;
            _saveContact(newContact);
        }
    }

    function _saveContact(newContact) {
        console.log("Saving contact...");
        vm.savedContact = newContact;
        vm.savedContact.Contact.AdminClientID = vm.items.userService.user.ClientID;
        vm.savedContact.Contact.CustClientID = vm.items.CustClientID;
        vm.$contactModalService.addContact(newContact.Contact, _onSaveContactSuccess, _onError);
    }

    function _onSaveContactSuccess(data) {
        if (vm.savedContact.ContactNote && vm.savedContact.ContactNote.Note !== '') {
            console.log("Saving contact note...");
            vm.savedContact.Contact.Id = data.Item;
            vm.savedContact.ContactNote.ContactID = vm.savedContact.Contact.Id;
            vm.savedContact.ContactNote.AddedByUserID = vm.items.userService.user.Id;
            vm.savedContact.ContactNote.CustClientID = vm.savedContact.Contact.CustClientID;
            vm.$contactNotesService.addContactNote(vm.savedContact.ContactNote, _onSaveContactNoteSuccess, _onError);
        } else {
            Notification({
                model: this,
                scope: $scope,
                message: "Contact Saved! <br> </br />",
                delay: 3000,
                closeOnClick: false
            });
            vm.notify(function () {
                if (vm.savedContact.IsRenewable) {
                    vm.savedContact.Contact.Id = data.Item;
                    vm.items.Contacts.push(vm.savedContact.Contact);
                    vm.NewContact.Contact = null;
                    vm.showFormErrors = false;
                }
                else {
                    //$customerDetailsService.CustClientID = vm.savedContact.Contact.CustClientID;
                    //vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
                    vm.savedContact.Contact.Id = data.Item;
                    $uibModalInstance.close(vm.items.Contacts.push(vm.savedContact.Contact));
                }
            });
        }
    }

    function _onSaveContactNoteSuccess(data) {
        Notification({
            model: this,
            scope: $scope,
            message: "Contact Note Saved! <br /><br />",
            delay: 3000,
            closeOnClick: false
        });
        vm.notify(function () {
            if (vm.savedContact.IsRenewable) {
                vm.items.Contacts.push(vm.savedContact.Contact);
                vm.NewContact.Contact = null;
                vm.NewContact.ContactNote = null;
                vm.showFormErrors = false;
            }
            else {
                //$customerDetailsService.CustClientID = vm.savedContact.Contact.CustClientID;
                //vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
                $uibModalInstance.close(vm.items.Contacts.push(vm.savedContact.Contact));
            }
        });
    }

    function _onError() {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }
}

degatech.ng.addController(degatech.ng.app.module
            , "addContactWizardModalController"
            , ['$scope',
                '$rootScope',
                '$uibModalInstance',
                '$contactModalService',
                'items',
                '$uibModal',
                '$customerDetailsService',
                '$contactNotesService',
                'Notification'
            ]
            , degatech.page.addContactWizardModalControllerFactory);