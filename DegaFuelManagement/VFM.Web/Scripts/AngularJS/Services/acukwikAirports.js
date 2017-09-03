if (!degatech.services.acukwikAirports) degatech.services.acukwikAirports = {};

degatech.services.acukwikAirports.addAirport = function (airportData, onSuccess, onError) {

    var url = "/api/AcukwikAirports";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: airportData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.acukwikAirports.updateAirport = function (id, airportData, onSuccess, onError) {

    var url = "/api/AcukwikAirports/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: airportData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.acukwikAirports.getAirport = function (id, onSuccess, onError) {

    var url = "/api/AcukwikAirports/" + id;

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

degatech.services.acukwikAirports.getListOfAirports = function (onSuccess, onError) {

    var url = "/api/AcukwikAirports";

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

degatech.services.acukwikAirports.getAirportsAndMarginsByAdminClient = function (clientId, onSuccess, onError) {

    var url = "/api/AcukwikAirports/Margins/" + clientId;

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