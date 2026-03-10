using Microsoft.AspNetCore.Mvc;
using DemoMVC.Data; 
using DemoMVC.Models.Entities; // Phải dùng Entities vì Student nằm ở đây
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

        // --- 1. HIỂN THỊ DANH SÁCH (Read) ---
        public IActionResult Index()
        {
            var data = _context.Students.ToList();
            return View(data);
        }

        // --- 2. THÊM MỚI (Create) ---
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(std);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(std);
        }

        // --- 3. CHỈNH SỬA (Edit) ---
        // Đổi 'int id' thành 'string id' vì StudentCode là kiểu chuỗi
        public IActionResult Edit(string id) 
        {
            if (id == null) return NotFound();
            var std = _context.Students.Find(id);
            if (std == null) return NotFound();
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
            return View(std);
        }

        // --- 4. XÓA (Delete) ---
        public IActionResult Delete(string id)
        {
            var std = _context.Students.Find(id);
            if (std != null)
            {
                _context.Students.Remove(std);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}