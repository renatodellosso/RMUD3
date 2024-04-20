import Component from "../Component";
import { SignInPageClientAction, SignInPageServerAction } from "../transpiled/RMUD3.Server.Components";
import { sha256 } from "../utils";

export default class SignInPageComponent extends Component {
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
        signInForm.className = "w-full flex flex-col";
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
        signInButton.onclick = async (e) => {
            e.preventDefault();

            if (usernameInput.value === "" || passwordInput.value === "") {
                signInErrorLabel.innerText = "Username and password cannot be empty";
                return;
            }

            this.send(SignInPageClientAction.SignIn, {
                Username: usernameInput.value,
                Password: await sha256(passwordInput.value)
            });
        };
        signInForm.appendChild(signInButton);

        signInForm.appendChild(signInErrorLabel);

        // Create create account form
        const createAccountForm = document.createElement("form");
        createAccountForm.className = "w-full flex flex-col";
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
        createAccountButton.onclick = async (e) => {
            e.preventDefault();

            if (newUsernameInput.value === "" || newPasswordInput.value === "") {
                createAccountErrorLabel.innerText = "Username and password cannot be empty";
                return;
            }

            if (newPasswordInput.value !== confirmPasswordInput.value) {
                createAccountErrorLabel.innerText = "Passwords do not match";
                return;
            }

            this.send(SignInPageClientAction.CreateAccount, {
                Username: newUsernameInput.value,
                Password: await sha256(newPasswordInput.value),
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
            case SignInPageServerAction.SignInError:
                document.getElementById("signInErrorLabel").innerHTML = args;
                break;
            case SignInPageServerAction.CreateAccountError:
                document.getElementById("createAccountErrorLabel").innerHTML = args;
                break;
            default:
                console.error("Unknown action:", action);
        }
    }
}