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
var RMUD3_Server_Components_1 = require("../transpiled/RMUD3.Server.Components");
var MainPageComponent = /** @class */ (function (_super) {
    __extends(MainPageComponent, _super);
    function MainPageComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    MainPageComponent.prototype.enable = function (args) {
        var _this = this;
        console.log("Main page enabled");
        var div = document.createElement("div");
        document.getElementsByTagName("body")[0].appendChild(div);
        // Title
        var title = document.createElement("h1");
        title.innerHTML = "RMUD3";
        div.appendChild(title);
        // Create sign in form
        var signInForm = document.createElement("form");
        div.appendChild(signInForm);
        var usernameInput = document.createElement("input");
        usernameInput.type = "text";
        usernameInput.placeholder = "Username";
        signInForm.appendChild(usernameInput);
        var passwordInput = document.createElement("input");
        passwordInput.type = "password";
        passwordInput.placeholder = "Password";
        signInForm.appendChild(passwordInput);
        var signInErrorLabel = document.createElement("label");
        signInErrorLabel.style.color = "red";
        var signInButton = document.createElement("button");
        signInButton.innerHTML = "Sign In";
        signInButton.onclick = function () {
            _this.send(RMUD3_Server_Components_1.MainPageClientAction.SignIn, {
                username: usernameInput.value,
                password: passwordInput.value
            });
        };
        signInForm.appendChild(signInButton);
        signInForm.appendChild(signInErrorLabel);
        // Create create account form
        var createAccountForm = document.createElement("form");
        div.appendChild(createAccountForm);
        var newUsernameInput = document.createElement("input");
        newUsernameInput.type = "text";
        newUsernameInput.placeholder = "Username";
        createAccountForm.appendChild(newUsernameInput);
        var newPasswordInput = document.createElement("input");
        newPasswordInput.type = "password";
        newPasswordInput.placeholder = "Password";
        createAccountForm.appendChild(newPasswordInput);
        var confirmPasswordInput = document.createElement("input");
        confirmPasswordInput.type = "password";
        confirmPasswordInput.placeholder = "Confirm Password";
        createAccountForm.appendChild(confirmPasswordInput);
        var createAccountErrorLabel = document.createElement("label");
        createAccountErrorLabel.style.color = "red";
        var createAccountButton = document.createElement("button");
        createAccountButton.innerHTML = "Create Account";
        createAccountButton.onclick = function () {
            if (newPasswordInput.value !== confirmPasswordInput.value) {
                console.error("Passwords do not match");
                return;
            }
            _this.send(RMUD3_Server_Components_1.MainPageClientAction.CreateAccount, {
                username: newUsernameInput.value,
                password: newPasswordInput.value,
                confirmPassword: confirmPasswordInput.value
            });
        };
        createAccountForm.appendChild(createAccountButton);
        createAccountForm.appendChild(createAccountErrorLabel);
    };
    MainPageComponent.prototype.action = function (action, args) {
        switch (action) {
            default:
                console.error("Unknown action:", action);
        }
    };
    return MainPageComponent;
}(Component_1.default));
exports.default = MainPageComponent;
//# sourceMappingURL=MainPageComponent.js.map