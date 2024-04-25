import Component from "../../Component";

export default class GamePageComponent extends Component {
    enable(args?: any): void {
        console.log("Game page enabled");

        const div = document.createElement("div");
        div.id = this.id;
        document.appendChild(div);
    }

    disable(): void {
    }

}