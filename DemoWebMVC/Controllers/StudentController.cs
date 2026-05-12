using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DemoMVC.Data; 
using DemoMVC.Models.Entities; 
using DemoMVC.Models; 
using Microsoft.EntityFrameworkCore;
using System.Linq;
using OfficeOpenXml;
using System.IO;

namespace DemoMVC.Controllers
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

        // --- 2. HÀM IMPORT EXCEL (Đã fix lỗi và thêm dò tìm tên Khoa) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                TempData["Error"] = "Vui lòng chọn file Excel!";
                return RedirectToAction(nameof(Index));
            }

            // Cấu hình bản quyền EPPlus chuẩn mới
            // ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            var studentList = new List<Student>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var sCode = worksheet.Cells[row, 1].Value?.ToString()?.Trim();
                        
                        if (string.IsNullOrEmpty(sCode)) continue;

                        // Kiểm tra trùng mã SV trong Database
                        bool isExist = _context.Students.Any(s => s.StudentCode == sCode);
                        if (isExist) continue;

                        // --- LOGIC DÒ TÌM TÊN KHOA ---
                        var facultyNameExcel = worksheet.Cells[row, 5].Value?.ToString()?.Trim();
                        int? facultyId = null;

                        if (!string.IsNullOrEmpty(facultyNameExcel))
                        {
                            var faculty = _context.Faculties
                                .FirstOrDefault(f => f.FacultyName == facultyNameExcel);
                            
                            if (faculty != null)
                            {
                                facultyId = faculty.FacultyID;
                            }
                        }

                        // --- MAPPING DỮ LIỆU ---
                        var std = new Student
                        {
                            StudentCode = sCode,
                            FullName = worksheet.Cells[row, 2].Value?.ToString()?.Trim() ?? "Không tên",
                            // Fix gạch vàng Age
                            Age = worksheet.Cells[row, 3].Value != null ? int.Parse(worksheet.Cells[row, 3].Value.ToString()!) : null,
                            Email = worksheet.Cells[row, 4].Value?.ToString()?.Trim(),
                            // Gán FacultyID tìm được từ tên khoa
                            FacultyID = facultyId
                        };
                        studentList.Add(std);
                    }
                }
            }

            if (studentList.Count > 0)
            {
                _context.Students.AddRange(studentList);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Đã nhập thành công {studentList.Count} sinh viên!";
            }
            else
            {
                TempData["Error"] = "Không có dữ liệu mới để nhập!";
            }

            return RedirectToAction(nameof(Index));
        }

        // --- 3. THÊM MỚI (GET) ---
        public IActionResult Create()
        {
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyID", "FacultyName");
            return View();
        }

        // --- 4. THÊM MỚI (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student std)
        {
            ModelState.Remove("Faculty");

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
            
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }

        // --- 5. CHỈNH SỬA (GET) ---
        public IActionResult Edit(string id) 
        {
            if (id == null) return NotFound();
            var std = _context.Students.Find(id);
            if (std == null) return NotFound();
            
            ViewBag.FacultyID = new SelectList(_context.Faculties, "FacultyID", "FacultyName", std.FacultyID);
            return View(std);
        }

        // --- 6. CHỈNH SỬA (POST) ---
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

        // --- 7. XÓA ---
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