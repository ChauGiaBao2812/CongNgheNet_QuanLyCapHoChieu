document.addEventListener("DOMContentLoaded", function () {
  const rows = document.querySelectorAll("table tbody tr");

  rows.forEach((row) => {
    const statusCell = row.cells[1];
    const storageCell = row.cells[2];

    if (statusCell.textContent.trim() === "Đồng ý cấp hộ chiếu") {
      const button = document.createElement("button");
      button.textContent = "Lưu";
      button.className = "save-btn";
      storageCell.appendChild(button);
    }
  });
});
