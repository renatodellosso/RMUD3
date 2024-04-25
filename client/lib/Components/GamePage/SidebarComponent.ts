import Component from "../../Component";

export default class SidebarComponent extends Component {
    enable(args?: any): void {
        console.log("Sidebar enabled");

        const div = document.createElement("div");
        div.id = this.id;
        div.className = "w-20% border-l border-white";
        document.getElementById("root").appendChild(div);
    }

    disable(): void {
    }
}