if (!degatech.services.siteSettings) degatech.services.siteSettings = {};

degatech.services.siteSettings.getSettings = function (onSuccess, onError) {

    var url = "/api/SiteSettings/";

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