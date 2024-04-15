"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ClientComponent = /** @class */ (function () {
    function ClientComponent(id, parent) {
        this.id = id;
        this.parent = parent;
    }
    ClientComponent.prototype.getPath = function () {
        if (this.parent) {
            return this.parent.getPath().concat([this.id]);
        }
        return [];
    };
    return ClientComponent;
}());
exports.default = ClientComponent;
//# sourceMappingURL=ClientComponent.js.map