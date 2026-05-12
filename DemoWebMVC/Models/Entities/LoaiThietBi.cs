using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models.Entities
{
    public class LoaiThietBi
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên loại thiết bị")]
        public string? TenLoai { get; set; }
        public string? MoTa { get; set; }
        public virtual ICollection<ThietBi>? ThietBis { get; set; }
    }
}