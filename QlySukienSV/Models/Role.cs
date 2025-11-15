using System.ComponentModel.DataAnnotations;

namespace QlySukienSV.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string TenRole { get; set; } = string.Empty;

        [StringLength(200)]
        public string? MoTa { get; set; }

        // Navigation property
        public ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();
    }
}
