degatech.page.customerManagerControllerFactory = function ($scope, $rootScope, $uibModal, $log,
    $baseController,
    $usersService,
    $clientsService,
    $customerDetailsService,
    $contactsService,
    $priceMarginsService,
    $aircraftDataService,
    $customerPriceMarginsService,
    Notification,
    $timeout
    ) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$clientsService = $clientsService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.$contactsService = $contactsService;
    vm.$priceMarginsService = $priceMarginsService;
    vm.$aircraftDataService = $aircraftDataService;
    vm.$customerPriceMarginsService = $customerPriceMarginsService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;
    vm.notify = vm.$usersService.getNotifier($scope);

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.addCompany = _addCompany;
    vm.addContact = _addContact;
    vm.cancel = _cancel;
    vm.onMarginChange = _onMarginChange;
    vm.changeDistribution = _changeDistribution;
    vm.viewCustomer = _viewCustomer;
    vm.editContact = _editContact;
    vm.exportContact = _exportContact;
    vm.exportCompany = _exportCompany;
    vm.clearFilters = _clearFilters;
    vm.viewCompanies = _viewCompanies;
    vm.viewContacts = _viewContacts;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.customers = null;
    vm.contacts = null;
    vm.savedCustomer = null;
    vm.deletedCustomer = null;
    vm.isCompaniesReady = false;
    vm.isContactsReady = false;
    vm.customerFilter = {
        MarginName: '',
        IsActive: ''
    }
    vm.contactFilter = {
        Distribute: ''
    };
    vm.RecordsPerPage = [10, 20, 50, 100, 200];
    vm.CustomerRecordsPerPage = 20;
    vm.ContactRecordsPerPage = 20;

    _init();

    $scope.$on('filteredRows', function (event, data) {
        vm.filteredRows = data;
    });

    //PRIVATE METHODS//////////////////////////////////////////////
    function _init() {
        storage.SetSessionItem("LastActiveSection", "CUSTOMERS");
        console.log("Getting Aircraft Data...");
        vm.$aircraftDataService.getListOfAircraftData(_onGetAircraftDataSuccess, _onError);
        console.log("Getting price margins...");
        vm.$priceMarginsService.getPriceMarginsByAdminClient($usersService.user.ClientID, _onGetPriceMarginsSuccess, _onError);

        if ($customerDetailsService.companies) {
            vm.customers = $customerDetailsService.companies;
            vm.customerCount = $customerDetailsService.customerCount;
            vm.savedCustomers = $customerDetailsService.savedCustomers;
            _orderCustomers();
            if (vm.$rootScope.ApplicationState.SubSection === 'COMPANIES') _viewCompanies();
        } else {
            console.log("Getting Customers...");
            vm.$customerDetailsService.getCustomersByAdminClient($usersService.user.ClientID, _onGetCustomersSuccess, _onError);
        }
        if ($contactsService.contacts) {
            vm.contacts = $contactsService.contacts;
            vm.contactsCount = $contactsService.contactsCount;
            vm.savedContacts = $contactsService.savedContacts;
            if (vm.$rootScope.ApplicationState.SubSection === 'CONTACTS') _viewContacts();
        } else {
            console.log("Getting Contacts...");
            vm.$contactsService.getContactsByAdminClient($usersService.user.ClientID, _onGetContactsSuccess, _onError);
        }
    }

    function _clearFilters() {
        for (var property in vm.customerFilter) {
            vm.customerFilter[property] = '';
        }
        for (var property in vm.contactFilter) {
            vm.contactFilter[property] = '';
        }
    }

    function _addCompany() {
        $timeout(function () {
            if (vm.customerFilter) $customerDetailsService.filter = vm.customerFilter;
            if (vm.contactFilter) $contactsService.filter = vm.contactFilter;
        });
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/AddNewCompany.html',
            controller: 'addCompanyModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        aircraftData: vm.aircraftData,
                        priceMargins: vm.priceMargins,
                        orderedCustomers: vm.orderedCustomers
                    }
                }
            }
        });
    }

    function _exportCompany() {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/ExportCSV.html',
            controller: 'exportCSVModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        userService: vm.$usersService,
                        companiesService: vm.$customerDetailsService,
                        exportData: {
                            ClientID: vm.$usersService.user.ClientID,
                            ClientName: vm.$usersService.user.Client.Name,
                            ListOfIDs: (vm.filteredRows.length == 1 ? vm.filteredRows[0].Id.toString() : vm.filteredRows.map(function (a) { return a.Id }).toString())
                        }
                    }
                }
            }
        });
    }

    function _cancel() {
        $uibModalInstance.dismiss('cancel');
    }

    function _onMarginChange(company) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to UPDATE this company's margin setting?";
                $scope.confirm = function () {
                    $uibModalInstance.close(
                        vm.$customerPriceMarginsService.updatePriceMarginAndGetHighestMargin(company, _onUpdatePriceMarginSuccess, _onError)
                        );
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss(
            angular.forEach(vm.savedCustomers, function (value) {
                if (company.Id == value.Id) company.MarginSetting = value.MarginSetting;
            })
                        );
                }
            }
        });
    }

    function _addContact() {
        $timeout(function () {
            if (vm.contactFilter) $contactsService.filter = vm.contactFilter;
            if (vm.customerFilter) $customerDetailsService.filter = vm.customerFilter;
        });
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/AddNewContact.html',
            controller: 'addContactModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        companies: vm.savedCustomers,
                        aircraftData: vm.aircraftData,
                        priceMargins: vm.priceMargins,
                        contacts: vm.contacts
                    }
                }
            }
        });
    }

    function _exportContact() {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/ExportCSV.html',
            controller: 'exportCSVModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        userService: vm.$usersService,
                        contactsService: vm.$contactsService,
                        exportData: {
                            ClientID: vm.$usersService.user.ClientID,
                            ClientName: vm.$usersService.user.Client.Name,
                            ListOfIDs: (vm.filteredRows.length == 1 ? vm.filteredRows[0].Id.toString() : vm.filteredRows.map(function (a) { return a.Id }).toString())
                        }
                    }
                }
            }
        });
    }

    function _changeDistribution(contact) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to UPDATE this contact's distribution?";
                $scope.confirm = function () {
                    $uibModalInstance.close(
            vm.$contactsService.updateContact(contact.Id, contact, _onUpdateDistributionSuccess, _onError)
                        );
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss(
            angular.forEach(vm.savedContacts, function (value) {
                if (contact.Id == value.Id) contact.Distribute = value.Distribute;
            })
                        );
                }
            }
        });
    }

    function _viewCustomer(customer) {
        $timeout(function () {
            $customerDetailsService.CustClientID = customer.CustClientID;
            if (vm.customerFilter) $customerDetailsService.filter = vm.customerFilter;
            if (vm.contactFilter) $contactsService.filter = vm.contactFilter;
            vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
        });
    }

    function _editContact(contact) {
        $timeout(function () {
            $contactsService.ContactID = contact.Id;
            if (vm.contactFilter) $contactsService.filter = vm.contactFilter;
            if (vm.customerFilter) $customerDetailsService.filter = vm.customerFilter;
            vm.$rootScope.ApplicationState.ActiveSection = 'CONTACT INFO';
        });
    }

    function _viewCompanies() {
        vm.$rootScope.ApplicationState.SubSection = 'COMPANIES';
        $timeout(function () {
            vm.isCompaniesReady = true;
            if ($customerDetailsService.filter) vm.customerFilter = $customerDetailsService.filter;
            $rootScope.triggerFilters();
        });
    }

    function _viewContacts() {
        vm.$rootScope.ApplicationState.SubSection = 'CONTACTS';
        $timeout(function () {
            vm.isContactsReady = true;
            if ($contactsService.filter) vm.contactFilter = $contactsService.filter;
            $rootScope.triggerFilters();
        });
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetAircraftDataSuccess(data) {
        vm.notify(function () {
            vm.aircraftData = JSON.parse(data.Item);
            console.log("AIRCRAFT DATA: ", vm.aircraftData);
        });
    }

    function _onGetPriceMarginsSuccess(data) {
        vm.notify(function () {
            vm.priceMargins = data.Items;
            console.log("PRICE MARGINS: ", vm.priceMargins);
        });
    }

    function _onGetCustomersSuccess(data) {
        vm.notify(function () {
            vm.customers = $customerDetailsService.companies = JSON.parse(data.Item);
            vm.customerCount = $customerDetailsService.customerCount = vm.customers.length;
            vm.savedCustomers = $customerDetailsService.savedCustomers = angular.copy(vm.customers);
            console.log("CUSTOMERS: ", vm.customers);
            _orderCustomers();
            if (vm.$rootScope.ApplicationState.SubSection === 'COMPANIES')  _viewCompanies();
        });
    }

    function _orderCustomers() {
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
    }

    function _onGetContactsSuccess(data) {
        vm.notify(function () {
            vm.contacts = $contactsService.contacts = data.Items;
            vm.contactsCount = $contactsService.contactsCount = vm.contacts.length;
            vm.savedContacts = $contactsService.savedContacts = angular.copy(data.Items);
            console.log("CONTACTS: ", vm.contacts);
            if (vm.$rootScope.ApplicationState.SubSection === 'CONTACTS') _viewContacts();
        });
    }

    function _onUpdatePriceMarginSuccess(data, company) {
        vm.notify(function () {
            company.MarginResult = data.Item.HighestMargin;
            $scope.$emit('updateCompanies', $usersService);
        });
    }

    function _onUpdateDistributionSuccess(data) {
        console.log("Distribution changed!");
    }

    function _onSaveClientSuccess(data) {
        vm.savedCustomer.CustClientID = data.Item;
        vm.$customerDetailsService.addCustomer(vm.savedCustomer, _onSaveCustomerSuccess, _onError);
    }

    function _onSaveCustomerSuccess(data) {
        vm.notify(function () {
            vm.savingCustomer = false;
            if (data.Item) {
                vm.savedCustomer.Id = data.Item;
                vm.savedCustomer.DateAdded = new Date();
            }
        });
    }

    function _onDeleteCustomerSuccess() {
        vm.notify(function () {
            var index = vm.customers.indexOf(vm.deletedCustomer);
            if (index > -1) vm.customers.splice(index, 1);
        });
    }

    function _onError() {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }

}
degatech.ng.addController(degatech.ng.app.module,
    "customerManagerController",
    [
        '$scope',
        '$rootScope',
        '$uibModal',
        '$log',
        '$baseController',
        '$usersService',
        "$clientsService",
        '$customerDetailsService',
        '$contactsService',
        '$priceMarginsService',
        '$aircraftDataService',
        '$customerPriceMarginsService',
        'Notification',
        '$timeout'
    ],
    degatech.page.customerManagerControllerFactory);

