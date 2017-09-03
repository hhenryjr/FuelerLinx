if (!degatech.services.contactNotes) degatech.services.contactNotes = {};

degatech.services.contactNotes.addContactNote = function (contactNoteData, onSuccess, onError) {

    var url = "/api/ContactNotes";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: contactNoteData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.contactNotes.updateContactNote = function (id, contactNoteData, onSuccess, onError) {

    var url = "/api/ContactNotes/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: contactNoteData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.contactNotes.getContactNote = function (id, onSuccess, onError) {

    var url = "/api/ContactNotes/" + id;

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

degatech.services.contactNotes.getListOfContactNotes = function (onSuccess, onError) {

    var url = "/api/ContactNotes";

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

degatech.services.contactNotes.deleteContactNote = function (id, onSuccess, onError) {

    var url = "/api/ContactNotes/" + id;

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
