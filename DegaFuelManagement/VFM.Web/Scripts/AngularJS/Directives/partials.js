
degatech.ng.app.module.directive('accountSettings', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/AccountSettings.html'
    };
});

///////////LOGIN////////////
degatech.ng.app.module.directive('largentFuel', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Login/LargentFuels.html'
    };
});

///////////ADMIN////////////
degatech.ng.app.module.directive('customers', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Customers.html',
        controller: 'customerManagerController',
        controllerAs: 'info'
    };
});

degatech.ng.app.module.directive('fuelOrders', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Transactions/FuelOrders.html',
        controller: 'fuelOrdersController',
        controllerAs: 'info'
    };
});

degatech.ng.app.module.directive('fuelOrderDetails', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Transactions/FuelOrderDetails.html',
        controller: 'fuelOrderController',
        controllerAs: 'fuelOrderInfo'
    };
});

degatech.ng.app.module.directive('transactionInfoDetails', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Transactions/TransactionInfoDetails.html'
    };
});

degatech.ng.app.module.directive('transactionInvoiceDetails', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Transactions/TransactionInvoiceDetails.html'
    };
});

degatech.ng.app.module.directive('transactionServiceDetails', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Transactions/TransactionServiceDetails.html'
    };
});

degatech.ng.app.module.directive('transactionPricingDetails', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Transactions/TransactionPricingDetails.html'
    };
});

degatech.ng.app.module.directive('transactionsDropzone', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Transactions/TransactionsDropzone.html'
    };
});

degatech.ng.app.module.directive('transactionMessenger', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Transactions/TransactionMessenger.html'
    };
});

degatech.ng.app.module.directive('priceMargins', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/PriceMargins.html',
        controller: 'priceMarginsController',
        controllerAs: 'info'
    };
});

degatech.ng.app.module.directive('emailRouting', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/EmailRouting.html',
        controller: 'emailSectionController',
        controllerAs: 'info'
    };
});

degatech.ng.app.module.directive('airportManager', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/AirportManager.html',
        controller: 'airportManagerController',
        controllerAs: 'info'
    };
});

degatech.ng.app.module.directive('fboDetails', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/AirportManager/FBODetails.html',
        controller: 'fboDetailsController',
        controllerAs: 'info'
    };
});

degatech.ng.app.module.directive('fileUploader', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Uploader/FileUploader.html'
    };
});

degatech.ng.app.module.directive('dbUploader', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Uploader/DBUploader.html',
        controller: 'uploaderController',
        controllerAs: 'upload'
    };
});

degatech.ng.app.module.directive('dashboard', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Dashboard.html'
    };
});

degatech.ng.app.module.directive('adhocFuelQuote', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Quoting/AdhocFuelQuote.html'
    };
});

degatech.ng.app.module.directive('customerDetails', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/CompanyDetails.html',
        controller: 'customerDetailsController',
        controllerAs: 'customerDetails'
    };
});

degatech.ng.app.module.directive('contactInfo', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/ContactInfo.html',
        controller: 'contactController',
        controllerAs: 'contactInfo'
    };
});

degatech.ng.app.module.directive('taskScheduler', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/TaskScheduler.html',
        controller: 'taskSchedulerController',
        controllerAs: 'task'
    };
});

degatech.ng.app.module.directive('taskEditor', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/ModalForms/TaskEditor.html'
    };
});

degatech.ng.app.module.directive('regionalDispatches', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Dashboard/RegionalDispatches.html',
        controller: 'analysisController',
        controllerAs: 'analysis'
    };
});

degatech.ng.app.module.directive('toDoList', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Dashboard/ToDoList.html',
        controller: 'toDoListController',
        controllerAs: 'todo'
    };
});

degatech.ng.app.module.directive('vendorManager', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Everest/VendorManager.html',
        controller: 'vendorManagerController',
        controllerAs: 'vendorGrid'
    };
});

degatech.ng.app.module.directive('analysis', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/Analysis.html',
        controller: 'analysisController',
        controllerAs: 'analysis'
    };
});

degatech.ng.app.module.directive('userManager', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/UserManager.html',
        controller: 'userManagerController',
        controllerAs: 'manager'
    };
});

degatech.ng.app.module.directive('userConfig', function () {
    return {
        restrict: 'E',
        templateUrl: '/Partials/Admin/UserManager/UserConfig.html',
        controller: 'userConfigController',
        controllerAs: 'config'
    };
});

//Status Dropdown Menu
degatech.ng.app.module.directive('statusDropdownMenu', [function () {
        return {
            restrict: 'E',
            templateUrl: '/Partials/Admin/Transactions/StatusDropdownMenu.html',
            scope: {
                onStatusChange: '=',
                viewStatusModel: '=',
                pageSettings: '='
            },
            link: function (scope) {
                scope.OnShowAllViewStatusClicked = function () {
                    for (var property in scope.viewStatusModel) {
                        if (property != 'AlwaysIncludeArchived')
                            scope.viewStatusModel[property] = false;
                    }
                    scope.viewStatusModel['IsPendingShown'] = true;
                    scope.viewStatusModel['IsReconciledShown'] = true;
                    scope.viewStatusModel['IsDiscrepancyShown'] = true;
                    scope.viewStatusModel['IsCancelledShown'] = true;
                    scope.viewStatusModel['IsBatchedShown'] = true;
                    scope.viewStatusModel.SelectedStatus = 'ShowAll';
                    scope.onStatusChange();
                };

                scope.OnShowOneViewStatusClicked = function (propertyToSetTrue) {
                    for (var property in scope.viewStatusModel) {
                        if (propertyToSetTrue == 'IsArchivedShown' && property != 'AlwaysIncludeArchived')
                            scope.viewStatusModel[property] = true;
                        else
                            scope.viewStatusModel[property] = false;
                    }
                    scope.viewStatusModel[propertyToSetTrue] = true;
                    scope.viewStatusModel.SelectedStatus = propertyToSetTrue;
                    scope.onStatusChange();
                };
            }
        };
    }]);