////////////////////////////MODAL - ADD COMPANY///////////////////////////////////

degatech.services.addCompanyModalFactory = function ($baseService) {
    var aServiceObject = degatech.services.customerDetails;
    var newService = $baseService.merge(true, {}, aServiceObject, $baseService);
    return newService;
}

degatech.services.aircraftsFactory = function ($baseService) {
    var serviceObject = degatech.services.aircrafts;
    var newService = $baseService.merge(true, {}, serviceObject, $baseService);
    return newService;
}

degatech.services.clientsFactory = function ($baseService) {
    var serviceObject = degatech.services.clients;
    var newService = $baseService.merge(true, {}, serviceObject, $baseService);
    return newService;
}

degatech.services.registrationFactory = function ($baseService) {
    var serviceObject = degatech.services.registration;
    var newService = $baseService.merge(true, {}, serviceObject, $baseService);
    return newService;
}

degatech.services.customerPriceMarginsFactory = function ($baseService) {
    var serviceObject = degatech.services.customerPriceMargins;
    var newService = $baseService.merge(true, {}, serviceObject, $baseService);
    return newService;
}

degatech.services.accountingInfoFactory = function ($baseService) {
    var serviceObject = degatech.services.customerAccountingInfo;
    var newService = $baseService.merge(true, {}, serviceObject, $baseService);
    return newService;
}

