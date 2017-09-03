degatech.page.userManagerControllerFactory = function ($scope,
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
    vm.addNewUser = _addNewUser;
    vm.getIsActiveClass = _getIsActiveClass;

    //PUBLIC HANDLERS//////////////////////////////////////////////    

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.test = "This is a test";
    vm.modalInstance = null;
    vm.adminUsers = [];

    //PRIVATE METHODS//////////////////////////////////////////////
    function _render() {
        storage.SetSessionItem("LastActiveSection", "USER MANAGER");
        vm.$usersService.getUsersByClient($usersService.user.ClientID, _onGetUsersSuccess, _onError);
    }

    function _addNewUser(user) {
        if (user) $permissionsService.user = user;
        else $permissionsService.user = null;
        vm.$rootScope.ApplicationState.ActiveSection = 'USER CONFIG';        
    }

    function _getIsActiveClass(user) {
        if (user.IsActive == false) return "Inactive";
        if (user.IsActive == true) return "Active";
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetUsersSuccess(data) {
        vm.notify(function () {
            vm.adminUsers = data.Items;
            console.log("ADMIN USERS: ", vm.adminUsers);
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
    "userManagerController",
    ['$scope',
    '$rootScope',
    '$uibModal',
    '$baseController',
    '$usersService',
    '$permissionsService',
    '$registrationService',
    'Notification'],
    degatech.page.userManagerControllerFactory);