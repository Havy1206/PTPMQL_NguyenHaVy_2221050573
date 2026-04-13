namespace DemoMVC.Models.Entities
{
    public class Faculty
    {
        public int FacultyID { get; set; }

        public string FacultyName { get; set; } = default!;

        public virtual ICollection<Student> Students { get; set; } = default!;
    }
}