degatech.ng.addService(degatech.ng.app.module, "$customerModalService", ["$baseService"], degatech.services.addCompanyModalFactory);

degatech.ng.addService(degatech.ng.app.module, "$aircraftsService", ["$baseService"], degatech.services.aircraftsFactory);

degatech.ng.addService(degatech.ng.app.module, "$clientsService", ["$baseService"], degatech.services.clientsFactory);

degatech.ng.addService(degatech.ng.app.module, "$registrationService", ["$baseService"], degatech.services.registrationFactory);

degatech.ng.addService(degatech.ng.app.module, "$customerPriceMarginsService", ["$baseService"], degatech.services.customerPriceMarginsFactory);

degatech.ng.addService(degatech.ng.app.module, "$accountingInfoService", ["$baseService"], degatech.services.accountingInfoFactory);

degatech.page.addCompanyModalControllerFactory = function ($scope, $rootScope, $uibModalInstance, $customerModalService, items, $uibModal,
    $usersService,
    $aircraftsService,
    $aircraftDataService,
    $clientsService,
    $registrationService,
    $customerPriceMarginsService,
    $accountingInfoService,
    $customerDetailsService,
    Notification) {

    var vm = this;

    vm.$scope = $scope;
    vm.$rootScope = $rootScope;
    vm.modalInstance = $uibModalInstance;
    vm.$customerModalService = $customerModalService;
    vm.$aircraftsService = $aircraftsService;
    vm.$clientsService = $clientsService;
    vm.$registrationService = $registrationService;
    vm.$customerPriceMarginsService = $customerPriceMarginsService;
    vm.$accountingInfoService = $accountingInfoService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.items = items;

    vm.notify = vm.$customerModalService.getNotifier($scope);

    vm.cancel = _cancel;
    vm.next = _next;
    vm.back = _back;
    vm.saveAll = _saveAll;
    vm.addAircraft = _addAircraft;
    vm.deleteAircraft = _deleteAircraft;
    vm.changeMakeModel = _changeMakeModel;


    vm.items = items;
    vm.State = {
        CurrentStep: 1,
        MaxSteps: 2
    };
    vm.NewCompany = {
        Aircrafts: [],
        Company: { IsActive: true }
    };
    vm.contactTypes = ["Primary", "Secondary", "Billing"];
    vm.creditCardFee = ["No", "Margin", "Invoice"];
    vm.paymentTerms = ["Credit Card", "Net 10", "Net 14", "Net 15", "Net 20", "Net 25", "Net 30", "Net 45", "Net 60"];
    vm.billToSetup = ["No", "Partial", "Full"];
    vm.schedulingSystem = ['FOS', 'BART', 'FltPlan.com'];
    vm.certificateType = ["PART 91 Corporate", "PART 91 Personal/Non Business", "PART 135 Charter Carrier",
                            "PART 135 Management (Airline)", "PART 135 Management (Non-Airline)", "PART 121 Scheduled Carrier"];

    _init();

    function _init() {
        console.log(vm.items);
    }

    function _cancel() {
        $uibModalInstance.close();
    }

    function _next() {
        vm.showFormErrors = true;
        if (vm.companyForm.$valid)
            vm.$registrationService.checkUsername(vm.NewCompany.Registration, _onCheckUsernameSuccess, _onError);
    }

    function _back() {
        vm.State.CurrentStep = vm.State.CurrentStep - 1;
    }

    function _addAircraft() {
        vm.NewCompany.Aircrafts.push({
            AdminClientID: $usersService.user.ClientID,
        });
    }

    function _changeMakeModel(aircraft) {
        angular.forEach(vm.items.aircraftData, function (value) {
            if (value.AircraftID == aircraft.MakeModelID) aircraft.MakeAndModel = value;
        })
    }

    function _deleteAircraft(aircraft) {
        var index = vm.NewCompany.Aircrafts.indexOf(aircraft);
        if (index > -1) vm.NewCompany.Aircrafts.splice(index, 1);
    }

    function _saveAll() {
        vm.IsSaving = true;
        _saveClient();
    }

    function _onCheckUsernameSuccess(data) {
        vm.notify(function () {
            if (!data.IsDuplicateUsername)
                vm.State.CurrentStep = vm.State.CurrentStep + 1;
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
            }
        });
    }

    function _reorder() {
        vm.items.orderedCustomers.push(vm.client);
        var orderedCustomers = vm.items.orderedCustomers.sort(function (a, b) {
            var x = a.Name;
            var y = b.Name;
            if (x < y) { return -1; }
            if (x > y) { return 1; }
            return 0;
        });
        $customerDetailsService.CustClientIDs = orderedCustomers.map(function (customer) { return customer.CustClientID });
        console.log("ORDERED CUSTOMER IDs: ", $customerDetailsService.CustClientIDs);
    }

    function _saveClient() {
        var registration = {
            Company: vm.NewCompany.Company.Name,
            Username: vm.NewCompany.Registration.Username,
            Password: vm.NewCompany.Registration.Password
        };
        vm.$registrationService.addRegistration(registration, _onSaveRegistrationSuccess, _onError);
    }

    function _onSaveRegistrationSuccess(data) {
        console.log("Registration Saved!");
        vm.regId = data.Item;
        vm.client = {
            Name: vm.NewCompany.Company.Name,
            ClientType: 2
        };
        vm.$clientsService.addClient(vm.client, _onSaveClientSuccess, _onError);
    }

    function _onSaveClientSuccess(data) {
        console.log("Client Saved!");
        vm.client.CustClientID = data.Item;
        _reorder();
        var user = {
            RegistrationID: vm.regId,
            ClientID: vm.client.CustClientID,
            IsActive: vm.NewCompany.Company.IsActive
        };
        $usersService.addUser(user, _onSaveUserSuccess, _onError);
    }

    function _onSaveUserSuccess(data) {
        console.log("User Saved!");
        var customerPriceMargin = {
            CustClientID: vm.client.CustClientID,
            PriceMarginID: vm.NewCompany.CustomerPriceMargin.MarginSetting,
        }
        vm.$customerPriceMarginsService.addCustomerPriceMargin(customerPriceMargin, _onSavePriceMarginSuccess, _onError);
    }

    function _onSavePriceMarginSuccess(data) {
        console.log("Price Margin Saved!");
        var customerDetail = {
            AdminClientID: $usersService.user.ClientID,
            CustClientID: vm.client.CustClientID,
            IsActive: vm.NewCompany.Company.IsActive,
            Name: vm.NewCompany.Company.Name,
            Phone: vm.NewCompany.Company.Phone || '',
            City: vm.NewCompany.Company.City || '',
            Country: vm.NewCompany.Company.Country || '',
            BaseICAO: vm.NewCompany.Company.BaseICAO || '',
            Note: vm.NewCompany.Company.Note || '',
            ParentName: vm.NewCompany.Company.ParentName || '',
            CertificateType: vm.NewCompany.Company.CertificateType || '',
            Address1: vm.NewCompany.Company.Address1 || '',
            Address2: vm.NewCompany.Company.Address2 || "",
            State: vm.NewCompany.Company.State || '',
            ZipCode: vm.NewCompany.Company.ZipCode || ''
        }
        vm.$customerModalService.addCustomer(customerDetail, _onSaveCustomerSuccess, _onError);
    }

    function _onSaveCustomerSuccess(data) {
        console.log("Customer Saved!");
        var accountingInfo = {
            AdminClientID: $usersService.user.ClientID,
            CustClientID: vm.client.CustClientID,
            StartDate: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.StartDate : new Date().toDateString(),
            AccountRep: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.AccountRep : '',
            AccountingCode: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.AccountingCode : '',
            BillingRep: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.BillingRep : '',
            SchedulingSystem: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.SchedulingSystem : '',
            CreditCardFee: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.CreditCardFee : '',
            PaymentTerms: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.PaymentTerms : '',
            BillToSetup: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.BillToSetup : '',
            IsFuelerLinxCustomer: (vm.NewCompany.Accounting) ? vm.NewCompany.Accounting.IsFuelerLinxCustomer : false
        };
        vm.$accountingInfoService.saveAccountingInfo(accountingInfo, _onSaveAccountingSuccess, _onError);
    }

    function _onSaveAccountingSuccess(data) {
        console.log("Accounting Info Saved!");
        //if (vm.accountingInfo.IsFuelerLinxCustomer) {
        //    vm.customer.Integration.ClientID = accountingInfo.CustClientID;
        //    vm.customer.Integration.Name = vm.customer.Name;
        //    //vm.$integrationsService.addAccountNumber(vm.customer.Integration, _onAddAccountSuccess, _onError);
        //}

        if (vm.NewCompany.Aircrafts.length > 0) {
            vm.savedAircrafts = [];
            angular.forEach(vm.NewCompany.Aircrafts, function (aircraft) {
                if (aircraft.TailNumber && aircraft.MakeAndModel) {
                    aircraft.CustClientID = vm.client.CustClientID;
                    vm.savedAircrafts.push(aircraft);
                }
            });
            console.log("And finally...");
            vm.$aircraftsService.addListOfAircraft(vm.savedAircrafts, _onSaveAircraftsSuccess, _onError);
        } else {
            Notification.success({
                model: this,
                scope: $scope,
                message: "Company is saved! <br /> <br />",
                delay: 3000,
                closeOnClick: false
            });
            if (vm.items.hasContactModal) {
                console.log("Updating companies...");
                $customerDetailsService.getCustomersByAdminClient($usersService.user.ClientID, _onGetCustomersSuccess, _onError);
            } else {
                $customerDetailsService.companies = null;
                $uibModalInstance.close($customerDetailsService.CustClientID = vm.client.CustClientID);
                vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
            }
        }
    }

    function _onSaveAircraftsSuccess(data) {
        console.log("Aircrafts Saved!");
        Notification.success({
            model: this,
            scope: $scope,
            message: "Company is saved! <br /> <br />",
            delay: 3000,
            closeOnClick: false
        });
        if (vm.items.hasContactModal) {
            console.log("Updating companies...");
            $customerDetailsService.getCustomersByAdminClient($usersService.user.ClientID, _onGetCustomersSuccess, _onError);
        } else {
            $customerDetailsService.companies = null;
            $uibModalInstance.close($customerDetailsService.CustClientID = vm.client.CustClientID);
            vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
        }
    }

    function _onGetCustomersSuccess(data) {
        vm.notify(function () {
            console.log('Companies updated!');
            $uibModalInstance.close(vm.$customerDetailsService.companies = JSON.parse(data.Item));
        });
    }

    function _onError(error) {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(error);
    }
}

