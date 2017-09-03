if (!degatech.services.fileUpload) degatech.services.fileUpload = {};

degatech.services.fileUpload.uploadFile = function (supplierId, onSuccess, onError) {

    var url = "/api/files/supplier/" + supplierId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        //, data: fuelOrderNoteData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}