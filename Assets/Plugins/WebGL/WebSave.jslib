mergeInto(LibraryManager.library, {
    SaveLevel: function(contents) {
        document.getElementById("Game-levelLoader").value = Pointer_stringify(contents);
    },
    ListenLevelChanged: function(object, callback) {
        object = Pointer_stringify(object);
        callback = Pointer_stringify(callback);
        document.getElementById("Game-levelLoader").addEventListener("change", function (evt) {
            console.log("WEB", evt.target.value);
            SendMessage(object, callback, evt.target.value);
        });
    }
});