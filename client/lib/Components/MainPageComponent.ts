import Component from "../Component";
import { MainPageClientAction, MainPageServerAction } from "../transpiled/RMUD3.Server.Components";

export default class MainPageComponent extends Component {
    enable(args?: any) {
        console.log("Main page enabled");

        const div = document.createElement("div");
        document.getElementsByTagName("body")[0].appendChild(div);

        // Title
        const title = document.createElement("h1");
        title.innerHTML = "RMUD3";
        div.appendChild(title);

        // Create sign in form
        const signInForm = document.createElement("form");
        div.appendChild(signInForm);

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

        const signInButton = document.createElement("button");
        signInButton.innerHTML = "Sign In";
        signInButton.onclick = () => {
            this.send(MainPageClientAction.SignIn, {
                username: usernameInput.value,
                password: passwordInput.value
            });
        };
        signInForm.appendChild(signInButton);

        signInForm.appendChild(signInErrorLabel);

        // Create create account form
        const createAccountForm = document.createElement("form");
        div.appendChild(createAccountForm);

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

        const createAccountButton = document.createElement("button");
        createAccountButton.innerHTML = "Create Account";
        createAccountButton.onclick = () => {
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

    action(action: number, args?: any) {
        switch (action) {
            default:
                console.error("Unknown action:", action);
        }
    }
}