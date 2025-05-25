document.addEventListener("DOMContentLoaded", function () {
    document
        .getElementById("search-input")
        .addEventListener("input", function () {
            const searchTerm = this.value.toLowerCase();
            const rows = document.querySelectorAll("table.profile-table tbody tr");

            rows.forEach((row) => {
                const rowText = row.textContent.toLowerCase();
                row.style.display = rowText.includes(searchTerm) ? "" : "none";
            });
        });
});