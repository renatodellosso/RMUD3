"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var GamePageComponent_1 = require("./Components/GamePage/GamePageComponent");
var LogComponent_1 = require("./Components/GamePage/LogComponent");
var SidebarComponent_1 = require("./Components/GamePage/SidebarComponent");
var MainMenuPageComponent_1 = require("./Components/MainMenuPageComponent");
var SignInPageComponent_1 = require("./Components/SignInPageComponent");
var ComponentManager = /** @class */ (function () {
    function ComponentManager() {
        this.componentMap = {
            SignInPageComponent: function (connectionManager, id, parent) { return new SignInPageComponent_1.default(connectionManager, id, parent); },
            MainMenuPageComponent: function (connectionManager, id, parent) { return new MainMenuPageComponent_1.default(connectionManager, id, parent); },
            GamePageComponent: function (connectionManager, id, parent) { return new GamePageComponent_1.default(connectionManager, id, parent); },
            SidebarComponent: function (connectionManager, id, parent) { return new SidebarComponent_1.default(connectionManager, id, parent); },
            LogComponent: function (connectionManager, id, parent) { return new LogComponent_1.default(connectionManager, id, parent); },
        };
        this.rootComponent = null;
    }
    ComponentManager.prototype.enableComponent = function (connectionManager, path, type, args) {
        var _a, _b;
        console.log("Enabling component ".concat(path, " of type ").concat(type));
        if (path.length > 0) {
            var parent_1 = this.rootComponent;
            for (var i = 0; i < path.length - 1; i++) {
                parent_1 = parent_1 === null || parent_1 === void 0 ? void 0 : parent_1.children[path[i]];
            }
            var id = path[path.length - 1];
            (_a = parent_1.children[id]) === null || _a === void 0 ? void 0 : _a.disable();
            parent_1.children[id] = this.componentMap[type](connectionManager, id, parent_1, args);
            parent_1.children[id].enable(args);
            return;
        }
        console.log("Root component is being changed!");
        (_b = this.rootComponent) === null || _b === void 0 ? void 0 : _b.disable();
        var component = this.componentMap[type];
        if (!component) {
            console.error("Component type ".concat(type, " not found"));
            return;
        }
        this.rootComponent = component(connectionManager, "root", undefined, args);
        this.rootComponent.enable(args);
    };
    ComponentManager.prototype.disableComponent = function (path) {
        console.log("Disabling component ".concat(path.join("/")));
    };
    ComponentManager.prototype.action = function (path, action, args) {
        // Get the component
        var component = this.rootComponent;
        for (var i = 0; i < path.length; i++) {
            component = component === null || component === void 0 ? void 0 : component.children[path[i]];
        }
        if (!component) {
            console.error("Component ".concat(path.join("/"), " not found"));
            return;
        }
        // Call the action
        component.handleServerAction(action, args);
    };
    return ComponentManager;
}());
exports.default = ComponentManager;
//# sourceMappingURL=ComponentManager.js.map