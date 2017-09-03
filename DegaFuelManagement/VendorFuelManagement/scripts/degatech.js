var degatech = {
    layout: {},
    page: {
        handlers: {},
        startUp: null
    },
    services: {},
    ui: {
        notifications: {},
        startUp: null
    }
};


degatech.layout.startUp = function () {

    //this does a null check on degatech.page.startUp
    if (degatech.page.startUp) {
        console.log("degatech.page.startup");
        degatech.page.startUp();
    }
};
$(document).ready(degatech.layout.startUp);