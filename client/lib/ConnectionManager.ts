import signalR = require("@microsoft/signalr");
import ComponentManager from "./ComponentManager";

export default class ConnectionManager {

    private componentManager: ComponentManager;
    private connection: signalR.HubConnection;

    constructor(componentManager: ComponentManager) {
        this.componentManager = componentManager;

        this.connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

        this.connection.on("Action", (path: string[], action: number, args?: any) => {
            console.log(`Received action ${action} for component:`, path, "Args:", args);

            this.componentManager.action(path, action, args);
        });

        this.connection.on("EnableComponent", (path: string[], type: string, args?: any) => this.componentManager.enableComponent(this, path, type, args))
        this.connection.on("DisableComponent", (path: string[]) => this.componentManager.disableComponent(path));

        this.connection.onclose(() => {
            console.log("Connection closed");
            window.onbeforeunload = null;
        });
    }

    start() {
        this.connection.start().then(() => {
            console.log("Connection started");

            window.onbeforeunload = () => true;
        }).catch(err => console.error(err.toString()));
    }

    send(componentPath: string[], action: number, args: any) {
        this.connection.invoke("Action", componentPath, action, args).catch(err => console.error(err.toString()));
    }
}