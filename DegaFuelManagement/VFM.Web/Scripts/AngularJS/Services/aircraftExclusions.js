if (!degatech.services.aircraftExclusions) degatech.services.aircraftExclusions = {};

degatech.services.aircraftExclusions.addAircraftExclusion = function (aircraftExclusionData, onSuccess, onError) {

    var url = "/api/AircraftExclusions/Add";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: aircraftExclusionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.aircraftExclusions.addListOfExclusions = function (aircraftExclusionData, onSuccess, onError) {

    var url = "/api/AircraftExclusions/AddList";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(aircraftExclusionData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.aircraftExclusions.updateAircraftExclusion = function (id, aircraftExclusionData, onSuccess, onError) {

    var url = "/api/AircraftExclusions/" + id;
    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: aircraftExclusionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

//degatech.services.aircraftExclusions.getAircraftExclusion = function (id, onSuccess, onError) {
//    var url = "/api/AircraftExclusions/" + id;
//    var settings = {
//        cache: false
//        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
//        , dataType: "json"
//        , success: onSuccess
//        , error: onError
//        , type: "GET"
//    };
//    $.ajax(url, settings);
//}
//degatech.services.aircraftExclusions.getListOfAircraftExclusions = function (onSuccess, onError) {
//    var url = "/api/AircraftExclusions";
//    var settings = {
//        cache: false
//        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
//        , dataType: "json"
//        , success: onSuccess
//        , error: onError
//        , type: "GET"
//    };
//    $.ajax(url, settings);
//}

degatech.services.aircraftExclusions.getAircraftExclusionsByICAOFBOAndAdminClientID = function (aircraftExclusionData, onSuccess, onError) {

    var url = "/api/AircraftExclusions/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: aircraftExclusionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.aircraftExclusions.deleteAircraftExclusion = function (id, onSuccess, onError) {

    var url = "/api/AircraftExclusions/" + id;

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
