async function checkStatus() {
    const applicationNumber = document.getElementById('applicationNumber').value;
    const statusResult = document.getElementById('statusResult');
    const statusMessage = document.getElementById('statusMessage');

    if (!applicationNumber) {
        statusResult.style.display = 'block';
        statusMessage.textContent = 'Vui lòng nhập mã hồ sơ.';
        fileNumber.textContent = '-';
        removeRejectedReason()
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

        if (data.rejectedReason) {
            showRejectedReason(data.rejectedReason);
        } else {
            removeRejectedReason();
        }
    } catch (error) {
        console.error('Fetch error:', error);
        statusMessage.textContent = 'Không thể kết nối đến máy chủ.';
        fileNumber.textContent = '-';
        removeRejectedReason();
    }
}

// Helper function to create or update the rejected reason element
function showRejectedReason(reason) {
    let existing = document.getElementById('rejectedReasonDiv');
    if (!existing) {
        existing = document.createElement('div');
        existing.id = 'rejectedReasonDiv';
        existing.className = 'info-item';
        existing.innerHTML = `
            <span class="info-label">Lý do từ chối:</span>
            <span class="info-value" id="rejectedReasonText"></span>
        `;
        document.querySelector('.result-content').appendChild(existing);
    }
    document.getElementById('rejectedReasonText').textContent = reason;
}

// Helper function to remove rejected reason element if exists
function removeRejectedReason() {
    const existing = document.getElementById('rejectedReasonDiv');
    if (existing) {
        existing.remove();
    }
}
