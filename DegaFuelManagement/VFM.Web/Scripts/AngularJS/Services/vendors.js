if (!degatech.services.vendors) degatech.services.vendors = {};

degatech.services.vendors.addVendor = function (vendorsData, onSuccess, onError) {

    var url = "/api/Vendors";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: vendorsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.vendors.updateVendor = function (id, vendorsData, onSuccess, onError) {

    var url = "/api/Vendors/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: vendorsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.vendors.getVendorsByAdminClient = function (adminId, onSuccess, onError) {

    var url = "/api/Vendors/" + adminId;

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

degatech.services.vendors.getListOfVendors = function (onSuccess, onError) {

    var url = "/api/Vendors";

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

degatech.services.vendors.deleteVendor = function (id, onSuccess, onError) {

    var url = "/api/Vendors/" + id;

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