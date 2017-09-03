if (!degatech.services.vendorExclusions) degatech.services.vendorExclusions = {};

//////////////////////CUSTOMER DETAIL///////////////////////////////////
degatech.services.vendorExclusions.addCustomerDetailExclusion = function (exclusionData, onSuccess, onError) {

    var url = "/api/VendorExclusions/CustomerDetail/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: exclusionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.vendorExclusions.updateCustomerDetailExclusion = function (id, exclusionData, onSuccess, onError) {

    var url = "/api/VendorExclusions/CustomerDetail/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: exclusionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.vendorExclusions.getCustomerDetailExclusions = function (custId, adminId, onSuccess, onError) {

    var url = "/api/VendorExclusions/CustomerDetail/" + custId + "/" + adminId;

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

degatech.services.vendorExclusions.deleteCustomerDetailExclusion = function (id, onSuccess, onError) {

    var url = "/api/VendorExclusions/CustomerDetail/" + id;

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

degatech.services.vendorExclusions.addAircraftExclusion = function (exclusionData, onSuccess, onError) {

    var url = "/api/VendorExclusions/Aircrafts/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: exclusionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

//////////////////////FBO DETAIL///////////////////////////////////
degatech.services.vendorExclusions.addFBODetailExclusion = function (exclusionData, onSuccess, onError) {

    var url = "/api/VendorExclusions/FBODetail/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: exclusionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.vendorExclusions.updateFBODetailExclusion = function (id, exclusionData, onSuccess, onError) {

    var url = "/api/VendorExclusions/FBODetail/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: exclusionData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.vendorExclusions.getFBODetailExclusions = function (custId, adminId, onSuccess, onError) {

    var url = "/api/VendorExclusions/FBODetail/" + custId + "/" + adminId;

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

degatech.services.vendorExclusions.deleteFBODetailExclusion = function (id, onSuccess, onError) {

    var url = "/api/VendorExclusions/FBODetail/" + id;

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