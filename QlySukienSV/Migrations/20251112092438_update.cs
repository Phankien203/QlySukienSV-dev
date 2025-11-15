using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QlySukienSV.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDungs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaSoSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Lop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDungs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NguoiDungs_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CLBs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCLB = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChuNhiemId = table.Column<int>(type: "int", nullable: false),
                    NgayThanhLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLBs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CLBs_NguoiDungs_ChuNhiemId",
                        column: x => x.ChuNhiemId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SuKiens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSuKien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CLBId = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    NoiBat = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuKiens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuKiens_CLBs_CLBId",
                        column: x => x.CLBId,
                        principalTable: "CLBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThanhVienCLBs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CLBId = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    VaiTro = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayThamGia = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhVienCLBs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanhVienCLBs_CLBs_CLBId",
                        column: x => x.CLBId,
                        principalTable: "CLBs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThanhVienCLBs_NguoiDungs_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DangKySuKiens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuKienId = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ThoiGianDangKy = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DangKySuKiens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DangKySuKiens_NguoiDungs_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DangKySuKiens_SuKiens_SuKienId",
                        column: x => x.SuKienId,
                        principalTable: "SuKiens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiemDanhSuKiens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuKienId = table.Column<int>(type: "int", nullable: false),
                    NguoiDungId = table.Column<int>(type: "int", nullable: false),
                    ThoiGianDiemDanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HinhThuc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemDanhSuKiens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiemDanhSuKiens_NguoiDungs_NguoiDungId",
                        column: x => x.NguoiDungId,
                        principalTable: "NguoiDungs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiemDanhSuKiens_SuKiens_SuKienId",
                        column: x => x.SuKienId,
                        principalTable: "SuKiens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "MoTa", "TenRole" },
                values: new object[,]
                {
                    { 1, "Quản trị viên hệ thống", "QuanTri" },
                    { 2, "Ban chấp hành Đoàn/Hội SV", "BanChapHanh" },
                    { 3, "Sinh viên", "SinhVien" },
                    { 4, "Giảng viên", "GiangVien" }
                });

            migrationBuilder.InsertData(
                table: "NguoiDungs",
                columns: new[] { "Id", "Email", "HoTen", "Lop", "MaSoSV", "MatKhau", "RoleId", "SoDienThoai" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "Quản trị viên", null, null, "$2a$11$abcdefghijklmnopqrstuu57/Uc/xTpWqycuFrOG98xEU9RAuvm36", 1, "0911000000" },
                    { 2, "bch001@gmail.com", "Ban chấp hành", "QLCL", null, "$2a$11$abcdefghijklmnopqrstuu57/Uc/xTpWqycuFrOG98xEU9RAuvm36", 2, "0911000001" },
                    { 3, "sv001@gmail.com", "Nguyễn Văn A", "CNTT K45", "SV001", "$2a$11$abcdefghijklmnopqrstuu57/Uc/xTpWqycuFrOG98xEU9RAuvm36", 3, "0911000002" },
                    { 4, "sv002@gmail.com", "Trần Thị B", "CNTT K45", "SV002", "$2a$11$abcdefghijklmnopqrstuu57/Uc/xTpWqycuFrOG98xEU9RAuvm36", 3, "0911000003" },
                    { 5, "sv003@gmail.com", "Lê Văn C", "KinhTe K46", "SV003", "$2a$11$abcdefghijklmnopqrstuu57/Uc/xTpWqycuFrOG98xEU9RAuvm36", 3, "0911000004" }
                });

            migrationBuilder.InsertData(
                table: "CLBs",
                columns: new[] { "Id", "ChuNhiemId", "MoTa", "NgayThanhLap", "TenCLB", "TrangThai" },
                values: new object[,]
                {
                    { 1, 2, "Hỗ trợ cộng đồng và tổ chức các chiến dịch tình nguyện.", new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLB Tình nguyện Xanh", "Active" },
                    { 2, 3, "Nơi chia sẻ kiến thức, tổ chức workshop công nghệ.", new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLB Công nghệ", "Active" },
                    { 3, 4, "Các hoạt động về âm nhạc, nhảy và nghệ thuật.", new DateTime(2017, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "CLB Nghệ thuật", "Active" }
                });

            migrationBuilder.InsertData(
                table: "SuKiens",
                columns: new[] { "Id", "CLBId", "DiaDiem", "MoTa", "NgayBatDau", "NgayKetThuc", "NoiBat", "TenSuKien", "TrangThai" },
                values: new object[,]
                {
                    { 1, 1, "Hội trường ABC", "Chiến dịch tình nguyện tại các tỉnh miền Trung.", new DateTime(2025, 6, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 5, 18, 0, 0, 0, DateTimeKind.Unspecified), true, "Chiến dịch mùa hè xanh", "SapDienRa" },
                    { 2, 2, "Phòng Lab 301", "Tọa đàm về xu hướng AI và Cloud.", new DateTime(2025, 2, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), true, "Tech Talk 2025", "SapDienRa" },
                    { 3, 3, "Sân khấu B", "Chương trình văn nghệ mừng năm mới.", new DateTime(2025, 1, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 20, 21, 0, 0, 0, DateTimeKind.Unspecified), false, "Đêm nghệ thuật Xuân", "SapDienRa" }
                });

            migrationBuilder.InsertData(
                table: "ThanhVienCLBs",
                columns: new[] { "Id", "CLBId", "NgayThamGia", "NguoiDungId", "TrangThai", "VaiTro" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2018, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "DaDuyet", "ChuNhiem" },
                    { 2, 1, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "DaDuyet", "ThanhVien" },
                    { 3, 2, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "DaDuyet", "BanChapHanh" },
                    { 4, 2, new DateTime(2019, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "DaDuyet", "ChuNhiem" },
                    { 5, 3, new DateTime(2017, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "DaDuyet", "ChuNhiem" }
                });

            migrationBuilder.InsertData(
                table: "DangKySuKiens",
                columns: new[] { "Id", "NguoiDungId", "SuKienId", "ThoiGianDangKy", "TrangThai" },
                values: new object[,]
                {
                    { 1, 3, 1, new DateTime(2025, 1, 5, 9, 30, 0, 0, DateTimeKind.Unspecified), "DaDuyet" },
                    { 2, 4, 2, new DateTime(2025, 1, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), "ChoDuyet" }
                });

            migrationBuilder.InsertData(
                table: "DiemDanhSuKiens",
                columns: new[] { "Id", "HinhThuc", "NguoiDungId", "SuKienId", "ThoiGianDiemDanh" },
                values: new object[] { 1, "QR", 3, 1, new DateTime(2025, 6, 1, 8, 15, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_CLBs_ChuNhiemId",
                table: "CLBs",
                column: "ChuNhiemId");

            migrationBuilder.CreateIndex(
                name: "IX_DangKySuKiens_NguoiDungId",
                table: "DangKySuKiens",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_DangKySuKiens_SuKienId",
                table: "DangKySuKiens",
                column: "SuKienId");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanhSuKiens_NguoiDungId",
                table: "DiemDanhSuKiens",
                column: "NguoiDungId");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDanhSuKiens_SuKienId",
                table: "DiemDanhSuKiens",
                column: "SuKienId");

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_Email",
                table: "NguoiDungs",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NguoiDungs_RoleId",
                table: "NguoiDungs",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_TenRole",
                table: "Roles",
                column: "TenRole",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuKiens_CLBId",
                table: "SuKiens",
                column: "CLBId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhVienCLBs_CLBId",
                table: "ThanhVienCLBs",
                column: "CLBId");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhVienCLBs_NguoiDungId",
                table: "ThanhVienCLBs",
                column: "NguoiDungId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DangKySuKiens");

            migrationBuilder.DropTable(
                name: "DiemDanhSuKiens");

            migrationBuilder.DropTable(
                name: "ThanhVienCLBs");

            migrationBuilder.DropTable(
                name: "SuKiens");

            migrationBuilder.DropTable(
                name: "CLBs");

            migrationBuilder.DropTable(
                name: "NguoiDungs");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
