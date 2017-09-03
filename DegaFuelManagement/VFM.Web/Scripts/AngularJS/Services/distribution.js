if (!degatech.services.distribution) degatech.services.distribution = {};

degatech.services.distribution.distributeCompany = function (adminId, custId, onSuccess, onError) {

    var url = "/api/Distribution/Company/" + adminId + "/" + custId;

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

degatech.services.distribution.distributeContact = function (adminId, contactId, onSuccess, onError) {

    var url = "/api/Distribution/Contact/" + adminId + "/" + contactId;

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

degatech.services.distribution.distributeAllMargins = function (adminId, onSuccess, onError) {

    var url = "/api/Distribution/Margins/" + adminId;

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

degatech.services.distribution.distributeMargin = function (adminId, custId, onSuccess, onError) {

    var url = "/api/Distribution/Margins/" + adminId + "/" + custId;

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
