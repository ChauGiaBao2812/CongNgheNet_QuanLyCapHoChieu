// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
async function hashPassword(password) {
    const encoder = new TextEncoder();
    const data = encoder.encode(password);
    const hashBuffer = await crypto.subtle.digest("SHA-256", data);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    return hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
}

document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("createAccountForm");

    form.addEventListener("submit", async function (e) {
        const passwordInput = document.querySelector("input[name='Password']");
        const rawPassword = passwordInput.value;

        // Only hash if it's not already hashed
        if (rawPassword && rawPassword.length < 64) {
            e.preventDefault(); // Stop default form submission

            const hashed = await hashPassword(rawPassword);
            passwordInput.value = hashed;

            form.submit(); // Re-submit the form with the hashed password
        }
    });
});
