if (!degatech.services.permissions) degatech.services.permissions = {};

degatech.services.permissions.addPermission = function (loginData, onSuccess, onError) {

    var url = "/api/permissions";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: loginData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.permissions.updatePermission = function (loginData, onSuccess, onError) {

    var url = "/api/permissions/" + loginData.Id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: loginData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.permissions.getByID = function (id, onSuccess, onError) {

    var url = "/api/permissions/" + id;

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

degatech.services.permissions.getByUser = function (user, onSuccess, onError) {

    var url = "/api/permissions/user/" + user.Id;

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

degatech.services.permissions.deletePermission = function (loginData, onSuccess, onError) {

    var url = "/api/permissions/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: loginData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
}

degatech.services.permissions.deleteByUserID = function (loginData, onSuccess, onError) {

    var url = "/api/permissions/user/" + loginData.UserID;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: loginData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
}