using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlySukienSV.Models
{
    public class DiemDanhSuKien
    {
        public int Id { get; set; }

        public int SuKienId { get; set; }

        [ForeignKey(nameof(SuKienId))]
        public SuKien? SuKien { get; set; }

        public int NguoiDungId { get; set; }

        [ForeignKey(nameof(NguoiDungId))]
        public NguoiDung? NguoiDung { get; set; }

        public DateTime ThoiGianDiemDanh { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(20)]
        public string HinhThuc { get; set; } = "QR";
    }
}
