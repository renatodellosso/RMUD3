"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var signalR = require("@microsoft/signalr");
var ConnectionManager = /** @class */ (function () {
    function ConnectionManager(componentManager) {
        var _this = this;
        this.componentManager = componentManager;
        this.connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();
        this.connection.on("Action", function (path, action, args) {
            console.log("Received action ".concat(action, " for component:"), path, "Args:", args);
            _this.componentManager.action(path, action, args);
        });
        this.connection.on("EnableComponent", function (path, type, args) { return _this.componentManager.enableComponent(_this, path, type, args); });
        this.connection.on("DisableComponent", function (path) { return _this.componentManager.disableComponent(path); });
        this.connection.onclose(function () {
            console.log("Connection closed");
            window.onbeforeunload = null;
        });
    }
    ConnectionManager.prototype.start = function () {
        this.connection.start().then(function () {
            console.log("Connection started");
            window.onbeforeunload = function () { return true; };
        }).catch(function (err) { return console.error(err.toString()); });
    };
    ConnectionManager.prototype.send = function (componentPath, action, args) {
        this.connection.invoke("Action", componentPath, action, args).catch(function (err) { return console.error(err.toString()); });
    };
    return ConnectionManager;
}());
exports.default = ConnectionManager;
//# sourceMappingURL=ConnectionManager.js.map