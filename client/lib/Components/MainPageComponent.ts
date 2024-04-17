﻿import Component from "../Component";
import { MainPageClientAction, MainPageServerAction } from "../transpiled/RMUD3.Server.Components";

export default class MainPageComponent extends Component {
    enable(args?: any) {
        console.log("Main page enabled");

        const page = document.createElement("div");
        page.id = this.id;
        page.className = "w-full h-screen flex flex-col justify-center items-center";
        document.getElementsByTagName("body")[0].appendChild(page);

        const centerDiv = document.createElement("div");
        centerDiv.className = "w-1/4";
        page.appendChild(centerDiv);

        // Title
        const title = document.createElement("h1");
        title.innerHTML = "RMUD3";
        title.className = "w-full text-center mb-4";
        centerDiv.appendChild(title);

        // Side by side forms
        const forms = document.createElement("div");
        forms.className = "flex justify-between";
        centerDiv.appendChild(forms);

        // Create sign in form
        const signInForm = document.createElement("form");
        signInForm.className = "flex flex-col";
        forms.appendChild(signInForm);

        const usernameInput = document.createElement("input");
        usernameInput.type = "text";
        usernameInput.placeholder = "Username";
        signInForm.appendChild(usernameInput);

        const passwordInput = document.createElement("input");
        passwordInput.type = "password";
        passwordInput.placeholder = "Password";
        signInForm.appendChild(passwordInput);

        const signInErrorLabel = document.createElement("label");
        signInErrorLabel.style.color = "red";
        signInErrorLabel.id = "signInErrorLabel";

        const signInButton = document.createElement("button");
        signInButton.innerHTML = "Sign In";
        signInButton.onclick = (e) => {
            e.preventDefault();

            this.send(MainPageClientAction.SignIn, {
                username: usernameInput.value,
                password: passwordInput.value
            });
        };
        signInForm.appendChild(signInButton);

        signInForm.appendChild(signInErrorLabel);

        // Create create account form
        const createAccountForm = document.createElement("form");
        createAccountForm.className = "flex flex-col";
        forms.appendChild(createAccountForm);

        const newUsernameInput = document.createElement("input");
        newUsernameInput.type = "text";
        newUsernameInput.placeholder = "Username";
        createAccountForm.appendChild(newUsernameInput);

        const newPasswordInput = document.createElement("input");
        newPasswordInput.type = "password";
        newPasswordInput.placeholder = "Password";
        createAccountForm.appendChild(newPasswordInput);

        const confirmPasswordInput = document.createElement("input");
        confirmPasswordInput.type = "password";
        confirmPasswordInput.placeholder = "Confirm Password";
        createAccountForm.appendChild(confirmPasswordInput);

        const createAccountErrorLabel = document.createElement("label");
        createAccountErrorLabel.style.color = "red";
        createAccountErrorLabel.style.height = "8px";
        createAccountErrorLabel.id = "createAccountErrorLabel";

        const createAccountButton = document.createElement("button");
        createAccountButton.innerHTML = "Create Account";
        createAccountButton.onclick = (e) => {
            e.preventDefault();

            if (newPasswordInput.value !== confirmPasswordInput.value) {
                console.error("Passwords do not match");
                return;
            }

            this.send(MainPageClientAction.CreateAccount, {
                username: newUsernameInput.value,
                password: newPasswordInput.value,
                confirmPassword: confirmPasswordInput.value
            });
        };
        createAccountForm.appendChild(createAccountButton);

        createAccountForm.appendChild(createAccountErrorLabel);
    }

    disable() {
        document.getElementById(this.id).remove();
    }

    action(action: number, args?: any) {
        switch (action) {
            case MainPageServerAction.SignInError:
                document.getElementById("signInErrorLabel").innerHTML = args;
                break;
            case MainPageServerAction.CreateAccountError:
                document.getElementById("createAccountErrorLabel").innerHTML = args;
                break;
            default:
                console.error("Unknown action:", action);
        }
    }
}