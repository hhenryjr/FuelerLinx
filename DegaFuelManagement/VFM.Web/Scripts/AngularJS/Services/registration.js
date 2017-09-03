if (!degatech.services.registration) degatech.services.registration = {};

degatech.services.registration.addRegistration = function (registrationData, onSuccess, onError) {

    var url = "/api/Registration";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: registrationData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.registration.updateRegistration = function (id, registrationData, onSuccess, onError) {

    var url = "/api/registration/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: registrationData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.registration.getRegistration = function (id, onSuccess, onError) {

    var url = "/api/registration/" + id;

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

degatech.services.registration.getListOfRegistration = function (onSuccess, onError) {

    var url = "/api/registration";

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

degatech.services.registration.deleteRegistration = function (id, onSuccess, onError) {

    var url = "/api/registration/" + id;

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

degatech.services.registration.checkUsername = function (registrationData, onSuccess, onError) {

    var url = "/api/Registration/CheckUsername";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: registrationData
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}
