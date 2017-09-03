if (!degatech.services.fuelOrderMessages) degatech.services.fuelOrderMessages = {};

degatech.services.fuelOrderMessages.addMessage = function (message, onSuccess, onError) {

    var url = "/api/FuelOrderMessages";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: message
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderMessages.getMessages = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrderMessages/" + fuelOrderId;

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

degatech.services.fuelOrderMessages.getAllMessages = function (onSuccess, onError) {

    var url = "/api/FuelOrderMessages";

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