using QlySukienSV.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlySukienSV.Models
{
    public class CLB
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenCLB { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? MoTa { get; set; }

        public int ChuNhiemId { get; set; }

        [ForeignKey("ChuNhiemId")]
        public NguoiDung? ChuNhiem { get; set; }

        public DateTime NgayThanhLap { get; set; }

        [Required]
        [StringLength(20)]
        public string TrangThai { get; set; } = TrangThaiCLB.Active;

        // Navigation properties
        public ICollection<ThanhVienCLB> ThanhViens { get; set; } = new List<ThanhVienCLB>();
        public ICollection<SuKien> SuKiens { get; set; } = new List<SuKien>();
    }
}
