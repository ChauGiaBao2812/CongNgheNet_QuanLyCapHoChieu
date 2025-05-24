// Hiển thị modal chỉnh sửa
      function showEditForm(event) {
        event.preventDefault();
        document.getElementById("editModal").style.display = "flex";
      }

      // Hiển thị modal thêm mới
      function showAddForm() {
        // Reset form
        const form = document.querySelector(".edit-form");
        form.querySelector("#id_user").value = "";
        form
          .querySelectorAll('input[type="checkbox"]')
          .forEach((cb) => (cb.checked = false));

        document.getElementById("editModal").style.display = "flex";
      }

      // Ẩn modal
      function hideModal() {
        document.getElementById("editModal").style.display = "none";
      }

      // Xác nhận xóa
      function confirmDelete(event) {
        event.preventDefault();
        if (confirm("Bạn có chắc chắn muốn xóa user này?")) {
          const row = event.target.closest("tr");
          row.remove();

          // Hiển thị thông báo (trong thực tế sẽ gọi API xóa)
          alert("Đã xóa user thành công!");
        }
      }

      // Đăng xuất
      function logout() {
        if (confirm("Bạn có chắc chắn muốn đăng xuất?")) {
          // Trong thực tế sẽ gọi API đăng xuất
          window.location.href = "/login";
        }
      }

      // Đóng modal khi click ra ngoài
      document
        .getElementById("editModal")
        .addEventListener("click", function (e) {
          if (e.target === this) {
            hideModal();
          }
        });