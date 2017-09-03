if (!degatech.services.fuelOrderFees) degatech.services.fuelOrderFees = {};

degatech.services.fuelOrderFees.addFee = function (feeData, onSuccess, onError) {

    var url = "/api/FuelOrderFees";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: feeData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderFees.addFuelOrderFees = function (feeData, onSuccess, onError) {

    var url = "/api/FuelOrderFees/List";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(feeData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderFees.updateFee = function (id, feeData, onSuccess, onError) {

    var url = "/api/FuelOrderFees/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: feeData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderFees.getFuelOrder = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrderFees/" + fuelOrderId;

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

degatech.services.fuelOrderFees.getListOfFuelOrderFees = function (onSuccess, onError) {

    var url = "/api/FuelOrderFees";

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

degatech.services.fuelOrderFees.deleteFee = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrderFees/" + fuelOrderId;

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