degatech.ng.addController(degatech.ng.app.module
            , "addCompanyModalController"
            , ['$scope',
                '$rootScope',
                '$uibModalInstance',
                '$customerModalService',
                'items',
                '$uibModal',
                '$usersService',
                '$aircraftsService',
                '$aircraftDataService',
                '$clientsService',
                '$registrationService',
                '$customerPriceMarginsService',
                '$accountingInfoService',
                '$customerDetailsService',
                'Notification'
            ]
            , degatech.page.addCompanyModalControllerFactory);

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

degatech.page.addContactModalControllerFactory = function ($scope, $uibModalInstance, $contactModalService, items, $uibModal,
    $usersService,
    $rootScope,
    $customerDetailsService,
    $contactNotesService,
    Notification) {

    var vm = this;

    vm.$scope = $scope;
    vm.modalInstance = $uibModalInstance;
    vm.$contactModalService = $contactModalService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.$contactNotesService = $contactNotesService;
    vm.items = items;
    vm.$rootScope = $rootScope;

    vm.notify = vm.$contactModalService.getNotifier($scope);

    vm.cancel = _cancel;
    vm.save = _save;
    vm.saveAndNew = _saveAndNew;
    vm.saveContact = _saveContact;
    vm.addCompany = _addCompany;
    vm.changeType = _changeType;
    vm.NewContact = {
        Contact: {
            Distribute: (vm.items.contacts.length == 0) ? true : false,
            ContactType: (vm.items.contacts.length == 0) ? 'Primary' : ''
        }
    };

    vm.items = items;

    $scope.$on('listOfCompanies', function (event, data) {
        console.log("Updating list of companies...");
        console.log(data);
        data.getCustomersByAdminClient($usersService.user.ClientID, _onGetCustomersSuccess, _onError);
    });

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
        vm.savedContact.Contact.AdminClientID = $usersService.user.ClientID;
        vm.$contactModalService.addContact(newContact.Contact, _onSaveContactSuccess, _onError);
    }

    function _onGetCustomersSuccess(data) {
        vm.notify(function () {
            console.log("List of companies updated");
            vm.items.companies = JSON.parse(data.Item);
        });
    }

    function _onSaveContactSuccess(data) {
        if (vm.savedContact.ContactNote && vm.savedContact.ContactNote.Note !== '') {
            console.log("Saving contact note...");
            vm.savedContact.Contact.Id = data.Item;
            vm.savedContact.ContactNote.ContactID = vm.savedContact.Contact.Id;
            vm.savedContact.ContactNote.AddedByUserID = $usersService.user.Id;
            vm.savedContact.ContactNote.CustClientID = vm.savedContact.Contact.CustClientID;
            vm.$contactNotesService.addContactNote(vm.savedContact.ContactNote, _onSaveContactNoteSuccess, _onError);
        } else {
            Notification.success({
                model: this,
                scope: $scope,
                message: "Contact Saved!",
                delay: 3000,
                closeOnClick: false
            });
            vm.notify(function () {
                if (vm.savedContact.IsRenewable) {
                    vm.savedContact.Contact.Id = data.Item;
                    vm.items.contacts.push(vm.savedContact.Contact);
                    vm.NewContact.Contact = null;
                    vm.showFormErrors = false;
                }
                else {
                    $uibModalInstance.close();
                    $customerDetailsService.CustClientID = vm.savedContact.Contact.CustClientID;
                    vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
                }
            });
        }
    }

    function _onSaveContactNoteSuccess(data) {
        Notification.success({
            model: this,
            scope: $scope,
            message: "Contact Note Saved! <br /> <br />",
            delay: 3000,
            closeOnClick: false
        });
        vm.notify(function () {
            if (vm.savedContact.IsRenewable) {
                vm.NewContact.Contact = null;
                vm.NewContact.ContactNote = null;
                vm.showFormErrors = false;
            }
            else {
                $uibModalInstance.close();
                $customerDetailsService.CustClientID = vm.savedContact.ContactNote.CustClientID;
                vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
            }
        });
    }

    function _onError() {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }

    /*********************COMPANY MODAL W/IN CONTACT MODAL**************************/

    function _addCompany() {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/AddNewCompany.html',
            controller: 'addCompanyModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        aircraftData: vm.items.aircraftData,
                        companies: vm.items.companies,
                        priceMargins: vm.items.priceMargins,
                        hasContactModal: true
                    }
                }
            }
        });
    }
}

