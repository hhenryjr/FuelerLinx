degatech.page.taskSchedulerControllerFactory = function ($scope,
    $rootScope,
    $uibModal,
    //$uibModalInstance,
    $baseController,
    $usersService,
    $calendarEventService,
    uiCalendarConfig) {

    //TODO: Add the uib date picker to the UI
    //TODO: Add the ability to select a time (right now you can only select a date)
    //TODO: Add ability to add a new draggable event

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$calendarEventService = $calendarEventService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;
    vm.dataSources = {
        CalendarEvents: [],
        DraggableEvents: []
    };
    vm.eventSources = [vm.dataSources.CalendarEvents];
    vm.calendarConfig = {
        calendar: {
            editable: true,
            header: {
                left: 'month basicWeek basicDay agendaWeek agendaDay',
                center: 'title',
                right: 'today prev,next'
            },
            droppable: true,
            eventClick: _viewEvent,
            eventReceive: _onNewEventDropped,
            //viewRender: vm.onCalendarViewRendered
            //eventResize: $scope.alertOnResize,
            eventDrop: _onEventDropped
        }
    };

    vm.dateFilter = {
        StartDate: moment().subtract(0, 'months').format('MM/DD/YYYY'),
        EndDate: moment().add(0, 'months').format('MM/DD/YYYY'),
        IsStartDateOpened: false,
        IsEndDateOpened: false,
        Format: "MM/dd/yyyy"
    };

    vm.dateConfigs = {
        StartDateOptionsConfig: {
            startingDay: 1,
            minDate: 0
        },
        EndDateOptionsConfig: {
            minDate: new Date(vm.dateFilter.StartDate),
            startingDay: 1
        }
    };

    vm.draggleTaskConfig = {
        'start': function (event, ui, model) {
            _onEventDragStart(event, ui, model);
        },
        'revert': true,
        'revertDuration': 0,
        'zIndex': 100000
    };
    vm.modalInstance = null;

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.notify = vm.$usersService.getNotifier($scope);

    //PUBLIC HANDLERS//////////////////////////////////////////////    
    vm.calendarConfig.calendar.viewRender = _onCalendarViewRendered;
    vm.addEvent = _addEvent;
    vm.deleteEvent = _deleteEvent;
    vm.deleteDraggableEvent = _deleteDraggableEvent;

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.test = "This is a test";


    //PRIVATE METHODS//////////////////////////////////////////////
    function _onCalendarViewRendered(element) {
        storage.SetSessionItem("LastActiveSection", "SCHEDULER");
        _showLoading();
        vm.$calendarEventService.getCalendarDates($usersService.user.ClientID, $usersService.user.Id, _onGetSuccess, _onError);
    }

    function _addEvent(event) {        
        console.log("Saving event...");
        _closeModal();
        _showLoading();
        vm.dataSources.CurrentEvent.end = moment(vm.dataSources.CurrentEvent.end).add(1, 'days').add(-1, 'minutes').format();
        vm.$calendarEventService.updateCalendar(vm.dataSources.CurrentEvent, _onSaveCalendarSuccess, _onError);
    }

    function _onEventDragStart(event, ui, model) {
        //This may not be needed
        //BaseObject.prototype.SetProperties.call($scope.PageSettings.CurrentEvent, model);
    }

    function _onNewEventDropped(event) {
        if (event._id) uiCalendarConfig.calendars.EventCalendar.fullCalendar('removeEvents', event._id);
        _onEventDropped(event);
    }

    function _onEventDropped(event) {
        console.log("Updating event...");
        _populateCurrentEventFromCalendarEvent(event);
        _showLoading();
        vm.$calendarEventService.updateCalendar(vm.dataSources.CurrentEvent, _onSaveCalendarSuccess, _onError);
    }

    function _viewEvent(event) {
        _populateCurrentEventFromCalendarEvent(event);
        vm.modalInstance = $uibModal.open({
            animation: false,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/TaskEditor.html',
            scope: $scope
        });
        vm.modalInstance.result.then(function () {
            //Button closed the modal
        }, function () {
            //User clicked outside of the modal window.  Reset our event
            _initializeCurrentEvent();
        });
    }

    function _populateCurrentEventFromCalendarEvent(event) {
        vm.dataSources.CurrentEvent = {
            className: event.color || '',
            ClientID: $usersService.user.ClientID,
            IsComplete: event.IsComplete,
            OID: event.OID,
            UserID: $usersService.user.Id,
            allDay: event.allDay,
            color: event.color,
            note: event.note || '',
            start: event.start.format('MM/DD/YYYY'),
            title: event.title || '',
            url: event.url || '',
            _id: event._id
        }
        if (event.end != null)
            vm.dataSources.CurrentEvent.end = event.end.format('MM/DD/YYYY');
        else
            vm.dataSources.CurrentEvent.end = vm.dataSources.CurrentEvent.start;
        if (event.className != null && event.className.length > 0)
            vm.dataSources.CurrentEvent.className = ReplaceAll(',', ' ', event.className.toString());
        else
            vm.dataSources.CurrentEvent.className = event.color;
    }

    function _deleteEvent(event) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE this event?";
                $scope.confirm = function () {
                    _closeModal();
                    _showLoading();
                    vm.deletedCalEvent = event;
                    uiCalendarConfig.calendars.EventCalendar.fullCalendar('removeEvents', event._id);
                    $uibModalInstance.close(vm.$calendarEventService.deleteCalendar(event.OID, _onDeleteCalSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _deleteDraggableEvent(event) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE this draggable event?";
                $scope.confirm = function () {
                    vm.deletedDragEvent = event;
                    $uibModalInstance.close(vm.$calendarEventService.deleteDraggable(event.OID, _onDeleteDragSuccess, _onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _showLoading() {
        //Don't show if it's already actively showing
        if (vm.modalInstance != null)
            return;
        vm.modalInstance = $uibModal.open({
            animation: true,
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            template: '<div><div class="text-center font-600">Loading</div><div class="text-center"><i class="fa fa-spinner fa-pulse fa-2x fa-fw txt-color-blue"></i></div></div>',
            scope: $scope
        });
    }

    function _closeModal() {
        if (vm.modalInstance == null)
            return;
        try {
            vm.modalInstance.close();
            vm.modalInstance = null;
        } catch (e) {
        }
    }

    function _initializeCurrentEvent() {
        vm.dataSources.CurrentEvent = {
            className: "bg-color-blue txt-color-white",
            ClientID: $usersService.user.ClientID,
            IsComplete: false,
            OID: 0,
            UserID: $usersService.user.Id,
            allDay: false,
            end: moment().format('MM/DD/YYYY'),
            note: '',
            start: moment().format('MM/DD/YYYY'),
            title: "",
            url: ""
        };
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetSuccess(data) {
        vm.notify(function () {
            vm.dataSources.CalendarEvents.length = 0;
            Array.prototype.push.apply(vm.dataSources.CalendarEvents, data.Items);
            //vm.dataSources.CalendarEvents.push(data.Items);
            //vm.dataSources.CalendarEvents.push({ title: 'Test Event', start: '10/31/2016', end: '11/2/2016'});
            //vm.dataSources.CalendarEvents.push({ title: 'Test Event2', start: '10/29/2016' });
            console.log(vm.dataSources.CalendarEvents);
            vm.$calendarEventService.getDraggableEvents($usersService.user.ClientID, $usersService.user.Id, _onGetDraggableSuccess, _onError);
        });
    }

    function _onGetDraggableSuccess(data) {
        vm.notify(function () {
            //TODO: Load draggable events.  Below is just a placeholder to test with
            vm.dataSources.DraggableEvents.length = 0;
            Array.prototype.push.apply(vm.dataSources.DraggableEvents, data.Items);
            console.log(vm.dataSources.DraggableEvents);
            //vm.dataSources.DraggableEvents.push({
            //    className: "bg-color-blue txt-color-white",
            //    ClientID: $usersService.user.ClientID,
            //    IsComplete: false,
            //    OID: 0,
            //    UserID: $usersService.user.Id,
            //    allDay: false,
            //    note: '',
            //    title: "Lunch Meeting",
            //    url: ""
            //});
            _closeModal();
        });
    }

    function _onSaveCalendarSuccess(data) {
        vm.notify(function () {
            console.log(data.Item);
            if (vm.dataSources.CurrentEvent.IsDraggable)
                vm.$calendarEventService.updateDraggable(vm.dataSources.CurrentEvent, _onSaveDraggableSuccess, _onError);
            else _closeAndRender();
        });
    }

    function _onSaveDraggableSuccess(data) {
        vm.notify(function () {
            console.log(data.Item);
            _closeAndRender();
        });
    }

    function _closeAndRender() {
        _initializeCurrentEvent();
        _onCalendarViewRendered();
        _closeModal();
        $scope.$emit('updateTasks', $usersService);
    }

    function _onDeleteCalSuccess(data) {
        vm.notify(function () {
            var index = vm.dataSources.CalendarEvents.indexOf(vm.deletedCalEvent);
            if (index > -1) vm.dataSources.CalendarEvents.splice(index, 1);
            _closeAndRender();
        });
    }

    function _onDeleteDragSuccess(data) {
        vm.notify(function () {
            var index = vm.dataSources.DraggableEvents.indexOf(vm.deletedDragEvent);
            if (index > -1) vm.dataSources.DraggableEvents.splice(index, 1);
            _closeModal();
        });
    }

    function _onError(error) {
        vm.notify(function () {
        });
        vm.error = "An error has occurred!";
        console.log(vm.error);
    }

    _initializeCurrentEvent();
}

degatech.ng.addController(degatech.ng.app.module,
    "taskSchedulerController",
    ['$scope',
    "$rootScope",
    '$uibModal',
    //'$uibModalInstance',
    '$baseController',
    "$usersService",
    '$calendarEventService',
    'uiCalendarConfig'],
    degatech.page.taskSchedulerControllerFactory);