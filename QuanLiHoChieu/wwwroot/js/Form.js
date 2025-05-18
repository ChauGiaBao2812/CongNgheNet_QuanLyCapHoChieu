const districtData = {
    "An Giang": ["Thành phố Long Xuyên", "Thành phố Châu Đốc", "Huyện Tân Châu", "Huyện Châu Phú", "Huyện Châu Thành", "Huyện Thoại Sơn", "Huyện Tri Tôn", "Huyện An Phú", "Huyện Phú Tân", "Huyện Chợ Mới"],
    "Bà Rịa - Vũng Tàu": ["Thành phố Vũng Tàu", "Thành phố Bà Rịa", "Thị xã Phú Mỹ", "Huyện Châu Đức", "Huyện Xuyên Mộc", "Huyện Long Điền", "Huyện Đất Đỏ", "Huyện Tân Thành"],
    "Bắc Giang": ["Thành phố Bắc Giang", "Huyện Yên Thế", "Huyện Tân Yên", "Huyện Lạng Giang", "Huyện Lục Nam", "Huyện Lục Ngạn", "Huyện Sơn Động", "Huyện Yên Dũng", "Huyện Việt Yên"],
    "Bắc Kạn": ["Thành phố Bắc Kạn", "Huyện Pác Nặm", "Huyện Bạch Thông", "Huyện Chợ Đồn", "Huyện Chợ Mới", "Huyện Na Rì", "Huyện Ngân Sơn"],
    "Bắc Ninh": ["Thành phố Bắc Ninh", "Thị xã Từ Sơn", "Huyện Yên Phong", "Huyện Quế Võ", "Huyện Tiên Du", "Huyện Thuận Thành", "Huyện Gia Bình", "Huyện Lương Tài"],
    "Bạc Liêu": ["Thành phố Bạc Liêu", "Thị xã Giá Rai", "Huyện Hồng Dân", "Huyện Phước Long", "Huyện Vĩnh Lợi", "Huyện Đông Hải"],
    "Bến Tre": ["Thành phố Bến Tre", "Huyện Ba Tri", "Huyện Bình Đại", "Huyện Châu Thành", "Huyện Chợ Lách", "Huyện Giồng Trôm", "Huyện Mỏ Cày Bắc", "Huyện Mỏ Cày Nam", "Huyện Thạnh Phú"],
    "Bình Dương": ["Thành phố Thủ Dầu Một", "Thành phố Dĩ An", "Thành phố Thuận An", "Thành phố Tân Uyên", "Huyện Bàu Bàng", "Huyện Bắc Tân Uyên", "Huyện Dầu Tiếng", "Huyện Phú Giáo"],
    "Bình Định": ["Thành phố Quy Nhơn", "Thị xã An Nhơn", "Thị xã Hoài Nhơn", "Huyện An Lão", "Huyện Hoài Ân", "Huyện Phù Cát", "Huyện Phù Mỹ", "Huyện Tây Sơn", "Huyện Tuy Phước", "Huyện Vân Canh"],
    "Bình Phước": ["Thành phố Đồng Xoài", "Thị xã Bình Long", "Thị xã Phước Long", "Huyện Bù Đốp", "Huyện Bù Đăng", "Huyện Bù Gia Mập", "Huyện Chơn Thành", "Huyện Đồng Phú", "Huyện Hớn Quản", "Huyện Lộc Ninh", "Huyện Phú Riềng"],
    "Bình Thuận": ["Thành phố Phan Thiết", "Thị xã La Gi", "Huyện Hàm Thuận Bắc", "Huyện Hàm Thuận Nam", "Huyện Hàm Tân", "Huyện Bắc Bình", "Huyện Tánh Linh", "Huyện Đức Linh", "Huyện Phú Quý"],
    "Cà Mau": ["Thành phố Cà Mau", "Huyện Ngọc Hiển", "Huyện U Minh", "Huyện Thới Bình", "Huyện Trần Văn Thời", "Huyện Cái Nước", "Huyện Năm Căn", "Huyện Phú Tân", "Huyện Đầm Dơi", "Huyện Thới Bình"],
    "Cao Bằng": ["Thành phố Cao Bằng", "Huyện Bảo Lạc", "Huyện Bảo Lâm", "Huyện Hạ Lang", "Huyện Hà Quảng", "Huyện Hòa An", "Huyện Nguyên Bình", "Huyện Quảng Hòa", "Huyện Thạch An", "Huyện Trùng Khánh"],
    "Cần Thơ": ["Quận Ninh Kiều", "Quận Bình Thủy", "Quận Cái Răng", "Quận Ô Môn", "Quận Thốt Nốt", "Huyện Cờ Đỏ", "Huyện Phong Điền", "Huyện Thới Lai", "Huyện Vĩnh Thạnh"],
    "Đà Nẵng": ["Quận Hải Châu", "Quận Thanh Khê", "Quận Cẩm Lệ", "Quận Sơn Trà", "Quận Ngũ Hành Sơn", "Quận Liên Chiểu", "Huyện Hòa Vang", "Huyện Hoàng Sa"],
    "Đồng Nai": ["Thành phố Biên Hòa", "Thị xã Long Khánh", "Huyện Cẩm Mỹ", "Huyện Định Quán", "Huyện Tân Phú", "Huyện Thống Nhất", "Huyện Trảng Bom", "Huyện Vĩnh Cửu", "Huyện Xuân Lộc"],
    "Đồng Tháp": ["Thành phố Cao Lãnh", "Thành phố Sa Đéc", "Thị xã Hồng Ngự", "Huyện Châu Thành", "Huyện Cao Lãnh", "Huyện Hồng Ngự", "Huyện Lai Vung", "Huyện Lấp Vò", "Huyện Tam Nông", "Huyện Tháp Mười", "Huyện Tân Hồng", "Huyện Thanh Bình", "Huyện Thường Thạnh"],
    "Đắk Lắk": ["Thành phố Buôn Ma Thuột", "Thị xã Buôn Hồ", "Huyện Buôn Đôn", "Huyện Cư Kuin", "Huyện Ea H'leo", "Huyện Ea Súp", "Huyện Krông Ana", "Huyện Krông Búk", "Huyện Krông Năng", "Huyện Krông Pắc", "Huyện Krông Bông", "Huyện Lắk", "Huyện M'Đrắk"],
    "Đắk Nông": ["Thành phố Gia Nghĩa", "Huyện Cư Jút", "Huyện Đắk Glong", "Huyện Đắk Mil", "Huyện Đắk R'Lấp", "Huyện Đắk Song", "Huyện Krông Nô", "Huyện Tuy Đức"],
    "Gia Lai": ["Thành phố Pleiku", "Thị xã An Khê", "Thị xã Ayun Pa", "Huyện Chư Păh", "Huyện Chư Prông", "Huyện Chư Pưh", "Huyện Chư Sê", "Huyện Đắk Đoa", "Huyện Đak Pơ", "Huyện Đức Cơ", "Huyện Ia Grai", "Huyện Ia Pa", "Huyện KBang", "Huyện Kông Chro", "Huyện Krông Pa", "Huyện Mang Yang", "Huyện Phú Thiện"],
    "Hà Giang": ["Thành phố Hà Giang", "Huyện Bắc Mê", "Huyện Bắc Quang", "Huyện Đồng Văn", "Huyện Hoàng Su Phì", "Huyện Mèo Vạc", "Huyện Quang Bình", "Huyện Quản Bạ", "Huyện Vị Xuyên", "Huyện Xín Mần", "Huyện Yên Minh"],
    "Hà Nội": ["Quận Ba Đình", "Quận Hoàn Kiếm", "Quận Tây Hồ", "Quận Long Biên", "Quận Cầu Giấy", "Quận Đống Đa", "Quận Hai Bà Trưng", "Quận Hoàng Mai", "Quận Thanh Xuân", "Quận Bắc Từ Liêm", "Quận Nam Từ Liêm", "Huyện Ba Vì", "Huyện Chương Mỹ", "Huyện Đan Phượng", "Huyện Đông Anh", "Huyện Gia Lâm", "Huyện Hoài Đức", "Huyện Mê Linh", "Huyện Mỹ Đức", "Huyện Phú Xuyên", "Huyện Phúc Thọ", "Huyện Quốc Oai", "Huyện Sóc Sơn", "Huyện Thạch Thất", "Huyện Thanh Oai", "Huyện Thanh Trì", "Huyện Thường Tín", "Huyện Ứng Hòa"],
    "Hà Tĩnh": ["Thành phố Hà Tĩnh", "Thị xã Hồng Lĩnh", "Huyện Cẩm Xuyên", "Huyện Can Lộc", "Huyện Đức Thọ", "Huyện Hương Khê", "Huyện Hương Sơn", "Huyện Kỳ Anh", "Huyện Lộc Hà", "Huyện Nghi Xuân", "Huyện Thạch Hà", "Huyện Vũ Quang"],
    "Hải Dương": ["Thành phố Chí Linh", "Huyện Bình Giang", "Huyện Cẩm Giàng", "Huyện Gia Lộc", "Huyện Kim Thành", "Thị xã Kinh Môn", "Huyện Nam Sách", "Huyện Ninh Giang", "Huyện Thanh Hà", "Huyện Thanh Miện", "Huyện Tứ Kỳ"],
    "Hải Phòng": ["Quận Hồng Bàng", "Quận Ngô Quyền", "Quận Lê Chân", "Quận Hải An", "Quận Kiến An", "Quận Dương Kinh", "Huyện An Dương", "Huyện An Lão", "Huyện Kiến Thụy", "Huyện Thủy Nguyên", "Huyện Tiên Lãng", "Huyện Vĩnh Bảo", "Huyện Cát Hải"],
    "Hậu Giang": ["Thành phố Vị Thanh", "Thị xã Long Mỹ", "Thị xã Ngã Bảy", "Huyện Châu Thành A", "Huyện Châu Thành", "Huyện Phụng Hiệp", "Huyện Vị Thủy", "Huyện Long Mỹ"],
    "Hòa Bình": ["Thành phố Hòa Bình", "Huyện Đà Bắc", "Huyện Kỳ Sơn", "Huyện Lạc Sơn", "Huyện Lạc Thủy", "Huyện Kim Bôi", "Huyện Tân Lạc", "Huyện Mai Châu", "Huyện Yên Thủy"],
    "Hưng Yên": ["Thành phố Hưng Yên", "Thị xã Mỹ Hào", "Huyện Ân Thi", "Huyện Khoái Châu", "Huyện Kim Động", "Huyện Phù Cừ", "Huyện Tiên Lữ", "Huyện Văn Giang", "Huyện Văn Lâm", "Huyện Yên Mỹ"],
    "TP. Hồ Chí Minh": ["Quận 1", "Quận 3", "Quận 4", "Quận 5", "Quận 6", "Quận 7", "Quận 8", "Quận 10", "Quận 11", "Quận 12", "Bình Tân", "Bình Thạnh", "Gò Vấp", "Phú Nhuận", "Tân Bình", "Tân Phú", "Thủ Đức", "Bình Chánh", "Cần Giờ", "Củ Chi", "Hóc Môn", "Nhà Bè"],
    "Khánh Hòa": ["Thành phố Nha Trang", "Thị xã Cam Ranh", "Huyện Cam Lâm", "Huyện Diên Khánh", "Huyện Khánh Vĩnh", "Huyện Khánh Sơn", "Huyện Trường Sa", "Huyện Vạn Ninh"],
    "Kiên Giang": ["Thành phố Rạch Giá", "Thị xã Hà Tiên", "Huyện An Biên", "Huyện An Minh", "Huyện Châu Thành", "Huyện Giang Thành", "Huyện Gò Quao", "Huyện Hòn Đất", "Huyện Kiên Hải", "Huyện Tân Hiệp", "Huyện U Minh Thượng", "Huyện Vĩnh Thuận", "Huyện Phú Quốc"],
    "Kon Tum": ["Thành phố Kon Tum", "Huyện Đắk Glei", "Huyện Đắk Hà", "Huyện Đắk Tô", "Huyện Ia H'Drai", "Huyện Kon Plông", "Huyện Kon Rẫy", "Huyện Ngọc Hồi", "Huyện Sa Thầy", "Huyện Tu Mơ Rông"],
    "Lai Châu": ["Thị xã Lai Châu", "Huyện Mường Tè", "Huyện Phong Thổ", "Huyện Sìn Hồ", "Huyện Tam Đường", "Huyện Tân Uyên", "Huyện Than Uyên"],
    "Lâm Đồng": ["Thành phố Đà Lạt", "Thành phố Bảo Lộc", "Huyện Bảo Lâm", "Huyện Cát Tiên", "Huyện Đạ Huoai", "Huyện Đạ Tẻh", "Huyện Đam Rông", "Huyện Di Linh", "Huyện Đức Trọng", "Huyện Lâm Hà"],
    "Lạng Sơn": ["Thành phố Lạng Sơn", "Huyện Bắc Sơn", "Huyện Bình Gia", "Huyện Cao Lộc", "Huyện Chi Lăng", "Huyện Đình Lập", "Huyện Hữu Lũng", "Huyện Lộc Bình", "Huyện Tràng Định", "Huyện Văn Lãng", "Huyện Văn Quan"],
    "Lào Cai": ["Thành phố Lào Cai", "Huyện Bát Xát", "Huyện Bắc Hà", "Huyện Bảo Thắng", "Huyện Bảo Yên", "Huyện Sa Pa", "Huyện Si Ma Cai", "Huyện Văn Bàn"],
    "Long An": ["Thành phố Tân An", "Huyện Bến Lức", "Huyện Cần Đước", "Huyện Cần Giuộc", "Huyện Châu Thành", "Huyện Đức Hòa", "Huyện Thạnh Hóa", "Huyện Tân Thạnh", "Huyện Tân Trụ", "Huyện Vĩnh Hưng", "Huyện Mộc Hóa", "Huyện Thủ Thừa"],
    "Nam Định": ["Thành phố Nam Định", "Huyện Hải Hậu", "Huyện Mỹ Lộc", "Huyện Nam Trực", "Huyện Nghĩa Hưng", "Huyện Trực Ninh", "Huyện Vụ Bản", "Huyện Xuân Trường", "Huyện Ý Yên"],
    "Nghệ An": ["Thành phố Vinh", "Thị xã Cửa Lò", "Huyện Anh Sơn", "Huyện Con Cuông", "Huyện Diễn Châu", "Huyện Đô Lương", "Huyện Hưng Nguyên", "Huyện Kỳ Sơn", "Huyện Nam Đàn", "Huyện Nghi Lộc", "Huyện Nghĩa Đàn", "Huyện Quế Phong", "Huyện Quỳ Châu", "Huyện Quỳ Hợp", "Huyện Quỳnh Lưu", "Huyện Tân Kỳ", "Huyện Tương Dương", "Huyện Yên Thành"],
    "Ninh Bình": ["Thành phố Ninh Bình", "Thị xã Tam Điệp", "Huyện Gia Viễn", "Huyện Hoa Lư", "Huyện Kim Sơn", "Huyện Nho Quan", "Huyện Yên Khánh", "Huyện Yên Mô"],
    "Ninh Thuận": ["Thành phố Phan Rang-Tháp Chàm", "Huyện Bác Ái", "Huyện Ninh Hải", "Huyện Ninh Phước", "Huyện Thuận Bắc", "Huyện Thuận Nam"],
    "Phú Thọ": ["Thị xã Phú Thọ", "Huyện Cẩm Khê", "Huyện Đoan Hùng", "Huyện Hạ Hòa", "Huyện Lâm Thao", "Huyện Phù Ninh", "Huyện Tam Nông", "Huyện Tân Sơn", "Huyện Thanh Ba", "Huyện Thanh Sơn", "Huyện Thanh Thủy", "Huyện Yên Lập"],
    "Phú Yên": ["Thành phố Tuy Hòa", "Thị xã Sông Cầu", "Huyện Đồng Xuân", "Huyện Phú Hòa", "Huyện Sơn Hòa", "Huyện Sông Hinh", "Huyện Tây Hòa", "Huyện Tuy An"],
    "Quảng Bình": ["Thành phố Đồng Hới", "Huyện Bố Trạch", "Huyện Lệ Thủy", "Huyện Quảng Ninh", "Huyện Quảng Trạch", "Huyện Tuyên Hóa", "Huyện Minh Hóa"],
    "Quảng Nam": ["Thành phố Hội An", "Thành phố Tam Kỳ", "Huyện Bắc Trà My", "Huyện Đại Lộc", "Huyện Đông Giang", "Huyện Điện Bàn", "Huyện Duy Xuyên", "Huyện Nam Giang", "Huyện Nam Trà My", "Huyện Nông Sơn", "Huyện Núi Thành", "Huyện Phú Ninh", "Huyện Phước Sơn", "Huyện Tây Giang", "Huyện Thăng Bình", "Huyện Tiên Phước"],
    "Quảng Ngãi": ["Thành phố Quảng Ngãi", "Huyện Ba Tơ", "Huyện Bình Sơn", "Huyện Đức Phổ", "Huyện Lý Sơn", "Huyện Minh Long", "Huyện Mộ Đức", "Huyện Nghĩa Hành", "Huyện Sơn Hà", "Huyện Sơn Tây", "Huyện Trà Bồng", "Huyện Tư Nghĩa"],
    "Quảng Ninh": ["Thành phố Hạ Long", "Thành phố Móng Cái", "Thị xã Cẩm Phả", "Thị xã Đông Triều", "Thị xã Uông Bí", "Huyện Ba Chẽ", "Huyện Bình Liêu", "Huyện Cô Tô", "Huyện Đầm Hà", "Huyện Hải Hà", "Huyện Hoành Bồ", "Huyện Tiên Yên", "Huyện Vân Đồn", "Huyện Yên Hưng"],
    "Quảng Trị": ["Thành phố Đông Hà", "Huyện Cam Lộ", "Huyện Cồn Cỏ", "Huyện Đa Krông", "Huyện Gio Linh", "Huyện Hải Lăng", "Huyện Hướng Hóa", "Huyện Triệu Phong", "Huyện Vĩnh Linh"],
    "Sóc Trăng": ["Thành phố Sóc Trăng", "Huyện Châu Thành", "Huyện Cù Lao Dung", "Huyện Kế Sách", "Huyện Long Phú", "Huyện Mỹ Tú", "Huyện Mỹ Xuyên", "Huyện Thạnh Trị", "Huyện Trần Đề"],
    "Sơn La": ["Thành phố Sơn La", "Huyện Bắc Yên", "Huyện Mai Sơn", "Huyện Mộc Châu", "Huyện Phù Yên", "Huyện Quỳnh Nhai", "Huyện Sông Mã", "Huyện Sốp Cộp", "Huyện Thuận Châu", "Huyện Vân Hồ"],
    "Tây Ninh": ["Thành phố Tây Ninh", "Huyện Bến Cầu", "Huyện Châu Thành", "Huyện Dương Minh Châu", "Huyện Gò Dầu", "Huyện Hòa Thành", "Huyện Tân Biên", "Huyện Tân Châu", "Huyện Trảng Bàng"],
    "Thái Bình": ["Thành phố Thái Bình", "Huyện Đông Hưng", "Huyện Hưng Hà", "Huyện Kiến Xương", "Huyện Quỳnh Phụ", "Huyện Tiền Hải", "Huyện Vũ Thư"],
    "Thái Nguyên": ["Thành phố Thái Nguyên", "Thị xã Phổ Yên", "Huyện Đại Từ", "Huyện Định Hóa", "Huyện Đồng Hỷ", "Huyện Phú Bình", "Huyện Phú Lương", "Huyện Võ Nhai"],
    "Thanh Hóa": ["Thành phố Thanh Hóa", "Thị xã Bỉm Sơn", "Thị xã Sầm Sơn", "Huyện Bá Thước", "Huyện Cẩm Thủy", "Huyện Đông Sơn", "Huyện Hà Trung", "Huyện Hậu Lộc", "Huyện Hoằng Hóa", "Huyện Lang Chánh", "Huyện Mường Lát", "Huyện Nga Sơn", "Huyện Ngọc Lặc", "Huyện Như Thanh", "Huyện Như Xuân", "Huyện Nông Cống", "Huyện Quan Hóa", "Huyện Quan Sơn", "Huyện Quảng Xương", "Huyện Thạch Thành", "Huyện Thiệu Hóa", "Huyện Thọ Xuân", "Huyện Triệu Sơn", "Huyện Vĩnh Lộc", "Huyện Yên Định"],
    "Thừa Thiên Huế": ["Thành phố Huế", "Huyện Phong Điền", "Huyện Quảng Điền", "Huyện Phú Vang", "Huyện Phú Lộc", "Huyện Nam Đông"],
    "Tiền Giang": ["Thành phố Mỹ Tho", "Thị xã Gò Công", "Thị xã Cai Lậy", "Huyện Cái Bè", "Huyện Cai Lậy", "Huyện Châu Thành", "Huyện Chợ Gạo", "Huyện Gò Công Đông", "Huyện Gò Công Tây", "Huyện Tân Phước"],
    "Trà Vinh": ["Thành phố Trà Vinh", "Huyện Càng Long", "Huyện Cầu Kè", "Huyện Cầu Ngang", "Huyện Duyên Hải", "Huyện Tiểu Cần", "Huyện Trà Cú", "Huyện Châu Thành"],
    "Tuyên Quang": ["Thành phố Tuyên Quang", "Huyện Chiêm Hóa", "Huyện Hàm Yên", "Huyện Na Hang", "Huyện Sơn Dương", "Huyện Yên Sơn"],
    "Vĩnh Long": ["Thành phố Vĩnh Long", "Huyện Bình Minh", "Huyện Long Hồ", "Huyện Mang Thít", "Huyện Tam Bình", "Huyện Trà Ôn", "Huyện Vũng Liêm"],
    "Vĩnh Phúc": ["Thành phố Vĩnh Yên", "Thị xã Phúc Yên", "Huyện Bình Xuyên", "Huyện Lập Thạch", "Huyện Sông Lô", "Huyện Tam Dương", "Huyện Tam Đảo", "Huyện Vĩnh Tường"],
    "Yên Bái": ["Thành phố Yên Bái", "Huyện Lục Yên", "Huyện Mù Căng Chải", "Huyện Trấn Yên", "Huyện Trạm Tấu", "Huyện Văn Chấn", "Huyện Văn Yên", "Huyện Yên Bình"]
};

function loadDistricts() {
    const provinceSelect = document.getElementById("province");
    const districtSelect = document.getElementById("district");
    const selectedProvince = provinceSelect.value;

    // Reset district select
    districtSelect.innerHTML = '<option value="">-- Chọn quận/huyện --</option>';

    if (selectedProvince && districtData[selectedProvince]) {
        districtData[selectedProvince].forEach(district => {
            const option = new Option(district, district);
            districtSelect.add(option);
        });
    }
}