if (!degatech.services.supplierFuelsPrices) degatech.services.supplierFuelsPrices = {};

degatech.services.supplierFuelsPrices.addSupplierFuelsPrice = function (supplierFuelsPricesData, onSuccess, onError) {

    var url = "/api/SupplierFuelsPrices";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: supplierFuelsPricesData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.supplierFuelsPrices.updateSupplierFuelsPrice = function (id, supplierFuelsPricesData, onSuccess, onError) {

    var url = "/api/SupplierFuelsPrices/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: supplierFuelsPricesData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.supplierFuelsPrices.getSupplierFuelsPrice = function (id, onSuccess, onError) {

    var url = "/api/SupplierFuelsPrices/" + id;

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

degatech.services.supplierFuelsPrices.getListOfSupplierFuelsPrices = function (onSuccess, onError) {

    var url = "/api/SupplierFuelsPrices";

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

degatech.services.supplierFuelsPrices.getSupplierFuelsPricesByAdmin = function (adminId, onSuccess, onError) {

    var url = "/api/SupplierFuelsPrices/List/" + adminId;

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

degatech.services.supplierFuelsPrices.deleteSupplierFuelsPrices = function (adminId, supplierId, onSuccess, onError) {

    var url = "/api/SupplierFuelsPrices/" + adminId + "/" + supplierId;

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

degatech.services.supplierFuelsPrices.exportSupplierFuelsPrices = function (exportData, onSuccess, onError) {

    var url = "/api/SupplierFuelsPrices/Export/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: exportData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.supplierFuelsPrices.getSupplierFuelsPricesByICAO = function (adminId, icao, onSuccess, onError) {

    var url = "/api/SupplierFuelsPrices/" + adminId + "/" + icao;

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