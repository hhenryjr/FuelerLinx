degatech.page.emailSectionControllerFactory = function ($scope,
    $baseController,
    $uibModal,
    $usersService,
    $emailRoutingService) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$emailRoutingService = $emailRoutingService;

    vm.$scope = $scope;
    vm.notify = vm.$usersService.getNotifier($scope);

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.addEmail = _addEmail;
    vm.saveEmail = _saveEmail;
    vm.deleteEmail = _deleteEmail;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.savedEmail = null;
    vm.deletedEmail = null;
    vm.emailTypes = ["Registration", "New Client Activated", "New Client Deactivated", "Fuel Order", "Price Distribution"];

    _init();

    //PRIVATE METHODS//////////////////////////////////////////////
    function _init() {
        console.log("Getting Email Addresses...");
        vm.$emailRoutingService.getEmailsByAdminClient($usersService.user.ClientID, _onGetEmailsSuccess, _onError);
        //vm.$emailRoutingService.getListOfEmails(_onGetEmailsSuccess, _onError);
    }

    function _addEmail() {
        vm.emailRouting.push({
            AdminClientID: $usersService.user.ClientID
        });
    }

    function _saveEmail(email) {
        vm.savingEmail = true;
        vm.savedEmail = email;
        if (email.Id > 0) {
            vm.$emailRoutingService.updateEmail(vm.savedEmail.Id, vm.savedEmail, _onSaveEmailSuccess, _onError);
        } else {
            vm.$emailRoutingService.addEmail(vm.savedEmail, _onSaveEmailSuccess, _onError);
        }
    }

    function _deleteEmail(email) {
        vm.deletedEmail = email;
        if (email.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = ("Are you sure you want to DELETE this email?");
                    $scope.confirm = function () {
                        $uibModalInstance.close(vm.$emailRoutingService.deleteEmail(email.Id, _onDeleteEmailSuccess, _onError));
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss()
                    }
                }
            });
        } else vm.emailRouting.pop();

    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetEmailsSuccess(data) {
        vm.notify(function () {
            vm.emailRouting = data.Items;
            vm.section = 'EMAILS';
            console.log("EMAILS: ", vm.emailRouting);
        });
    }

    function _onSaveEmailSuccess(data) {
        vm.notify(function () {
            vm.savingEmail = false;
            if (data.Item) {
                vm.savedEmail.Id = data.Item;
                vm.savedEmail.DateAdded = new Date();
            }
        });
    }

    function _onDeleteEmailSuccess() {
        vm.notify(function () {
            var index = vm.emailRouting.indexOf(vm.deletedEmail);
            if (index > -1) vm.emailRouting.splice(index, 1);
        });
    }

    function _onError() {
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }
}
degatech.ng.addController(degatech.ng.app.module,
    "emailSectionController",
    [
        '$scope',
        '$baseController',
        '$uibModal',
        "$usersService",
        "$emailRoutingService"
    ],
    degatech.page.emailSectionControllerFactory);