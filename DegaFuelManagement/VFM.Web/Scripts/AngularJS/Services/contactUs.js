if (!degatech.services.contactUs) degatech.services.contactUs = {};

degatech.services.contactUs.addContactUs = function (contactUsData, onSuccess, onError) {

    var url = "/api/ContactUs";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: contactUsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.contactUs.updateContactUs = function (id, contactUsData, onSuccess, onError) {

    var url = "/api/ContactUs/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: contactUsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.contactUs.getContactUs = function (id, onSuccess, onError) {

    var url = "/api/ContactUs/" + id;

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

degatech.services.contactUs.getListOfContactUs = function (onSuccess, onError) {

    var url = "/api/ContactUs";

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

degatech.services.contactUs.deleteContactUs = function (id, onSuccess, onError) {

    var url = "/api/ContactUs/" + id;

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
