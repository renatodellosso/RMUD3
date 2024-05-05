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
var RMUD3_Server_Components_GamePage_1 = require("../../transpiled/RMUD3.Server.Components.GamePage");
var LogComponent = /** @class */ (function (_super) {
    __extends(LogComponent, _super);
    function LogComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    LogComponent.prototype.enable = function (args) {
        console.log("Log enabled");
        var div = document.createElement("div");
        div.id = this.id;
        div.className = "w-80% border-white p-1 flex-1";
        document.getElementById("main-pane").appendChild(div);
    };
    LogComponent.prototype.disable = function () {
    };
    LogComponent.prototype.handleServerAction = function (action, args) {
        switch (action) {
            case RMUD3_Server_Components_GamePage_1.LogServerAction.SendMsg:
                var div = document.createElement("div");
                div.className = "text-white";
                div.innerText = args;
                document.getElementById(this.id).appendChild(div);
                break;
        }
    };
    return LogComponent;
}(Component_1.default));
exports.default = LogComponent;
//# sourceMappingURL=LogComponent.js.map