if (!degatech.services.fboDetailCustomFields) degatech.services.fboDetailCustomFields = {};

degatech.services.fboDetailCustomFields.addFBODetailCustomField = function (customFieldData, onSuccess, onError) {

    var url = "/api/FBODetailCustomFields";

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

degatech.services.fboDetailCustomFields.updateFBODetailCustomField = function (id, customFieldData, onSuccess, onError) {

    var url = "/api/FBODetailCustomFields/" + id;

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

degatech.services.fboDetailCustomFields.getCustomFields = function (customFieldData, onSuccess, onError) {

    var url = "/api/FBODetailCustomFields/Details";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: customFieldData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fboDetailCustomFields.getListOfFBODetailCustomFields = function (onSuccess, onError) {

    var url = "/api/FBODetailCustomFields";

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

degatech.services.fboDetailCustomFields.deleteFBODetailCustomField = function (customFieldData, onSuccess, onError) {

    var url = "/api/FBODetailCustomFields/" + customFieldData.Id;

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
