if (!degatech.services.suppliers) degatech.services.suppliers = {};

degatech.services.suppliers.addSupplier = function (suppliersData, onSuccess, onError) {

    var url = "/api/Suppliers";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: suppliersData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.suppliers.updateSupplier = function (id, suppliersData, onSuccess, onError) {

    var url = "/api/Suppliers/" + id;
    var data = angular.copy(suppliersData);
    delete data.dropZoneConfig;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: data
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.suppliers.getSupplier = function (id, onSuccess, onError) {

    var url = "/api/Suppliers/" + id;

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

degatech.services.suppliers.getListOfSuppliers = function (onSuccess, onError) {

    var url = "/api/Suppliers";

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

degatech.services.suppliers.getSuppliersByAdminClient = function (adminId, onSuccess, onError) {

    var url = "/api/Suppliers/Admin/" + adminId;

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

degatech.services.suppliers.deleteSuppliers = function (id, onSuccess, onError) {

    var url = "/api/Suppliers/" + id;

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
