(function () {
    angular.module('Logins.Services').services('LoginsModel', ['$http', '$q', function ($http, $q) {
        return new function () {
            //Private Members
            var _This = this;
            var _Clients = [];

            //Public Members
            this.Logins = [];

            //Public Methods
            this.GetInitialData = function () {
                //var clientsPromise = InitializeClients();
                //return $q.all([clientsPromise]).then(function () {
                //    return {
                //        Clients: _Clients,
                //    };
                //});
            };

            this.Login = function(username, password) {
                return $http.post('/Services/LoginsService.asmx/Login', '{username: "' + username + '", password: "' + password +'"}');
            };

            this.GetUserClient = function (userClient) {
                return $http.post('/Services/LoginsService.asmx/GetUserClient', '{userClient: "' + userClient + '"}');
            };

            this.GetLogin = function (id) {
                return $http.post('/Services/LoginsService.asmx/GetLogin', '{id: "' + id + '"}');
            };

            this.GetListOfLogins = function (logins) {
                return $http.post('/Services/LoginsService.asmx/GetListOfLogins', '{logins: "' + logins + '"}');
            };

            this.DeleteLogin = function (login) {
                $http.post('/Services/LoginsService.asmx/DeleteLogin', '{login: ' + login + '}').then(function () {
                    _This.Logins = [];
                });
            };

            //Private Methods
            //function InitializeUser() {
            //    var deferred = $q.defer();
            //    if (_Users == null) {
            //        $http.post('/Services/UsersService.asmx/GetListOfUsers', '{users: ' + users + '}').success(function (data) {
            //            _Users = data;
            //        });
            //    } else {
            //        deferred.resolve(_Users);
            //    }
            //    return deferred.promise;
            //}

            function InitializeClients() {
                var deferred = $q.defer();
                if (_Clients == null) {
                    $http.post('/Services/ClientsService.asmx/GetListOfClients', '{clients: ' + clients + '}').success(function (data) {
                        _Clients = data;
                    });
                } else {
                    deferred.resolve(_Clients);
                }
                return deferred.promise;
            }

        }

    }]);
})();