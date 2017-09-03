/// <reference path="degatech.js" />
/// <reference path="/scripts/AngularJS/angular.js" /> , 'angularNumberPicker', 'enumFlag', 'ngMessages', "ngSanitize", "isteven-multi-select", 'ckeditor'


degatech.ng = {
    app: {
        services: {}
		, controllers: {}
    }
    , controllerInstances: []
	, exceptions: {}
	, examples: {}
    , defaultDependencies: ["ngAnimate", "ngRoute", "ui.bootstrap", 'ui.bootstrap.typeahead', "ngSanitize", "ngChosen", "ngDropzone", "smart-table", 'FuelerLinx.Directives', 'FL.Directives', "summernote", 'ui.calendar', 'FuelerLinx.Filters', 'JqueryUI', 'highcharts-ng', 'angular.filter', 'ui-notification']
    , getModuleDependencies: function () {
        if (degatech.extraNgDependencies) {
            var newItems = degatech.ng.defaultDependencies.concat(degatech.extraNgDependencies);
            return newItems;
        }
        return degatech.ng.defaultDependencies;
    }
};

angular.module('FL.Directives', []);

degatech.ng.app.module = angular.module('degatechApp', degatech.ng.getModuleDependencies());

degatech.ng.app.module.run(['$rootScope', function ($rootScope) {
    $rootScope.ApplicationState = {
        ActiveSection: null,
        ClientName: null,
        SubSection: null,
        triggerFilters: null
    };
}]);

// Sitewide Loading Icon
degatech.ng.app.module.directive('loadingIcon', [
    function () {
        return {
            restrict: 'E',
            template: '<div class="text-center"><i class="fa fa-spinner fa-pulse fa-2x fa-fw txt-color-blue"></i></div>',
            replace: true
        };
    }
]);

degatech.ng.app.module.directive('savingNotification', [
    function () {
        return {
            restrict: 'E',
            template: '<div><div class="saving-notification-background"></div><div class="saving-notification"><div>{{savingText}}</div><loading-icon/></div></div></div>',
            replace: true,
            scope: {
            },
            link: function(scope, element, attr) {
                scope.savingText = attr.savingText || '';
            }
        };
    }
]);

//Extenstion of UIB DatePicker
degatech.ng.app.module.directive('uibDatepickerPopup',[
    function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            priority: 9999,
            link: function(scope, element, attributes, ngModel) {
                ngModel.$formatters.push( function (value) {
                    var output = new Date();
                    if (value) {
                        output = moment(value).toDate();
                    }
                    return output;
                });
 
                ngModel.$parsers.push(function(value) {
                    var output = null;
                    if (value) {
                        output = moment(value).format();
                    }
                    return output;
                });
            }
        };
    }
    ]);

degatech.ng.app.module.directive('stFilteredRowLength', [
    function () {
        return {
            restrict: 'A',
            require: '^stTable',
            link: function (scope, element, attr, table) {
                scope.FilteredRowLength = 0;
                scope.FilteredRows = '';
                scope.FilteredCompanies = '';
                scope.FilteredContacts = '';

                function SetCurrentlyFilteredRowLength(currentTable) {
                    var collection = currentTable.getFilteredCollection();
                    if (collection == null) scope.FilteredRowLength = 0;
                    else {
                        scope.FilteredRowLength = currentTable.getFilteredCollection().length;
                        scope.$emit('filteredRows', currentTable.getFilteredCollection());
                    }
                }

                //Initialize our FilteredRowLength property
                SetCurrentlyFilteredRowLength(table);

                //Set our FilteredRowLength whenever the table is filtered
                scope.$watch(function () {
                    //return table.tableState().search;
                    return table.getFilteredCollection();
                }, function (newValue, oldValue) {
                    SetCurrentlyFilteredRowLength(table);
                }, true);
            }
        };
    }
]);

degatech.ng.app.module.directive('stSelectSearch', [
    function() {
        return {
            restrict: 'A',
            require: '^stTable',
            link: function(scope, element, attr, table) {

                //i.e. the html attribute Predicate="TailNumber" would give "TailNumber"
                var predicateName = attr.predicate;

                //Add an "onchange" event... this function will trigger when the dropdown's value changes
                element.bind('change', function() {

                    //Call the table's search method
                    //Pass in the currently selected value, and the predicate's name, i.e. "TailNumber"
                    scope.$apply(function() {
                        table.search(scope.$eval(attr.ngModel), predicateName);
                    });
                });
            }
        };
    }
]);

