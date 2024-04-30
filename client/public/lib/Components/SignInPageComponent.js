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
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (g && (g = 0, op[0] && (_ = 0)), _) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var Component_1 = require("../Component");
var RMUD3_Server_Components_1 = require("../transpiled/RMUD3.Server.Components");
var utils_1 = require("../utils");
var SignInPageComponent = /** @class */ (function (_super) {
    __extends(SignInPageComponent, _super);
    function SignInPageComponent() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    SignInPageComponent.prototype.enable = function (args) {
        var _this = this;
        console.log("Main page enabled");
        var page = document.createElement("div");
        page.id = this.id;
        page.className = "w-full h-screen flex flex-col justify-center items-center";
        document.getElementsByTagName("body")[0].appendChild(page);
        var centerDiv = document.createElement("div");
        centerDiv.className = "w-1/4";
        page.appendChild(centerDiv);
        // Title
        var title = document.createElement("h1");
        title.innerHTML = "RMUD3";
        title.className = "w-full text-center mb-4";
        centerDiv.appendChild(title);
        // Side by side forms
        var forms = document.createElement("div");
        forms.className = "flex justify-between";
        centerDiv.appendChild(forms);
        // Create sign in form
        var signInForm = document.createElement("form");
        signInForm.className = "w-full flex flex-col";
        forms.appendChild(signInForm);
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
        signInErrorLabel.id = "signInErrorLabel";
        var signInButton = document.createElement("button");
        signInButton.innerHTML = "Sign In";
        signInButton.onclick = function (e) { return __awaiter(_this, void 0, void 0, function () {
            var _a, _b;
            var _c;
            return __generator(this, function (_d) {
                switch (_d.label) {
                    case 0:
                        e.preventDefault();
                        if (usernameInput.value === "" || passwordInput.value === "") {
                            signInErrorLabel.innerText = "Username and password cannot be empty";
                            return [2 /*return*/];
                        }
                        _a = this.send;
                        _b = [RMUD3_Server_Components_1.SignInPageClientAction.SignIn];
                        _c = {
                            Username: usernameInput.value
                        };
                        return [4 /*yield*/, (0, utils_1.sha256)(passwordInput.value)];
                    case 1:
                        _a.apply(this, _b.concat([(_c.Password = _d.sent(),
                                _c)]));
                        return [2 /*return*/];
                }
            });
        }); };
        signInForm.appendChild(signInButton);
        signInForm.appendChild(signInErrorLabel);
        // Create create account form
        var createAccountForm = document.createElement("form");
        createAccountForm.className = "w-full flex flex-col";
        forms.appendChild(createAccountForm);
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
        createAccountErrorLabel.style.height = "8px";
        createAccountErrorLabel.id = "createAccountErrorLabel";
        var createAccountButton = document.createElement("button");
        createAccountButton.innerHTML = "Create Account";
        createAccountButton.onclick = function (e) { return __awaiter(_this, void 0, void 0, function () {
            var _a, _b;
            var _c;
            return __generator(this, function (_d) {
                switch (_d.label) {
                    case 0:
                        e.preventDefault();
                        if (newUsernameInput.value === "" || newPasswordInput.value === "") {
                            createAccountErrorLabel.innerText = "Username and password cannot be empty";
                            return [2 /*return*/];
                        }
                        if (newPasswordInput.value !== confirmPasswordInput.value) {
                            createAccountErrorLabel.innerText = "Passwords do not match";
                            return [2 /*return*/];
                        }
                        _a = this.send;
                        _b = [RMUD3_Server_Components_1.SignInPageClientAction.CreateAccount];
                        _c = {
                            Username: newUsernameInput.value
                        };
                        return [4 /*yield*/, (0, utils_1.sha256)(newPasswordInput.value)];
                    case 1:
                        _a.apply(this, _b.concat([(_c.Password = _d.sent(),
                                _c)]));
                        return [2 /*return*/];
                }
            });
        }); };
        createAccountForm.appendChild(createAccountButton);
        createAccountForm.appendChild(createAccountErrorLabel);
    };
    SignInPageComponent.prototype.disable = function () {
        document.getElementById(this.id).remove();
    };
    SignInPageComponent.prototype.handleServerAction = function (action, args) {
        switch (action) {
            case RMUD3_Server_Components_1.SignInPageServerAction.SignInError:
                document.getElementById("signInErrorLabel").innerHTML = args;
                break;
            case RMUD3_Server_Components_1.SignInPageServerAction.CreateAccountError:
                document.getElementById("createAccountErrorLabel").innerHTML = args;
                break;
            default:
                console.error("Unknown action:", action);
        }
    };
    return SignInPageComponent;
}(Component_1.default));
exports.default = SignInPageComponent;
//# sourceMappingURL=SignInPageComponent.js.map