import ConnectionManager from "./ConnectionManager";

export default abstract class Component {

    private connection: ConnectionManager;

    id: string;

    parent: Component | undefined;
    children: Map<string, Component> = new Map<string, Component>();

    constructor(connectionManager: ConnectionManager, id: string, parent?: Component | undefined) {
        this.connection = connectionManager;
        this.id = id;
        this.parent = parent;
        this.children = new Map<string, Component>();
    }

    getPath(): string[] {
        if (this.parent) {
            return this.parent.getPath().concat([this.id]);
        }
        return [];
    }

    send(action: number, args?: any) {
        this.connection.send(this.getPath(), action, args);
    }

    abstract enable(args?: any): void;

    abstract disable(): void;

    action(action: number, args?: any) {

    }
}