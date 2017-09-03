degatech.page.fuelOrdersControllerFactory = function ($scope,
    $rootScope,
    $baseController,
    $usersService,
    $fuelOrdersService,
    $fuelOrderInvoicesService,
    $uibModal,
    $timeout,
    Notification,
    $siteSettingsService,
    $customerDetailsService) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$fuelOrdersService = $fuelOrdersService;
    vm.$fuelOrderInvoicesService = $fuelOrderInvoicesService;
    vm.$siteSettingsService = $siteSettingsService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.$scope = $scope;
    vm.$rootScope = $rootScope;
    vm.notify = vm.$usersService.getNotifier($scope);

    //Private Members
    var _IsCheckingAll = false;

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.getDetails = _getDetails;
    vm.onToggleRowSubDetailsClicked = _onToggleRowSubDetailsClicked;
    //vm.onEditFuelOrderClicked = _onEditFuelOrderClicked;
    vm.processOrder = _processOrder;
    vm.applyDateFilters = _applyDateFilters;
    vm.addTransaction = _addTransaction;
    vm.getInvoiceStatusClass = _getInvoiceStatusClass;
    vm.getFuelReleaseStatusClass = _getFuelReleaseStatusClass; // mikes fuel release class
    vm.exportTransactions = _exportTransactions;
    vm.fuelRelease = _fuelRelease;
    vm.OnCheckAllClicked = _OnCheckAllClicked;
    vm.OnUnCheckAllClicked = _OnUnCheckAllClicked;
    vm.viewFuelOrders = _viewFuelOrders;
    vm.clearFilters = _clearFilters;
    vm.checkStatus = _checkStatus;
    vm.changeStatus = _changeStatus;
    vm.downloadInvoice = _downloadInvoice;
    vm.deleteInvoice = _deleteInvoice;
    vm.uploadInvoice = _uploadInvoice;
    vm.deleteFuelOrder = _deleteFuelOrder;
    vm.archiveFuelOrder = _archiveFuelOrder;
    vm.deleteFilteredFuelOrders = _deleteFilteredFuelOrders;
    vm.archiveFilteredFuelOrders = _archiveFilteredFuelOrders;
    vm.calculateTotal = _calculateTotal;
    vm.saveTotal = _saveTotal;
    vm.toCompanyDetails = _toCompanyDetails;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.displayedFuelOrders = [];
    vm.fuelOrders = null;
    vm.orderedFuelOrders = null;
    vm.TransactionsShowingDetails = [];
    vm.RecordsPerPage = [10, 20, 50, 100, 200];
    vm.FuelOrderRecordsPerPage = 20;
    vm.TransactionsFlaggedToMove = [];
    vm.isFuelOrdersReady = false;
    vm.loadingFuelRelease = false;
    vm.HighestTransactionIDFromDatabase = 0;
    vm.dateFilter = {
        StartDate: moment().subtract(6, 'months').format('MM/DD/YYYY'),
        EndDate: moment().add(2, 'months').format('MM/DD/YYYY'),
        IsStartDateOpened: false,
        IsEndDateOpened: false,
        Format: "MM/dd/yyyy"
    };
    vm.filter = {
        InvoiceStatus: "",
        InvoiceStatusLabel: "Show All",
        IsArchived: false
    };
    vm.dateConfigs = {
        StartDateOptionsConfig: {
            maxDate: new Date(vm.dateFilter.EndDate),
            startingDay: 1
        },
        EndDateOptionsConfig: {
            minDate: new Date(vm.dateFilter.StartDate),
            startingDay: 1
        }
    };

    vm.siteSettings = {
        AllowTransactionDetailEditing: true
    };

    //PUBLIC FILTERS//////////////////////////////////////////////
    vm.filterArchived = _filterArchived;

    _init();

    $scope.$on('filteredRows', function (event, data) {
        vm.filteredRows = data;
    });

    $scope.$emit('updateTotal', $usersService.dashboard);

    //PRIVATE METHODS//////////////////////////////////////////////
    function _init() {
        storage.SetSessionItem("LastActiveSection", "FUEL ORDERS");
        console.log("Getting Fuel Orders...");
        if ($fuelOrdersService.fuelordersHighestID > 0 && vm.HighestTransactionIDFromDatabase == 0)
            _getHighestTransactionIDFromDatabase();
        if ($fuelOrdersService.fuelOrders && vm.HighestTransactionIDFromDatabase == $fuelOrdersService.fuelordersHighestID) {
            vm.fuelOrders = $fuelOrdersService.fuelOrders;
            _viewFuelOrders();
        }
        else _applyDateFilters();
        vm.$siteSettingsService.getSettings(_onGetSettingsSuccess, _onError);
    }

    function _getHighestTransactionIDFromDatabase() {
        vm.$fuelOrdersService.getHighestTransactionIDFromDatabase($usersService.user, _onGetHighestTransactionIDFromDatabaseSuccess, _onError);
    }

    function _clearFilters() {
        for (var property in vm.filter) {
            if (property == 'InvoiceStatusLabel') vm.filter[property] = 'Show All';
            else vm.filter[property] = '';
        }
    }

    function _applyDateFilters() {
        vm.fuelOrders = null;
        vm.$fuelOrdersService.getFuelOrdersByClient($usersService.user, vm.dateFilter.StartDate, vm.dateFilter.EndDate, _onGetFuelOrdersSuccess, _onError);
    }

    function _getDetails(fuelOrder) {
        if (!vm.siteSettings.AllowTransactionDetailEditing)
            return;
        vm.$fuelOrdersService.FuelOrderID = fuelOrder.Id;
        if (vm.filter) $fuelOrdersService.filter = vm.filter;
        $rootScope.ApplicationState.ActiveSection = 'FUEL ORDER DETAILS';
    }

    function _toCompanyDetails(transaction) {
        $customerDetailsService.CustClientID = transaction.CustClientID;
        vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
    }

    function _onToggleRowSubDetailsClicked(index, transaction) {
        transaction.ShowingSubDetails = !transaction.ShowingSubDetails;
        if (transaction.ShowingSubDetails) {
            vm.TransactionsShowingDetails.push(transaction);
        } else
            vm.TransactionsShowingDetails.splice(vm.TransactionsShowingDetails.indexOf(transaction), 1);
        $('#divTransactionSubDetails' + index).toggle('medium');
    }

    function _addTransaction() {
        $fuelOrdersService.FuelOrderID = 0;
        vm.$rootScope.ApplicationState.ActiveSection = 'FUEL ORDER DETAILS';
    }

    function _processOrder(transaction) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to PROCESS this transaction?";

                $scope.confirm = function () {
                    transaction.AdminStatus = 2;
                    $uibModalInstance.close(vm.$fuelOrdersService.updateFuelOrder(transaction.Id, transaction, _onUpdateOrderSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _exportTransactions() {
        if (vm.filter.InvoiceStatus == '')
            vm.filteredRows = vm.filteredRows.filter(function (row) { return !row.IsArchived });
        console.log("Exporting transactions...");
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/ExportCSV.html',
            controller: 'exportTransactionsModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        expObj: {
                            ClientID: $usersService.user.ClientID,
                            ClientName: $usersService.user.Client.Name,
                            ClientType: $usersService.user.Client.ClientType,
                            StartDate: vm.dateFilter.StartDate,
                            EndDate: vm.dateFilter.EndDate,
                            ListOfIDs: (vm.filteredRows.length == 1 ? vm.filteredRows[0].Id.toString() : (vm.filteredRows.map(function (a) { return a.Id })).toString())
                        }
                    }
                }
            }
        });
    }

    function _fuelRelease(transaction) {
        console.log('Fuel Release = true');
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to send a fuel release for this transaction?";
                $scope.confirm = function () {
                    transaction.loadingFuelRelease = true;
                    $uibModalInstance.close(vm.$fuelOrdersService.sendConfirmationEmail(transaction.Id, function () {
                        onDispatchEmailSuccess(transaction);

                    }, _onError));
                    console.log('Fuel Release = false');
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _changeStatus(transaction) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to change this status to " + transaction.TransactionStatus + "?";
                $scope.confirm = function () {
                    _getInvoiceStatus(transaction);
                    vm.transaction = transaction;
                    $uibModalInstance.close(vm.$fuelOrdersService.updateFuelOrder(transaction.Id, transaction, _onUpdateOrderSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss(_getTransactionStatus(transaction));
                }
            }
        });
    }

    function _getInvoiceStatusClass(transaction) {
        if (transaction.InvoiceStatus == 1) return "newcolor";
        if (transaction.InvoiceStatus == 2) return "modifycolor";
        if (transaction.InvoiceStatus == 3) return "postverifycolor";
        if (transaction.InvoiceStatus == 4) return "marginanalysiscolor";
        if (transaction.InvoiceStatus == 5) return "discrepancycolor";
        if (transaction.InvoiceStatus == 6) return "accountingcolor";
        if (transaction.InvoiceStatus == 7) return "pendingcolor";
        if (transaction.InvoiceStatus == 8) return "reconciledcolor";
        if (transaction.InvoiceStatus == 9) return "canceledcolor";
        if (transaction.InvoiceStatus == 10) return "nofuelcolor";
    }

    function _getTransactionStatus(fuelOrder) {
        if (fuelOrder.InvoiceStatus == 1) fuelOrder.TransactionStatus =  "New";
        if (fuelOrder.InvoiceStatus == 2) fuelOrder.TransactionStatus =  "Modify";
        if (fuelOrder.InvoiceStatus == 3) fuelOrder.TransactionStatus =  "Post Verify";
        if (fuelOrder.InvoiceStatus == 4) fuelOrder.TransactionStatus =  "Margin Analysis";
        if (fuelOrder.InvoiceStatus == 5) fuelOrder.TransactionStatus = 'Discrepancy';
        if (fuelOrder.InvoiceStatus == 6) fuelOrder.TransactionStatus =  "Accounting";
        if (fuelOrder.InvoiceStatus == 7) fuelOrder.TransactionStatus = 'Pending';
        if (fuelOrder.InvoiceStatus == 8) fuelOrder.TransactionStatus = 'Reconciled';
        if (fuelOrder.InvoiceStatus == 9) fuelOrder.TransactionStatus = 'Cancelled';
        if (fuelOrder.InvoiceStatus == 10) fuelOrder.TransactionStatus =  "No Fuel";

        //switch (fuelOrder.InvoiceStatus) {
        //    case 5:
        //        fuelOrder.TransactionStatus = 'Discrepancy';
        //        break;
        //    case 7:
        //        fuelOrder.TransactionStatus = 'Pending';
        //        break;
        //    case 8:
        //        fuelOrder.TransactionStatus = 'Reconciled';
        //        break;
        //    case 9:
        //        fuelOrder.TransactionStatus = 'Cancelled';
        //        break;
        //    default:
        //        fuelOrder.TransactionStatus = 'Pending';
        //}
    }

    function _getInvoiceStatus(fuelOrder) {
        if (fuelOrder.TransactionStatus == "New") fuelOrder.InvoiceStatus = 1;
        if (fuelOrder.TransactionStatus == "Modify") fuelOrder.InvoiceStatus = 2;
        if (fuelOrder.TransactionStatus == "Post Verify") fuelOrder.InvoiceStatus = 3;
        if (fuelOrder.TransactionStatus == "Margin Analysis") fuelOrder.InvoiceStatus = 4;
        if (fuelOrder.TransactionStatus == 'Discrepancy') fuelOrder.InvoiceStatus = 5;
        if (fuelOrder.TransactionStatus == "Accounting") fuelOrder.InvoiceStatus = 6;
        if (fuelOrder.TransactionStatus == 'Pending') fuelOrder.InvoiceStatus = 7;
        if (fuelOrder.TransactionStatus == 'Reconciled') fuelOrder.InvoiceStatus = 8;
        if (fuelOrder.TransactionStatus == 'Cancelled') fuelOrder.InvoiceStatus = 9;
        if (fuelOrder.TransactionStatus == "No Fuel") fuelOrder.InvoiceStatus = 10;

        //switch (fuelOrder.TransactionStatus) {
        //    case 'Discrepancy':
        //        fuelOrder.InvoiceStatus = 5;
        //        break;
        //    case 'Pending':
        //        fuelOrder.InvoiceStatus = 7;
        //        break;
        //    case 'Reconciled':
        //        fuelOrder.InvoiceStatus = 8;
        //        break;
        //    case 'Cancelled':
        //        fuelOrder.InvoiceStatus = 9;
        //        break;
        //    default:
        //        fuelOrder.InvoiceStatus = 7;
        //}
    }

    function _OnCheckAllClicked() {
        _IsCheckingAll = true;
        $(vm.filteredRows).each(function (index) {
            this.IsFlaggedToMove = true;
            $scope.OnFlaggedToMoveChanged(this);
        });
        //HideSubDetails();
        _IsCheckingAll = false;
    }

    function _OnUnCheckAllClicked() {
        $(vm.filteredRows).each(function () {
            this.IsFlaggedToMove = false;
            $scope.OnFlaggedToMoveChanged(this);
        });
    };

    $scope.OnFlaggedToMoveChanged = function (transaction) {
        if (transaction.IsFlaggedToMove) {
            vm.TransactionsFlaggedToMove.push(transaction);
            _changeButtonDisplay();
            //if (!_IsCheckingAll)
            //    HideSubDetails();
        } else {
            vm.TransactionsFlaggedToMove.splice(vm.TransactionsFlaggedToMove.indexOf(transaction), 1);
            _changeButtonDisplay();
        }
    };

    function _changeButtonDisplay() {
        var filteredRows = vm.TransactionsFlaggedToMove.filter(function (transaction) { return transaction.IsFlaggedToMove; });
        if (filteredRows.length > 0) vm.showButtons = true;
        else vm.showButtons = false;
    }

    function HideSubDetails() {
        $timeout(function () {
            $('a[id*="lnkToggleSubDetails"].active').click();
        });
    }

    function _getFuelReleaseStatusClass(transaction) {
        if (transaction.AdminStatus == 0) return "notReleased";
        if (transaction.AdminStatus == 1) return "isReleased";
        if (transaction.AdminStatus == 2) return "customerEmailSent";
    }

    function _viewFuelOrders() {
        $timeout(function () {
            vm.isFuelOrdersReady = true;
            if ($fuelOrdersService.filter) vm.filter = $fuelOrdersService.filter;
            $rootScope.triggerFilters();
        });
    }

    function _displayStatus() {
        angular.forEach(vm.fuelOrders, function (fuelOrder) {
            if (fuelOrder.IsArchived) fuelOrder.TransactionStatus = 'Archived';
            else _getTransactionStatus(fuelOrder);
        }
        );
    }

    function _checkStatus(transaction) {
        if (Math.abs(transaction.InvoicedPPG - transaction.QuotedPPG) < 0.05) {
            transaction.TransactionStatus = 'Reconciled';
            transaction.InvoiceStatus = 8;
        } else {
            transaction.TransactionStatus = 'Discrepancy';
            transaction.InvoiceStatus = 5;
        }
        vm.$fuelOrdersService.updateFuelOrder(transaction.Id, transaction, _onUpdateOrderSuccess, _onError);
    }

    function _downloadInvoice(transaction) {
        vm.$fuelOrderInvoicesService.getFuelOrderInvoices(transaction.Id, _onGetInvoicesSuccess, _onError);
    }

    function _uploadInvoice(transaction) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/InvoiceUploader2.html',
            controller: function ($scope, $uibModalInstance, $timeout) {
                $scope.dropzoneConfig = {
                    uploadMultiple: false,
                    previewsContainer: "#Dropzone-Preview-Placeholder",
                    maxFileSize: 4,
                    url: "/api/files/attachment/" + transaction.Id,
                    acceptedFiles: '.pdf,.xlsx,.xls,.csv',
                    addRemoveLinks: true,
                    accept: function (file, done) {
                        if (file.size > 4000000) {
                            Notification.warning({
                                model: this,
                                scope: $scope,
                                message: "File size exceeded.",
                                delay: 3000,
                                closeOnClick: false
                            });
                        }
                        else if (file.type == 'application/pdf' || file.type == 'application/vnd.ms-excel') {
                            var container = $(file.previewElement).parent();
                            $(file.previewElement).hide();
                            done();
                        } else {
                            Notification.warning({
                                model: this,
                                scope: $scope,
                                message: "That type of file is currently not supported.",
                                delay: 3000,
                                closeOnClick: false
                            });
                        };
                    },
                    success: function (data) {
                        $timeout(function () {
                            $uibModalInstance.close(transaction.HasAttachments = true);
                        });
                        Notification.success({
                            model: this,
                            scope: $scope,
                            message: "File uploaded successfully! <br /> <br />",
                            delay: 3000
                        });
                    },
                    init: function () {
                        var myDropzone = this;

                        this.on("success", function (data) {
                        });

                        this.on("processing", function (file) {
                            this.options.url = "/api/files/attachment/" + transaction.Id;
                        });
                    }
                };
                $scope.close = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _deleteInvoice(transaction) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE this invoice?";
                $scope.confirm = function () {
                    vm.transaction = transaction;
                    $uibModalInstance.close(vm.$fuelOrderInvoicesService.deleteFuelOrderInvoices(transaction.Id, _onDeleteInvoiceSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _deleteFuelOrder(transaction) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE this transaction?";
                $scope.confirm = function () {
                    vm.deletedTransaction = transaction;
                    $uibModalInstance.close(vm.$fuelOrdersService.deleteFuelOrders(transaction.Id, _onDeleteSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _deleteFilteredFuelOrders() {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE these transactions?";
                $scope.confirm = function () {
                    $uibModalInstance.close(vm.$fuelOrdersService.deleteFuelOrdersList(vm.TransactionsFlaggedToMove, _onDeleteSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _archiveFuelOrder(transaction) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to ARCHIVE this transaction?";
                $scope.confirm = function () {
                    transaction.IsArchived = true;
                    transaction.TransactionStatus = 'Archived';
                    $uibModalInstance.close(vm.$fuelOrdersService.updateFuelOrder(transaction.Id, transaction, _onUpdateOrderSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _archiveFilteredFuelOrders() {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to ARCHIVE these transactions?";
                $scope.confirm = function () {
                    vm.TransactionsFlaggedToMove.map(function (transaction) {
                        transaction.IsArchived = true;
                        transaction.TransactionStatus = 'Archived';
                    });
                    $uibModalInstance.close(vm.$fuelOrdersService.updateFuelOrderList(vm.TransactionsFlaggedToMove, _onUpdateListSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _calculateTotal(transaction) {
        if (!transaction.IsCalcBypassed)
            transaction.Total = transaction.InvoicedVolume * transaction.InvoicedPPG;
        _saveTotal(transaction);
    }

    function _saveTotal(transaction) {
        console.log("Saving Total...");
        if (transaction.Total !== transaction.InvoicedVolume * transaction.InvoicedPPG)
            transaction.IsCalcBypassed = true;
        vm.$fuelOrdersService.updateFuelOrder(transaction.Id, transaction, _onUpdateOrderSuccess, _onError);
    }

    function _updateTotal() {
        console.log("Updating total...");
        $scope.$emit('updateTotal', $usersService.dashboard);
    }

    //PRIVATE FILTERS//////////////////////////////////////////////
    function _filterArchived(transaction) {
        if (vm.filter.InvoiceStatusLabel == 'Show All' && transaction.TransactionStatus !== 'Archived') return true;
        else if (vm.filter.InvoiceStatus == transaction.TransactionStatus) return true;
        else return false;
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetHighestTransactionIDFromDatabaseSuccess(data) {
        vm.HighestTransactionIDFromDatabase = data;
        _init();
    }

    function _onGetFuelOrdersSuccess(data) {
        vm.notify(function () {
            if (vm.fuelOrders == null) vm.fuelOrders = $fuelOrdersService.fuelOrders = data.Items;//[];
            //vm.fuelOrders.length = 0;
            //Array.prototype.push.apply(vm.fuelOrders, data.Items);
            GetHighestTransactionID();
            $fuelOrdersService.fuelordersHighestID = vm.orderedFuelOrders[0].Id;
            console.log("FUEL ORDERS: ", vm.fuelOrders);
            _viewFuelOrders();
            _displayStatus();
        });
    }

    function GetHighestTransactionID() {
        vm.orderedFuelOrders = vm.fuelOrders;
        vm.orderedFuelOrders.sort(function(a, b) {
            return parseFloat(b.Id) - parseFloat(a.Id);
        });
    }

    function _onUpdateOrderSuccess(data) {
        vm.notify(function () {
            Notification.success({
                model: this,
                scope: $scope,
                message: "Transaction processed! <br /> <br />",
                delay: 3000,
                closeOnClick: false
            });
        });
    }

    function _onUpdateListSuccess() {
        vm.notify(function () {
            vm.TransactionsFlaggedToMove.length = 0;
            _changeButtonDisplay();
            Notification.success({
                model: this,
                scope: $scope,
                message: "Transactions processed! <br /> <br />",
                delay: 3000,
                closeOnClick: false
            });
        });
    }

    function onDispatchEmailSuccess(transaction) {
        vm.notify(function () {
            transaction.loadingFuelRelease = false;
            console.log("Email dispatched!");
            transaction.AdminStatus = 2;
        });
    }
    //function _onSaveFuelOrderSuccess(data) {
    //    vm.notify(function () {
    //        Notification.success({
    //            model: this,
    //            scope: $scope,
    //            //templateUrl: '/Partials/Common/Notifications/Login.html',
    //            message: "Transaction processed!",
    //            delay: 3000,
    //            closeOnClick: false
    //        });
    //    });
    //}

    function _onGetInvoicesSuccess(data) {
        vm.notify(function () {
            vm.invoices = data.Items;
            console.log("FUEL ORDER INVOICES: ", vm.invoices);
            console.log("Downloading invoice...");
            location.href = "/FileController/DownloadPDF?invoiceId=" + vm.invoices[0].Id;
        });
    }

    function _onDeleteSuccess(data) {
        vm.notify(function () {
            if (vm.deletedTransaction) {
                var index = vm.filteredRows.indexOf(vm.deletedTransaction);
                if (index > 1) vm.filteredRows.splice(index, 1);
                vm.deletedTransaction = null;
                Notification.warning({
                    model: this,
                    scope: $scope,
                    message: "Transaction has been deleted!",
                    delay: 3000,
                    closeOnClick: false
                });
            } else {
                angular.forEach(vm.TransactionsFlaggedToMove, function (fuelOrder) {
                    var index = vm.filteredRows.indexOf(fuelOrder);
                    if (index > 1) vm.filteredRows.splice(index, 1);
                });
                vm.TransactionsFlaggedToMove.length = 0;
                _changeButtonDisplay();
                Notification.warning({
                    model: this,
                    scope: $scope,
                    message: "Transactions have been deleted!",
                    delay: 3000,
                    closeOnClick: false
                });
            }
            _applyDateFilters();
            _updateTotal();
        });
    }

    function _onDeleteInvoiceSuccess(data) {
        vm.notify(function () {
            vm.transaction.HasAttachments = false;
            Notification.warning({
                model: this,
                scope: $scope,
                //templateUrl: '/Partials/Common/Notifications/Login.html',
                message: "This invoice has been deleted!",
                delay: 3000,
                closeOnClick: false
            });
        });
    }

    function _onGetSettingsSuccess(data) {
        vm.notify(function () {
            vm.siteSettings.AllowTransactionDetailEditing = data.Item.AllowTransactionDetailEditing;
        });
    }

    function _onError(error) {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }

}
degatech.ng.addController(degatech.ng.app.module,
    "fuelOrdersController",
    [
        '$scope',
        '$rootScope',
        '$baseController',
        '$usersService',
        '$fuelOrdersService',
        '$fuelOrderInvoicesService',
        '$uibModal',
        '$timeout',
        'Notification',
        '$siteSettingsService',
        '$customerDetailsService'
    ],
    degatech.page.fuelOrdersControllerFactory);

///////////////////////////////////////MODAL - EXPORT CSV///////////////////////////////////

degatech.services.exportModalFactory = function ($baseService) {
    var aServiceObject = degatech.services.fuelOrders;
    var newService = $baseService.merge(true, {}, aServiceObject, $baseService);
    return newService;
}

degatech.ng.addService(degatech.ng.app.module, "$exportModalService", ["$baseService"], degatech.services.exportModalFactory);

degatech.page.exportTransactionsModalControllerFactory = function ($scope, $timeout, $uibModalInstance, $exportModalService, items, $uibModal) {

    var vm = this;

    vm.$scope = $scope;
    vm.modalInstance = $uibModalInstance;
    vm.$exportModalService = $exportModalService;
    vm.items = items;

    vm.notify = vm.$exportModalService.getNotifier($scope);

    vm.cancel = _cancel;

    vm.items = items;

    _init();

    function _init() {
        $timeout(function () {
            console.log(vm.items);
            vm.$exportModalService.exportTransactions(vm.items.expObj, _onExportSuccess, _onError);
        });
    }

    function _cancel() {
        $uibModalInstance.close();
    }

    function _onExportSuccess(data) {
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
            , "exportTransactionsModalController"
            , ['$scope',
                '$timeout',
                '$uibModalInstance',
                "$exportModalService",
                'items',
                '$uibModal'
            ]
            , degatech.page.exportTransactionsModalControllerFactory);