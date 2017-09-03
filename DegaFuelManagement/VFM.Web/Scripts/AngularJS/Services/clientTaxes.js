if (!degatech.services.clientTaxes) degatech.services.clientTaxes = {};

degatech.services.clientTaxes.addTax = function (taxData, onSuccess, onError) {

    var url = "/api/ClientTaxes";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: taxData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.clientTaxes.addClientTaxes = function (taxData, onSuccess, onError) {

    var url = "/api/ClientTaxes/List";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(taxData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.clientTaxes.updateTax = function (id, taxData, onSuccess, onError) {

    var url = "/api/ClientTaxes/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: taxData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.clientTaxes.getClient = function (clientId, onSuccess, onError) {

    var url = "/api/ClientTaxes/" + clientId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
}

degatech.services.clientTaxes.getListOfClientTaxes = function (onSuccess, onError) {

    var url = "/api/ClientTaxes";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
}

degatech.services.clientTaxes.deleteTax = function (clientID, taxDesc, onSuccess, onError) {

    var url = "/api/ClientTaxes/" + clientID + "/" + taxDesc;

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