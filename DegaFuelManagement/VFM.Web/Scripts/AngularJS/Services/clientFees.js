if (!degatech.services.clientFees) degatech.services.clientFees = {};

degatech.services.clientFees.addFee = function (feeData, onSuccess, onError) {

    var url = "/api/ClientFees";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: feeData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.clientFees.addClientFees = function (feeData, onSuccess, onError) {

    var url = "/api/ClientFees/List";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(feeData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.clientFees.updateFee = function (id, feeData, onSuccess, onError) {

    var url = "/api/ClientFees/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: feeData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.clientFees.getClient = function (clientId, onSuccess, onError) {

    var url = "/api/ClientFees/" + clientId;

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

degatech.services.clientFees.getListOfClientFees = function (onSuccess, onError) {

    var url = "/api/ClientFees";

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

degatech.services.clientFees.deleteFee = function (clientID, feeDesc, onSuccess, onError) {

    var url = "/api/ClientFees/" + clientID + "/" + feeDesc;

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