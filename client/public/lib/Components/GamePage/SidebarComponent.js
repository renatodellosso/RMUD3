"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var Component_1 = require("../../Component");
var SidebarComponent = /** @class */ (function (_super) {
    __extends(SidebarComponent, _super);
    function SidebarComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SidebarComponent.prototype.enable = function (args) {
        console.log("Sidebar enabled");
        var div = document.createElement("div");
        div.id = this.id;
        div.className = "w-20% border-l border-white";
        document.getElementById("root").appendChild(div);
    };
    SidebarComponent.prototype.disable = function () {
    };
    return SidebarComponent;
}(Component_1.default));
exports.default = SidebarComponent;
//# sourceMappingURL=SidebarComponent.js.map