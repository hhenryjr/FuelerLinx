if (!degatech.services.aircraftPriceMargins) degatech.services.aircraftPriceMargins = {};

degatech.services.aircraftPriceMargins.addAircraftPriceMargin = function (aircraftPriceMarginsData, onSuccess, onError) {

    var url = "/api/AircraftPriceMargin";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: aircraftPriceMarginsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.aircraftPriceMargins.updateAircraftPriceMargin = function (id, aircraftPriceMarginsData, onSuccess, onError) {

    var url = "/api/AircraftPriceMargin/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: aircraftPriceMarginsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.aircraftPriceMargins.getAircraftPriceMarginByAircraftID = function (id, onSuccess, onError) {

    var url = "/api/AircraftPriceMargin/" + id;

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

degatech.services.aircraftPriceMargins.getListOfAircraftPriceMargin = function (onSuccess, onError) {

    var url = "/api/AircraftPriceMargin";

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

degatech.services.aircraftPriceMargins.deleteAircraftPriceMargin = function (id, onSuccess, onError) {

    var url = "/api/AircraftPriceMargin/" + id;

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
