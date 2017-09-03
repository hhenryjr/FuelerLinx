if (!degatech.services.customerDetails) degatech.services.customerDetails = {};

degatech.services.customerDetails.addCustomer = function (customerDetailData, onSuccess, onError) {

    var url = "/api/Details";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: customerDetailData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.customerDetails.updateCustomer = function (id, customerDetailData, onSuccess, onError) {

    var url = "/api/Details/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: customerDetailData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.customerDetails.getCustomer = function (id, onSuccess, onError) {

    var url = "/api/Details/" + id;

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

degatech.services.customerDetails.getListOfCustomers = function (onSuccess, onError) {

    var url = "/api/Details";

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

degatech.services.customerDetails.getCustomersByAdminClient = function (clientId, onSuccess, onError) {

    var url = "/api/Details/Admin/" + clientId;

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

degatech.services.customerDetails.deleteCustomer = function (id, onSuccess, onError) {

    var url = "/api/Details/" + id;

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

degatech.services.customerDetails.exportCompanies = function (exportData, onSuccess, onError) {

    var url = "/api/details/Export/";

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
