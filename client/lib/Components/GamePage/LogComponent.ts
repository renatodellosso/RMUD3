import Component from "../../Component";
import { SidebarClientAction, SidebarData, SidebarServerAction } from "../../transpiled/RMUD3.Server.Components.GamePage";

export default class LogComponent extends Component {
    enable(args?: any): void {
        console.log("Log enabled");

        const div = document.createElement("div");
        div.id = this.id;
        div.innerText = "Log";
        div.className = "w-80% border-l border-white p-1";
        document.getElementById("main-pane").appendChild(div);
    }

    disable(): void {
    }
}