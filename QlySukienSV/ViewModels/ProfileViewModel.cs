using System.ComponentModel.DataAnnotations;

namespace QlySukienSV.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Mã sinh viên")]
        public string MaSoSV { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Lớp")]
        public string? Lop { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Số điện thoại")]
        public string? SoDienThoai { get; set; }

        public string VaiTro { get; set; } = string.Empty;
    }
}
