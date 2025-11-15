using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlySukienSV.Models
{
    public class NguoiDung
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string? MaSoSV { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Lop { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; } = string.Empty;

        [StringLength(15)]
        [Phone]
        public string? SoDienThoai { get; set; }

        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role? Role { get; set; }

        // Navigation properties
        public ICollection<ThanhVienCLB> ThanhVienCLBs { get; set; } = new List<ThanhVienCLB>();
        public ICollection<DangKySuKien> DangKySuKiens { get; set; } = new List<DangKySuKien>();
        public ICollection<DiemDanhSuKien> DiemDanhSuKiens { get; set; } = new List<DiemDanhSuKien>();
    }
}
