if (!degatech.services.calendarEvent) degatech.services.calendarEvent = {};

degatech.services.calendarEvent.getCalendar = function (onSuccess, onError) {

    var url = "/api/Calendar/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
};

degatech.services.calendarEvent.updateCalendar = function (calendar, onSuccess, onError) {

    var url = "/api/Calendar/";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(calendar)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
};

degatech.services.calendarEvent.getCalendarDates = function (clientId, userId, onSuccess, onError) {

    var url = "/api/Calendar/" + clientId + "/" + userId;
    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };

    $.ajax(url, settings);
};

degatech.services.calendarEvent.getCalendarDateTimes = function (calendar, onSuccess, onError) {

    var url = "/api/Calendar/" + calendar.ClientID + "/" + calendar.UserID + "/" + calendar.StartDate + "/" + calendar.EndDate;
    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };

    $.ajax(url, settings);
};

degatech.services.calendarEvent.sortByDate = function (onSuccess, onError) {

    var url = "/api/Calendar/Sort";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
};

degatech.services.calendarEvent.deleteCalendar = function (eventId, onSuccess, onError) {

    var url = "/api/Calendar/" + eventId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
};

////////////////////////////////////D R A G G A B L E/////////////////////////////////////////

degatech.services.calendarEvent.getDraggable = function (onSuccess, onError) {

    var url = "/api/Draggable/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "GET"
    };

    $.ajax(url, settings);
};

degatech.services.calendarEvent.updateDraggable = function (calendar, onSuccess, onError) {

    var url = "/api/Draggable/";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(calendar)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
};

degatech.services.calendarEvent.getDraggableEvents = function (clientId, userId, onSuccess, onError) {

    var url = "/api/Draggable/" + clientId + "/" + userId;
    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "GET"
    };

    $.ajax(url, settings);
};

degatech.services.calendarEvent.deleteDraggable = function (eventId, onSuccess, onError) {

    var url = "/api/Draggable/" + eventId;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
};