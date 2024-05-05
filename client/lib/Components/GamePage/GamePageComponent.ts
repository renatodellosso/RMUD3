import Component from "../../Component";

export default class GamePageComponent extends Component {
    enable(args?: any): void {
        console.log("Game page enabled");

        const div = document.createElement("div");
        div.id = this.id;
        div.className = "w-screen h-screen flex flex-row";
        document.getElementsByTagName("body")[0].appendChild(div);

        const mainPane = document.createElement("div");
        mainPane.id = "main-pane";
        mainPane.className = "w-[80%] border-r border-white flex flex-col";
        div.appendChild(mainPane);
    }

    disable(): void {
        document.getElementById(this.id).remove();
    }

}