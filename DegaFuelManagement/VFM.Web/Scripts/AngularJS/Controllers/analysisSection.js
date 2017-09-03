degatech.page.analysisControllerFactory = function ($scope,
    $rootScope,
    $uibModal,
    $baseController,
    $siteSettingsService,
    $usersService,
    $analysisService,
    $fuelOrdersService,
    $permissionsService
    ) {


    var vm = this;

    $baseController.merge(vm, $baseController);

    vm.$usersService = $usersService;
    vm.$analysisService = $analysisService;
    vm.$fuelOrdersService = $fuelOrdersService;
    vm.$siteSettingsService = $siteSettingsService;
    vm.$rootScope = $rootScope;
    vm.$scope = $scope;

    //PUBLIC METHODS//////////////////////////////////////////////
    vm.notify = vm.$usersService.getNotifier($scope);
    vm.applyFilter = _applyFilter;
    vm.resetFilter = _resetFilter;
    vm.evaluateEndDate = _evaluateEndDate;
    vm.evaluateStartDate = _evaluateStartDate;
    vm.handleDrillUpClick = _handleDrillUpClick;

    //PUBLIC HANDLERS//////////////////////////////////////////////    

    //PUBLIC MEMBERS//////////////////////////////////////////////
    vm.test = "This is a test";
    vm.mapJoinField = ($usersService.user.Client.Name == 'Everest Fuel Management') ? 'postal-code' : 'name';
    vm.buttonLabel = 'World';
    vm.filter = {};
    vm.dateFilter = {
        IsStartDateOpened: false,
        IsEndDateOpened: false,
        Format: "MM/dd/yyyy"
    };
    vm.dateConfigs = {
        StartDateOptionsConfig: {
            maxDate: vm.filter.EndDateFilter,
            startingDay: 1,
        },
        EndDateOptionsConfig: {
            minDate: vm.filter.StartDateFilter,
            startingDay: 1,
        }
    };
    vm.chartConfigs = {
        //Regional map configuration for highmaps
        regionalMapConfig: {
            options: {
                legend: {
                    layout: 'vertical',
                    //backgroundColor: 'rgba(255,255,255,0.85)',
                    verticalAlign: 'bottom',
                    align: 'left',
                    title: {
                        text: 'Visits'
                    },
                    enabled: true
                },
                colorAxis: {
                    type: 'logarithmic'
                },
                plotOptions: {
                    map: {
                        mapData: ($usersService.user.Client.Name == 'Everest Fuel Management') ? Highcharts.maps['countries/us/us-all'] : Highcharts.maps['custom/world'],
                        joinBy: [vm.mapJoinField, 'name'],
                        animation: {
                            duration: 1000
                        },
                        dataLabels: {
                            enabled: true,
                            //color: '#black',
                            //format: '{point.postal-code}',
                            //format: '{point.' + vm.mapJoinField + '}'
                            formatter: function () {
                                if (vm.filter.MapRegion == 'world' || vm.filter.MapRegion == 'county')
                                    return this.point.properties['name'];
                                else
                                    return this.point.properties['postal-code'];
                            } 
                        },
                        name: 'Visits',
                        states: {
                            hover: {
                                color: '#9BCD77'
                            }
                        },
                        point: {
                            events: {
                                click: function () {
                                    _handleMapClick(this);
                                },
                                drillup: function () {
                                    this.setTitle(null, { text: 'World' });
                                    // alert('drill Up');
                                    console.log(this);
                                    console.log(this.options.series[0].name);
                                    console.log(this.options.series[0].data[0].name);
                                }
                            }
                        }
                    }
                }
            },
            chartType: 'map',
            title: {
                text: 'Regional Visits'
            },

            //legend: small ? {} : {
            //    layout: 'vertical',
            //    align: 'right',
            //    verticalAlign: 'middle'
            //},

            colorAxis: {
                min: 0,
                minColor: '#E6E7E8',
                maxColor: '#005645'
            },
            series: [],

            drilldown: {
                activeAxisLabelStyle: {
                    cursor: 'pointer',
                    color: '#039',
                    fontWeight: 'bold',
                    textDecoration: 'none'
                },
                activeDataLabelStyle: {
                    cursor: 'pointer',
                    color: '#FFFFFF',
                    textDecoration: 'none',
                    textOutline: '1px #000000'
                },
                drillUpButton: {
                    relativeTo: 'spacingBox',
                    position: {
                        x: 0,
                        y: 60
                    },
                    theme: {
                        fill: 'white',
                        'stroke-width': 1,
                        stroke: 'silver',
                        r: 0,
                        states: {
                            hover: {
                                fill: '#bada55'
                            },
                            select: {
                                stroke: '#039',
                                fill: '#bada55'
                            }
                        }
                    }
                }
            }
        },
        //Customer Market Share configuration for pie chart
        customerMarketShareConfig: {
            options: {
                chart: {
                    type: 'pie'
                },
                tooltip: {
                    pointFormat: '<b>{point.y} Total Fuel Orders</b> <br / > {point.percentage:.1f} %'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        size: "89%",
                        dataLabels: {
                            connectorPadding: 2,
                            connectorWidth: 0,
                            distance: 5,
                            enabled: true,
                            useHTML: true,
                            format: '<div style="100px !important;"><div style="width: 90px !important;" class="truncate">{point.name}</div></div>',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                }
            },
            title: {
                text: 'Customer Market Share'
            },
            series: []
        },
        //Transactions configuration for column chart
        fuelOrdersConfig: {
            series: [],
            title: {
                text: 'Transaction History'
            },
            xAxis: [{
                title: {
                    text: 'Date',
                },
                type: 'datetime',
            }],
            yAxis: [{
                title: {
                    text: 'Number of Transactions',
                },
            }],
        }
    };

    $('highchart-label>text').width('100px');
    console.log('100px');

    vm.dataSources = {
        regionalDispatches: {},
        customerMarketShares: {},
        fuelOrders: {}
    };
    vm.modalInstance = null;

    //PRIVATE METHODS//////////////////////////////////////////////
    function _render() {
        if (storage.GetSessionItem("LastActiveSection") == 'DASHBOARD')
            storage.SetSessionItem("LastActiveSection", "DASHBOARD");
        else storage.SetSessionItem("LastActiveSection", "ANALYSIS");
        console.log("Getting analysis...");
        vm.$siteSettingsService.getSettings(_onGetSettingsSuccess, _onError);
    }

    function _evaluateEndDate() {
        console.log('evaluate End Date');
        vm.dateConfigs.StartDateOptionsConfig.maxDate = vm.filter.EndDateFilter;
    }

    function _evaluateStartDate() {
        console.log('evaluate Start Date');
        vm.dateConfigs.EndDateOptionsConfig.minDate = vm.filter.StartDateFilter;
    }

    function _applyFilter() {
        $analysisService.getRegionalDispatches(vm.filter, _onGetRegionalDispatchesSuccess, _onError);
        $fuelOrdersService.getRanking(vm.filter, _onGetCustomerMarketShareSuccess, _onError);
        $fuelOrdersService.getAnalysis(vm.filter, _onGetTransactionsSuccess, _onError);
    }

    function _resetFilter() {
        vm.filter = {
            StartDateFilter: moment().subtract(1, 'years').format('MM/DD/YYYY'),
            EndDateFilter: moment().add(0, 'months').format('MM/DD/YYYY'),
            ClientID: $usersService.user.ClientID,
            MapRegion: vm.mapSettings,
            State: '',
            IsStartDateOpened: false,
            IsEndDateOpened: false,
            Format: "MM/dd/yyyy"
        };
        vm.dateConfigs.StartDateOptionsConfig.maxDate = vm.filter.EndDateFilter
        vm.dateConfigs.EndDateOptionsConfig.minDate = vm.filter.StartDateFilter;
    }

    function _setRegionalDispatchesSeries() {
        var series = vm.chartConfigs.regionalMapConfig.series;
        series.length = 0;
        series.push({
            name: 'Regional Visits',
            data: _getRegionalDispatchData()
        });
    }

    function _getRegionalDispatchData() {
        var result = [];
        $(vm.dataSources.regionalDispatches.Data).each(function () {
            if (vm.mapJoinField == 'name')
                this.Label = this.Label.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
            if (this.Label.toLowerCase() == 'united states' || this.Label.toLowerCase() == 'usa')
                this.Label = 'United States of America';
            result.push({
                name: this.Label,
                value: this.Value
            });
        }
        );
        return result;
    }

    function _setCustomerMarketShareSeries() {
        var series = vm.chartConfigs.customerMarketShareConfig.series;
        series.length = 0;
        series.push({
            name: 'Customer Market Share',
            data: _getCustomerMarketShareSeries()
        });
    }

    function _getCustomerMarketShareSeries() {
        var result = [];
        $(vm.dataSources.customerMarketShares).each(function () {
            result.push({
                name: this.CustomerName,
                y: this.TotalDispatches
            });
        }
        );
        return result;
    }

    function _setFuelOrdersSeries() {
        var series = vm.chartConfigs.fuelOrdersConfig.series;
        series.length = 0;
        series.push(
           {
               name: 'Pending',
               data: _getFuelOrdersSeries('Pending'),
               id: 'series1',
               type: 'column',
               xAxis: 0
           },
            {
                name: 'Canceled',
                data: _getFuelOrdersSeries('Canceled'),
                id: 'series2',
                type: 'column',
                xAxis: 0
            },
            {
                name: 'Discrepancy',
                data: _getFuelOrdersSeries('Discrepancy'),
                id: 'series3',
                type: 'column',
                xAxis: 0
            },
            {
                name: 'Reconciled',
                data: _getFuelOrdersSeries('Reconciled'),
                id: 'series4',
                type: 'column',
                xAxis: 0
            });
    }

    function _getFuelOrdersSeries(status) {
        var results = [];
        switch (status) {
            case 'Pending':
                results = vm.dataSources.fuelOrders.map(function (a) { return [Date.UTC(a.Year, a.Month, 1), a.TotalPending]; });
                return results;
                break;
            case 'Canceled':
                results = vm.dataSources.fuelOrders.map(function (a) { return [Date.UTC(a.Year, a.Month, 1), a.TotalCancelled]; });
                return
                break;
            case 'Discrepancy':
                results = vm.dataSources.fuelOrders.map(function (a) { return [Date.UTC(a.Year, a.Month, 1), a.TotalDiscrepancies]; });
                return results;
                break;
            case 'Reconciled':
                results = vm.dataSources.fuelOrders.map(function (a) { return [Date.UTC(a.Year, a.Month, 1), a.TotalReconciled]; });
                return results;
                break;
        }
        //var result = [];
        //$(vm.dataSources.fuelOrders).each(function () {
        //    result.push({
        //        name: this.Label,
        //        y: this.Value
        //    });
        //}
        //);
        //return result;
    }

    function _handleDrillUpClick() {
        console.log("Drilling up...");
        switch (vm.filter.MapRegion) {
            case 'united states': 
                vm.filter.MapRegion = vm.initialRegion;
                vm.mapJoinField = vm.initialJoinField;
                vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData = vm.initialMapData;
                break;
            case 'County':
                vm.filter.State = '';
                vm.filter.MapRegion = vm.previousRegion;
                vm.mapJoinField = vm.previewJoinField;
                vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData = vm.previousMapData;
                vm.buttonLabel = 'World';
                if ($usersService.user.Client.Name == 'Everest Fuel Management') vm.showButton = false;
                break;
        }

        vm.chartConfigs.regionalMapConfig.options.plotOptions.map.joinBy = [vm.mapJoinField, 'name'];
        $analysisService.getRegionalDispatches(vm.filter, _onGetRegionalDispatchesSuccess, _onError);
        if (vm.filter.MapRegion.toLowerCase() == 'world') vm.showButton = false;
    }

    function _handleMapClick(mapRegion) {
        console.log("Drilling down...");
        if (!vm.previousRegion) {
            vm.initialRegion = vm.filter.MapRegion;
            vm.initialJoinField = vm.mapJoinField;
            vm.initialMapData = vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData;
        }
        if (vm.filter.MapRegion !== 'County') {
            vm.previousRegion = vm.filter.MapRegion;
            vm.previewJoinField = vm.mapJoinField;
            vm.previousMapData = vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData;
        }

        //Moving the map to a view of the USA
        if (mapRegion.name == 'United States of America') {
            vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData = Highcharts.maps['countries/us/us-all'];
            vm.filter.MapRegion = 'united states';
            vm.mapJoinField = 'postal-code';
            //Moving the map to a view of Canada
        } else if (mapRegion.name == 'Canada') {
            vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData = Highcharts.maps['countries/ca/ca-all'];
            vm.filter.MapRegion = 'canada';
            vm.mapJoinField = 'postal-code';
            //Moving to a view of a specific continent
        } else if (vm.filter.MapRegion.toLowerCase() == 'world') {
            var continent = mapRegion.continent;
            var mapReference = ReplaceAll(' ', '-', continent.toLowerCase());
            vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData = Highcharts.maps['custom/' + mapReference];
            vm.filter.MapRegion = mapReference;
            vm.mapJoinField = 'name';
            vm.buttonLabel = 'World';
            //Moving to a specific state's view, to show individual counties
        } else if (vm.filter.MapRegion.toLowerCase() == 'united states') {
            $.getScript("https://code.highcharts.com/mapdata/countries/us/us-" + mapRegion.properties['postal-code'].toLowerCase() + "-all.js").done(function (script, textStatus) {
                vm.filter.MapRegion = 'County';
                vm.filter.State = mapRegion.properties['postal-code'].toLowerCase();
                var mapValue = 'countries/us/us-' + vm.filter.State + '-all';
                vm.mapJoinField = 'name';
                vm.buttonLabel = 'USA';
                vm.showButton = true;
                vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData = Highcharts.maps[mapValue];
                vm.chartConfigs.regionalMapConfig.options.plotOptions.map.joinBy = [vm.mapJoinField, 'name'];

                $analysisService.getRegionalDispatches(vm.filter, _onGetRegionalDispatchesSuccess, _onError);
            }).fail(function (jqxhr, settings, exception) {
                var test = '';
            });
            return;
        }
        vm.chartConfigs.regionalMapConfig.options.plotOptions.map.joinBy = [vm.mapJoinField, 'name'];
        vm.showButton = true;
        $analysisService.getRegionalDispatches(vm.filter, _onGetRegionalDispatchesSuccess, _onError);
    }

    //PRIVATE HANDLERS//////////////////////////////////////////////
    function _onGetSettingsSuccess(data) {
        vm.notify(function () {
            vm.mapSettings = data.Item.MapSettings;
            if (vm.mapSettings == 'united states')
                vm.chartConfigs.regionalMapConfig.options.plotOptions.map.mapData = Highcharts.maps['countries/us/us-all'];
            _resetFilter();
            _applyFilter();
        });
    }

    function _onGetRegionalDispatchesSuccess(data) {
        vm.notify(function () {
            vm.dataSources.regionalDispatches = JSON.parse(data);
            _setRegionalDispatchesSeries();
            console.log("REGIONAL DISPATCHES: ", vm.dataSources.regionalDispatches);
        });
    }

    function _onGetCustomerMarketShareSuccess(data) {
        vm.notify(function () {
            vm.dataSources.customerMarketShares = JSON.parse(data);
            _setCustomerMarketShareSeries();
            console.log(vm.dataSources.customerMarketShares);
        });
    }

    function _onGetTransactionsSuccess(data) {
        vm.notify(function () {
            vm.dataSources.fuelOrders = JSON.parse(data);
            _setFuelOrdersSeries();
            console.log("FUEL ORDERS: ", vm.dataSources.fuelOrders);
        });
    }

    function _onGetUsersSuccess(data) {//MOVE CODE
        vm.notify(function () {
            vm.adminUsers = data.Items;
            console.log("ADMIN USERS: ", vm.adminUsers);
        });
    }

    function _onGetPermissionsSuccess(data) {//MOVE CODE
        vm.notify(function () {
            vm.adminUser.Permissions = data.Item;
            console.log("USER PERMISSIONS: ", vm.adminUsers.Permissions);
        });
    }

    function _onSaveRegSuccess(data) {//MOVE CODE
        vm.notify(function () {
            vm.user.RegistrationID = data.Item;
            vm.user.ClientID = $usersService.user.ClientID;
            console.log("Saving user...");
            vm.$usersService.addUser(vm.user, _onSaveUserSuccess, _onError);
        });
    }

    function _onSaveUserSuccess(data) {//MOVE CODE
        console.log("Saving permission...");
        vm.notify(function () {
            vm.user.Permissions.UserID = data.Item;
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
            vm.$permissionsService.addPermission(vm.user.Permissions, _onSavePermissionSuccess, _onError);
        });
    }

    function _onSavePermissionSuccess(data) {//MOVE CODE
        vm.notify(function () {
            console.log("Admin user saved!");
            vm.adminUsers.push(vm.user);
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
    "analysisController",
    ['$scope',
    '$rootScope',
    '$uibModal',
    '$baseController',
    '$siteSettingsService',
    '$usersService',
    '$analysisService',
    '$fuelOrdersService'],
    degatech.page.analysisControllerFactory);