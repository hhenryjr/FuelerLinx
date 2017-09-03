function MasterClass() {
    console.log("Checking user login...");
    //Private Members
    var _HasFailedLoginCheck = false;

    //Public Members

    //Public Methods

    //Private Methods
    function checkIfUserIsStillLoggedIn() {
        $.ajax({
            type: "POST",
            url: "/api/master/check?session=readonly",
            dataType: "json",
            data: '{}',
            contentType: "application/json; charset=utf-8",
            success: onCheckIfUserIsStillLoggedInCompleted,
            error: onError
        });
    }

    function onError(error) {
        var test = '';
    }

    function onCheckIfUserIsStillLoggedInCompleted(result) {
        var IsLoggedIn = Boolean(result);
        if (!IsLoggedIn && _HasFailedLoginCheck) {
            $('#divSessionTimeout').modal('show');
        } else {
            _HasFailedLoginCheck = true;
            setTimeout(function() { checkIfUserIsStillLoggedIn(); }, 120000);
        }
    }

    //Constructor
    setTimeout(function () { checkIfUserIsStillLoggedIn(); }, 120000);
}

var Master = new MasterClass();