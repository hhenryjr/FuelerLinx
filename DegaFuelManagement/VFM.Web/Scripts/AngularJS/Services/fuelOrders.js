if (!degatech.services.fuelOrders) degatech.services.fuelOrders = {};

if (!degatech.services.fuelOrdersHighestID) degatech.services.fuelOrdersHighestID = 0;

degatech.services.fuelOrders.addFuelOrder = function (fuelOrdersData, onSuccess, onError) {

    var url = "/api/FuelOrders";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrdersData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.dispatchEmails = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrders/DispatchEmails";

    var settings = {
        cache: false
        , contentType: "application/json"
        , data: JSON.stringify({fuelOrderId: fuelOrderId})
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.sendConfirmationEmail = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrders/SendRequestConfirmation";

    var settings = {
        cache: false
        , contentType: "application/json"
        , data: JSON.stringify({ fuelOrderId: fuelOrderId })
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.addFuelOrderList = function (fuelOrdersData, onSuccess, onError) {

    var url = "/api/FuelOrders/List";

    var settings = {
        cache: false
        , contentType: 'application/json'
        , data: JSON.stringify(fuelOrdersData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.updateFuelOrder = function (id, fuelOrdersData, onSuccess, onError) {

    var url = "/api/FuelOrders/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrdersData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.updateFuelOrderList = function (fuelOrdersData, onSuccess, onError) {

    var url = "/api/FuelOrders/List";

    var settings = {
        cache: false
        , contentType: "application/json"
        , data: JSON.stringify(fuelOrdersData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.getFuelOrder = function (id, onSuccess, onError) {

    var url = "/api/FuelOrders/" + id;

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

degatech.services.fuelOrders.getListOfFuelOrders = function (onSuccess, onError) {

    var url = "/api/FuelOrders";

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

degatech.services.fuelOrders.getFuelOrdersByAdminClient = function (clientId, startDate, endDate, onSuccess, onError) {

    var url = "/api/FuelOrders/Admin";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: {
            ClientID: clientId,
            StartDate: startDate,
            EndDate: endDate
        }
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.getFuelOrdersByClient = function (user, startDate, endDate, onSuccess, onError) {

    var url = "/api/FuelOrders/" + ((user.Client.ClientType == 1) ? "Admin" : "Cust");

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: {
            ClientID: user.ClientID,
            StartDate: startDate,
            EndDate: endDate
        }
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.deleteFuelOrders = function (id, onSuccess, onError) {

    var url = "/api/FuelOrders/" + id;

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

degatech.services.fuelOrders.deleteFuelOrdersList = function (fuelOrders, onSuccess, onError) {

    var url = "/api/FuelOrders/List";

    var settings = {
        cache: false
        , contentType: "application/json"
        , data: JSON.stringify(fuelOrders)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "DELETE"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.exportTransactions = function (total, onSuccess, onError) {

    var url = "/api/FuelOrders/Export/" + ((total.ClientType == 1) ? "Admin" : "Cust");

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: total
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.getAnalysis = function (total, onSuccess, onError) {

    var url = "/api/FuelOrders/Analysis";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: total
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.getHighestTransactionIDFromDatabase = function (user, onSuccess, onError) {

    var url = "/api/FuelOrders/HighestTransactionIDFromDatabase/" + ((user.Client.ClientType == 1) ? "Admin" : "Cust");

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: {
            ClientID: user.ClientID
        }
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

///////////////////////////////FOR DASHBOARD////////////////////////////

degatech.services.fuelOrders.getRanking = function (ranking, onSuccess, onError) {

    var url = "/api/FuelOrders/GetRanking/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: ranking
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.exportRanking = function (ranking, onSuccess, onError) {

    var url = "/api/FuelOrders/ExportRanking/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: ranking
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.getTotal = function (total, onSuccess, onError) {

    var url = "/api/FuelOrders/GetTotal/" + ((total.ClientType == 1) ? "Admin" : "Cust");

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: total
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.exportTotal = function (total, onSuccess, onError) {

    var url = "/api/FuelOrders/ExportTotal/" + ((total.ClientType == 1) ? "Admin" : "Cust");

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: total
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrders.getSummary = function (summary, onSuccess, onError) {

    var url = "/api/FuelOrders/Summary/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: summary
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}
