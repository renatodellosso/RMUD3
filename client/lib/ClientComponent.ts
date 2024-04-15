export default class ClientComponent {

    id: string;
    parent: ClientComponent | undefined;

    constructor(id: string, parent?: ClientComponent | undefined) {
        this.id = id;
        this.parent = parent;
    }

    getPath(): string[] {
        if (this.parent) {
            return this.parent.getPath().concat([this.id]);
        }
        return [];
    }
}