degatech.page.uploaderControllerFactory = function ($scope,
    $rootScope,
    $timeout,
    $uibModal,
    $baseController,
    $usersService,
    $acukwikUploadsService) {

    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$acukwikUploadsService = $acukwikUploadsService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.notify = vm.$usersService.getNotifier($scope);

    //PUBLIC HANDLERS//////////////////////////////////////////////    

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.test = "This is a test";
    vm.acukwikNames = [
        { 'Name': 'Airports' }, { 'Name': 'Countries' },
        { 'Name': 'FBOHandlerDetail' }, { 'Name': 'SubdivisionStates' }, { 'Name': 'SupplierDetail' }];
    vm.dropzoneConfig = {
        uploadMultiple: false,
        previewsContainer: "#Dropzone-Preview-Placeholder",
        maxFileSize: 4,
        url: "/api/files/acukwik/",
        acceptedFiles: '.xlsx,.xls,.csv',
        addRemoveLinks: true,
        accept: function (file, done) {
            if (file.size > 20000000) {
                Notification.warning({
                    model: this,
                    scope: $scope,
                    //templateUrl: '/Partials/Common/Notifications/Login.html',
                    message: "File size exceeded.",
                    delay: 3000,
                    closeOnClick: true
                });
            }
            else if (file.type == 'application/vnd.ms-excel' || file.type == 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' || file.type == '') {
                $timeout(function () {
                    vm.IsUploading = true;
                    vm.ValidPricingPercentage = 50;
                    vm.UploadState = 'Processing';
                    vm.uploadedDate = '';
                });
                var container = $(file.previewElement).parent();
                $(file.previewElement).hide();
                done();
            } else {
                Notification.error({
                    model: this,
                    scope: $scope,
                    //templateUrl: '/Partials/Common/Notifications/Login.html',
                    message: "That type of file is currently not supported.",
                    delay: 3000,
                    closeOnClick: true
                });
            }
        },
        success: function (data) {
            var res = JSON.parse(data.xhr.responseText);
            console.log(res.Item);
            $timeout(function () {
                vm.showingProgress = false;
                vm.IsUploading = false;
                vm.UploadState = "Completed";
                vm.ValidPricingPercentage = 100;
                vm.IsComplete = true;
                vm.uploadedDate = moment().format('MM/DD/YYYY');
            });
            //alert(res.Item);
        },
        init: function () {
            var myDropzone = this;

            this.on("success", function (data) {
                var res = JSON.parse(data.xhr.responseText);
            });

            this.on("processing", function (file) {
                $timeout(function () {
                    vm.showingProgress = true;
                });
                this.options.url = "/api/files/acukwik/" + vm.acukwikId;
            });
        }
    };

    //PRIVATE METHODS//////////////////////////////////////////////
    function _render() {
        storage.SetSessionItem("LastActiveSection", "DB UPLOADER");
        angular.forEach(vm.acukwikNames, function (acukwik) {
            acukwik.dropZoneConfig = _getSupplierDropzoneConfig(acukwik);
            acukwik.IsComplete = true;
            _getUploadStatus(acukwik);
        });
    }

    function _getSupplierDropzoneConfig(acukwik) {
        return {
            uploadMultiple: false,
            previewsContainer: "#Dropzone-Preview-Placeholder",
            maxFileSize: 4,
            url: "/api/files/acukwik/" + acukwik.Name,
            acceptedFiles: '.xlsx,.xls,.csv',
            addRemoveLinks: true,
            accept: function (file, done) {
                if (file.size > 4000000) {
                    Notification.warning({
                        model: this,
                        scope: $scope,
                        //templateUrl: '/Partials/Common/Notifications/Login.html',
                        message: "File size exceeded.",
                        delay: 3000,
                        closeOnClick: true
                    });
                }
                else if (file.type == 'application/vnd.ms-excel' || file.type == 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' || file.type == '') {
                    $timeout(function () {
                        acukwik.IsComplete = false;
                        acukwik.IsUploading = true;
                        acukwik.ValidPricingPercentage = 50;
                        acukwik.UploadState = 'Processing';
                    });
                    var container = $(file.previewElement).parent();
                    $(file.previewElement).hide();

                    done();
                } else {
                    Notification.error({
                        model: this,
                        scope: $scope,
                        //templateUrl: '/Partials/Common/Notifications/Login.html',
                        message: "That type of file is currently not supported.",
                        delay: 3000,
                        closeOnClick: true
                    });
                }
            },
            success: function (data) {
                var res = JSON.parse(data.xhr.responseText);
                console.log(res.Item);
                if (res.Item == "Upload failed.") {
                    $timeout(function () {
                        acukwik.IsComplete = true;
                        acukwik.IsUploading = false;
                        acukwik.ValidPricingPercentage = 0;
                        acukwik.UploadState = "Failed";
                    });
                } else {
                    $timeout(function () {
                        acukwik.IsComplete = true;
                        acukwik.IsUploading = false;
                        acukwik.ValidPricingPercentage = 100;
                        acukwik.UploadState = "Completed";
                        acukwik.LastUpdateDisplayText = moment().format('MM/DD/YYYY');
                    });
                }
            },
            init: function () {

                this.on("processing", function (file) {
                    this.options.url = "/api/files/acukwik/" + acukwik.Name;
                });
            }
        };
    }

    function _getUploadStatus(acukwik) {
        console.log('Getting upload status...');
        vm.$acukwikUploadsService.getUploadData(acukwik, _onGetSuccess, _onError);
    }
    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetSuccess(data, acukwik) {
        vm.notify(function () {
            if (data.Item.DateUploaded !== "0001-01-01T00:00:00") {
                acukwik.UploadState = 'Completed';
                acukwik.LastUpdateDisplayText = moment(data.Item.DateUploaded).format('MM/DD/YYYY');
            }
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
    "uploaderController",
    ['$scope',
    '$rootScope',
    '$timeout',
    '$uibModal',
    '$baseController',
    '$usersService',
    '$acukwikUploadsService'],
    degatech.page.uploaderControllerFactory);