if (!degatech.services.aircraftData) degatech.services.aircraftData = {};

/*degatech.services.aircraftData.addAircraft = function (aircraftData, onSuccess, onError) {

    var url = "/api/AircraftData";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: aircraftData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.aircraftData.updateAircraft = function (id, aircraftData, onSuccess, onError) {

    var url = "/api/AircraftData/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: aircraftData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}*/

degatech.services.aircraftData.getAircraftData = function (id, onSuccess, onError) {

    var url = "/api/AircraftData/" + id;

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

degatech.services.aircraftData.getListOfAircraftData = function (onSuccess, onError) {

    var url = "/api/AircraftData";

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

degatech.services.aircraftData.deleteAircraft = function (id, onSuccess, onError) {

    var url = "/api/AircraftData/" + id;

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
