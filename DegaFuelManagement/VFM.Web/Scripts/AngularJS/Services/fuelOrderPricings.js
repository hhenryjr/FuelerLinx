if (!degatech.services.fuelOrderPricings) degatech.services.fuelOrderPricings = {};

/////////////////////SUPPLIER PRICINGS////////////////////////////////////////////

degatech.services.fuelOrderPricings.addSupplierPricing = function (fuelOrderPricingsData, onSuccess, onError) {

    var url = "/api/FuelOrderPricings";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrderPricingsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.addSupplierPricings = function (fuelOrderPricingsData, onSuccess, onError) {

    var url = "/api/FuelOrderPricings/List";

    var settings = {
        cache: false
        , contentType: "application/json"
        , data: JSON.stringify(fuelOrderPricingsData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.updateSupplierPricing = function (id, fuelOrderPricingsData, onSuccess, onError) {

    var url = "/api/FuelOrderPricings/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrderPricingsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.getSupplierPricing = function (id, onSuccess, onError) {

    var url = "/api/FuelOrderPricings/" + id;

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

degatech.services.fuelOrderPricings.getWholesaleInvoice = function (fuelOrderPricingsData, onSuccess, onError) {

    var url = "/api/FuelOrderPricings/" + fuelOrderPricingsData.Volume;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrderPricingsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.getListOfSupplierPricings = function (onSuccess, onError) {

    var url = "/api/FuelOrderPricings";

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

degatech.services.fuelOrderPricings.deleteSupplierPricing = function (id, onSuccess, onError) {

    var url = "/api/FuelOrderPricings/" + id;

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

degatech.services.fuelOrderPricings.getQuoteForLocation = function (adminClientId, custClientId, icao, tailNumber, onSuccess, onError) {

    var url = "/api/FuelOrderPricings/GetQuoteForLocation";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: {
            AdminClientID: adminClientId,
            CustClientID: custClientId,
            ICAO: icao,
            TailNumber: tailNumber
        }
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.getQuoteForLocationForAllVendors = function(adminClientId, custClientId, icao, tailNumber, onSuccess, onError) {

    var url = "/api/FuelOrderPricings/GetQuoteForLocationForAllVendors";

    var settings = {
        cache: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        data: {
            AdminClientID: adminClientId,
            CustClientID: custClientId,
            ICAO: icao,
            TailNumber: tailNumber
        },
        dataType: "json",
        success: onSuccess,
        error: onError,
        type: "POST"
    };

    $.ajax(url, settings);
}

/////////////////////CUSTOMER PRICINGS////////////////////////////////////////////


degatech.services.fuelOrderPricings.addCustomerPricing = function (fuelOrderPricingsData, onSuccess, onError) {

    var url = "/api/FuelOrderCustomerPricings";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrderPricingsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.addCustomerPricings = function (fuelOrderPricingsData, onSuccess, onError) {

    var url = "/api/FuelOrderCustomerPricings/List";

    var settings = {
        cache: false
        , contentType: "application/json"
        , data: JSON.stringify(fuelOrderPricingsData)
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.updateCustomerPricing = function (id, fuelOrderPricingsData, onSuccess, onError) {

    var url = "/api/FuelOrderCustomerPricings/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrderPricingsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "PUT"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.getFuelOrderCustomerPricingByFuelOrderId = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrderCustomerPricings/" + fuelOrderId;

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

degatech.services.fuelOrderPricings.getCustomerInvoice = function (fuelOrderPricingsData, onSuccess, onError) {

    var url = "/api/FuelOrderCustomerPricings/" + fuelOrderPricingsData.Volume;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: fuelOrderPricingsData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}

degatech.services.fuelOrderPricings.getListOfCustomerPricings = function (onSuccess, onError) {

    var url = "/api/FuelOrderCustomerPricings";

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

degatech.services.fuelOrderPricings.deleteCustomerPricing = function (id, onSuccess, onError) {

    var url = "/api/FuelOrderCustomerPricings/" + id;

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


/////////////////////SUPPLIER PRICINGS////////////////////////////////////////////

degatech.services.fuelOrderPricings.getFuelOrderSupplierPricingByFuelOrderId = function (fuelOrderId, onSuccess, onError) {

    var url = "/api/FuelOrderSupplierPricings/" + fuelOrderId;

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
