using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities
{
    public class ChiTietPhieuNhap
    {
        [Key]
        public int Id { get; set; }
        public int PhieuNhapId { get; set; }
        [ForeignKey("PhieuNhapId")]
        public virtual PhieuNhap? PhieuNhap { get; set; }

        public int ThietBiId { get; set; }
        [ForeignKey("ThietBiId")]
        public virtual ThietBi? ThietBi { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
    }
}