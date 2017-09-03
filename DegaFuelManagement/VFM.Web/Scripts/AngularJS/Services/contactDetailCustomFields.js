if (!degatech.services.contactDetailCustomFields) degatech.services.contactDetailCustomFields = {};

degatech.services.contactDetailCustomFields.addCustomField = function (customFieldData, onSuccess, onError) {

    var url = "/api/ContactDetailCustomFields";

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

degatech.services.contactDetailCustomFields.updateCustomField = function (id, customFieldData, onSuccess, onError) {

    var url = "/api/ContactDetailCustomFields/" + id;

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

degatech.services.contactDetailCustomFields.getCustomFields = function (contactId, onSuccess, onError) {

    var url = "/api/ContactDetailCustomFields/" + contactId;

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

degatech.services.contactDetailCustomFields.getListOfCustomFields = function (onSuccess, onError) {

    var url = "/api/ContactDetailCustomFields";

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

degatech.services.contactDetailCustomFields.deleteCustomField = function (customFieldData, onSuccess, onError) {

    var url = "/api/ContactDetailCustomFields/" + customFieldData.Id;

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
