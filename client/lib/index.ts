import ComponentManager from "./ComponentManager";
import ConnectionManager from "./ConnectionManager";

console.log("Starting client...");

const componentManager = new ComponentManager();
const connectionManager = new ConnectionManager(componentManager);
connectionManager.start();