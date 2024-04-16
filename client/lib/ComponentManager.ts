import Component from "./Component";
import MainPageComponent from "./Components/MainPageComponent";
import ConnectionManager from "./ConnectionManager";

export default class ComponentManager {
    componentMap: { [id: string]: (ConnectionManager: ConnectionManager, id: string, parent?: Component, args?: any) => Component } = {
        MainPageComponent: (connectionManager, id, parent) => new MainPageComponent(connectionManager, id, parent)
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

        this.rootComponent = this.componentMap[type](connectionManager, "root", undefined, args);
        this.rootComponent?.disable();
        this.rootComponent = this.componentMap[type](connectionManager, "root", undefined, args);
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
        component.action(action, args);
    }
}