degatech.ng.addController(degatech.ng.app.module
            , "addContactModalController"
            , ['$scope',
                '$uibModalInstance',
                '$contactModalService',
                'items',
                '$uibModal',
                '$usersService',
                '$rootScope',
                '$customerDetailsService',
                '$contactNotesService',
                'Notification'
            ]
            , degatech.page.addContactModalControllerFactory);

////////////////////////////MODAL - EXPORT CSV///////////////////////////////////

degatech.page.exportCSVModalControllerFactory = function ($scope, $uibModalInstance, items, $uibModal, $usersService) {

    var vm = this;

    vm.$scope = $scope;
    vm.modalInstance = $uibModalInstance;
    vm.$usersService = $usersService;
    vm.items = items;

    vm.notify = vm.$usersService.getNotifier($scope);

    vm.cancel = _cancel;

    vm.items = items;

    _init();

    function _init() {
        console.log(vm.items);
        console.log("Exporting...");
        if (vm.items.companiesService) vm.items.companiesService.exportCompanies(vm.items.exportData, _onExportSuccess, _onError);
        if (vm.items.contactsService) vm.items.contactsService.exportContacts(vm.items.exportData, _onExportSuccess, _onError);
    }

    function _cancel() {
        $uibModalInstance.close();
    }

    function _onExportSuccess(data) {
        console.log("Exported!")
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
            , "exportCSVModalController"
            , ['$scope',
                '$uibModalInstance',
                'items',
                '$uibModal',
                '$usersService'
            ]
            , degatech.page.exportCSVModalControllerFactory);


