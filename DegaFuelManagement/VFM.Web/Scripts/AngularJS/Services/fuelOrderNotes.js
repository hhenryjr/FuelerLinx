if (!degatech.services.fuelOrderNotes) degatech.services.fuelOrderNotes = {};

degatech.services.fuelOrderNotes.addFuelOrderNote = function (fuelOrderNoteData, onSuccess, onError) {

    var url = "/api/FuelOrderNotes";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrderNoteData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderNotes.updateFuelOrderNote = function (id, fuelOrderNoteData, onSuccess, onError) {

    var url = "/api/FuelOrderNotes/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrderNoteData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderNotes.getFuelOrderNote = function (id, onSuccess, onError) {

    var url = "/api/FuelOrderNotes/" + id;

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

degatech.services.fuelOrderNotes.getListOfFuelOrderNotes = function (onSuccess, onError) {

    var url = "/api/FuelOrderNotes";

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

degatech.services.fuelOrderNotes.deleteFuelOrderNote = function (id, onSuccess, onError) {

    var url = "/api/FuelOrderNotes/" + id;

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
