using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoMVC.Data; 
using DemoMVC.Models.Entities; 
using DemoMVC.Models; 
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DemoWebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- 1. HIỂN THỊ DANH SÁCH ---
        public IActionResult Index()
        {
            var data = _context.Students
                .Include(s => s.Faculty) 
                .Select(s => new StudentVM 
                {
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    Age = s.Age,
                    FacultyName = s.Faculty != null ? s.Faculty.FacultyName : "Chưa có khoa"
                })
                .ToList();

            return View(data);
        }

        // --- 2. THÊM MỚI (GET) ---
        public IActionResult Create()
        {
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View();
        }

        // --- 2. THÊM MỚI (POST) - Đã fix lỗi trùng mã ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student std)
        {
            // Bỏ qua kiểm tra object Faculty liên kết
            ModelState.Remove("Faculty");

            // KIỂM TRA TRÙNG MÃ SINH VIÊN (Fix lỗi PK Students)
            bool isExist = _context.Students.Any(s => s.StudentCode == std.StudentCode);
            if (isExist)
            {
                ModelState.AddModelError("StudentCode", "Mã sinh viên này đã tồn tại!");
            }

            if (ModelState.IsValid)
            {
                _context.Students.Add(std);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            
            // Nếu lỗi thì load lại danh sách khoa cho Dropdown
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }

        // --- 3. CHỈNH SỬA (GET) ---
        public IActionResult Edit(string id) 
        {
            if (id == null) return NotFound();
            var std = _context.Students.Find(id);
            if (std == null) return NotFound();
            
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }

        // --- 3. CHỈNH SỬA (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student std)
        {
            ModelState.Remove("Faculty");

            if (ModelState.IsValid)
            {
                _context.Update(std);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }

        // --- 4. XÓA ---
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