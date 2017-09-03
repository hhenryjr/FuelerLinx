if (!degatech.services.emailRouting) degatech.services.emailRouting = {};

degatech.services.emailRouting.addEmail = function (emailData, onSuccess, onError) {

    var url = "/api/EmailRouting";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: emailData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}


degatech.services.emailRouting.updateEmail = function (id, emailData, onSuccess, onError) {

    var url = "/api/EmailRouting/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: emailData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
};

degatech.services.emailRouting.getEmail = function (id, onSuccess, onError) {

    var url = "/api/EmailRouting/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
};

degatech.services.emailRouting.getListOfEmails = function (onSuccess, onError) {

    var url = "/api/EmailRouting/";
    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };

    $.ajax(url, settings);

}

degatech.services.emailRouting.getEmailsByAdminClient = function (id, onSuccess, onError) {

    var url = "/api/EmailRouting/Admin/" + id;
    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };

    $.ajax(url, settings);

}

degatech.services.emailRouting.deleteEmail = function (id, onSuccess, onError) {

    var url = "/api/EmailRouting/" + id;

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