degatech.ng.app.module.directive('stDropdownFilter', ['$timeout',
    function ($timeout) {
        return {
            restrict: 'A',
            require: '^stTable',
            link: function (scope, element, attr, table) {

                //i.e. the html attribute Predicate="TailNumber" would give "TailNumber"
                var predicateName = attr.predicate;

                //Add an "onchange" event... this function will trigger when the dropdown's value changes

                    //Call the table's search method
                    //Pass in the currently selected value, and the predicate's name, i.e. "TailNumber"

                scope.$watch(function () { return scope.$eval(attr.ngModel); }, function () {
                    $timeout(function () {
                        table.search(scope.$eval(attr.ngModel), predicateName);
                    });
                });
            }
        };
    }
]);

degatech.ng.app.module.directive('stTransactionStatusFilter', ['$timeout',
    function ($timeout) {
        return {
            restrict: 'A',
            require: '^stTable',
            link: function (scope, element, attr, table) {

                //i.e. the html attribute Predicate="TailNumber" would give "TailNumber"
                var predicateName = attr.predicate;

                //Add an "onchange" event... this function will trigger when the dropdown's value changes

                //Call the table's search method
                //Pass in the currently selected value, and the predicate's name, i.e. "TailNumber"

                scope.$watch(function () { return scope.$eval(attr.ngModel); }, function () {
                    $timeout(function () {
                        table.search(scope.$eval(attr.ngModel), predicateName);
                        table.search(scope.$eval('info.filter.IsArchived'), 'IsArchived');
                    });
                });
            }
        };
    }
]);


degatech.ng.app.module.directive('vectorMap', [
function () {
    return {
        restrict: 'E',
        link: function (scope, element, attrs, ctrl) {
            var backGroundColor = attrs.bgColor || '#ffffff';

            data_array = {
                "US-VA": 4977,
                "US-PA": 4873,
                "US-TN": 3671,
                "US-WV": 2476,
                "US-NV": 1476,
                "US-TX": 146

            };

            var data_sparkline = [2700, 3631, 2471, 1300, 1877, 2500, 2577, 2700, 3631, 2471, 2000, 2100, 3000];

            $('.sparkline').sparkline(data_sparkline, {
                type: 'bar'
            });

            $(element).vectorMap({
                map: 'us_aea',
                backgroundColor: backGroundColor,
                regionStyle: {
                    initial: {
                        fill: '#c4c4c4'
                    },
                    hover: {
                        "fill-opacity": 1
                    }
                },
                series: {
                    regions: [{
                        values: data_array,
                        scale: ['#85a8b6', '#4d7686'],
                        normalizeFunction: 'polynomial'
                    }]
                },
                onRegionLabelShow: function (e, el, code) {
                    if (typeof data_array[code] == 'undefined') {
                        e.preventDefault();
                    } else {
                        var countrylbl = data_array[code];
                        el.html(el.html() + ': ' + countrylbl + ' dispatches');
                    }
                }
            });
        }
    };
}
]);


degatech.ng.app.module.directive('stResetSearch', ['$timeout',
function ($timeout) {
        return {
            restrict: 'EA',
            require: '^stTable',
            link: function(scope, element, attrs, ctrl) {
                return element.bind('click', function() {
                return $timeout(function () {
                        var tableState;
                        tableState = ctrl.tableState();
                        tableState.search = {}; //.predicateObject = {};
                        tableState.pagination.start = 0;
                        return ctrl.search('', ''); //pipe();
                    });
                });
            }
        };
    }
]);

