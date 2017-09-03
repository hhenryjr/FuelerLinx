if (!degatech.services.contacts) degatech.services.contacts = {};

degatech.services.contacts.addContact = function(contactData, onSuccess, onError) {

    var url = "/api/Contacts";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: contactData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings); 
}

degatech.services.contacts.updateContact = function(id, contactData, onSuccess, onError) {

    var url = "/api/Contacts/" + id;

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: contactData
        , dataType: "json" 
        , success: onSuccess     
        , error: onError       
        , type: "PUT"           
    };

    $.ajax(url, settings);           
}

degatech.services.contacts.getContact = function(id, onSuccess, onError) {

    var url = "/api/Contacts/" + id;

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

degatech.services.contacts.getListOfContacts = function(onSuccess, onError) {
    
    var url = "/api/Contacts";

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

degatech.services.contacts.getContactsByAdminClient = function(clientId, onSuccess, onError) {
    
    var url = "/api/Contacts/Admin/" + clientId;

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

degatech.services.contacts.getContactsByCustClient = function(clientId, onSuccess, onError) {
    
    var url = "/api/Contacts/Cust/" + clientId;

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

degatech.services.contacts.deleteContact = function(id, onSuccess, onError) {
    
    var url = "/api/Contacts/" + id;

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

degatech.services.contacts.exportContacts = function (exportData, onSuccess, onError) {

    var url = "/api/Contacts/Export/";

    var settings = {
        cache: false
        , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
        , data: exportData
        , dataType: "json"
        , success: onSuccess
        , error: onError
        , type: "POST"
    };

    $.ajax(url, settings);
}
