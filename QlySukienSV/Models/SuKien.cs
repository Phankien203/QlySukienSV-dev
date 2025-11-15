using QlySukienSV.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlySukienSV.Models
{
    public class SuKien
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TenSuKien { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar(max)")]
        public string? MoTa { get; set; }

        public int CLBId { get; set; }

        [ForeignKey(nameof(CLBId))]
        public CLB? CLB { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        [StringLength(200)]
        public string? DiaDiem { get; set; }

        [Required]
        [StringLength(30)]
        public string TrangThai { get; set; } = TrangThaiSuKien.SapDienRa;

        public bool NoiBat { get; set; }

        public ICollection<DangKySuKien> DangKySuKiens { get; set; } = new List<DangKySuKien>();
        public ICollection<DiemDanhSuKien> DiemDanhSuKiens { get; set; } = new List<DiemDanhSuKien>();
    }
}
