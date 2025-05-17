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