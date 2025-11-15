using QlySukienSV.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QlySukienSV.Models
{
    public class ThanhVienCLB
    {
        public int Id { get; set; }

        public int CLBId { get; set; }

        [ForeignKey(nameof(CLBId))]
        public CLB? CLB { get; set; }

        public int NguoiDungId { get; set; }

        [ForeignKey(nameof(NguoiDungId))]
        public NguoiDung? NguoiDung { get; set; }

        [Required]
        [StringLength(20)]
        public string VaiTro { get; set; } = CLBRole.ThanhVien;

        [Required]
        [StringLength(20)]
        public string TrangThai { get; set; } = TrangThaiThanhVien.ChoDuyet;

        public DateTime NgayThamGia { get; set; } = DateTime.UtcNow;
    }
}
