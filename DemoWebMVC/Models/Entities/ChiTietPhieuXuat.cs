using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoMVC.Models.Entities
{
    public class ChiTietPhieuXuat
    {
        [Key]
        public int Id { get; set; }
        public int PhieuXuatId { get; set; }
        [ForeignKey("PhieuXuatId")]
        public virtual PhieuXuat? PhieuXuat { get; set; }

        public int ThietBiId { get; set; }
        [ForeignKey("ThietBiId")]
        public virtual ThietBi? ThietBi { get; set; }

        public int SoLuong { get; set; }
        public decimal DonGiaXuat { get; set; }
    }
}