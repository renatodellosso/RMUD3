"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var signalR = require("@microsoft/signalr");
console.log("Starting client...");
var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();
connection.on("Receive", function (user, message) {
    console.log("Received message: ".concat(message, " from ").concat(user));
});
connection.start().then(function () {
    console.log("Connection started");
    window.onbeforeunload = function () { return true; };
    connection.invoke("Send", "testUser", "testMessage").catch(function (err) { return console.error(err.toString()); });
}).catch(function (err) { return console.error(err.toString()); });
//# sourceMappingURL=index.js.map