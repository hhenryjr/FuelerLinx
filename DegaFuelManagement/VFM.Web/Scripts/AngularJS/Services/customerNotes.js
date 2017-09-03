if (!degatech.services.customerNotes) degatech.services.customerNotes = {};

degatech.services.customerNotes.addCustomerNote = function (customerNoteData, onSuccess, onError) {

    var url = "/api/CustomerNotes";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: customerNoteData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.customerNotes.updateCustomerNote = function (id, customerNoteData, onSuccess, onError) {

    var url = "/api/CustomerNotes/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: customerNoteData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.customerNotes.getCustomerNote = function (id, onSuccess, onError) {

    var url = "/api/CustomerNotes/" + id;

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

degatech.services.customerNotes.getListOfCustomerNotes = function (onSuccess, onError) {

    var url = "/api/CustomerNotes";

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

degatech.services.customerNotes.deleteCustomerNote = function (id, onSuccess, onError) {

    var url = "/api/CustomerNotes/" + id;

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