//Smart-Table Custom Search
degatech.ng.app.module.directive('stCustomSearch', ['$timeout',
    function($timeout) {
        return {
            restrict: 'A',
            require: '^stTable',

            link: function(scope, element, attr, table) {
                if (scope.$parent.SmartTable != null)
                    scope.$parent.SmartTable = table;

                //i.e. the html attribute Predicate="TailNumber" would give "TailNumber"
                var predicateName = attr.predicate;
                if (!predicateName)
                    predicateName = attr.stCustomSearch;
                var searchFunction = function() {

                    //Call the table's search method
                    //Pass in the current value, and the predicate's name, i.e. "TailNumber"
                    $timeout(function () {
                        var value = element.val();
                        if (value == "")
                            table.search("", predicateName);
                        else
                            table.search(value, predicateName);
                    });
                };

                //Add an "onchange" event... this function will trigger when the dropdown's value changes
                element.bindWithDelay('keyup', searchFunction, 750);
                element.bind('change', searchFunction);
            }
        };
    }
]);

degatech.ng.app.module.directive('decimal', ['$filter', function ($filter) {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ctrl) {
            var requireDecimal = (attrs.requireDecimal != null && attrs.requireDecimal);
            var decimalPlaces = 4;
            var showCurrency = attrs.showCurrency || false;
            var showNegative = attrs.showNegative || false;
            if (attrs.decimalPlaces != null)
                decimalPlaces = parseInt(attrs.decimalPlaces);

            var listener = function (value) {
                if (!IsNumeric(value))
                    value = '0';
                value = $filter('ToNumber')(value, decimalPlaces, requireDecimal, showNegative);
                if (showCurrency)
                    value = '$' + value;
                element.val(value);
            };

            ctrl.$render = function () {
                var initialValue = ctrl.$viewValue;
                if (IsNumeric(initialValue) && initialValue.indexOf('.') == -1)
                    initialValue = (initialValue.toString() + ".0");
                listener(initialValue);
            };

            ctrl.$parsers.unshift(function (value) {
                value = value.toString().replace(/[^\d.-]/g, '');
                if (!IsNumeric(value))
                    value = '0';
                return parseFloat(value);
            });

            element.bind('change', function () {
                listener(element.val());
                var newValue = element.val().toString().replace(/[^\d.-]/g, '');
                if (ctrl && ctrl.$modelValue != newValue) {
                    ctrl.$setViewValue(newValue);
                }
            });

            element.bind('focus', function() {
                var newValue = element.val().toString().replace(/[^\d.-]/g, '');
                element.val(newValue);
            });
        }
    };
}]);


degatech.ng.app.module.directive('numberSpinner', [function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ctrl) {
        return;
            //$(element).spinner({
            //    change: function (event, ui) {
            //        $(element).change();
            //    }
            //});
        }
    };
}]);


degatech.ng.app.module.directive("showOnLink", [function () {
    return {
        link: function (scope, element, iAttrs) {
            $(element).show();
        }
    };
}]);

function IsNumeric(value) {
    return !isNaN(value) && !isNaN(parseFloat(value)) && isFinite(value);
}

degatech.ng.app.module.filter('ToNumber', function() {
    return function (input, places, requireDecimal, showNegative) {
        if (typeof places == 'undefined' || !IsNumeric(places))
            places = 0;
        if (!IsNumeric(input))
            input = '0';
        if (typeof requireDecimal == 'undefined')
            requireDecimal = false;
        if (typeof showNegative == 'undefined')
            showNegative = false;
        if (requireDecimal && input.toString().indexOf('.') === -1){
            if (showNegative) input = input * -1;
            return (parseFloat(input) / (Math.pow(10, places))).toFixed(places);
        }
        if (showNegative) input = input * -1;
        return parseFloat(input).toFixed(places);
    }
});

degatech.ng.app.module.filter('ToCurrency', [
            '$filter', function ($filter, places) {
                var decimalPlaces = 4;
                if (places && IsNumeric(places))
                    decimalPlaces = places;

                return function (input, omitSymbol, decimalPlacesToDisplay) {
                    if (!IsNumeric(input))
                        return input;
                    if (decimalPlacesToDisplay != null)
                        decimalPlaces = parseInt(decimalPlacesToDisplay);
                    if (omitSymbol)
                        return ($filter('ToNumber')(input, decimalPlaces));
                    return '$' + ($filter('ToNumber')(input, decimalPlaces));
                };
            }
]);

degatech.ng.app.module.filter('percentage', ['$filter', function ($filter) {
    return function (input, decimals) {
        return $filter('number')(input * 100, decimals) + '%';
    };
}]);

