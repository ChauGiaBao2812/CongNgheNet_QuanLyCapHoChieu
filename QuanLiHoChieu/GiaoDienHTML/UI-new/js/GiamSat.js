function logout() {
    if (confirm("Bạn có chắc chắn muốn đăng xuất?")) {
        window.location.href = "/login";
    }
}

function redirectToLogin() {
    window.location.href = "/login";
}