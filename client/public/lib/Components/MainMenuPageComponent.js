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
var Component_1 = require("../Component");
var MainMenuPageComponent = /** @class */ (function (_super) {
    __extends(MainMenuPageComponent, _super);
    function MainMenuPageComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MainMenuPageComponent.prototype.enable = function (args) {
        var data = args;
        console.log("Main menu page enabled");
        var page = document.createElement("div");
        page.id = this.id;
        page.className = "w-full h-screen flex flex-col";
        document.getElementsByTagName("body")[0].appendChild(page);
        var title = document.createElement("h1");
        title.innerText = "Welcome back ".concat(data.username);
        title.className = "text-xl border-b border-white";
        page.appendChild(title);
        var horizontalSplit = document.createElement("div");
        horizontalSplit.className = "h-full flex flex-row";
        page.appendChild(horizontalSplit);
        var sidebar = document.createElement("div");
        sidebar.className = "w-[10%] border-r border-white";
        horizontalSplit.appendChild(sidebar);
        var playButton = document.createElement("button");
        playButton.innerText = "Play";
        playButton.className = "w-full";
        sidebar.appendChild(playButton);
        var news = document.createElement("div");
        news.className = "w-[90%] p-1";
        horizontalSplit.appendChild(news);
        var newsTitle = document.createElement("h2");
        newsTitle.innerText = "News";
        newsTitle.className = "text-lg";
        news.appendChild(newsTitle);
        // Add news
        for (var _i = 0, _a = data.news; _i < _a.length; _i++) {
            var item = _a[_i];
            var element = document.createElement("div");
            element.className = "border border-white p-1 mb-1";
            news.appendChild(element);
            var title_1 = document.createElement("h3");
            title_1.innerText = item.title;
            title_1.className = "text-lg";
            element.appendChild(title_1);
            var content = document.createElement("p");
            content.innerText = "".concat(item.date.toLocaleString(), "\n").concat(item.content);
            element.appendChild(content);
        }
    };
    MainMenuPageComponent.prototype.disable = function () {
        document.getElementById(this.id).remove();
    };
    return MainMenuPageComponent;
}(Component_1.default));
exports.default = MainMenuPageComponent;
//# sourceMappingURL=MainMenuPageComponent.js.map