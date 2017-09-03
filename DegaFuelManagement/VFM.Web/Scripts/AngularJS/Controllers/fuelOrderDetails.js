degatech.page.fuelOrderControllerFactory = function ($scope,
    $rootScope,
    $timeout,
    $baseController,
    $fuelOrdersService,
    $fuelOrderNotesService,
    $aircraftsService,
    $usersService,
    $customerDetailsService,
    $uibModal,
    $fuelOrderTaxesService,
    $fuelOrderFeesService,
    $fuelOrderInvoicesService,
    $fuelOrderPricingsService,
    $fuelOrderMessagesService,
    $clientTaxesService,
    $clientFeesService,
    Notification) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$fuelOrdersService = $fuelOrdersService;
    vm.$fuelOrderNotesService = $fuelOrderNotesService;
    vm.$aircraftsService = $aircraftsService;
    vm.$usersService = $usersService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.$fuelOrderTaxesService = $fuelOrderTaxesService;
    vm.$fuelOrderFeesService = $fuelOrderFeesService;
    vm.$fuelOrderInvoicesService = $fuelOrderInvoicesService;
    vm.$fuelOrderPricingsService = $fuelOrderPricingsService;
    vm.$fuelOrderMessagesService = $fuelOrderMessagesService;
    vm.$clientTaxesService = $clientTaxesService;
    vm.$clientFeesService = $clientFeesService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;
    vm.dateFormat = "MM/dd/yyyy";
    vm.CalculatedTotals = {
        ContractSavings: 0,
        VolumeSavings: 0,
        TankeringSavings: 0,
        TotalSavings: 0,
        RetailTotal: 0,
        TotalSavingsPercent: 0,
        DiscrepancyAmount: 0
    };

    //PUBLIC METHODS
    vm.notify = vm.$fuelOrdersService.getNotifier($scope);
    vm.getTransactionStatus = _getTransactionStatus;
    vm.getInvoiceStatusClass = _getInvoiceStatusClass; // apply class to dropdown-button
    vm.closeDetails = _closeDetails;
    vm.onVolumeChange = _onVolumeChange;
    vm.processOrder = _processOrder;
    vm.saveFuelOrder = _saveFuelOrder;
    vm.addNote = _addNote;
    vm.saveNote = _saveNote;
    vm.deleteNote = _deleteNote;
    vm.editTransactionIconClicked = _editTransactionIconClicked;
    vm.onCustomFeeChanged = _onCustomFeeChanged;
    vm.onCustomTaxChanged = _onCustomTaxChanged;
    vm.storeTax = _storeTax;
    vm.storeFee = _storeFee;
    vm.removeFee = _removeFee;
    vm.removeTax = _removeTax;
    vm.calculateInvoicedPPG = _calculateInvoicedPPG;
    vm.calculateTotals = _calculateTotals;
    vm.getServicesTotal = _getServicesTotal;
    vm.calculateRetailTotal = _calculateRetailTotal;
    vm.viewInvoices = _viewInvoices;
    vm.changeCompany = _changeCompany;
    vm.onSaveFuelOrderSuccess = _onSaveFuelOrderSuccess;
    vm.onSaveNoteSuccess = _onSaveNoteSuccess;
    vm.onDeleteNoteSuccess = _onDeleteNoteSuccess;
    vm.saveMessage = _saveMessage;
    vm.onError = _onError;

    //PUBLIC MEMBERS
    vm.test = "This is a test";
    vm.aircrafts = null;
    vm.fuelOrder = null;
    vm.savedFuelOrder = null;
    vm.savedNote = null;
    vm.deletedNote = null;
    vm.dropzoneConfig = {
        uploadMultiple: false,
        previewsContainer: "#Dropzone-Preview-Placeholder",
        maxFileSize: 4,
        url: "/api/files/attachment/",
        acceptedFiles: '.pdf,.xlsx,.xls,.csv',
        addRemoveLinks: true,
        accept: function (file, done) {
            if (file.size > 4000000) {
                Notification.warning({
                    model: this,
                    scope: $scope,
                    //templateUrl: '/Partials/Common/Notifications/Login.html',
                    message: "File size exceeded.",
                    delay: 3000,
                    closeOnClick: false
                });
            }
            else if (file.type == 'application/pdf' || file.type == 'application/vnd.ms-excel' || file.type == '') {
                var container = $(file.previewElement).parent();
                $(file.previewElement).hide();
                done();
            } else {
                Notification.warning({
                    model: this,
                    scope: $scope,
                    //templateUrl: '/Partials/Common/Notifications/Login.html',
                    message: "That type of file is currently not supported.",
                    delay: 3000,
                    closeOnClick: false
                });
            };
        },
        success: function (data) {
            //var res = JSON.parse(data.xhr.responseText);
            //console.log(res.Item);
            $timeout(function () {
                vm.showingUploads = true;
            });
            Notification.success({
                model: this,
                scope: $scope,
                //templateUrl: '/Partials/Common/Notifications/Login.html',
                message: "File uploaded successfully! <br /> <br />",
                delay: 3000
            });
        },
        init: function () {
            var myDropzone = this;

            this.on("success", function (data) {
                //var res = JSON.parse(data.xhr.responseText);
                console.log("Getting fuel order invoices...");
                vm.$fuelOrderInvoicesService.getFuelOrderInvoices(vm.fuelOrder.Id, _onGetInvoicesSuccess, _onError);
            });

            this.on("processing", function (file) {
                //$timeout(function () {
                //    vm.showingProgress = true;
                //});
                this.options.url = "/api/files/attachment/" + vm.fuelOrder.Id;
            });
        }
    };

    render();

    //PRIVATE METHODS
    function render() {
        console.log("Getting transaction details...");
        vm.$fuelOrdersService.getFuelOrder($fuelOrdersService.FuelOrderID, _onGetInfoSuccess, vm.onError);
        vm.$fuelOrderMessagesService.getMessages($fuelOrdersService.FuelOrderID, _onGetMessagesSuccess, _onError);
    }

    function _closeDetails(fuelOrder) {
        if (vm.transactionForm.$dirty) {
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/UnsavedChanges.html',
                controller: 'unsavedModalController',
                controllerAs: 'modal',
                resolve: {
                    items: function () {
                        return {
                            fuelOrder: vm.fuelOrder,
                            process: _saveFuelOrder
                        }
                    }
                }
            });
        }
        else vm.$rootScope.ApplicationState.ActiveSection = 'FUEL ORDERS';
    }

    function _processOrder(fuelOrder) {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Discrepancy.html',
            controller: 'discrepancyModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        fuelOrder: vm.fuelOrder,
                        process: _saveFuelOrder
                    }
                }
            }
        });
    }

    function _onVolumeChange(volume) {
        console.log("Changing volume");
        $(vm.customerPrices).each(function () {
            if (_isMatchingFuelOrderPrice(this))
                vm.fuelOrder.QuotedPPG = this.Price;
        });

        $(vm.supplierPrices).each(function () {
            if (_isMatchingSupplierPrice(this))
                vm.fuelOrder.WholesaleQuoted = this.TotalWithTax;
        });
        _calculateTotals();
    }

    function _isMatchingFuelOrderPrice(fuelOrderPrice) {
        return (fuelOrderPrice.FBOName === vm.fuelOrder.FBO
            && fuelOrderPrice.Vendor === vm.fuelOrder.Vendor
            && fuelOrderPrice.SupplierID === vm.fuelOrder.SupplierID
            && fuelOrderPrice.PriceTierMin <= vm.fuelOrder.InvoicedVolume
            && fuelOrderPrice.PriceTierMax >= vm.fuelOrder.InvoicedVolume
            && fuelOrderPrice.Product === vm.fuelOrder.Product);
    }

    function _isMatchingSupplierPrice(supplierPrice) {
        return (supplierPrice.FBOName === vm.fuelOrder.FBO
            && supplierPrice.Vendor === vm.fuelOrder.Vendor
            && supplierPrice.SupplierID === vm.fuelOrder.SupplierID
            && supplierPrice.Min <= vm.fuelOrder.InvoicedVolume
            && supplierPrice.Max >= vm.fuelOrder.InvoicedVolume
            && supplierPrice.Product === vm.fuelOrder.Product);
    }

    function _changeCompany(custId) {
        console.log("Getting Tail Numbers...");
        vm.$aircraftsService.getAircraftsByAdminAndCustClientID($usersService.user.ClientID, custId, _onGetAircraftsSuccess, vm.onError);
    }

    function _saveFuelOrder(fuelOrder) {
        if (fuelOrder.Aircraft.TailNumber && fuelOrder.Aircraft.TailNumber !== '') fuelOrder.TailNumber = fuelOrder.Aircraft.TailNumber;
        vm.savedFuelOrder = fuelOrder;
        vm.savedFuelOrder.FuelingDateTime = moment(vm.savedFuelOrder.FuelingDateString).format('MM/DD/YYYY') + " " + vm.savedFuelOrder.FuelingTimeString;
        _getTransactionStatus(vm.savedFuelOrder.InvoiceStatus, false);
        if (vm.savedFuelOrder.Id == 0) {
            console.log("Saving transaction...");
            vm.savedFuelOrder.AdminClientID = $usersService.user.ClientID;
            vm.savedFuelOrder.DateRequested = new Date().toDateString();
            vm.$fuelOrdersService.addFuelOrder(vm.savedFuelOrder, vm.onSaveFuelOrderSuccess, vm.onError);
        } else {
            console.log("Updating transaction...");
            vm.savingFuelOrder = true;
            //vm.FuelOrderTaxes = vm.savedFuelOrder.FuelOrderTaxes;
            vm.$fuelOrdersService.updateFuelOrder(vm.savedFuelOrder.Id, vm.savedFuelOrder, vm.onSaveFuelOrderSuccess, vm.onError);
        }
    }

    function _addNote() {
        vm.fuelOrder.FuelOrderNotes.push({
            Id: "",
            ClientID: vm.fuelOrder.AdminClientID,
            FuelOrderID: $fuelOrdersService.FuelOrderID,
            Note: "",
            AddedByUserID: $usersService.user.Id,
        });
    }

    function _saveNote(note) {
        vm.savingNote = true;
        vm.savedNote = note;
        if (note.Id > 0) {
            note.UpdatedByUserID = $usersService.user.Id;
            vm.$fuelOrderNotesService.updateFuelOrderNote(note.Id, note, vm.onSaveNoteSuccess, vm.onError);
        } else {
            vm.$fuelOrderNotesService.addFuelOrderNote(note, vm.onSaveNoteSuccess, vm.onError);
        }
    }

    function _deleteNote(note) {
        vm.deletedNote = note;
        if (note.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = ("Are you sure you want to DELETE this note?");
                    $scope.confirm = function () {
                        $uibModalInstance.close(vm.$fuelOrderNotesService.deleteFuelOrderNote(note.Id, vm.onDeleteNoteSuccess, vm.onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }
                }
            });
        } else vm.fuelOrder.FuelOrderNotes.pop();

    }

    function _editTransactionIconClicked() {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/Transactions/EditDivertedTransaction.html'
        });
    }

    function _onCustomFeeChanged(indexOfFee) {
        var feeBeingEdited = vm.fuelOrder.FuelOrderFees[indexOfFee];
        if (feeBeingEdited.FeeDesc.length == 0) {
            //If we are at the end of the list, then return
            if (vm.fuelOrder.FuelOrderFees.length == (indexOfFee + 1))
                return;
            //If the next tax is empty, and this current tax is empty, then remove the next tax
            var nextFee = vm.fuelOrder.FuelOrderFees[indexOfFee + 1];
            if (nextFee.FeeDesc.length == 0)
                _removeFee(nextFee);
        } else {
            //If we are at the end of the list, add a new custom tax
            if (vm.fuelOrder.FuelOrderFees.length == (indexOfFee + 1))
                _addFee();
        }
    }

    function _onCustomTaxChanged(indexOfTax) {
        console.log("Changing custom tax...");
        var taxBeingEdited = vm.fuelOrder.FuelOrderTaxes[indexOfTax];
        if (taxBeingEdited.TaxDesc.length == 0) {
            //If we are at the end of the list, then return
            if (vm.fuelOrder.FuelOrderTaxes.length == (indexOfTax + 1))
                return;
            //If the next tax is empty, and this current tax is empty, then remove the next tax
            var nextTax = vm.fuelOrder.FuelOrderTaxes[indexOfTax + 1];
            if (nextTax.TaxDesc.length == 0)
                _removeTax(nextTax);
        } else {
            //If we are at the end of the list, add a new custom tax
            if (vm.fuelOrder.FuelOrderTaxes.length == (indexOfTax + 1))
                _addTax();
        }
    }

    function _removeFee(fee) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = ("Are you sure you want to DELETE this fee?");
                $scope.confirm = function () {
                    vm.deletedFee = fee;
                    vm.$fuelOrderFeesService.deleteFee(vm.deletedFee.Id, _onDeleteFuelOrderFeeSuccess, _onError);
                    $uibModalInstance.close();
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _addFee() {
        var fee = { FeeDesc: '', FeeAmount: 0, FeeNameAsKey: '' };
        vm.fuelOrder.FuelOrderFees.push(fee);
    }

    function _storeFee(fee) {
        console.log("Storing Client Fee...");
        if (fee.Id > 0) vm.$clientFeesService.updateFee(fee.Id, fee, _onError);
        else {
            vm.fee = fee;
            vm.fee.ClientID = vm.fuelOrder.CustClientID;
            vm.$clientFeesService.addFee(fee, _onClientFeeSavedSuccess, _onError);
        }
    }

    function _addTax() {
        var tax = { TaxDesc: '', TaxGal: 0, TaxAmt: 0, TaxPcnt: 0, IsCustomizable: true };
        vm.fuelOrder.FuelOrderTaxes.push(tax);
    }

    function _storeTax(tax) {
        console.log("Storing Client Tax...");
        if (tax.Id > 0) vm.$clientTaxesService.updateTax(tax.Id, tax, _onClientTaxSavedSuccess, _onError);
        else {
            vm.tax = tax;
            vm.tax.ClientID = vm.fuelOrder.CustClientID;
            vm.$clientTaxesService.addTax(vm.tax, _onClientTaxSavedSuccess, _onError);
        }
    }

    function _removeTax(tax) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = ("Are you sure you want to DELETE this tax?");
                $scope.confirm = function () {
                    vm.deletedTax = tax;
                    vm.$fuelOrderTaxesService.deleteTax(vm.deletedTax.Id, _onDeleteFuelOrderTaxSuccess, _onError);
                    $uibModalInstance.close();
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _calculateInvoicedPPG() {
        var taxes = vm.fuelOrder.FuelOrderTaxes.map(function (a) { return a.TaxGal });
        vm.fuelOrder.InvoicedPPG = vm.fuelOrder.BasePPG + taxes.reduce(function (a, b) { return a + b });
    }

    function _calculateTotals(tax) {
        if (tax) {
            tax.TaxAmt = vm.fuelOrder.InvoicedVolume * tax.TaxGal;
            _calculateInvoicedPPG();
        }
        else {
            angular.forEach(vm.fuelOrder.FuelOrderTaxes, function (t) {
                t.TaxAmt = vm.fuelOrder.InvoicedVolume * t.TaxGal;
            });
        }
    }

    function _calculateEverything() {
        //Call each calculate method, so everything gets calculated
        //CalculateFromTotalFuelCost();
        //CalculateTaxes();
        _calculateActualPPG();
        //CalculateContractSavings();
        //CalculateVolumeSavings();
        //CalculateTotalSavings();
        _calculateRetailTotal();
        //Calculate our percentages
        //$scope.CalculatedTotals.TotalSavingsPercent = GetCalculatedPercent($scope.CalculatedTotals.RetailTotal + $scope.CalculatedTotals.TotalSavings, $scope.CalculatedTotals.TotalSavings);
        //$scope.CalculatedTotals.VolumeSavingsPercent = GetCalculatedPercent($scope.CalculatedTotals.RetailTotal, $scope.CalculatedTotals.VolumeSavings);
        //$scope.CalculatedTotals.ContractSavingsPercent = GetCalculatedPercent(($scope.Transaction.postedRetail), ($scope.Transaction.postedRetail - $scope.Transaction.ActualPPG));
        //$scope.CalculatedTotals.TankeringSavingsPercent = GetCalculatedPercent($scope.CalculatedTotals.RetailTotal, $scope.CalculatedTotals.TankeringSavings);

    };

    //function GetCalculatedPercent(divideBy, dividend) {
    //    if (divideBy < .000001)
    //        return 0;
    //    return (dividend / divideBy) * 100;
    //}

    function _calculateActualPPG() {
        //Set the actual PPG equal to the base price initially
        vm.fuelOrder.InvoicedPPG = vm.fuelOrder.BasePPG;
        //Add all of the 'other taxes' to the actual PPG
        $(vm.fuelOrder.FuelOrderTaxes)
            .each(function () {
                vm.fuelOrder.InvoicedPPG += this.TaxGal;
            });
    }

    function _calculateContractSavings() {
        if (vm.fuelOrder.PostedRetail > 0 && vm.fuelOrder.InvoicedPPG > 0)
            vm.CalculatedTotals.ContractSavings = (vm.fuelOrder.PostedRetail - vm.fuelOrder.InvoicedPPG) *
                vm.fuelOrder.InvoicedVolume;
        else
            vm.CalculatedTotals.ContractSavings = 0;
    }

    function _calculateVolumeSavings() {
        if (vm.fuelOrder.InvoicedVolume > 0) {
            vm.CalculatedTotals.VolumeSavings = (vm.fuelOrder.QuotedPPG * vm.fuelOrder.InvoicedVolume) -
                (vm.fuelOrder.InvoicedPPG * vm.fuelOrder.InvoicedVolume);
        } else
            vm.CalculatedTotals.VolumeSavings = 0;
    }

    function _calculateTotalSavings() {
        vm.CalculatedTotals.TotalSavings = vm.CalculatedTotals.VolumeSavings + vm.CalculatedTotals.ContractSavings;
        if (vm.fuelOrder.InvoicedVolume >= vm.fuelOrder.RampFeeWaivedAt &&
            vm.fuelOrder.RampFeeWaivedAt > 0)
            vm.CalculatedTotals.TotalSavings += vm.fuelOrder.RampFee;
    }

    function _calculateRetailTotal() {
        vm.CalculatedTotals.RetailTotal = vm.fuelOrder.InvoicedVolume * vm.fuelOrder.InvoicedPPG;
        vm.CalculatedTotals.RetailTotal += _getServicesTotal();
        return vm.CalculatedTotals.RetailTotal;
    }

    function _getServicesTotal() {
        var servicesTotal = 0;
        $(vm.fuelOrder.FuelOrderFees)
            .each(function () {
                if (IsNumeric(this.FeeAmount))
                    servicesTotal += this.FeeAmount;
            });
        //servicesTotal += vm.fuelOrder.LandingFee | 0;
        //servicesTotal += vm.fuelOrder.LAVServiceFee | 0;
        //servicesTotal += vm.fuelOrder.CateringChargeFee | 0;
        //servicesTotal += vm.fuelOrder.GPUAPUFee | 0;
        //servicesTotal += vm.fuelOrder.PotableWaterFee | 0;
        //if (vm.fuelOrder.RampFeeWaivedAt < 1 || vm.fuelOrder.RampFeeWaivedAt > vm.fuelOrder.InvoicedVolume)
        //    servicesTotal += vm.fuelOrder.RampFee;
        return servicesTotal;
    }

    function _getInvoiceStatusClass(fuelOrder) {
        //if (transaction.InvoiceStatus == 0) return "pendingcolor";
        //if (transaction.InvoiceStatus == 1) return "reconciledcolor";
        //if (transaction.InvoiceStatus == 2) return "discrepancycolor";
        //if (transaction.InvoiceStatus == 3) return "canceledcolor";
        if (fuelOrder == 1) return "newcolor";
        if (fuelOrder == 2) return "modifycolor";
        if (fuelOrder == 3) return "postverifycolor";
        if (fuelOrder == 4) return "marginanalysiscolor";
        if (fuelOrder == 5) return "discrepancycolor";
        if (fuelOrder == 6) return "accountingcolor";
        if (fuelOrder == 7) return "pendingcolor";
        if (fuelOrder == 8) return "reconciledcolor";
        if (fuelOrder == 9) return "canceledcolor";
        if (fuelOrder == 10) return "nofuelcolor";
    }


    function _getTransactionStatus(status, dirty) {
        //if (vm.fuelOrder.InvoiceStatus == 0) return 'Pending';
        //if (vm.fuelOrder.InvoiceStatus == 1) return 'Reconciled';
        //if (vm.fuelOrder.InvoiceStatus == 2) return 'Discrepancy';
        //if (vm.fuelOrder.InvoiceStatus == 3) return 'Cancelled';
        //return 'Pending';
        switch (status) {
            case 1:
                vm.TransactionStatus = 'New';
                vm.fuelOrder.InvoiceStatus = 1;
                break;
            case 2:
                vm.TransactionStatus = 'Modify';
                vm.fuelOrder.InvoiceStatus = 2;
                break;
            case 3:
                vm.TransactionStatus = 'Post Verify';
                vm.fuelOrder.InvoiceStatus = 3;
                break;
            case 4:
                vm.TransactionStatus = 'Margin Analysis';
                vm.fuelOrder.InvoiceStatus = 4;
                break;
            case 5:
                vm.TransactionStatus = 'Discrepancy';
                vm.fuelOrder.InvoiceStatus = 5;
                break;
            case 6:
                vm.TransactionStatus = 'Accounting';
                vm.fuelOrder.InvoiceStatus = 6;
                break;
            case 7:
                vm.TransactionStatus = 'Pending';
                vm.fuelOrder.InvoiceStatus = 7;
                break;
            case 8:
                vm.TransactionStatus = 'Reconciled';
                vm.fuelOrder.InvoiceStatus = 8;
                break;
            case 9:
                vm.TransactionStatus = 'Cancelled';
                vm.fuelOrder.InvoiceStatus = 9;
                break;
            case 10:
                vm.TransactionStatus = 'No Fuel';
                vm.fuelOrder.InvoiceStatus = 10;
                break;
            default:
                vm.TransactionStatus = 'Pending';
                vm.fuelOrder.InvoiceStatus = 7;
        }
        if (dirty) vm.transactionForm.$setDirty();
    }

    function _viewInvoices(invoices) {
        var modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/UploadInvoices.html',
            controller: 'invoicesModalController',
            controllerAs: 'modal',
            resolve: {
                items: function () {
                    return {
                        fuelOrderID: vm.fuelOrder.Id,
                        //invoices: invoices,
                        invoicesService: vm.$fuelOrderInvoicesService
                    }
                }
            }
        });
    }

    function _saveMessage(message) {
        console.log("Saving message...");
        vm.savedMessage = {
            UserID: $usersService.user.Id,
            FuelOrderID: $fuelOrdersService.FuelOrderID,
            Name: $usersService.user.Registration.FirstName + " " + $usersService.user.Registration.LastName,
            Message: message,
            DateAdded: moment().format('MM/DD/YYYY')
        };
        vm.$fuelOrderMessagesService.addMessage(vm.savedMessage, _onSaveMessageSuccess, _onError);
        vm.Message = '';
    }

    //PRIVATE HANDLERS
    function _onGetInfoSuccess(data) {
        vm.notify(function () {
            vm.fuelOrder = data.Item;
            _getTransactionStatus(vm.fuelOrder.InvoiceStatus, false)
            //Add an empty fee and tax to the end of the arrays
            _addTax();
            _calculateTotals();
            _calculateInvoicedPPG();
            _addFee();
            console.log("FUEL ORDER: ", vm.fuelOrder);
            console.log("Getting fuel order invoices...");
            vm.$fuelOrderInvoicesService.getFuelOrderInvoices(vm.fuelOrder.Id, _onGetInvoicesSuccess, _onError);
            vm.$aircraftsService.getAircraftsByAdminAndCustClientID(vm.fuelOrder.AdminClientID, vm.fuelOrder.CustClientID, _onGetAircraftsSuccess, vm.onError);
        });
    }

    function _onGetInvoicesSuccess(data) {
        vm.notify(function () {
            vm.invoices = data.Items;
            console.log("FUEL ORDER INVOICES: ", vm.invoices);
            vm.$fuelOrderPricingsService.getFuelOrderCustomerPricingByFuelOrderId(vm.fuelOrder.Id, _onGetCustomerPricesSuccess, _onError);
            if ($usersService.user.Client.ClientType == 1)
                vm.$fuelOrderPricingsService.getFuelOrderSupplierPricingByFuelOrderId(vm.fuelOrder.Id, _onGetSupplierPricesSuccess, _onError);
            if (vm.fuelOrder.Id == 0)
                vm.$customerDetailsService.getCustomersByAdminClient($usersService.user.ClientID, _onGetCustomersSuccess, _onError);
        });
    }

    function _onGetMessagesSuccess(data) {
        vm.notify(function () {
            vm.messages = data.Items;
            console.log(vm.messages);
        });
    }

    function _onGetAircraftsSuccess(data) {
        vm.notify(function () {
            vm.aircrafts = data.Items;
            console.log("AIRCRAFTS: ", vm.aircrafts);
            angular.forEach(vm.aircrafts, function (aircraft) {
                if (vm.fuelOrder.AircraftID == aircraft.Id)
                    vm.fuelOrder.Aircraft = aircraft;
            });
        });
    }

    function _onGetCustomersSuccess(data) {
        vm.notify(function () {
            vm.customers = JSON.parse(data.Item);
            console.log("CUSTOMERS: ", vm.customers);
        });
    }

    function _onGetCustomerPricesSuccess(data) {
        vm.notify(function () {
            vm.customerPrices = data.Items;
            console.log("CUSTOMER PRICES: ", vm.customerPrices);
        });
    }

    function _onGetSupplierPricesSuccess(data) {
        vm.notify(function () {
            vm.supplierPrices = data.Items;
            console.log("SUPPLIER PRICES: ", vm.supplierPrices);
        });
    }

    function _onSaveFuelOrderSuccess(data) {
        vm.notify(function () {
            console.log("Transaction Updated!");
            Notification.success({
                model: this,
                scope: $scope,
                message: "Transaction Updated!",
                delay: 3000
            });
            vm.savingFuelOrder = false;
            if (data.Item) {
                vm.savedFuelOrder.Id = data.Item;
                vm.savedFuelOrder.DateAdded = new Date();
            }
            if (vm.savedFuelOrder.FuelOrderTaxes.length > 1 && vm.savedFuelOrder.FuelOrderTaxes[0].TaxDesc !== '') {
                angular.forEach(vm.savedFuelOrder.FuelOrderTaxes, function (tax) {
                    tax.FuelOrderID = vm.fuelOrder.Id;
                });
                var fuelOrderTaxes = angular.copy(vm.savedFuelOrder.FuelOrderTaxes);
                if (fuelOrderTaxes[fuelOrderTaxes.length - 1].TaxDesc == '' && fuelOrderTaxes[0].TaxDesc !== '') {
                    fuelOrderTaxes.pop();
                    vm.$fuelOrderTaxesService.addFuelOrderTaxes(fuelOrderTaxes, _onFuelOrderTaxSavedSuccess, _onError);
                }
            }
            if (vm.savedFuelOrder.FuelOrderFees.length > 1 && vm.savedFuelOrder.FuelOrderFees[0].FeeDesc !== '') {
                angular.forEach(vm.savedFuelOrder.FuelOrderFees, function (fee) {
                    fee.FuelOrderID = vm.fuelOrder.Id;
                });
                vm.fuelOrderFees = angular.copy(vm.savedFuelOrder.FuelOrderFees);
                if (vm.fuelOrderFees[vm.fuelOrderFees.length - 1].FeeDesc == '' && vm.fuelOrderFees[0].FeeDesc !== '') {
                    vm.fuelOrderFees.pop();
                    vm.$fuelOrderFeesService.addFuelOrderFees(vm.fuelOrderFees, _onFuelOrderFeeSavedSuccess, _onError);
                }
            }
            $fuelOrdersService.fuelOrders = null;
            vm.transactionForm.$setPristine();
            $scope.$emit('updateTotal', $usersService.dashboard);
        });
    }

    function _onWholesaleSuccess(data) {
        vm.notify(function () {
            console.log("Wholesale invoice changed!");
            vm.fuelOrder.WholesaleInvoiced = data.Item.TotalWithTax;
        });
    }

    function _onCustomerSuccess(data) {
        vm.notify(function () {
            console.log("Customer invoice changed!");
            vm.fuelOrder.InvoicedPPG = data.Item.TotalWithTax;
        });
    }

    function _onClientTaxSavedSuccess(data) {
        vm.notify(function () {
            if (data.Item) vm.tax.Id = data.Item.Id;
            Notification.success({
                model: this,
                scope: $scope,
                message: "Client Tax saved!",
                delay: 3000
            });
        });
        console.log("Client Tax saved!");
    }

    function _onFuelOrderTaxSavedSuccess(data) {
        vm.notify(function () {
            Notification.success({
                model: this,
                scope: $scope,
                message: "Fuel Order Taxes saved!",
                delay: 3000
            });
        });
        console.log("Fuel Order Taxes saved!");
    }

    function _onClientFeeSavedSuccess(data) {
        vm.notify(function () {
            if (data.Item) vm.fee.Id = data.Item.Id;
            Notification.success({
                model: this,
                scope: $scope,
                message: "Client Fee saved!",
                delay: 3000
            });
        });
        console.log("Client Fee saved!");
    }

    function _onFuelOrderFeeSavedSuccess(data) {
        vm.notify(function () {
            vm.fuelOrder.FuelOrderFees.length = 0;
            vm.fuelOrder.FuelOrderFees = data.Items;
            _addFee();
            console.log("Fuel Order Fees saved!");
            Notification.success({
                model: this,
                scope: $scope,
                message: "Fuel Order Fees saved!",
                delay: 3000
            });
        });
    }

    function _onSaveNoteSuccess(data) {
        vm.notify(function () {
            vm.savingNote = false;
            if (data.Item) {
                vm.savedNote.Id = data.Item;
            }
        });
    }

    function _onSaveMessageSuccess(data) {
        vm.notify(function () {
            if (data.Item) {
                console.log("Message saved!", data.Item);
                vm.messages.push(data.Item);
                vm.transactionForm.$setPristine();
            }
        });
    }

    function _onDeleteNoteSuccess() {
        vm.notify(function () {
            var index = vm.fuelOrder.FuelOrderNotes.indexOf(vm.deletedNote);
            if (index > -1) vm.fuelOrder.FuelOrderNotes.splice(index, 1);
        });
    }

    function _onDeleteFuelOrderTaxSuccess() {
        vm.notify(function () {
            var index = vm.fuelOrder.FuelOrderTaxes.indexOf(vm.deletedTax);
            vm.fuelOrder.FuelOrderTaxes.splice(index, 1);
        });
    }

    function _onDeleteFuelOrderFeeSuccess() {
        vm.notify(function () {
            var indexOfFee = vm.fuelOrder.FuelOrderFees.indexOf(vm.deletedFee);
            vm.fuelOrder.FuelOrderFees.splice(indexOfFee, 1);
        });
    }

    function _onError() {
        vm.notify(function () {
            vm.savingFuelOrder = false;
            vm.savingUser = false;
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }
}

degatech.ng.addController(degatech.ng.app.module,
    "fuelOrderController",
    ['$scope',
    '$rootScope',
    '$timeout',
    '$baseController',
    "$fuelOrdersService",
    "$fuelOrderNotesService",
    "$aircraftsService",
    "$usersService",
    "$customerDetailsService",
    "$uibModal",
    "$fuelOrderTaxesService",
    "$fuelOrderFeesService",
    "$fuelOrderInvoicesService",
    "$fuelOrderPricingsService",
    "$fuelOrderMessagesService",
    "$clientTaxesService",
    "$clientFeesService",
    "Notification"],
    degatech.page.fuelOrderControllerFactory);

//////////////////////////UNSAVED CHANGES MODAL//////////////////////////////

degatech.page.unsavedModalFactory = function ($scope, $rootScope, $uibModalInstance, items, $uibModal, Notification) {

    var vm = this;

    vm.$scope = $scope;
    vm.$rootScope = $rootScope;
    vm.$uibModalInstance = $uibModalInstance;
    vm.items = items;

    //vm.close = _close;

    _init();

    function _init() {
        console.log(vm.items);
    }

    //function _close() {
    //    vm.items.process(vm.items.fuelOrder)
    //}

    function _onError() {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }
}

degatech.ng.addController(degatech.ng.app.module
            , "unsavedModalController"
            , ['$scope',
                '$rootScope',
                '$uibModalInstance',
                'items',
                '$uibModal',
                'Notification'
            ]
            , degatech.page.unsavedModalFactory);

//////////////////////////DISCREPANCY MODAL//////////////////////////////

degatech.page.discrepancyModalFactory = function ($scope, $rootScope, $uibModalInstance, items, $uibModal, Notification) {

    var vm = this;

    vm.$scope = $scope;
    vm.$rootScope = $rootScope;
    vm.$uibModalInstance = $uibModalInstance;
    vm.items = items;

    vm.close = _close;
    vm.accept = _accept;
    vm.decline = _decline;

    _init();

    function _init() {
        console.log(vm.items);
    }

    function _close() {
        $uibModalInstance.close();
    }

    function _accept() {
        vm.items.fuelOrder.InvoiceStatus = 8;
        $uibModalInstance.close(vm.items.process(vm.items.fuelOrder));
    }

    function _decline() {
        vm.items.fuelOrder.InvoiceStatus = 5;
        $uibModalInstance.close(vm.items.process(vm.items.fuelOrder));
    }

    function _onError() {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }
}

degatech.ng.addController(degatech.ng.app.module
            , "discrepancyModalController"
            , ['$scope',
                '$rootScope',
                '$uibModalInstance',
                'items',
                '$uibModal',
                'Notification'
            ]
            , degatech.page.discrepancyModalFactory);

//////////////////////////DOWNLOAD MODAL//////////////////////////////

degatech.page.invoicesModalFactory = function ($scope, $rootScope, $uibModalInstance, items, $uibModal, Notification) {

    var vm = this;

    vm.$scope = $scope;
    vm.$rootScope = $rootScope;
    vm.$uibModalInstance = $uibModalInstance;
    vm.items = items;

    vm.notify = items.invoicesService.getNotifier($scope);

    vm.close = _close;
    vm.downloadInvoice = _downloadInvoice;
    vm.deleteInvoice = _deleteInvoice;

    _init();

    function _init() {
        console.log(vm.items);
        vm.items.invoicesService.getFuelOrderInvoices(vm.items.fuelOrderID, _onGetInvoicesSuccess, _onError)
    }

    function _close() {
        $uibModalInstance.close();
    }

    function _downloadInvoice(invoice) {
        location.href = "/FileController/DownloadPDF?invoiceId=" + invoice.Id;
        $uibModalInstance.close();
    }

    function _deleteInvoice(invoice) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE '" + invoice.InvoiceName + "'?";
                $scope.confirm = function () {
                    vm.deletedInvoice = invoice;
                    $uibModalInstance.close(vm.items.invoicesService.deleteInvoice(invoice.Id, _onDeleteInvoiceSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _onGetInvoicesSuccess(data) {
        vm.notify(function () {
            vm.invoices = data.Items;
            console.log("FUEL ORDER INVOICES: ", vm.invoices);
        });
    }

    function _onDownloadSuccess(data) {
        vm.notify(function () {
            $uibModalInstance.close();
        });
    }

    function _onDeleteInvoiceSuccess(data) {
        vm.notify(function () {
            $uibModalInstance.close(function () {
                var index = vm.invoices.indexOf(vm.deletedInvoice);
                if (index > -1) vm.invoices.splice(index, 1);
            });
            Notification.warning({
                model: this,
                scope: $scope,
                //templateUrl: '/Partials/Common/Notifications/Login.html',
                message: "'" + vm.deletedInvoice.InvoiceName + "' has been deleted!",
                delay: 3000,
                closeOnClick: false
            });
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
            , "invoicesModalController"
            , ['$scope',
                '$rootScope',
                '$uibModalInstance',
                'items',
                '$uibModal',
                'Notification'
            ]
            , degatech.page.invoicesModalFactory);