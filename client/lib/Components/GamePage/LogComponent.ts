import Component from "../../Component";
import { LogServerAction } from "../../transpiled/RMUD3.Server.Components.GamePage";

export default class LogComponent extends Component {
    enable(args?: any): void {
        console.log("Log enabled");

        const div = document.createElement("div");
        div.id = this.id;
        div.className = "w-80% border-white p-1 flex-1";
        document.getElementById("main-pane").appendChild(div);
    }

    disable(): void {
    }

    handleServerAction(action: LogServerAction, args?: any): void {
        switch (action) {
            case LogServerAction.SendMsg:
                const div = document.createElement("div");
                div.className = "text-white";
                div.innerText = args;
                document.getElementById(this.id).appendChild(div);
                break;
        }
    }
}