if (!degatech.services.fboPriceMargins) degatech.services.fboPriceMargins = {};

degatech.services.fboPriceMargins.addFBOPriceMargin = function (priceMarginData, onSuccess, onError) {

    var url = "/api/FBOPriceMargins";

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

degatech.services.fboPriceMargins.updateFBOPriceMargin = function (id, priceMarginData, onSuccess, onError) {

    var url = "/api/FBOPriceMargins/" + id;

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

degatech.services.fboPriceMargins.getFBOPriceMarginsByAdminClientAndICAO = function (id, margin, onSuccess, onError) {

    var url = "/api/FBOPriceMargins/" + id + "/" + margin.ICAO;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: function (data) {
            onSuccess(data, margin);
        }
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
}

degatech.services.fboPriceMargins.getFBODetails = function (priceMarginData, onSuccess, onError) {

    var url = "/api/FBOPriceMargins/Details";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: priceMarginData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fboPriceMargins.getListOfFBOPriceMargins = function (onSuccess, onError) {

    var url = "/api/FBOPriceMargins";

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

degatech.services.fboPriceMargins.deleteFBOPriceMargin = function (priceMarginData, onSuccess, onError) {

    var url = "/api/FBOPriceMargins/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: priceMarginData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
}
