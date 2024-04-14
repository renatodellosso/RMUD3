import * as signalR from "@microsoft/signalr";

console.log("Starting client...");

const connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

connection.on("Receive", (user, message) => {
    console.log(`Received message: ${message} from ${user}`);
});

connection.start().then(() => {
    console.log("Connection started");

    window.onbeforeunload = () => true;

    connection.invoke("Send", "testUser", "testMessage").catch(err => console.error(err.toString()));
}).catch(err => console.error(err.toString()));