using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities
{
    public class ThietBi
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? TenTB { get; set; }
        public int SoLuongTon { get; set; }
        public decimal DonGia { get; set; }

        public int LoaiThietBiId { get; set; }
        [ForeignKey("LoaiThietBiId")]
        public virtual LoaiThietBi? LoaiThietBi { get; set; }

        public int NhaCungCapId { get; set; }
        [ForeignKey("NhaCungCapId")]
        public virtual NhaCungCap? NhaCungCap { get; set; }
    }
}