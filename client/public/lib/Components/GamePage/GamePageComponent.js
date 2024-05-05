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
var GamePageComponent = /** @class */ (function (_super) {
    __extends(GamePageComponent, _super);
    function GamePageComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    GamePageComponent.prototype.enable = function (args) {
        console.log("Game page enabled");
        var div = document.createElement("div");
        div.id = this.id;
        div.className = "w-screen h-screen flex flex-row";
        document.getElementsByTagName("body")[0].appendChild(div);
        var mainPane = document.createElement("div");
        mainPane.id = "main-pane";
        mainPane.className = "w-[80%] border-r border-white flex flex-col";
        div.appendChild(mainPane);
    };
    GamePageComponent.prototype.disable = function () {
        document.getElementById(this.id).remove();
    };
    return GamePageComponent;
}(Component_1.default));
exports.default = GamePageComponent;
//# sourceMappingURL=GamePageComponent.js.map