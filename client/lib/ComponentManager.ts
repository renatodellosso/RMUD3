import Component from "./Component";
import GamePageComponent from "./Components/GamePage/GamePageComponent";
import SidebarComponent from "./Components/GamePage/SidebarComponent";
import MainMenuPageComponent from "./Components/MainMenuPageComponent";
import SignInPageComponent from "./Components/SignInPageComponent";
import ConnectionManager from "./ConnectionManager";

export default class ComponentManager {
    componentMap: { [id: string]: (ConnectionManager: ConnectionManager, id: string, parent?: Component, args?: any) => Component } = {
        SignInPageComponent: (connectionManager, id, parent) => new SignInPageComponent(connectionManager, id, parent),
        MainMenuPageComponent: (connectionManager, id, parent) => new MainMenuPageComponent(connectionManager, id, parent),
        GamePageComponent: (connectionManager, id, parent) => new GamePageComponent(connectionManager, id, parent),
        SidebarComponent: (connectionManager, id, parent) => new SidebarComponent(connectionManager, id, parent),
    };

    rootComponent: Component | null = null;

    enableComponent(connectionManager: ConnectionManager, path: string[], type: string, args?: any) {
        console.log(`Enabling component ${path} of type ${type}`);

        if (path.length > 0) {
            let parent = this.rootComponent;
            for (let i = 0; i < path.length - 1; i++) {
                parent = parent?.children[path[i]];
            }

            const id = path[path.length - 1];
            parent.children[id]?.disable();
            parent.children[id] = this.componentMap[type](connectionManager, id, parent, args);
            parent.children[id].enable(args);
            return;
        }

        console.log("Root component is being changed!");

        this.rootComponent?.disable();

        const component = this.componentMap[type];
        if (!component) {
            console.error(`Component type ${type} not found`);
            return;
        }

        this.rootComponent = component(connectionManager, "root", undefined, args);
        this.rootComponent.enable(args);
    }

    disableComponent(path: string[]) {
        console.log(`Disabling component ${path.join("/")}`);
    }

    action(path: string[], action: number, args?: any) {
        // Get the component
        let component = this.rootComponent;
        for (let i = 0; i < path.length; i++) {
            component = component?.children[path[i]];
        }

        if (!component) {
            console.error(`Component ${path.join("/")} not found`);
            return;
        }

        // Call the action
        component.handleServerAction(action, args);
    }
}