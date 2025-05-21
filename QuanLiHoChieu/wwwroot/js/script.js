document.addEventListener('DOMContentLoaded', function () {
    const togglePassword = document.getElementById('togglePassword');
    const passwordInput = document.getElementById('password');

    togglePassword.addEventListener('click', function () {
        const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
        passwordInput.setAttribute('type', type);
        this.classList.toggle('fa-eye');
        this.classList.toggle('fa-eye-slash');
    });

    async function hashPassword(password) {
        const encoder = new TextEncoder();
        const data = encoder.encode(password);
        const hashBuffer = await crypto.subtle.digest("SHA-256", data);
        const hashArray = Array.from(new Uint8Array(hashBuffer));
        return hashArray.map(b => b.toString(16).padStart(2, '0')).join('');
    }

    const loginForm = document.getElementById('loginForm');

    loginForm.addEventListener('submit', async function (e) {
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

            if (password.length < 64) {
                const hashed = await hashPassword(password);
                passwordInput.value = hashed;
            }

            this.submit();
        }
    });
});

// Hàm xử lý ảnh
function handleImageUpload(inputId, previewId) {
    const input = document.getElementById(inputId);
    const preview = document.getElementById(previewId);
    const uploadBox = input.nextElementSibling.querySelector('.image-upload-box');
    
    input.addEventListener('change', function(e) {
        const file = e.target.files[0];
        if (file) {
            if (file.size > 5 * 1024 * 1024) {
                alert('Kích thước ảnh không được vượt quá 5MB');
                return;
            }

            if (!file.type.match('image.*')) {
                alert('Vui lòng chọn file ảnh (JPG/PNG)');
                return;
            }
            
            const reader = new FileReader();
            
            reader.onload = function(event) {
                preview.src = event.target.result;
                preview.style.display = 'block';
                uploadBox.classList.add('has-image');
            }
            
            reader.readAsDataURL(file);
        }
    });
}

function removeImage(inputId, event) {
    event.stopPropagation(); 
    
    const input = document.getElementById(inputId);
    const previewId = inputId + '-preview';
    const preview = document.getElementById(previewId);
    const uploadBox = input.nextElementSibling.querySelector('.image-upload-box');
    
    input.value = ''; 
    preview.src = ''; 
    preview.style.display = 'none';
    uploadBox.classList.remove('has-image');
}

document.addEventListener('DOMContentLoaded', function() {
    handleImageUpload('portrait', 'portrait-preview');
    handleImageUpload('cccd-front', 'cccd-front-preview');
    handleImageUpload('cccd-back', 'cccd-back-preview');
});