degatech.ng.app.module.filter('encodeUri', function ($window) {

    //function newEncode(data) {
    //    return $window.encodeURIComponent(data).replace(/%20/g, '+');
    //}

    //return newEncode;
    return $window.encodeURIComponent;
});

degatech.ng.app.module.filter('htmlToPlaintext', function ($window) {
    return function (text) {
        return text ? String(text).replace(/<[^>]+>/gm, '') : '';
    };
});

degatech.ng.app.module.value('$degatech', degatech.page);

degatech.ng.app.module.factory('$sizeEnum', [
    function () {
        return Object.freeze({
            NotSet: 0,
            LightJet: 1,
            MediumJet: 2,
            HeavyJet: 3,
            LightHelicopter: 4,
            WideBody: 5,
            SingleTurboProp: 6,
            VeryLightJet: 7,
            SingleEnginePiston: 8,
            MediumHelicopter: 9,
            HeavyHelicopter: 10,
            LightTwin: 11,
            HeavyTwin: 12,
            LightTurboProp: 13,
            MediumTurboprop: 14,
            HeavyTurboprop: 15,
            SuperHeavyJet: 16
        });
    }
]);

//A placeholder for CommonServices which is used by some of the imported FuelerLinx directives/filters
degatech.ng.app.module.service('CommonServices', [
    function () {
        //Private Members//////
        var _This = this;

        //Public Methods///////
    }
]);

degatech.ng.app.module.factory('$eventsEnum', [
    function () {
        return Object.freeze({
            HEADER_DATA: 'header_data'
        });
    }
]);

//#region - base functions and objects -

degatech.ng.exceptions.argumentException = function (msg) {
    this.message = msg;
    var err = new Error();


    console.error(msg + "\n" + err.stack);
}

degatech.ng.app.services.baseEventServiceFactory = function ($rootScope) {

    var factory = this;

    factory.$rootScope = $rootScope;

    factory.eventService = function (eventName) {

        var thisEventService = this;

        thisEventService.eventName = eventName;

        thisEventService.broadcast = function () {
            factory.$rootScope.$broadcast(thisEventService.eventName, arguments)
        }

        thisEventService.emit = function () {
            factory.$rootScope.$emit(thisEventService.eventName, arguments)
        }

        thisEventService.listen = function (callback) {
            factory.$rootScope.$on(thisEventService.eventName, callback)
        }

    }

    return factory.eventService;
}


degatech.ng.app.services.baseService = function ($win, $loc, $util) {
    /*
        when this function is envoked by Angular, Angular wants an instance of the Service object. 
		
    */

    var getChangeNotifier = function ($scope) {
        /*
        will be called when there is an event outside Angular that has modified
        our data and we need to let Angular know about it.
        */
        var self = this;

        self.scope = $scope;

        return function (fx) {
            self.scope.$apply(fx);//this is the magic right here that cause ng to re-evaluate bindings
        }


    }

    var baseService = {
        $window: $win
        , getNotifier: getChangeNotifier
        , $location: $loc
        , $utils: $util
        , merge: $.extend
    };

    return baseService;
}

degatech.ng.app.controllers.baseController = function ($doc, $logger, $dega, $route, $routeParams, /*$eventHandlerService,*/ $eventsEnum, $location/*, $alertService*/) {
    /*
        this is intended to serve as the base controller
    */

    baseControler = {
        $document: $doc
		, $log: $logger
        , $degatech: $dega
        , $location: $location
        , merge: $.extend
        //, $eventHandlerService: $eventHandlerService
        //, $alertService: $alertService
        , $eventsEnum: $eventsEnum
        , setUpCurrentRequest: function (viewModel) {

            viewModel.currentRequest = { originalPath: "/", isTop: true };

            if (viewModel.$route.current) {
                viewModel.currentRequest = viewModel.$route.current;
                viewModel.currentRequest.locals = {};
                viewModel.currentRequest.isTop = false;
            }

            viewModel.$log.log("setUpCurrentRequest firing:");
            viewModel.$log.debug(viewModel.currentRequest);

        }

    };

    return baseControler;
}

//#endregion

//#region  - core ng wrapper functions --

