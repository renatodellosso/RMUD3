"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.SignInPageServerAction = exports.SignInPageClientAction = exports.MainMenuClientAction = void 0;
/** Transpiled from RMUD3.Server.Components.MainMenuClientAction */
var MainMenuClientAction;
(function (MainMenuClientAction) {
    MainMenuClientAction[MainMenuClientAction["NewGame"] = 0] = "NewGame";
    MainMenuClientAction[MainMenuClientAction["LoadGame"] = 1] = "LoadGame";
})(MainMenuClientAction || (exports.MainMenuClientAction = MainMenuClientAction = {}));
/** Transpiled from RMUD3.Server.Components.SignInPageClientAction */
var SignInPageClientAction;
(function (SignInPageClientAction) {
    SignInPageClientAction[SignInPageClientAction["SignIn"] = 0] = "SignIn";
    SignInPageClientAction[SignInPageClientAction["CreateAccount"] = 1] = "CreateAccount";
})(SignInPageClientAction || (exports.SignInPageClientAction = SignInPageClientAction = {}));
/** Transpiled from RMUD3.Server.Components.SignInPageServerAction */
var SignInPageServerAction;
(function (SignInPageServerAction) {
    SignInPageServerAction[SignInPageServerAction["SignInError"] = 0] = "SignInError";
    SignInPageServerAction[SignInPageServerAction["CreateAccountError"] = 1] = "CreateAccountError";
})(SignInPageServerAction || (exports.SignInPageServerAction = SignInPageServerAction = {}));
//# sourceMappingURL=RMUD3.Server.Components.js.map