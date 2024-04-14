"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var signalR = require("@microsoft/signalr");
var RMUD3_Server_clientactiondicts_1 = require("./transpiled/RMUD3.Server.clientactiondicts");
console.log("Starting client...");
var connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();
connection.on("Send", function (user, message) {
    console.log("Received message: ".concat(message, " from ").concat(user));
});
connection.onclose(function () {
    console.log("Connection closed");
    window.onbeforeunload = null;
});
connection.start().then(function () {
    console.log("Connection started");
    window.onbeforeunload = function () { return true; };
    //send("Ping", "Hello world");
    //send("Ping", ["a", "b"]);
    //send("Ping", { a: 1, b: 2 });
    send(RMUD3_Server_clientactiondicts_1.MainPageClientAction.MainPage, "Hello world");
}).catch(function (err) { return console.error(err.toString()); });
function send(action, args) {
    connection.invoke("Send", action, args).catch(function (err) { return console.error(err.toString()); });
}
//# sourceMappingURL=index.js.map