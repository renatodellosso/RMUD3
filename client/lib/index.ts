import * as signalR from "@microsoft/signalr";

console.log("Starting client...");

const connection = new signalR.HubConnectionBuilder().withUrl("/testHub").build();

connection.on("ReceiveMessage", (user, message) => {
    console.log(`Received message: ${message} from ${user}`);
});

connection.start().then(() => {
    console.log("Connection started");
    connection.invoke("SendMessage", "testUser", "testMessage").catch(err => console.error(err.toString()));
}).catch(err => console.error(err.toString()));