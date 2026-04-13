using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;

namespace DemoMVC.Controllers
{
    public class FacultyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacultyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. HIỂN THỊ DANH SÁCH KHOA
        // GET: Faculty
        public async Task<IActionResult> Index()
        {
            var faculties = await _context.Faculties.ToListAsync();
            return View(faculties);
        }

        // 2. XEM CHI TIẾT KHOA
        // GET: Faculty/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.FacultyID == id);
            
            if (faculty == null) return NotFound();

            return View(faculty);
        }

        // 3. THÊM MỚI KHOA (GIAO DIỆN)
        // GET: Faculty/Create
        public IActionResult Create()
        {
            return View();
        }

        // 4. THÊM MỚI KHOA (XỬ LÝ LƯU)
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Faculty faculty) 
{
    // Xóa bỏ kiểm tra FacultyID vì nó tự tăng, không cần check IsValid cho nó
    ModelState.Remove("FacultyID");
    // Xóa bỏ kiểm tra danh sách Students kèm theo (nếu có)
    ModelState.Remove("Students");

    if (ModelState.IsValid)
    {
        try 
        {
            _context.Add(faculty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Nếu lỗi Database nó sẽ hiện thông báo này
            ModelState.AddModelError("", "Lỗi Database: " + ex.Message);
        }
    }
    else 
    {
        // Nếu dữ liệu không hợp lệ, nó sẽ nhảy vào đây
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors) {
            Console.WriteLine("Lỗi Validation: " + error.ErrorMessage);
        }
    }
    
    return View(faculty);
}

        // 5. CẬP NHẬT KHOA (GIAO DIỆN)
        // GET: Faculty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty == null) return NotFound();
            
            return View(faculty);
        }

        // 6. CẬP NHẬT KHOA (XỬ LÝ LƯU)
        // POST: Faculty/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacultyID,FacultyName")] Faculty faculty)
        {
            if (id != faculty.FacultyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faculty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.FacultyID))
                    {
                        return NotFound();
                    }
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(faculty);
        }

        // 7. XÓA KHOA (GIAO DIỆN XÁC NHẬN)
        // GET: Faculty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var faculty = await _context.Faculties
                .FirstOrDefaultAsync(m => m.FacultyID == id);
            
            if (faculty == null) return NotFound();

            return View(faculty);
        }

        // 8. XÓA KHOA (XỬ LÝ XÓA THẬT)
        // POST: Faculty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faculty = await _context.Faculties.FindAsync(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // HÀM HỖ TRỢ KIỂM TRA TỒN TẠI
        private bool FacultyExists(int id)
        {
            return _context.Faculties.Any(e => e.FacultyID == id);
        }
    }
}