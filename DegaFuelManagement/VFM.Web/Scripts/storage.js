var storage = new function () {
    //Private Members
    var _This = this;

    //Public Methods
    this.GetCacheItem = function (key) {
        try {
            if (!IsStorageAvailble())
                return null;
            var item = localStorage.getItem(key);
            if (item == null)
                return null;
            try {
                return JSON.parse(item.toString());
            } catch (e) {
                return item.toString();
            }
        } catch (e) {
            return null;
        }
    };

    this.GetSessionItem = function (key) {
        try {
            if (!IsStorageAvailble())
                return null;
            var item = sessionStorage.getItem(key);
            if (item == null)
                return null;
            try {
                return JSON.parse(item.toString());
            } catch (e) {
                return item.toString();
            }
        } catch (e) {
            return null;
        }
    };

    this.SetCacheItem = function(key, value) {
        try {
            if (!IsStorageAvailble())
                return;
            if (typeof value === 'string')
                localStorage.setItem(key, value);
            else
                localStorage.setItem(key, JSON.stringify(value));
        } catch (e) {}
    };

    this.SetSessionItem = function(key, value) {
        try {
            if (!IsStorageAvailble())
                return;
            if (typeof value === 'string')
                sessionStorage.setItem(key, value);
            else
                sessionStorage.setItem(key, JSON.stringify(value));
        } catch (e) {}
    };

    this.ClearSession = function() {
        try {
            if (!IsStorageAvailble())
                return;
            sessionStorage.clear();
        } catch (e) {
            return null;
        }
    };

    //Private Methods
    function IsStorageAvailble() {
        return ((typeof (Storage) !== "undefined"));
    }
}

var storageHelper = new function () {
    //Private Members
    var _This = this;
    var _UserEmailSelectionKey = 'Cached_UserEmailSelection';

    //Public Members

    //Public Methods
    this.GetUserEmailSelection = function () {
        var value = storage.GetCacheItem(_UserEmailSelectionKey);
        if (value == null)
            return 0;
        return parseInt(storage.GetCacheItem(_UserEmailSelectionKey));
    };

    this.SetUserEmailSelection = function(value) {
        storage.SetCacheItem(_UserEmailSelectionKey, value.toString());
    };
};