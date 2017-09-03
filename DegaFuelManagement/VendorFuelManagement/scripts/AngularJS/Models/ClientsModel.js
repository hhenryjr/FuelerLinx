(function () {
    angular.module('Clients.Services').services('ClientsModel', ['$http', '$q', function ($http, $q) {
        return new function () {
            //Private Members
            var _This = this;
            var _Contacts = [];
            var _Users = [];

            //Public Members
            this.Clients = [];

            //Public Methods
            this.GetInitialData = function () {
                var contactsPromise = InitializeContacts();
                var usersPromise = InitializeUser();
                return $q.all([usersPromise, contactsPromise]).then(function () {
                    return {
                        Contacts: _Contacts,
                        Users: _Users
                    };
                });
            };

            this.GetDetailedClientInfo = function () {
                return $http.post('/Services/ClientsService.asmx/GetDetailedClientInfo', '{id: "' + id + '"}');
            };

            this.UpdateClient = function (client) {
                return $http.post('/Services/ClientsService.asmx/UpdateClient', '{client: "' + client + '"}');
            };

            this.GetClient = function (id) {
                return $http.post('/Services/ClientsService.asmx/GetClient', '{id: "' + id + '"}');
            };

            this.GetListOfClients = function (clients) {
                return $http.post('/Services/ClientsService.asmx/GetListOfClients', '{clients: "' + clients + '"}');
            };

            this.DeleteClient = function (client) {
                $http.post('/Services/ClientsService.asmx/DeleteClient', '{client: ' + client + '}').then(function () {
                    _This.Clients = [];
                });
            };

            //Private Methods
            function InitializeUser() {
                var deferred = $q.defer();
                if (_Users == null) {
                    $http.post('/Services/UsersService.asmx/GetListOfUsers', '{users: ' + users + '}').success(function (data) {
                        _Users = data;
                    });
                } else {
                    deferred.resolve(Users);
                }
                return deferred.promise;
            }

            function InitializeContacts() {
                var deferred = $q.defer();
                if (_Contacts == null) {
                    $http.post('/Services/ContactsService.asmx/GetListOfContacts', '{contacts: ' + contacts + '}').success(function (data) {
                        _Contacts = data;
                    });
                    deferred.resolve(data);
                } else {
                    deferred.resolve(Contacts);
                }
                return deferred.promise;
            }

        }

    }]);
})();