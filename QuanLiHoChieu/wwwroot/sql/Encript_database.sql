CREATE DATABASE PASSPORT_SYSTEM
GO
USE PASSPORT_SYSTEM
GO

-- Tạo bảng TaiKhoan
CREATE TABLE TaiKhoan (
    Username VARCHAR(20) PRIMARY KEY,
    MatKhau VARBINARY(64) NOT NULL -- Mật khẩu được băm (Hash)
);


-- Tạo bảng User
CREATE TABLE [User] (
    UserID VARCHAR(20) PRIMARY KEY,
    HoTen VARBINARY(256) NOT NULL, -- Họ tên mã hóa AES
    GioiTinh NVARCHAR(10) NOT NULL,
    NgaySinh DATE NOT NULL,
    QueQuan NVARCHAR(100) NOT NULL,
    SĐT VARBINARY(128) NOT NULL, -- Số điện thoại mã hóa
    Email VARBINARY(256) NOT NULL, -- Email mã hóa
    ChucVu NVARCHAR(50) NOT NULL,
    Username VARCHAR(20) UNIQUE NOT NULL,
    FOREIGN KEY (Username) REFERENCES TaiKhoan(Username)
);


-- Tạo bảng ResidentData
CREATE TABLE ResidentData (
    CCCD VARBINARY(256) PRIMARY KEY, -- CCCD mã hóa AES
    HoTen VARBINARY(256) NOT NULL, -- Mã hóa
    GioiTinh NVARCHAR(10) NOT NULL,
    NgaySinh DATE NOT NULL,
    NoiSinh VARBINARY(256) NOT NULL,
    NgayCap DATE NOT NULL,
    NoiCap VARBINARY(256) NOT NULL,
    DanToc NVARCHAR(50) NOT NULL,
    TonGiao NVARCHAR(50),
    SĐT VARBINARY(128) NOT NULL,
    Hinh NVARCHAR(256),
    
    -- Địa chỉ thường trú (tt)
    ttTinhThanh VARBINARY(256) NOT NULL,
    ttQuanHuyen VARBINARY(256) NOT NULL,
    ttPhuongXa VARBINARY(256) NOT NULL,
    ttSoNhaDuong VARBINARY(256) NOT NULL,

    -- Địa chỉ tạm trú (tht)
    thtTinhThanh VARBINARY(256) NOT NULL,
    thtQuanHuyen VARBINARY(256) NOT NULL,
    thtPhuongXa VARBINARY(256) NOT NULL,
    thtSoNhaDuong VARBINARY(256) NOT NULL,

    HoTenCha VARBINARY(256),
    NgaySinhCha DATE,
    HoTenMe VARBINARY(256),
    NgaySinhMe DATE
);



-- Tạo bảng PassportData
CREATE TABLE PassportData (
    FormID VARCHAR(20) PRIMARY KEY,
    HoTen VARBINARY(256) NOT NULL,
    GioiTinh NVARCHAR(10) NOT NULL,
    NgaySinh DATE NOT NULL,
    NoiSinh VARBINARY(256) NOT NULL,
    CCCD VARBINARY(256) NOT NULL,
    NgayCap DATE NOT NULL,
    NoiCap VARBINARY(256) NOT NULL,
    DanToc NVARCHAR(50) NOT NULL,
    TonGiao NVARCHAR(50),
    SĐT VARBINARY(128) NOT NULL,

    -- Địa chỉ thường trú (tt)
    ttTinhThanh VARBINARY(256) NOT NULL,
    ttQuanHuyen VARBINARY(256) NOT NULL,
    ttPhuongXa VARBINARY(256) NOT NULL,
    ttSoNhaDuong VARBINARY(256) NOT NULL,

    -- Địa chỉ tạm trú (tht)
    thtTinhThanh VARBINARY(256) NOT NULL,
    thtQuanHuyen VARBINARY(256) NOT NULL,
    thtPhuongXa VARBINARY(256) NOT NULL,
    thtSoNhaDuong VARBINARY(256) NOT NULL,

    NgheNghiep VARBINARY(256),
    CoQuan VARBINARY(256),
    DiaChiCoQuan VARBINARY(256),
    HoTenCha VARBINARY(256),
    NgaySinhCha DATE,
    HoTenMe VARBINARY(256),
    NgaySinhMe DATE,

    NoiDungDeNghi NVARCHAR(MAX) NOT NULL,
    NoiTiepNhanHS NVARCHAR(100) NOT NULL,
    NgayNop DATE NOT NULL,

    FOREIGN KEY (CCCD) REFERENCES ResidentData(CCCD)
);


-- Tạo bảng XuLy
CREATE TABLE XuLy (
    XuLyID INT PRIMARY KEY IDENTITY(1,1),
    FormID VARCHAR(20) NOT NULL,
    UserID VARCHAR(20) NOT NULL,
    NgayXuLy DATE NOT NULL,
    TrangThai NVARCHAR(50) NOT NULL,
    GhiChu NVARCHAR(255),
    LoaiXuLy NVARCHAR(50) NOT NULL,
    FOREIGN KEY (FormID) REFERENCES PassportData(FormID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);


-- Tạo bảng LuuTru
CREATE TABLE LuuTru (
    PassportID VARCHAR(20) PRIMARY KEY,
    UserID VARCHAR(20) NOT NULL,
    FormID VARCHAR(20) UNIQUE NOT NULL,
    NgayNop DATE NOT NULL,
    NgayCap DATE NOT NULL,
    CoGiaTriDen DATE NOT NULL,
    FOREIGN KEY (FormID) REFERENCES PassportData(FormID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);