USE PASSPORT_SYSTEM
GO

--Tạo Master Key
IF NOT EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = '##MS_DatabaseMasterKey##')
BEGIN
    CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'DotnetBMCSDL';
END


-- Mở khóa đối xứng (AES)
CREATE PROCEDURE sp_OpenSymmetricKey
AS
BEGIN
    OPEN SYMMETRIC KEY SymmetricKey_AES DECRYPTION BY CERTIFICATE Cert_AES;
END
GO

-- Đóng khóa
CREATE PROCEDURE sp_CloseSymmetricKey
AS
BEGIN
    CLOSE SYMMETRIC KEY SymmetricKey_AES;
END
GO

-- Thêm người dùng (mã hóa họ tên, email, sđt)
CREATE PROCEDURE sp_InsertUser
    @UserID VARCHAR(20),
    @HoTen NVARCHAR(100),
    @GioiTinh NVARCHAR(10),
    @NgaySinh DATE,
    @QueQuan NVARCHAR(100),
    @SĐT NVARCHAR(15),
    @Email NVARCHAR(100),
    @ChucVu NVARCHAR(50),
    @Username VARCHAR(20)
AS
BEGIN
    EXEC sp_OpenSymmetricKey;

    INSERT INTO [User](UserID, HoTen, GioiTinh, NgaySinh, QueQuan, SĐT, Email, ChucVu, Username)
    VALUES (
        @UserID,
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @HoTen),
        @GioiTinh,
        @NgaySinh,
        @QueQuan,
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @SĐT),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @Email),
        @ChucVu,
        @Username
    );

    EXEC sp_CloseSymmetricKey;
END
GO

-- Giải mã thông tin người dùng
CREATE PROCEDURE sp_GetUsers_Decrypted
AS
BEGIN
    EXEC sp_OpenSymmetricKey;

    SELECT 
        UserID,
        CONVERT(NVARCHAR, DecryptByKey(HoTen)) AS HoTen,
        GioiTinh,
        NgaySinh,
        QueQuan,
        CONVERT(NVARCHAR, DecryptByKey(SĐT)) AS SĐT,
        CONVERT(NVARCHAR, DecryptByKey(Email)) AS Email,
        ChucVu,
        Username
    FROM [User];

    EXEC sp_CloseSymmetricKey;
END
GO

-- Thêm dữ liệu ResidentData
CREATE PROCEDURE sp_InsertResidentData
    @CCCD VARCHAR(20),
    @HoTen NVARCHAR(100),
    @GioiTinh NVARCHAR(10),
    @NgaySinh DATE,
    @NoiSinh NVARCHAR(100),
    @NgayCap DATE,
    @NoiCap NVARCHAR(100),
    @DanToc NVARCHAR(50),
    @TonGiao NVARCHAR(50),
    @SĐT NVARCHAR(15),
    @ttTinhThanh NVARCHAR(100),
    @ttQuanHuyen NVARCHAR(100),
    @ttPhuongXa NVARCHAR(100),
    @ttSoNhaDuong NVARCHAR(100),
    @thtTinhThanh NVARCHAR(100),
    @thtQuanHuyen NVARCHAR(100),
    @thtPhuongXa NVARCHAR(100),
    @thtSoNhaDuong NVARCHAR(100),
    @HoTenCha NVARCHAR(100),
    @NgaySinhCha DATE,
    @HoTenMe NVARCHAR(100),
    @NgaySinhMe DATE
AS
BEGIN
    EXEC sp_OpenSymmetricKey;

    INSERT INTO ResidentData (
        CCCD, HoTen, GioiTinh, NgaySinh, NoiSinh, NgayCap, NoiCap,
        DanToc, TonGiao, SĐT,
        ttTinhThanh, ttQuanHuyen, ttPhuongXa, ttSoNhaDuong,
        thtTinhThanh, thtQuanHuyen, thtPhuongXa, thtSoNhaDuong,
        HoTenCha, NgaySinhCha, HoTenMe, NgaySinhMe
    )
    VALUES (
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @CCCD),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @HoTen),
        @GioiTinh,
        @NgaySinh,
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @NoiSinh),
        @NgayCap,
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @NoiCap),
        @DanToc,
        @TonGiao,
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @SĐT),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @ttTinhThanh),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @ttQuanHuyen),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @ttPhuongXa),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @ttSoNhaDuong),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @thtTinhThanh),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @thtQuanHuyen),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @thtPhuongXa),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @thtSoNhaDuong),
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @HoTenCha),
        @NgaySinhCha,
        EncryptByKey(Key_GUID('SymmetricKey_AES'), @HoTenMe),
        @NgaySinhMe
    );

    EXEC sp_CloseSymmetricKey;
END
GO

-- Tạo bảng Audit log
CREATE TABLE AuditLog (
    LogID INT IDENTITY PRIMARY KEY,
    Username VARCHAR(50),
    Action NVARCHAR(100),
    TableName NVARCHAR(100),
    TimeStamp DATETIME DEFAULT GETDATE()
);
GO

-- Trigger ghi log khi cập nhật bảng User
CREATE TRIGGER trg_Audit_User_Update
ON [User]
AFTER UPDATE
AS
BEGIN
    INSERT INTO AuditLog(Username, Action, TableName)
    VALUES (SYSTEM_USER, 'UPDATE', 'User');
END
GO