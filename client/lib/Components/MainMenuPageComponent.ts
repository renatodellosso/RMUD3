import Component from "../Component";
import { MainMenuClientAction, MainMenuEnableData } from "../transpiled/RMUD3.Server.Components";
import { parseDate } from "../utils";

export default class MainMenuPageComponent extends Component {
    enable(args?: any): void {
        const data = args as MainMenuEnableData;

        console.log("Main menu page enabled");

        const page = document.createElement("div");
        page.id = this.id;
        page.className = "w-full h-screen flex flex-col";
        document.getElementsByTagName("body")[0].appendChild(page);

        const title = document.createElement("h1");
        title.innerText = `Welcome back, ${data.username}.`;
        title.className = "text-xl border-b border-white";
        page.appendChild(title);

        const horizontalSplit = document.createElement("div");
        horizontalSplit.className = "h-full flex flex-row";
        page.appendChild(horizontalSplit);

        const sidebar = document.createElement("div");
        sidebar.className = "w-[10%] border-r border-white";
        horizontalSplit.appendChild(sidebar);

        // Add buttons to the sidebar
        const newGameButton = document.createElement("button");
        newGameButton.innerText = "New Game";
        newGameButton.className = "w-full";
        newGameButton.onclick = () => this.send(MainMenuClientAction.NewGame);
        sidebar.appendChild(newGameButton);

        for (const player of data.players) {
            const loadGameButton = document.createElement("button");
            loadGameButton.innerText = `Load: ${player.location} - Last Played: ${parseDate(player.lastPlayed).toLocaleString()}`;
            loadGameButton.className = "w-full";
            loadGameButton.onclick = () => this.send(MainMenuClientAction.LoadGame, player.id);
            sidebar.appendChild(loadGameButton);
        }

        const news = document.createElement("div");
        news.className = "w-[90%] p-1";
        horizontalSplit.appendChild(news);

        const newsTitle = document.createElement("h2");
        newsTitle.innerText = "News";
        newsTitle.className = "text-lg";
        news.appendChild(newsTitle);

        // Add news
        for (const item of data.news) {
            const element = document.createElement("div");
            element.className = "border border-white p-1 mb-1";
            news.appendChild(element);

            const title = document.createElement("h3");
            title.innerText = item.title;
            title.className = "text-lg";
            element.appendChild(title);

            const content = document.createElement("p");
            content.innerText = `${parseDate(item.date).toLocaleString()}\n${item.content}`;
            element.appendChild(content);
        }
    }

    disable(): void {
        document.getElementById(this.id).remove();
    }

}