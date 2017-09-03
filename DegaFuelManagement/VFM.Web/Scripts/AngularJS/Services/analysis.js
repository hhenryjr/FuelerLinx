if (!degatech.services.analysis) degatech.services.analysis = {};

degatech.services.analysis.getRegionalDispatches = function (filter, onSuccess, onError) {

    var url = "/api/Analysis/RegionalDispatches";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(filter)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}
