if (!degatech.services.aircrafts) degatech.services.aircrafts = {};

degatech.services.aircrafts.addAircraft = function (aircraftData, onSuccess, onError) {

    var url = "/api/Aircrafts";

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

degatech.services.aircrafts.addListOfAircraft = function (aircraftData, onSuccess, onError) {

    var url = "/api/Aircrafts/AddList";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(aircraftData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.aircrafts.updateAircraft = function (id, aircraftData, onSuccess, onError) {

    var url = "/api/Aircrafts/" + id;

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
}

degatech.services.aircrafts.getAircraft = function (id, onSuccess, onError) {

    var url = "/api/Aircrafts/" + id;

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

degatech.services.aircrafts.getAircraftsByAdminAndCustClientID = function (adminId, custId, onSuccess, onError) {

    var url = "/api/Aircrafts/" + adminId + "/" + custId;

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

degatech.services.aircrafts.getListOfAircrafts = function (onSuccess, onError) {

    var url = "/api/Aircrafts";

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

degatech.services.aircrafts.getAircraftsByClientID = function (id, onSuccess, onError) {

    var url = "/api/Aircrafts/TailNumbers/" + id;

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

degatech.services.aircrafts.getRemainingAircrafts = function (exclusion, onSuccess, onError) {

    var url = "/api/Aircrafts/Remaining/" + exclusion.AdminClientID + "/" + exclusion.CustClientID;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: exclusion
        , dataType: "json"
        , success: function (successData) {
            onSuccess(successData, exclusion);
        }
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.aircrafts.deleteAircraft = function (aircraftData, onSuccess, onError) {

    var url = "/api/Aircrafts/";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(aircraftData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
}
