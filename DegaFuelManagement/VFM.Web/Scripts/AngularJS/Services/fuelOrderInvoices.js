if (!degatech.services.fuelOrderInvoices) degatech.services.fuelOrderInvoices = {};

degatech.services.fuelOrderInvoices.addInvoice = function (invoiceData, onSuccess, onError) {

    var url = "/api/FuelOrderInvoices";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: invoiceData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderInvoices.addFuelOrderInvoices = function (invoiceData, onSuccess, onError) {

    var url = "/api/FuelOrderInvoices/List";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(invoiceData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderInvoices.updateInvoice = function (id, invoiceData, onSuccess, onError) {

    var url = "/api/FuelOrderInvoices/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: invoiceData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderInvoices.getFuelOrderInvoices = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrderInvoices/" + fuelOrderId;

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

degatech.services.fuelOrderInvoices.getListOfFuelOrderInvoices = function (onSuccess, onError) {

    var url = "/api/FuelOrderInvoices";

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

degatech.services.fuelOrderInvoices.deleteInvoice = function (id, onSuccess, onError) {

    var url = "/api/FuelOrderInvoices/" + id;

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

degatech.services.fuelOrderInvoices.deleteFuelOrderInvoices = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrderInvoices/List/" + fuelOrderId;

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

degatech.services.fuelOrderInvoices.downloadInvoice = function (id, onSuccess, onError) {

    var url = "/api/FuelOrderInvoices/Download/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}