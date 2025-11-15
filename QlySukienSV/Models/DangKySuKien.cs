using QlySukienSV.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlySukienSV.Models
{
    public class DangKySuKien
    {
        public int Id { get; set; }

        public int SuKienId { get; set; }

        [ForeignKey(nameof(SuKienId))]
        public SuKien? SuKien { get; set; }

        public int NguoiDungId { get; set; }

        [ForeignKey(nameof(NguoiDungId))]
        public NguoiDung? NguoiDung { get; set; }

        [Required]
        [StringLength(20)]
        public string TrangThai { get; set; } = TrangThaiDangKy.ChoDuyet;

        public DateTime ThoiGianDangKy { get; set; } = DateTime.UtcNow;
    }
}
