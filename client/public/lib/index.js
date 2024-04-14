"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var signalR = require("@microsoft/signalr");
console.log("Starting client...");
var connection = new signalR.HubConnectionBuilder().withUrl("/testHub").build();
var x = {
    x: 0,
    z: 0,
    w: 0,
    other: {}
};
connection.on("ReceiveMessage", function (user, message) {
    console.log("Received message: ".concat(message, " from ").concat(user));
});
connection.start().then(function () {
    console.log("Connection started");
    connection.invoke("SendMessage", "testUser", "testMessage").catch(function (err) { return console.error(err.toString()); });
}).catch(function (err) { return console.error(err.toString()); });
//# sourceMappingURL=index.js.map