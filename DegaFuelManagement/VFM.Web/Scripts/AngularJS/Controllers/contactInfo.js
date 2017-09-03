degatech.page.contactsControllerFactory = function ($scope, $rootScope, $uibModal,
    $baseController,
    $usersService,
    $contactsService,
    $contactNotesService,
    $contactDetailCustomFieldsService,
    $distributionService,
    $customerDetailsService,
    Notification) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$rootScope = $rootScope;
    vm.$usersService = $usersService;
    vm.$contactsService = $contactsService;
    vm.$contactNotesService = $contactNotesService;
    vm.$contactDetailCustomFieldsService = $contactDetailCustomFieldsService;
    vm.$distributionService = $distributionService;
    vm.$customerDetailsService = $customerDetailsService;
    vm.$scope = $scope;

    //PUBLIC METHODS
    vm.notify = vm.$contactsService.getNotifier($scope);
    vm.leftContact = _leftContact;
    vm.rightContact = _rightContact;
    vm.back = _back;
    vm.exit = _exit;
    vm.saveName = _saveName;
    vm.addNote = _addNote;
    vm.saveNote = _saveNote;
    vm.editNote = _editNote;
    //vm.deleteNote = _deleteNote;
    vm.cancelNote = _cancelNote;
    vm.editName = _editName;
    vm.cancelName = _cancelName;
    vm.deleteContact = _deleteContact;
    vm.editPhone = _editPhone;
    vm.cancelPhone = _cancelPhone;
    vm.savePhone = _savePhone;
    vm.editEmail = _editEmail;
    vm.cancelEmail = _cancelEmail;
    vm.saveEmail = _saveEmail;
    vm.updateContactDetails = _updateContactDetails;
    vm.changeType = _changeType;
    vm.onDistributeChanged = _onDistributeChanged;
    vm.distribute = _distribute;
    vm.addCustomField = _addCustomField;
    vm.editFields = _editFields;
    vm.saveFields = _saveFields;
    vm.cancelFields = _cancelFields;
    vm.deleteFields = _deleteFields;

    //PUBLIC HANDLERS
    vm.onGetInfoSuccess = _onGetInfoSuccess;
    vm.onSaveContactSuccess = _onSaveContactSuccess;
    vm.onSaveNoteSuccess = _onSaveNoteSuccess;
    vm.onDeleteNoteSuccess = _onDeleteNoteSuccess;
    vm.onDeleteContactSuccess = _onDeleteContactSuccess;
    //vm.onUpdateContactSuccess = _onUpdateContactSuccess;
    vm.onError = _onError;

    //PUBLIC MEMBERS
    vm.test = "This is a test";
    vm.contact = null;
    vm.savedContact = null;
    vm.savedNote = null;
    vm.deletedNote = null;
    vm.contactTypes = ["Primary","Secondary","Billing"];
    //vm.IsNameEditable = false;

    render();

    //PRIVATE METHODS
    function render() {
        vm.$contactsService.getContact($contactsService.ContactID, vm.onGetInfoSuccess, vm.onError);
        //vm.contactIDs = $contactsService.orderedContacts.map(function (contact) { return contact.Id });
        //console.log(vm.contactIDs);
    }

    function _leftContact(contactId) {
        var index = vm.contactIDs.indexOf(contactId);
        var leftContactID = vm.contactIDs[index - 1];
        vm.$contactsService.getContact(leftContactID, vm.onGetInfoSuccess, vm.onError);
    }

    function _rightContact(contactId) {
        var index = vm.contactIDs.indexOf(contactId);
        var rightContactID = vm.contactIDs[index + 1];
        vm.$contactsService.getContact(rightContactID, vm.onGetInfoSuccess, vm.onError);
    }

    function _back() {
        vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMERS';
        vm.$rootScope.ApplicationState.SubSection = "CONTACTS";
    }

    function _exit() {
        $customerDetailsService.CustClientID = vm.contact.CustClientID;
        vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
    }

    function _changeType(contact) {
        if (vm.contact.ContactType == "Primary") vm.contact.Distribute = true;
        else vm.contact.Distribute = false;
        _updateContactDetails(vm.contact);
    }

    function _onDistributeChanged(dstb) {
        if (dstb) {
            Notification.success({
                model: this,
                scope: $scope,
                message: 'You have ENABLED price distribution for this contact',
                delay: 3000,
                closeOnClick: false
            });
        }
        else {
            Notification.warning({
                model: this,
                scope: $scope,
                message: 'You have DISABLED price distribution for this contact',
                delay: 3000,
                closeOnClick: false,
            });
        }
        vm.contact.Distribute = dstb;
        _updateContactDetails(vm.contact);
    }

    function _editName() {
        vm.IsNameEditable = !vm.IsNameEditable;
    }

    function _cancelName(name) {
        vm.contact.FirstName = vm.updatedContact.FirstName;
        vm.contact.LastName = vm.updatedContact.LastName;
        vm.IsNameEditable = !vm.IsNameEditable;
    }

    function _saveName(name) {
        vm.contact.FirstName = name.FirstName;
        vm.contact.LastName = name.LastName;
        _updateContactDetails(vm.contact);
    }

    function _deleteContact(contact) {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DELETE this contact?";
                $scope.confirm = function () {
                    vm.clientId = contact.CustClientID;
                    $uibModalInstance.close(
                    vm.$contactsService.deleteContact($contactsService.ContactID, vm.onDeleteContactSuccess, vm.onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss()
                }
            }
        });
    }

    function _addNote() {
        vm.contact.ContactNotes.push({
            ContactID: vm.contact.Id,
            AddedByUserID: $usersService.user.Id,
            CustClientID: vm.contact.CustClientID
        });
    }

    function _saveNote(note) {
        vm.contact.Note = note;
        _updateContactDetails(vm.contact);
    }

    function _editNote() {
        vm.IsNoteEditable = !vm.IsNoteEditable;
    }

    function _cancelNote(note) {
        vm.contact.Note = vm.updatedContact.Note;
        vm.IsNoteEditable = !vm.IsNoteEditable;
    }

    function _editPhone() {
        vm.IsCustomFieldEditable = true;
        vm.IsPhoneEditable = !vm.IsPhoneEditable;
    }

    function _cancelPhone(phone) {
        vm.IsCustomFieldEditable = false;
        vm.contact.Phone = vm.updatedContact.Phone;
        vm.IsPhoneEditable = !vm.IsPhoneEditable;
    }

    function _savePhone(phone) {
        vm.IsCustomFieldEditable = false;
        vm.contact.Phone = phone;
        _updateContactDetails(vm.contact);
    }

    function _editEmail() {
        vm.IsCustomFieldEditable = true;
        vm.IsEmailEditable = !vm.IsEmailEditable;
    }

    function _cancelEmail(email) {
        vm.IsCustomFieldEditable = false;
        vm.contact.Email = vm.updatedContact.Email;
        vm.IsEmailEditable = !vm.IsEmailEditable;
    }

    function _saveEmail(email) {
        vm.contact.Email = email;
        _updateContactDetails(vm.contact);
    }

    function _updateContactDetails(contact) {
        vm.updatedContact = angular.copy(contact);
        vm.$contactsService.updateContact(vm.updatedContact.Id, contact, vm.onSaveContactSuccess, _onError);
    }

    function _distribute() {
        $uibModal.open({
            ariaLabelledBy: 'modal-title',
            ariaDescribedBy: 'modal-body',
            templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
            controller: function ($scope, $uibModalInstance) {
                $scope.message = "Are you sure you want to DISTRIBUTE pricing to this contact?";
                $scope.confirm = function () {
                    $uibModalInstance.close(
                        vm.$distributionService.distributeContact(vm.contact.AdminClientID, vm.contact.Id, _onDistributeSuccess, vm.onError));
                }
                $scope.cancel = function () {
                    $uibModalInstance.dismiss();
                }
            }
        });
    }

    function _onDistributeSuccess(data) {
        Notification.success({
            model: this,
            scope: $scope,
            message: "Company distributed! <br /> <br />",
            delay: 3000,
            closeOnClick: false
        });
    }

    function _addCustomField(type) {
        vm.IsCustomFieldEditable = true;
        vm.contact.CustomFields.push({
            ContactID: vm.contact.Id,
            FieldType: type,
            IsEditable: true
        });
    }

    function _editFields(field) {
        vm.IsCustomFieldEditable = true;
        field.IsEditable = true;
    }

    function _saveFields(customField) {
        vm.IsCustomFieldEditable = false;
        vm.savedCustomField = customField;
        if (vm.savedCustomField.Id > 0)
            vm.$contactDetailCustomFieldsService.updateCustomField(vm.savedCustomField.Id, vm.savedCustomField, _onSaveCustomFieldsSuccess, _onError);
        else vm.$contactDetailCustomFieldsService.addCustomField(vm.savedCustomField, _onSaveCustomFieldsSuccess, _onError);
    }

    function _cancelFields(field) {
        vm.IsCustomFieldEditable = false;
        field.IsEditable = false;
    }

    function _deleteFields(field) {
        vm.IsCustomFieldEditable = false;
        if (field.Id > 0) {
            $uibModal.open({
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: '/Partials/Admin/ModalForms/Confirmations/Confirmation.html',
                controller: function ($scope, $uibModalInstance) {
                    $scope.message = "Are you sure you want to DELETE this field?";
                    $scope.confirm = function () {
                            vm.deletedField = field
                            vm.$contactDetailCustomFieldsService.deleteCustomField(field, _onDeleteCustomFieldSuccess, _onError);
                    }
                    $scope.cancel = function () {
                        $uibModalInstance.dismiss()
                    }
                }
            });
        }
        else vm.contact.CustomFields.pop();
    }

    //PRIVATE HANDLERS
    function _onGetInfoSuccess(data) {
        vm.notify(function () {
            vm.contact = data.Item;
            console.log(vm.contact);
            vm.updatedContact = angular.copy(data.Item);
            vm.$contactsService.getContactsByCustClient(vm.contact.CustClientID, _onGetContactsSuccess, _onError);
        });
    }

    function _onGetContactsSuccess(data) {
        vm.notify(function () {
            var orderedContacts = data.Items.sort(function (a, b) {
                var x = a.LastName;
                var y = b.LastName;
                if (x < y) { return -1; }
                if (x > y) { return 1; }
                return 0;
            });
            console.log("ORDERED CONTACTS: ", orderedContacts);
            vm.contactIDs = orderedContacts.map(function (contact) { return contact.Id });
            console.log("ORDERED CONTACT IDs: ", vm.contactIDs);
            var index = vm.contactIDs.indexOf(vm.contact.Id);
            console.log(index);
            if (index == 0) vm.HideLeft = true;
            else if (vm.contactIDs.length > 0 && index == (vm.contactIDs.length - 1)) vm.HideRight = true;
            else {
                vm.HideLeft = false;
                vm.HideRight = false;
            }
        });
    }

    function _onSaveContactSuccess(data) {
        vm.notify(function () {
            vm.IsPhoneEditable = false;
            vm.IsEmailEditable = false;
            vm.IsNoteEditable = false;
            vm.IsNameEditable = false;
            $contactsService.contacts = null;
        });
    }

    function _onDeleteContactSuccess() {
        vm.notify(function () {
            $customerDetailsService.CustClientID = vm.clientId;
            $contactsService.contacts = null;
            vm.$rootScope.ApplicationState.ActiveSection = 'CUSTOMER DETAILS';
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

    function _onDeleteNoteSuccess() {
        vm.notify(function () {
            var index = vm.contact.ContactNotes.indexOf(vm.deletedNote);
            if (index > -1) {
                vm.contact.ContactNotes.splice(index, 1);
            }
        });
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
            var index = vm.contact.CustomFields.indexOf(vm.deletedField);
            if (index > -1) vm.contact.CustomFields.splice(index, 1);
        });
    }

    function _onError() {
        vm.notify(function () {
            vm.savingContact = false;
            vm.savingUser = false;
        });
        vm.error = "An error has occurred! <br/> <br />";
        console.log(vm.error);
        Notification.error({
            model: this,
            scope: $scope,
            message: vm.error,
            delay: 3000,
            closeOnClick: false
        });
    }
}

degatech.ng.addController(degatech.ng.app.module,
    "contactController",
        ['$scope',
        "$rootScope",
        '$uibModal',
        '$baseController',
        '$usersService',
        "$contactsService",
        "$contactNotesService",
        "$contactDetailCustomFieldsService",
        "$distributionService",
        "$customerDetailsService",
        "Notification"],
    degatech.page.contactsControllerFactory);