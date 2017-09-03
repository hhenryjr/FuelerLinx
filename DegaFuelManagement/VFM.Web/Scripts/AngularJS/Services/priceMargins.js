if (!degatech.services.priceMargins) degatech.services.priceMargins = {};

degatech.services.priceMargins.addPriceMargin = function (priceMarginsData, onSuccess, onError) {

    var url = "/api/PriceMargins";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: priceMarginsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.priceMargins.updatePriceMargin = function (id, priceMarginsData, onSuccess, onError) {

    var url = "/api/PriceMargins/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: priceMarginsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.priceMargins.getPriceMargin = function (id, onSuccess, onError) {

    var url = "/api/PriceMargins/" + id;

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

degatech.services.priceMargins.getListOfPriceMargins = function (onSuccess, onError) {

    var url = "/api/PriceMargins";

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

degatech.services.priceMargins.getPriceMarginsByAdminClient = function (clientId, onSuccess, onError) {

    var url = "/api/PriceMargins/Admin/" + clientId;

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

degatech.services.priceMargins.deletePriceMargin = function (id, onSuccess, onError) {

    var url = "/api/PriceMargins/" + id;

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

degatech.services.priceMargins.distributeAllPriceMargins = function (adminId, onSuccess, onError) {

    var url = "/api/Distribution/Margins/" + adminId;

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

degatech.services.priceMargins.distributePriceMargin = function (adminId, marginId, onSuccess, onError) {

    var url = "/api/Distribution/Margins/" + adminId + "/" + marginId;

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
