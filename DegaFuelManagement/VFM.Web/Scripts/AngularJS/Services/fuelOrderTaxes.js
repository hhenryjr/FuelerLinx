if (!degatech.services.fuelOrderTaxes) degatech.services.fuelOrderTaxes = {};

degatech.services.fuelOrderTaxes.addTax = function (taxData, onSuccess, onError) {

    var url = "/api/FuelOrderTaxes";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: taxData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderTaxes.addFuelOrderTaxes = function (taxData, onSuccess, onError) {

    var url = "/api/FuelOrderTaxes/List";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(taxData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderTaxes.updateTax = function (id, taxData, onSuccess, onError) {

    var url = "/api/FuelOrderTaxes/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: taxData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderTaxes.getFuelOrder = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrderTaxes/" + fuelOrderId;

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

degatech.services.fuelOrderTaxes.getListOfFuelOrderTaxes = function (onSuccess, onError) {

    var url = "/api/FuelOrderTaxes";

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

degatech.services.fuelOrderTaxes.deleteTax = function (id, onSuccess, onError) {

    var url = "/api/FuelOrderTaxes/" + id;

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