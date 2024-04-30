"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var Component = /** @class */ (function () {
    function Component(connectionManager, id, parent) {
        this.children = new Map();
        this.connection = connectionManager;
        this.id = id;
        this.parent = parent;
        this.children = new Map();
    }
    Component.prototype.getPath = function () {
        if (this.parent) {
            return this.parent.getPath().concat([this.id]);
        }
        return [];
    };
    Component.prototype.send = function (action, args) {
        this.connection.send(this.getPath(), action, args);
    };
    Component.prototype.handleServerAction = function (action, args) {
    };
    return Component;
}());
exports.default = Component;
//# sourceMappingURL=Component.js.map