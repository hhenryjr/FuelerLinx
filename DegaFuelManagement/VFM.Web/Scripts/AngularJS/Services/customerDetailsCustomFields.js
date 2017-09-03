if (!degatech.services.customerDetailsCustomFields) degatech.services.customerDetailsCustomFields = {};

degatech.services.customerDetailsCustomFields.addCustomField = function (customFieldData, onSuccess, onError) {

    var url = "/api/CustomerDetailsCustomFields";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"//'application/json'
        , data: customFieldData//JSON.stringify(customFieldData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.customerDetailsCustomFields.updateCustomField = function (id, customFieldData, onSuccess, onError) {

    var url = "/api/CustomerDetailsCustomFields/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: customFieldData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.customerDetailsCustomFields.getCustomFields = function (adminId, custId, onSuccess, onError) {

    var url = "/api/CustomerDetailsCustomFields/" + adminId + "/" + custId;

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

degatech.services.customerDetailsCustomFields.getListOfCustomFields = function (onSuccess, onError) {

    var url = "/api/CustomerDetailsCustomFields";

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

degatech.services.customerDetailsCustomFields.deleteCustomField = function (customFieldData, onSuccess, onError) {

    var url = "/api/CustomerDetailsCustomFields/" + customFieldData.Id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //, data: customFieldData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
}
