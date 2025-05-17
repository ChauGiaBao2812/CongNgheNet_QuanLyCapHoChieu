document.addEventListener('DOMContentLoaded', function () {
    const togglePassword = document.getElementById('togglePassword');
    const passwordInput = document.getElementById('password');

    togglePassword.addEventListener('click', function () {
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', type);
        this.classList.toggle('fa-eye');
        this.classList.toggle('fa-eye-slash');
    });
    const loginForm = document.getElementById('loginForm');

    loginForm.addEventListener('submit', function (e) {
        e.preventDefault();

        document.getElementById('username-error').textContent = '';
        document.getElementById('password-error').textContent = '';

        const username = document.getElementById('username').value.trim();
        const password = document.getElementById('password').value.trim();
        let isValid = true;

        if (username === '') {
            document.getElementById('username-error').textContent = 'Vui lòng nhập tên đăng nhập';
            isValid = false;
        }

        if (password === '') {
            document.getElementById('password-error').textContent = 'Vui lòng nhập mật khẩu';
            isValid = false;
        } else if (password.length < 6) {
            document.getElementById('password-error').textContent = 'Mật khẩu phải có ít nhất 6 ký tự';
            isValid = false;
        }

        if (isValid) {
            const submitBtn = loginForm.querySelector('button[type="submit"]');
            submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> ĐANG XỬ LÝ...';
            submitBtn.disabled = true;

            setTimeout(() => {
                this.submit();
            }, 1500);
        }
    });
});