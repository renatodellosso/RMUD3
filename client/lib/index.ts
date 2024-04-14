import * as signalR from "@microsoft/signalr";
import { MainPageClientAction } from "./transpiled/RMUD3.Server.clientactiondicts";

console.log("Starting client...");

const connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

connection.on("Send", (user, message) => {
    console.log(`Received message: ${message} from ${user}`);
});

connection.onclose(() => {
    console.log("Connection closed");
    window.onbeforeunload = null;
});

connection.start().then(() => {
    console.log("Connection started");

    window.onbeforeunload = () => true;

    //send("Ping", "Hello world");
    //send("Ping", ["a", "b"]);
    //send("Ping", { a: 1, b: 2 });
    send(MainPageClientAction.MainPage, "Hello world");
}).catch(err => console.error(err.toString()));

function send(action: number, args: any) {
    connection.invoke("Send", action, args).catch(err => console.error(err.toString()));
}