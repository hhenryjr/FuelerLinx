degatech.page.toDoListControllerFactory = function ($scope,
    $rootScope,
    $uibModal,
    $baseController,
    $usersService,
    $calendarEventService) {


    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$calendarEventService = $calendarEventService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.notify = vm.$usersService.getNotifier($scope);
    vm.updateEvent = _updateEvent;
    vm.deleteEvent = _deleteEvent;

    //PUBLIC HANDLERS//////////////////////////////////////////////    

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.test = "This is a test";

    //PRIVATE METHODS//////////////////////////////////////////////
    function _render() {
        vm.$calendarEventService.getCalendarDates($usersService.user.ClientID, $usersService.user.Id, _onGetEventsSuccess, _onError);
    }

    function _updateEvent(event) {
        console.log("Updating event...");
        vm.$calendarEventService.updateCalendar(event, _onSaveEventSuccess, _onError);
    }

    function _deleteEvent(event) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE this event?";
                $scope.confirm = function () {
                    console.log("Deleting event...");
                    vm.deletedCalEvent = event;
                    $uibModalInstance.close(vm.$calendarEventService.deleteCalendar(event.OID, _onDeleteCalSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetEventsSuccess(data) {
        vm.notify(function () {
            vm.calendarEvents = data.Items;
            console.log("EVENTS: ", vm.calendarEvents);
        });
    }

    function _onSaveEventSuccess(data) {
        vm.notify(function () {
            console.log("Event saved!");
        });
    }

    function _onDeleteCalSuccess() {
        vm.notify(function () {
            var index = vm.calendarEvents.indexOf(vm.deletedCalEvent);
            if (index > -1) vm.calendarEvents.splice(index, 1);
            $scope.$emit('updateTasks', $usersService);
        });
    }

    function _onError() {
        vm.notify(function () {
            vm.error = "An error has occurred!";
            console.log(vm.error);
        });
    }

    //CONSTRUCTOR////////////////////////////////////////////
    _render();
}

degatech.ng.addController(degatech.ng.app.module,
    "toDoListController",
    ['$scope',
    '$rootScope',
    '$uibModal',
    '$baseController',
    '$usersService',
    '$calendarEventService'],
    degatech.page.toDoListControllerFactory);