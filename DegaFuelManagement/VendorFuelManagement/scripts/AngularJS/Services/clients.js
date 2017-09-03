if (!degatech.services.clients) degatech.services.clients = {};
//var clients = degatech.services.clients;

degatech.services.clients.getDetailedClientInfo = function (id, onSuccess, onError) {
    //return $http.post('/Services/ClientsService.asmx/GetDetailedClientInfo', '{id: "' + id + '"}');

    var url = "/api/Clients/" + id;
    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };

    $.ajax(url, settings);

};

degatech.services.clients.updateClient = function (client) {
    return $http.post('/Services/ClientsService.asmx/UpdateClient', '{client: "' + client + '"}');
};

degatech.services.clients.getClient = function (id) {
    return $http.post('/Services/ClientsService.asmx/GetClient', '{id: "' + id + '"}');
};

//degatech.services.clients.getListOfClients = function (clients) {
//    return $http.post('/Services/ClientsService.asmx/GetListOfClients', '{clients: "' + clients + '"}');
//};

degatech.services.clients.getListOfClients = function(onSuccess, onError) {
    
    var url = "/api/Clients/";
    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };

    $.ajax(url, settings);

}

