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
var SidebarComponent = /** @class */ (function (_super) {
    __extends(SidebarComponent, _super);
    function SidebarComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SidebarComponent.prototype.enable = function (args) {
        var _this = this;
        console.log("Sidebar enabled");
        var div = document.createElement("div");
        div.id = this.id;
        div.className = "w-20% border-l border-white p-1";
        document.getElementById("root").appendChild(div);
        this.updateInterval = setInterval((function () { return _this.send(RMUD3_Server_Components_GamePage_1.SidebarClientAction.RequestUpdate); }), 100);
    };
    SidebarComponent.prototype.disable = function () {
        clearInterval(this.updateInterval);
    };
    SidebarComponent.prototype.handleServerAction = function (action, args) {
        switch (action) {
            case RMUD3_Server_Components_GamePage_1.SidebarServerAction.Update:
                var data = args;
                var div = document.getElementById(this.id);
                div.innerHTML = "";
                var playerName = document.createElement("div");
                playerName.className = "text-white";
                playerName.innerText = data.playerName;
                div.appendChild(playerName);
                var locationName = document.createElement("div");
                locationName.className = "text-white";
                locationName.innerText = data.locationName;
                div.appendChild(locationName);
                var creatures = document.createElement("div");
                creatures.className = "text-white mt-4";
                creatures.innerText = "Creatures:";
                div.appendChild(creatures);
                var creaturesList = document.createElement("ul");
                creaturesList.className = "text-white";
                for (var _i = 0, _a = data.creatures; _i < _a.length; _i++) {
                    var creature = _a[_i];
                    var creatureItem = document.createElement("li");
                    creatureItem.innerText = creature;
                    creaturesList.appendChild(creatureItem);
                }
        }
    };
    return SidebarComponent;
}(Component_1.default));
exports.default = SidebarComponent;
//# sourceMappingURL=SidebarComponent.js.map