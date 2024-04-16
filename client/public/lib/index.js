"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ComponentManager_1 = require("./ComponentManager");
var ConnectionManager_1 = require("./ConnectionManager");
console.log("Starting client...");
var componentManager = new ComponentManager_1.default();
var connectionManager = new ConnectionManager_1.default(componentManager);
connectionManager.start();
//# sourceMappingURL=index.js.map