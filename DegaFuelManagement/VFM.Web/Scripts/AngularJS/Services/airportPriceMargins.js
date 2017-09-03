if (!degatech.services.airportPriceMargins) degatech.services.airportPriceMargins = {};

degatech.services.airportPriceMargins.addAirportPriceMargin = function (priceMarginData, onSuccess, onError) {

    var url = "/api/AirportPriceMargins";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"//'application/json'
        , data: priceMarginData//JSON.stringify(priceMarginData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.airportPriceMargins.updateAirportPriceMargin = function (/*id, */priceMarginData, onSuccess, onError) {

    var url = "/api/AirportPriceMargins/";//+ id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: priceMarginData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.airportPriceMargins.getAirportPriceMarginsByAdminClientID = function (id, onSuccess, onError) {

    var url = "/api/AirportPriceMargins/" + id;

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

degatech.services.airportPriceMargins.getListOfAirportPriceMargins = function (onSuccess, onError) {

    var url = "/api/AirportPriceMargins";

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

degatech.services.airportPriceMargins.deleteAirportPriceMargin = function (id, onSuccess, onError) {

    var url = "/api/AirportPriceMargins/" + id;

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

degatech.services.airportPriceMargins.exportAirports = function (airport, onSuccess, onError) {

    var url = "/api/AirportPriceMargins/Export/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: airport
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}
