async function checkStatus() {
    const applicationNumber = document.getElementById('applicationNumber').value;
    const statusResult = document.getElementById('statusResult');
    const statusMessage = document.getElementById('statusMessage');

    if (!applicationNumber) {
        statusResult.style.display = 'block';
        statusMessage.textContent = 'Vui lòng nhập mã hồ sơ.';
        fileNumber.textContent = '-';
        return;
    }

    statusResult.style.display = 'block';
    statusMessage.textContent = 'Đang tải...';

    try {
        const response = await fetch(`/api/Form/${encodeURIComponent(applicationNumber)}`);

        if (!response.ok) {
            const errorData = await response.json();
            statusMessage.textContent = errorData.message || 'Lỗi không xác định';
            fileNumber.textContent = '-';
            return;
        }

        const data = await response.json();

        fileNumber.textContent = data.formID;
        statusMessage.textContent = data.status;
    } catch (error) {
        console.error('Fetch error:', error);
        statusMessage.textContent = 'Không thể kết nối đến máy chủ.';
        fileNumber.textContent = '-';
    }
}