let loadingIconDiv = document.getElementById("loadingIconDiv");
let buttonTextDiv = document.getElementById("buttonTextDiv");
let loginButton = document.getElementById("loginButton");
async function loadingIcon() {
    await new Promise(r => setTimeout(r, 300));
    buttonTextDiv.style.display = "none";
    loadingIconDiv.style.display = "";
    loginButton.disabled
}