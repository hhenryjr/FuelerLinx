if (!degatech.services.customerPriceMargins) degatech.services.customerPriceMargins = {};

degatech.services.customerPriceMargins.addCustomerPriceMargin = function (customerPriceMarginsData, onSuccess, onError) {

    var url = "/api/CustomerPriceMargins";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: customerPriceMarginsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.customerPriceMargins.addCustomerPriceMargins = function (customerPriceMarginsData, onSuccess, onError) {

    var url = "/api/CustomerPriceMargins/List";

    var settings = {
        cache: false
        , contentType: "application/json"
        , data: JSON.stringify(customerPriceMarginsData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.customerPriceMargins.updateCustomerPriceMargin = function (id, customerPriceMarginsData, onSuccess, onError) {

    var url = "/api/CustomerPriceMargins/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: customerPriceMarginsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.customerPriceMargins.getCustomerPriceMarginByCustClientID = function (id, onSuccess, onError) {

    var url = "/api/CustomerPriceMargins/" + id;

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

degatech.services.customerPriceMargins.getPrices = function (adminId, custId, leg, onSuccess, onError) {

    var url = "/api/CustomerPriceMargins/" + custId + "/" + adminId + "/" + leg.ICAO;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: function (data) {
            onSuccess(data, leg);
        }
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
}

degatech.services.customerPriceMargins.getPricesForAllVendors = function (adminId, custId, leg, onSuccess, onError) {

    var url = "/api/CustomerPriceMargins/AllVendors/" + custId + "/" + adminId + "/" + leg.ICAO;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: function (data) {
            onSuccess(data, leg);
        }
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
}

degatech.services.customerPriceMargins.getListOfCustomerPriceMargins = function (onSuccess, onError) {

    var url = "/api/CustomerPriceMargins";

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

degatech.services.customerPriceMargins.updatePriceMarginAndGetHighestMargin = function (company, onSuccess, onError) {

    var url = "/api/CustomerPriceMargins/Highest/" + company.Id + "/" + company.CustClientID + "/" + company.MarginSetting;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: function (data) {
            onSuccess(data, company);
        }
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
}

degatech.services.customerPriceMargins.deleteCustomerPriceMargin = function (id, onSuccess, onError) {

    var url = "/api/CustomerPriceMargins/" + id;

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
