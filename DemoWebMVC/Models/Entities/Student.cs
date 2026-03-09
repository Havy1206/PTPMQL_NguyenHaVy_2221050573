
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace DemoMVC.Models.Entities
{
    public class Student
    {
        [Key]
        public string StudentCode { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public int? Age { get; set; }
    }
}