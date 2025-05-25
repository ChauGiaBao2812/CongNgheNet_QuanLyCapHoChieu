document.addEventListener("DOMContentLoaded", () => {
    fetch('/api/Form/stats')
        .then(res => res.json())
        .then(data => {
            const statsText = document.querySelector('.stats-text');
            if (statsText) {
                const paragraphs = statsText.querySelectorAll('p');
                paragraphs[1].innerHTML = `<b>Tổng: </b> ${data.tong}`;
                paragraphs[2].innerHTML = `<b>Đã giải quyết: </b> ${data.daGiaiQuyet}`;
                paragraphs[3].innerHTML = `<b>Đúng hạn: </b> ${data.dungHan}`;
            }

            const pieChart = document.querySelector('.pie-chart');
            if (pieChart && data.tong > 0) {
                const percentage = (data.daGiaiQuyet / data.tong) * 100;
                pieChart.style.background = `conic-gradient(var(--primary-color) ${percentage}%, rgba(139, 0, 0, 0.1) 0%)`;

                // Select the inner span that shows percentage text
                const percentageText = pieChart.querySelector('.percentage-text');
                if (percentageText) {
                    percentageText.textContent = `${percentage.toFixed(1)}%`;
                }
            }
        })
        .catch(err => console.error('Failed to load stats:', err));
});