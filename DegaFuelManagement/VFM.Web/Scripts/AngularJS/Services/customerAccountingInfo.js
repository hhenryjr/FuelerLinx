if (!degatech.services.customerAccountingInfo) degatech.services.customerAccountingInfo = {};

degatech.services.customerAccountingInfo.saveAccountingInfo = function (accountInfoData, onSuccess, onError) {

    var url = "/api/CustomerAccountingInfo";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: accountInfoData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.customerAccountingInfo.getAccountingByAdminAndCustClientID = function (adminId, clientId, onSuccess, onError) {

    var url = "/api/CustomerAccountingInfo/" + adminId + "/" + clientId;

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