degatech.ng.getControllerInstance = function (jQueryObj) {///used to grab an instance of a controller bound to an Element
    console.log(jQueryObj);
    return angular.element(jQueryObj[0]).controller();
}

degatech.ng.addService = function (ngModule, serviceName, dependencies, factory) {
    /*
    degatech.ng.app.module.service(
    '$baseService', 
    ['$window', '$location', '$utils', degatech.ng.app.services.baseService]);
    */
    if (!ngModule ||
		!serviceName || !factory ||
		!angular.isFunction(factory)) {
        throw new degatech.ng.exceptions.argumentException("Invalid Service Call");
    }

    if (dependencies && !angular.isArray(dependencies)) {
        throw new degatech.ng.exceptions.argumentException("Invalid Service Call [dependencies]");
    }
    else if (!dependencies) {
        dependencies = [];
    }

    dependencies.push(factory);

    ngModule.service(serviceName, dependencies);

}

degatech.ng.registerService = degatech.ng.addService;

degatech.ng.addController = function (ngModule, controllerName, dependencies, factory) {
    if (!ngModule ||
		!controllerName || !factory ||
		!angular.isFunction(factory)) {
        throw new degatech.ng.exceptions.argumentException("Invalid Service defined");
    }

    if (dependencies && !angular.isArray(dependencies)) {
        throw new degatech.ng.exceptions.argumentException("Invalid Service Call [dependencies]");
    }
    else if (!dependencies) {
        dependencies = [];
    }

    dependencies.push(factory);
    ngModule.controller(controllerName, dependencies);

}

degatech.ng.registerController = degatech.ng.addController;


//#endregions


//#region - adding in baseService and baseController

/*
the basic explaination for these three function arguments

name of thing we are creating:'baseService'
array containing the dependancies of the function we will use to create said thing
the last item in this array is invoked to create the object and passed the preceding dependancies.


*/
degatech.ng.addService(degatech.ng.app.module
					, "$baseService"
					, ['$window', '$location']
					, degatech.ng.app.services.baseService);

degatech.ng.addService(degatech.ng.app.module
					, "$eventServiceFactory"
					, ["$rootScope"]
					, degatech.ng.app.services.baseEventServiceFactory);

degatech.ng.addService(degatech.ng.app.module
					, "$baseController"
					, ['$document', '$log', '$degatech', "$route", "$routeParams",/* "$eventHandlerService",*/ "$eventsEnum", "$location"/*, "$alertService"*/]
					, degatech.ng.app.controllers.baseController);


//#endregion

//#region - Examples on how to use the core functions

//***************************************************************************************
//------------------------ Examples -------------------------------------
/*
 * 
 *              How to call the .addService and .addController
 * 
 */




degatech.ng.examples.exampleServices = function ($baseService) {
    /*
		when this function is envoked by Angular,
		Angular wants an instance of the Service object. here
		we reuse the same instance of the JS object we have above
	*/

    /*
		we are using this as an example to demonstrate we can use our existing
		code with this new pattern.
	*/

    var adegatechServiceObject = degatech.services.users;
    var newService = angular.merge(true, {}, adegatechServiceObject, baseService);

    return newService;
}

degatech.ng.examples.exampleController = function ($scope, $baseController, $exampleSvc) {

    var vm = this;
    vm.items = null;
    vm.receiveItems = _receiveItems;
    vm.testTitle = "Get this to show first";

    //-- this line to simulate inheritance
    $baseController.merge(vm, $baseController);

    //You first pass at creating a new controller end here. 
    //go make this work first
    //-----------------------

    //this is a wrapper for our small dependency on $scope
    vm.notify = $exampleSvc.getNotifier($scope);

    function _receiveItems(data) {
        //this receives the data and calls the special
        //notify method that will trigger ng to refresh UI
        vm.notify(function () {
            vm.items = data.items;
        });
    }
}


degatech.ng.addService(degatech.ng.app.module
					, "$exampleSvc"
					, ['$baseService']
					, degatech.ng.examples.exampleServices);

degatech.ng.addController(degatech.ng.app.module
	, 'controllerName'
	, ['$scope', '$baseController', '$exampleSvc']
	, degatech.ng.examples.exampleController
	);


//------------------------ Examples -------------------------------------
//***************************************************************************************


//#endregion
