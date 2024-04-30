import Component from "../../Component";
import { SidebarClientAction, SidebarData, SidebarServerAction } from "../../transpiled/RMUD3.Server.Components.GamePage";

export default class SidebarComponent extends Component {
    updateInterval: number;

    enable(args?: any): void {
        console.log("Sidebar enabled");

        const div = document.createElement("div");
        div.id = this.id;
        div.className = "w-20% border-l border-white p-1";
        document.getElementById("root").appendChild(div);

        this.updateInterval = setInterval((() => this.send(SidebarClientAction.RequestUpdate)) as TimerHandler, 100);
    }

    disable(): void {
        clearInterval(this.updateInterval);
    }

    handleServerAction(action: SidebarServerAction, args?: any): void {
        switch (action) {
            case SidebarServerAction.Update:
                const data = args as SidebarData;
                const div = document.getElementById(this.id);

                div.innerHTML = "";

                const playerName = document.createElement("div");
                playerName.className = "text-white";
                playerName.innerText = data.playerName;
                div.appendChild(playerName);

                const locationName = document.createElement("div");
                locationName.className = "text-white";
                locationName.innerText = data.locationName;
                div.appendChild(locationName);

                const creatures = document.createElement("div");
                creatures.className = "text-white mt-4";
                creatures.innerText = "Creatures:";
                div.appendChild(creatures);

                const creaturesList = document.createElement("ul");
                creaturesList.className = "text-white";
                for (const creature of data.creatures) {
                    const creatureItem = document.createElement("li");
                    creatureItem.innerText = creature;
                    creaturesList.appendChild(creatureItem);
                }
        }
    }
}