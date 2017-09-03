if (!degatech.services.schedulingUploads) degatech.services.schedulingUploads = {};

degatech.services.schedulingUploads.getUploadData = function (scheduling, clientId, onSuccess, onError) {

    var url = "/api/schedulingUploads/" + scheduling.name + "/" + clientId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: function (data) {
            onSuccess(data, scheduling);
        }
        , error: onError
        , type: "Get"
    };

    $.ajax(url, settings);
}

degatech.services.schedulingUploads.generateFuelOrders = function (scheduling, clientId, onSuccess, onError) {

    var url = "/api/schedulingUploads/" + scheduling.Id + "/" + clientId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: function (data) {
            onSuccess(data, scheduling);
        }
        , error: onError
        , type: "Get"
    };

    $.ajax(url, settings);
}