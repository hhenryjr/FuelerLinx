if (!degatech.services.clients) degatech.services.clients = {};

degatech.services.clients.addClient = function(clientData, onSuccess, onError) {
    
    var url = "/api/Clients";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: clientData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}


degatech.services.clients.updateClient = function (id, clientData, onSuccess, onError) {

    var url = "/api/Clients/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: clientData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
};

degatech.services.clients.getClient = function (id, onSuccess, onError) {

    var url = "/api/Clients/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
};

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

degatech.services.clients.getDetailedClientInfo = function (id, onSuccess, onError) {

    var url = "/api/Clients/info/" + id;
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

degatech.services.clients.DeleteClient = function(id, onSuccess, onError) {

    var url = "/api/Clients/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
}
