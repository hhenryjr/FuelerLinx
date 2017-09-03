if (!degatech.services.acukwikFBOHandlerDetail) degatech.services.acukwikFBOHandlerDetail = {};

degatech.services.acukwikFBOHandlerDetail.addDetail = function (detailData, onSuccess, onError) {

    var url = "/api/AcukwikFBOHandlerDetail";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: detailData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.acukwikFBOHandlerDetail.updateDetail = function (id, detailData, onSuccess, onError) {

    var url = "/api/AcukwikFBOHandlerDetail/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: detailData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.acukwikFBOHandlerDetail.getDetail = function (id, onSuccess, onError) {

    var url = "/api/AcukwikFBOHandlerDetail/" + id;

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

degatech.services.acukwikFBOHandlerDetail.getListOfDetails = function (onSuccess, onError) {

    var url = "/api/AcukwikFBOHandlerDetail";

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