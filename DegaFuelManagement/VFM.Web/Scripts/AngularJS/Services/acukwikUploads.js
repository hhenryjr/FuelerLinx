if (!degatech.services.acukwikUploads) degatech.services.acukwikUploads = {};

degatech.services.acukwikUploads.getUploadData = function (acukwik, onSuccess, onError) {

    var url = "/api/AcukwikUploads/" + acukwik.Name;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: function (data) {
            onSuccess(data, acukwik);
        }
        , error: onError
        , type: "Get"
    };

    $.ajax(url, settings);
}