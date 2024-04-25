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
        this.connection.on("SetSessionId", function (sessionId) { return sessionId && localStorage.setItem("sessionId", sessionId); });
        this.connection.onclose(function () {
            console.log("Connection closed");
            window.onbeforeunload = null;
        });
    }
    ConnectionManager.prototype.start = function () {
        var _this = this;
        this.connection.start().then(function () {
            var _a;
            console.log("Connection started");
            _this.connection.invoke("ClientConnect", (_a = localStorage.getItem("sessionId")) !== null && _a !== void 0 ? _a : "");
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