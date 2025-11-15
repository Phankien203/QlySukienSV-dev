using Microsoft.EntityFrameworkCore;
using QlySukienSV.Constants;
using QlySukienSV.Models;

namespace QlySukienSV.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var passwordHash = Hash("123123");

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, TenRole = SystemRoles.QuanTri, MoTa = "Quản trị viên hệ thống" },
                new Role { Id = 2, TenRole = SystemRoles.BanChapHanh, MoTa = "Ban chấp hành Đoàn/Hội SV" },
                new Role { Id = 3, TenRole = SystemRoles.SinhVien, MoTa = "Sinh viên" },
                new Role { Id = 4, TenRole = SystemRoles.GiangVien, MoTa = "Giảng viên" }
            );

            // Seed NguoiDung
            modelBuilder.Entity<NguoiDung>().HasData(
                new NguoiDung
                {
                    Id = 1,
                    MaSoSV = null, // Admin không có mã SV
                    HoTen = "Quản trị viên",
                    Lop = null,
                    Email = "admin@gmail.com",
                    MatKhau = passwordHash,
                    SoDienThoai = "0911000000",
                    RoleId = 1 // QuanTri
                },
                new NguoiDung
                {
                    Id = 2,
                    MaSoSV = null, // BCH không có mã SV
                    HoTen = "Ban chấp hành",
                    Lop = "QLCL",
                    Email = "bch001@gmail.com",
                    MatKhau = passwordHash,
                    SoDienThoai = "0911000001",
                    RoleId = 2 // BanChapHanh
                },
                new NguoiDung
                {
                    Id = 3,
                    MaSoSV = "SV001",
                    HoTen = "Nguyễn Văn A",
                    Lop = "CNTT K45",
                    Email = "sv001@gmail.com",
                    MatKhau = passwordHash,
                    SoDienThoai = "0911000002",
                    RoleId = 3 // SinhVien
                },
                new NguoiDung
                {
                    Id = 4,
                    MaSoSV = "SV002",
                    HoTen = "Trần Thị B",
                    Lop = "CNTT K45",
                    Email = "sv002@gmail.com",
                    MatKhau = passwordHash,
                    SoDienThoai = "0911000003",
                    RoleId = 3 // SinhVien
                },
                new NguoiDung
                {
                    Id = 5,
                    MaSoSV = "SV003",
                    HoTen = "Lê Văn C",
                    Lop = "KinhTe K46",
                    Email = "sv003@gmail.com",
                    MatKhau = passwordHash,
                    SoDienThoai = "0911000004",
                    RoleId = 3 // SinhVien
                }
            );

            // Seed CLB
            modelBuilder.Entity<CLB>().HasData(
                new CLB
                {
                    Id = 1,
                    TenCLB = "CLB Tình nguyện Xanh",
                    MoTa = "Hỗ trợ cộng đồng và tổ chức các chiến dịch tình nguyện.",
                    ChuNhiemId = 2,
                    NgayThanhLap = new DateTime(2018, 9, 1),
                    TrangThai = TrangThaiCLB.Active
                },
                new CLB
                {
                    Id = 2,
                    TenCLB = "CLB Công nghệ",
                    MoTa = "Nơi chia sẻ kiến thức, tổ chức workshop công nghệ.",
                    ChuNhiemId = 3,
                    NgayThanhLap = new DateTime(2019, 3, 15),
                    TrangThai = TrangThaiCLB.Active
                },
                new CLB
                {
                    Id = 3,
                    TenCLB = "CLB Nghệ thuật",
                    MoTa = "Các hoạt động về âm nhạc, nhảy và nghệ thuật.",
                    ChuNhiemId = 4,
                    NgayThanhLap = new DateTime(2017, 5, 20),
                    TrangThai = TrangThaiCLB.Active
                }
            );

            // Seed ThanhVienCLB
            modelBuilder.Entity<ThanhVienCLB>().HasData(
                new ThanhVienCLB
                {
                    Id = 1,
                    CLBId = 1,
                    NguoiDungId = 2,
                    VaiTro = CLBRole.ChuNhiem,
                    TrangThai = TrangThaiThanhVien.DaDuyet,
                    NgayThamGia = new DateTime(2018, 9, 1)
                },
                new ThanhVienCLB
                {
                    Id = 2,
                    CLBId = 1,
                    NguoiDungId = 3,
                    VaiTro = CLBRole.ThanhVien,
                    TrangThai = TrangThaiThanhVien.DaDuyet,
                    NgayThamGia = new DateTime(2023, 10, 1)
                },
                new ThanhVienCLB
                {
                    Id = 3,
                    CLBId = 2,
                    NguoiDungId = 3,
                    VaiTro = CLBRole.BanChapHanh,
                    TrangThai = TrangThaiThanhVien.DaDuyet,
                    NgayThamGia = new DateTime(2024, 1, 10)
                },
                new ThanhVienCLB
                {
                    Id = 4,
                    CLBId = 2,
                    NguoiDungId = 4,
                    VaiTro = CLBRole.ChuNhiem,
                    TrangThai = TrangThaiThanhVien.DaDuyet,
                    NgayThamGia = new DateTime(2019, 3, 15)
                },
                new ThanhVienCLB
                {
                    Id = 5,
                    CLBId = 3,
                    NguoiDungId = 5,
                    VaiTro = CLBRole.ChuNhiem,
                    TrangThai = TrangThaiThanhVien.DaDuyet,
                    NgayThamGia = new DateTime(2017, 5, 20)
                }
            );

            // Seed SuKien
            modelBuilder.Entity<SuKien>().HasData(
                new SuKien
                {
                    Id = 1,
                    CLBId = 1,
                    TenSuKien = "Chiến dịch mùa hè xanh",
                    MoTa = "Chiến dịch tình nguyện tại các tỉnh miền Trung.",
                    NgayBatDau = new DateTime(2025, 6, 1, 8, 0, 0),
                    NgayKetThuc = new DateTime(2025, 6, 5, 18, 0, 0),
                    DiaDiem = "Hội trường ABC",
                    TrangThai = TrangThaiSuKien.HoanThanh,
                    NoiBat = true
                },
                new SuKien
                {
                    Id = 2,
                    CLBId = 2,
                    TenSuKien = "Tech Talk 2025",
                    MoTa = "Tọa đàm về xu hướng AI và Cloud.",
                    NgayBatDau = new DateTime(2025, 2, 15, 14, 0, 0),
                    NgayKetThuc = new DateTime(2025, 2, 15, 17, 0, 0),
                    DiaDiem = "Phòng Lab 301",
                    TrangThai = TrangThaiSuKien.HoanThanh,
                    NoiBat = true
                },
                new SuKien
                {
                    Id = 3,
                    CLBId = 3,
                    TenSuKien = "Đêm nghệ thuật Xuân",
                    MoTa = "Chương trình văn nghệ mừng năm mới.",
                    NgayBatDau = new DateTime(2025, 1, 20, 19, 0, 0),
                    NgayKetThuc = new DateTime(2025, 1, 20, 21, 0, 0),
                    DiaDiem = "Sân khấu B",
                    TrangThai = TrangThaiSuKien.HoanThanh,
                    NoiBat = false
                }
            );

            // Seed DangKySuKien
            modelBuilder.Entity<DangKySuKien>().HasData(
                new DangKySuKien
                {
                    Id = 1,
                    SuKienId = 1,
                    NguoiDungId = 3,
                    TrangThai = TrangThaiDangKy.DaDuyet,
                    ThoiGianDangKy = new DateTime(2025, 1, 5, 9, 30, 0)
                },
                new DangKySuKien
                {
                    Id = 2,
                    SuKienId = 2,
                    NguoiDungId = 4,
                    TrangThai = TrangThaiDangKy.ChoDuyet,
                    ThoiGianDangKy = new DateTime(2025, 1, 10, 10, 0, 0)
                }
            );

            // Seed DiemDanhSuKien
            modelBuilder.Entity<DiemDanhSuKien>().HasData(
                new DiemDanhSuKien
                {
                    Id = 1,
                    SuKienId = 1,
                    NguoiDungId = 3,
                    ThoiGianDiemDanh = new DateTime(2025, 6, 1, 8, 15, 0),
                    HinhThuc = "QR"
                }
            );
        }

        private static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, "$2a$11$abcdefghijklmnopqrstuv");
        }
    }
}
