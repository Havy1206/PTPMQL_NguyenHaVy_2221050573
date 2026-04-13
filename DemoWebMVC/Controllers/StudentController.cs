using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data; 
using DemoMVC.Models.Entities; 
using System.Linq;
using Microsoft.EntityFrameworkCore; // 1. BẮT BUỘC có dòng này để dùng .Include()
using DemoMVC.Models; // 2. Để nhận diện StudentVM

namespace DemoWebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. HIỂN THỊ DANH SÁCH (Sửa lại ở đây) ---
        public IActionResult Index()
        {
            // Sử dụng LINQ để "nối" bảng Students và Faculties
            var data = _context.Students
                .Include(s => s.Faculty) // Kéo dữ liệu từ bảng Faculty sang
                .Select(s => new StudentVM 
                {
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    // Nếu sinh viên chưa có khoa thì hiện "N/A"
                    Age = s.Age, // BẮT BUỘC phải có dòng này
                    FacultyName = s.Faculty != null ? s.Faculty.FacultyName : "Chưa có khoa"
                })
                .ToList();

            return View(data);
        }

        // --- 2. THÊM MỚI (GET) - Sửa để hiện danh sách chọn Khoa ---
        public IActionResult Create()
        {
            // Gửi danh sách khoa sang View để làm Dropdown
            ViewBag.FacultyID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(std);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            // Nếu lỗi, phải gửi lại danh sách khoa để Dropdown không bị trống
            ViewBag.FacultyID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View(std);
        }

        // --- 3. CHỈNH SỬA (Edit) ---
        public IActionResult Edit(string id) 
        {
            if (id == null) return NotFound();
            var std = _context.Students.Find(id);
            if (std == null) return NotFound();
            
            // Tương tự, gửi danh sách khoa cho trang Edit
            ViewBag.FacultyID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(std);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyID = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }

        // --- 4. XÓA (Delete) ---
        [HttpGet]
        public IActionResult Delete(string id)
        {
            if (id == null) return NotFound();
            var std = _context.Students.Find(id);
            if (std == null) return NotFound();
            return View(std);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var std = _context.Students.Find(id);
            if (std != null)
            {
                _context.Students.Remove(std);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}