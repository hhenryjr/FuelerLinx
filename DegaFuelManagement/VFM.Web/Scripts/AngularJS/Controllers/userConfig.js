degatech.page.userConfigControllerFactory = function ($scope,
    $rootScope,
    $uibModal,
    $baseController,
    $usersService,
    $permissionsService,
    $registrationService,
    Notification) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$permissionsService = $permissionsService;
    vm.$registrationService = $registrationService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.notify = vm.$usersService.getNotifier($scope);
    vm.cancel = _cancel;
    vm.save = _save;
    vm.checkUsername = _checkUsername;
    vm.deleteUser = _deleteUser;

    //PUBLIC HANDLERS//////////////////////////////////////////////    

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.test = "This is a test";
    vm.modalInstance = null;
    vm.user = {
        IsActive: true,
        Registration: {},
        Permissions: {
            IsMainAdmin: false,
            Dashboard: true,
            CompanyGrid: true,
            CompanyDetail: true,
            ContactGrid: true,
            ContactDetail: true,
            AirportGrid: true,
            AirportDetail: true,
            VendorGrid: true,
            VendorDetail: true,
            MarginMgr: true,
            DropZone: true,
            Transactions: true,
            TaskScheduler: true,
            Analysis: true,
            IsMarginEnabled: true,
            IsDistributionEnabled: true
        }
    };

    //PRIVATE METHODS//////////////////////////////////////////////
    function _render() {
        if ($permissionsService.user) {
            vm.user = $permissionsService.user;
            vm.$permissionsService.getByUser(vm.user, _onGetPermissionsSuccess, _onError);
        }
    }

    function _cancel() {
        vm.$rootScope.ApplicationState.ActiveSection = 'USER MANAGER';
    }

    function _save() {
        if (!vm.user.RegistrationID || vm.user.RegistrationID == 0) {
            console.log("Saving registration...");
            vm.user.Registration.Company = $usersService.user.Registration.Company;
            vm.$registrationService.addRegistration(vm.user.Registration, _onSaveRegSuccess, _onError);
        } else {
            console.log("Updating registration...");
            vm.$registrationService.updateRegistration(vm.user.RegistrationID, vm.user.Registration, _onSaveRegSuccess, _onError);
        }
    }

    function _checkUsername() {
        if (!vm.user.Registration.Password || vm.user.Registration.Password == '') {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Error.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.title = "Password is blank!";
                    $scope.message = "Please enter a password.";
                    $scope.ok = function () {
                        $uibModalInstance.close();
                    }
                }
            });
        }
        else {
            vm.user.Registration.Id = vm.user.RegistrationID;
            vm.$registrationService.checkUsername(vm.user.Registration, _onCheckUsernameSuccess, _onError);
        }
    }

    function _deleteUser() {
        if (vm.user.Permissions.UserID) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = "Are you sure you want to DELETE this user's account?";
                    $scope.confirm = function () {
                        vm.$permissionsService.deletePermission(vm.user.Permissions, _onDeletePermissionSuccess, _onError);
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss();
                    }

                    function _onDeletePermissionSuccess() {
                        vm.$usersService.deleteUserByRegID(vm.user.RegistrationID, _onDeleteUserSuccess, _onError);
                    }

                    function _onDeleteUserSuccess() {
                        vm.notify(function () {
                            Notification.success({
                                model: this,
                                scope: $scope,
                                message: "User account deleted! <br /> <br />",
                                delay: 3000,
                                closeOnClick: false
                            });
                            $uibModalInstance.close();
                            vm.$rootScope.ApplicationState.ActiveSection = 'USER MANAGER';
                        });
                    }
                }
            });
        }
    }

    function _savePermission() {
        vm.user.Permissions.Dashboard = vm.user.Permissions.Dashboard ? 0 : 1;
        vm.user.Permissions.CompanyGrid = vm.user.Permissions.CompanyGrid ? 0 : 1;
        vm.user.Permissions.CompanyDetail = vm.user.Permissions.CompanyDetail ? 0 : 1;
        vm.user.Permissions.ContactGrid = vm.user.Permissions.ContactGrid ? 0 : 1;
        vm.user.Permissions.ContactDetail = vm.user.Permissions.ContactDetail ? 0 : 1;
        vm.user.Permissions.AirportGrid = vm.user.Permissions.AirportGrid ? 0 : 1;
        vm.user.Permissions.AirportDetail = vm.user.Permissions.AirportDetail ? 0 : 1;
        vm.user.Permissions.VendorGrid = vm.user.Permissions.VendorGrid ? 0 : 1;
        vm.user.Permissions.VendorDetail = vm.user.Permissions.VendorDetail ? 0 : 1;
        vm.user.Permissions.MarginMgr = vm.user.Permissions.MarginMgr ? 0 : 1;
        vm.user.Permissions.DropZone = vm.user.Permissions.DropZone ? 0 : 1;
        vm.user.Permissions.Transactions = vm.user.Permissions.Transactions ? 0 : 1;
        vm.user.Permissions.TaskScheduler = vm.user.Permissions.TaskScheduler ? 0 : 1;
        vm.user.Permissions.Analysis = vm.user.Permissions.Analysis ? 0 : 1;
        if (vm.user.Permissions.Id > 0) vm.$permissionsService.updatePermission(vm.user.Permissions, _onSavePermissionSuccess, _onError);
        else vm.$permissionsService.addPermission(vm.user.Permissions, _onSavePermissionSuccess, _onError);
    }

    function _convertPermissions(permission) {
        vm.user.Permissions.Dashboard = permission.Dashboard == 0 ? true : false;
        vm.user.Permissions.CompanyGrid = permission.CompanyGrid == 0 ? true : false;
        vm.user.Permissions.CompanyDetail = permission.CompanyDetail == 0 ? true : false;
        vm.user.Permissions.ContactGrid = permission.ContactGrid == 0 ? true : false;
        vm.user.Permissions.ContactDetail = permission.ContactDetail == 0 ? true : false;
        vm.user.Permissions.AirportGrid = permission.AirportGrid == 0 ? true : false;
        vm.user.Permissions.AirportDetail = permission.AirportDetail == 0 ? true : false;
        vm.user.Permissions.VendorGrid = permission.VendorGrid == 0 ? true : false;
        vm.user.Permissions.VendorDetail = permission.VendorDetail == 0 ? true : false;
        vm.user.Permissions.MarginMgr = permission.MarginMgr == 0 ? true : false;
        vm.user.Permissions.DropZone = permission.DropZone == 0 ? true : false;
        vm.user.Permissions.Transactions = permission.Transactions == 0 ? true : false;
        vm.user.Permissions.TaskScheduler = permission.TaskScheduler == 0 ? true : false;
        vm.user.Permissions.Analysis = permission.Analysis == 0 ? true : false;
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetPermissionsSuccess(data) {
        vm.notify(function () {
            vm.user.Permissions = data.Item;
            _convertPermissions(data.Item);
            console.log("USER PERMISSIONS: ", vm.user.Permissions);
        });
    }

    function _onCheckUsernameSuccess(data) {
        vm.notify(function () {
            if (!data.IsDuplicateUsername) _save();
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

    function _onSaveRegSuccess(data) {
        vm.notify(function () {
            vm.user.ClientID = $usersService.user.ClientID;
            if (data.Item) {
                vm.user.Registration.Id = data.Item;
                vm.user.RegistrationID = vm.user.Registration.Id;
                console.log("Saving user...");
                vm.$usersService.addUser(vm.user, _onSaveUserSuccess, _onError);
            }
            else vm.$usersService.updateUser(vm.user.Id, vm.user, _onSaveUserSuccess, _onError);
        });
    }

    function _onSaveUserSuccess(data) {
        console.log("Saving permission...");
        vm.notify(function () {
            Notification.success({
                model: this,
                scope: $scope,
                message: "User account saved!",
                delay: 4000
            });
            if (data.Item) {
                vm.user.Id = data.Item;
                vm.user.Permissions.UserID = vm.user.Id;
            }
            _savePermission();
        });
    }

    function _onSavePermissionSuccess(data) {
        vm.notify(function () {
            console.log("Admin user saved!");
            vm.$rootScope.ApplicationState.ActiveSection = 'USER MANAGER';
        });
    }

    function _onError() {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }

    //CONSTRUCTOR////////////////////////////////////////////
    _render();
}

degatech.ng.addController(degatech.ng.app.module,
    "userConfigController",
    ['$scope',
    '$rootScope',
    '$uibModal',
    '$baseController',
    '$usersService',
    '$permissionsService',
    '$registrationService',
    'Notification'],
    degatech.page.userConfigControllerFactory);