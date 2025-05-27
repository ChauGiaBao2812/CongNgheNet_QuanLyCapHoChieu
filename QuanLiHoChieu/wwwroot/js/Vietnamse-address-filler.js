document.addEventListener("DOMContentLoaded", () => {
    const provinceSelect = document.getElementById("province");
    const districtSelect = document.getElementById("district");
    const wardSelect = document.getElementById("ward");

    const tempProvinceSelect = document.getElementById("temp-province");
    const tempDistrictSelect = document.getElementById("temp-district");
    const tempWardSelect = document.getElementById("temp-ward");

    // Hidden inputs for main selects (send names)
    const hiddenProvince = document.getElementById("ttTinhThanh");
    const hiddenDistrict = document.getElementById("ttQuanHuyen");
    const hiddenWard = document.getElementById("ttPhuongXa");

    // Hidden inputs for temp selects (send names)
    const hiddenTempProvince = document.getElementById("thtTinhThanh");
    const hiddenTempDistrict = document.getElementById("thtQuanHuyen");
    const hiddenTempWard = document.getElementById("thtPhuongXa");

    function resetSelect(select, placeholder) {
        select.innerHTML = `<option value="">${placeholder}</option>`;
    }

    function updateHiddenInput(select, hiddenInput) {
        const selectedOption = select.selectedOptions[0];
        hiddenInput.value = selectedOption ? selectedOption.getAttribute("data-name") || "" : "";
    }

    // Load provinces into both selects
    function loadProvinces() {
        fetch("/api/Address/addresses")
            .then(res => res.json())
            .then(provinces => {
                resetSelect(provinceSelect, "-- Chọn Tỉnh/Thành phố --");
                resetSelect(tempProvinceSelect, "-- Chọn Tỉnh/Thành phố --");

                provinces.forEach(p => {
                    // Main selects
                    const option1 = document.createElement("option");
                    option1.value = p.code;
                    option1.textContent = p.name;
                    option1.setAttribute("data-name", p.name);
                    provinceSelect.appendChild(option1);

                    // Temp selects
                    const option2 = document.createElement("option");
                    option2.value = p.code;
                    option2.textContent = p.name;
                    option2.setAttribute("data-name", p.name);
                    tempProvinceSelect.appendChild(option2);
                });
            })
            .catch(err => console.error("Failed to load provinces:", err));
    }

    loadProvinces();

    // Main province change
    provinceSelect.addEventListener("change", () => {
        const provinceCode = provinceSelect.value;

        resetSelect(districtSelect, "-- Chọn Quận/Huyện --");
        resetSelect(wardSelect, "-- Chọn Phường/Xã --");

        updateHiddenInput(provinceSelect, hiddenProvince);
        hiddenDistrict.value = "";
        hiddenWard.value = "";

        if (!provinceCode) return;

        fetch(`/api/Address/addresses/p/${provinceCode}`)
            .then(res => res.json())
            .then(province => {
                province.districts.forEach(district => {
                    const option = document.createElement("option");
                    option.value = district.code;
                    option.textContent = district.name;
                    option.setAttribute("data-name", district.name);
                    districtSelect.appendChild(option);
                });
            })
            .catch(err => console.error("Failed to load districts:", err));
    });

    // Main district change
    districtSelect.addEventListener("change", () => {
        const districtCode = districtSelect.value;

        resetSelect(wardSelect, "-- Chọn Phường/Xã --");

        updateHiddenInput(districtSelect, hiddenDistrict);
        hiddenWard.value = "";

        if (!districtCode) return;

        fetch(`/api/Address/addresses/d/${districtCode}`)
            .then(res => res.json())
            .then(district => {
                district.wards.forEach(ward => {
                    const option = document.createElement("option");
                    option.value = ward.code;
                    option.textContent = ward.name;
                    option.setAttribute("data-name", ward.name);
                    wardSelect.appendChild(option);
                });
            })
            .catch(err => console.error("Failed to load wards:", err));
    });

    // Main ward change
    wardSelect.addEventListener("change", () => {
        updateHiddenInput(wardSelect, hiddenWard);
    });

    // Temp province change
    tempProvinceSelect.addEventListener("change", () => {
        const provinceCode = tempProvinceSelect.value;

        resetSelect(tempDistrictSelect, "-- Chọn Quận/Huyện --");
        resetSelect(tempWardSelect, "-- Chọn Phường/Xã --");

        updateHiddenInput(tempProvinceSelect, hiddenTempProvince);
        hiddenTempDistrict.value = "";
        hiddenTempWard.value = "";

        if (!provinceCode) return;

        fetch(`/api/Address/addresses/p/${provinceCode}`)
            .then(res => res.json())
            .then(province => {
                province.districts.forEach(district => {
                    const option = document.createElement("option");
                    option.value = district.code;
                    option.textContent = district.name;
                    option.setAttribute("data-name", district.name);
                    tempDistrictSelect.appendChild(option);
                });
            })
            .catch(err => console.error("Failed to load temp districts:", err));
    });

    // Temp district change
    tempDistrictSelect.addEventListener("change", () => {
        const districtCode = tempDistrictSelect.value;

        resetSelect(tempWardSelect, "-- Chọn Phường/Xã --");

        updateHiddenInput(tempDistrictSelect, hiddenTempDistrict);
        hiddenTempWard.value = "";

        if (!districtCode) return;

        fetch(`/api/Address/addresses/d/${districtCode}`)
            .then(res => res.json())
            .then(district => {
                district.wards.forEach(ward => {
                    const option = document.createElement("option");
                    option.value = ward.code;
                    option.textContent = ward.name;
                    option.setAttribute("data-name", ward.name);
                    tempWardSelect.appendChild(option);
                });
            })
            .catch(err => console.error("Failed to load temp wards:", err));
    });

    // Temp ward change
    tempWardSelect.addEventListener("change", () => {
        updateHiddenInput(tempWardSelect, hiddenTempWard);
    });

    fetch('/api/Form/ethnicities')
        .then(res => res.json())
        .then(data => {
            const ethnicitySelect = document.getElementById('ethnicity-select');
            data.forEach(item => {
                const option = document.createElement('option');
                option.value = item;
                option.textContent = item;
                ethnicitySelect.appendChild(option);
            });
        });

    fetch('/api/Form/religions')
        .then(res => res.json())
        .then(data => {
            const religionSelect = document.getElementById('religion-select');
            data.forEach(item => {
                const option = document.createElement('option');
                option.value = item;
                option.textContent = item;
                religionSelect.appendChild(option);
            });
        });
});