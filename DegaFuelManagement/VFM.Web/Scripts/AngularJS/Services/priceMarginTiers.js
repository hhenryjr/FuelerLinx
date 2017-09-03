if (!degatech.services.priceMarginTiers) degatech.services.priceMarginTiers = {};

degatech.services.priceMarginTiers.addPriceMarginTier = function (priceMarginTiersData, onSuccess, onError) {

    var url = "/api/PriceMarginTiers";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: priceMarginTiersData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.priceMarginTiers.addListOfTiers = function (priceMarginTiersData, onSuccess, onError) {

    var url = "/api/PriceMarginTiers/List";

    var settings = {
        cache: false
        , contentType: "application/json"
        , data: JSON.stringify(priceMarginTiersData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.priceMarginTiers.updatePriceMarginTier = function (id, priceMarginTiersData, onSuccess, onError) {

    var url = "/api/PriceMarginTiers/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: priceMarginTiersData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.priceMarginTiers.getPriceMarginTier = function (id, onSuccess, onError) {

    var url = "/api/PriceMarginTiers/" + id;

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

degatech.services.priceMarginTiers.getPriceMarginTiersByMarginID = function (margin, onSuccess, onError) {

    var url = "/api/PriceMarginTiers/Margin/" + margin.Id;

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

degatech.services.priceMarginTiers.getListOfPriceMarginTiers = function (onSuccess, onError) {

    var url = "/api/PriceMarginTiers";

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

degatech.services.priceMarginTiers.deletePriceMarginTiers = function (id, onSuccess, onError) {

    var url = "/api/PriceMarginTiers/" + id;

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
