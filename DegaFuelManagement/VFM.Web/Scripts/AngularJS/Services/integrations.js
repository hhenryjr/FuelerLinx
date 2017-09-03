if (!degatech.services.integrations) degatech.services.integrations = {};

degatech.services.integrations.addAccountNumber = function (integrationData, onSuccess, onError) {

    var url = "/api/integrations/account";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: integrationData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.integrations.getFuelQuote = function (integrationData, onSuccess, onError) {

    var url = "/api/integrations/account/FuelQuote/" + integrationData.AccountNumber;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: integrationData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.integrations.getFuelQuote = function (integrationData, onSuccess, onError) {

    var url = "/api/integrations/FuelQuote/" + integrationData.AccountNumber;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: integrationData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.integrations.addFuelOrder = function (fuelOrder, onSuccess, onError) {

    var url = "/api/integrations/FuelOrder/" + fuelOrder.AccountNumber;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrder
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.integrations.updateFuelOrder = function (fuelOrder, onSuccess, onError) {

    var url = "/api/integrations/FuelOrder/" + fuelOrder.Id + "/" + fuelOrder.AccountNumber;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrder
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.integrations.getFuelOrder = function (fuelOrder, onSuccess, onError) {

    var url = "/api/integrations/FuelOrder/" + fuelOrder.Id + "/" + fuelOrder.AccountNumber;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrder
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.integrations.getInvoices = function (fuelOrder, onSuccess, onError) {

    var url = "/api/integrations/FuelOrder/" + fuelOrder.Id + "/" + fuelOrder.AccountNumber;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrder
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}