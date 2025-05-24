// ========LOGIN FORM================
document.addEventListener('DOMContentLoaded', () => {
    const togglePassword = document.getElementById('togglePassword');
    const passwordInput = document.getElementById('password');
    const loginForm = document.getElementById('loginForm');

    togglePassword?.addEventListener('click', function () {
        const type = passwordInput.type === 'password' ? 'text' : 'password';
        passwordInput.type = type;
        this.classList.toggle('fa-eye');
        this.classList.toggle('fa-eye-slash');
    });

    const hashPassword = async (password) => {
        const data = new TextEncoder().encode(password);
        const hashBuffer = await crypto.subtle.digest('SHA-256', data);
        return Array.from(new Uint8Array(hashBuffer))
            .map(b => b.toString(16).padStart(2, '0')).join('');
    };

    const showError = (id, message) => {
        document.getElementById(id).textContent = message;
    };

    loginForm?.addEventListener('submit', async (e) => {
        e.preventDefault();
        const username = document.getElementById('username').value.trim();
        const password = passwordInput.value.trim();

        showError('username-error', '');
        showError('password-error', '');

        let isValid = true;

        if (!username) {
            showError('username-error', 'Vui lòng nhập tên đăng nhập');
            isValid = false;
        }

        if (!password) {
            showError('password-error', 'Vui lòng nhập mật khẩu');
            isValid = false;
        } else if (password.length < 6) {
            showError('password-error', 'Mật khẩu phải có ít nhất 6 ký tự');
            isValid = false;
        }

        if (isValid) {
            const submitBtn = loginForm.querySelector('button[type="submit"]');
            submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> ĐANG XỬ LÝ...';
            submitBtn.disabled = true;

            if (password.length < 64) {
                passwordInput.value = await hashPassword(password);
            }

            loginForm.submit();
        }
    });

    const handleImageUpload = (inputId, previewId) => {
        const input = document.getElementById(inputId);
        const preview = document.getElementById(previewId);
        const uploadBox = input?.nextElementSibling?.querySelector('.image-upload-box');

        input?.addEventListener('change', (e) => {
            const file = e.target.files[0];
            if (!file) return;

            if (file.size > 5 * 1024 * 1024) {
                alert('Kích thước ảnh không được vượt quá 5MB');
                return;
            }

            if (!file.type.match('image.*')) {
                alert('Vui lòng chọn file ảnh (JPG/PNG)');
                return;
            }

            const reader = new FileReader();
            reader.onload = (event) => {
                preview.src = event.target.result;
                preview.style.display = 'block';
                uploadBox?.classList.add('has-image');
            };
            reader.readAsDataURL(file);
        });
    };

    const removeImage = (inputId, event) => {
        event.stopPropagation();

        const input = document.getElementById(inputId);
        const preview = document.getElementById(`${inputId}-preview`);
        const uploadBox = input?.nextElementSibling?.querySelector('.image-upload-box');

        if (input && preview) {
            input.value = '';
            preview.src = '';
            preview.style.display = 'none';
            uploadBox?.classList.remove('has-image');
        }
    };

    // Khởi tạo các input ảnh
    ['portrait', 'cccd-front', 'cccd-back'].forEach(id => {
        handleImageUpload(id, `${id}-preview`);
    });

    // Export removeImage nếu cần gọi từ HTML onclick
    window.removeImage = removeImage;
});

// ========FORGOT PASSWD FORM================
document.addEventListener('DOMContentLoaded', function () {
    const forgotPasswordForm = document.getElementById('forgotPasswordForm');

    forgotPasswordForm.addEventListener('submit', function (e) {
        e.preventDefault();

        document.getElementById('email-error').textContent = '';

        const email = document.getElementById('email').value.trim();
        let isValid = true;

        if (email === '') {
            document.getElementById('email-error').textContent = 'Vui lòng nhập email';
            isValid = false;
        } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
            document.getElementById('email-error').textContent = 'Email không hợp lệ';
            isValid = false;
        }

        if (isValid) {
            const submitBtn = this.querySelector('button[type="submit"]');
            const originalText = submitBtn.innerHTML;
            submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> ĐANG XỬ LÝ...';
            submitBtn.disabled = true;

            setTimeout(() => {
                alert('Yêu cầu khôi phục mật khẩu đã được gửi! Vui lòng kiểm tra email của bạn.');
                submitBtn.innerHTML = originalText;
                submitBtn.disabled = false;
            }, 1500);
        }
    });
});