using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoMVC.Data;
using DemoMVC.Models.Entities;

namespace DemoMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Danh sách khách hàng (Seed Data đa dạng)
        public async Task<IActionResult> Index()
        {
            if (!_context.Customers.Any())
            {
                var seedCustomer = new Customer { FullName = "Nguyễn Hà Vy", Address = "Hanoi University of Mining and Geology" };
                _context.Customers.Add(seedCustomer);
                await _context.SaveChangesAsync();
            }

            var data = await _context.Customers.ToListAsync();
            return View(data);
        }

        // 2. Chi tiết đơn hàng
        public async Task<IActionResult> Details(int? id)
{
    if (id == null) return NotFound();

    var customer = await _context.Customers
        .Include(c => c.Orders!)
            .ThenInclude(o => o.OrderDetails!)
                .ThenInclude(od => od.Product)
        .FirstOrDefaultAsync(m => m.CustomerId == id);

    if (customer == null) return NotFound();

    // Nếu khách này chưa mua gì, tui tặng họ 1 đơn hàng mẫu luôn để bà test cho đẹp
    if (customer.Orders == null || !customer.Orders.Any())
    {
        var product = await _context.Products.FirstOrDefaultAsync() 
                      ?? new Product { ProductName = "iPhone 15 Pro Max", Price = 30000000 };
        
        if (product.ProductID == 0) _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var order = new Order { OrderDate = DateTime.Now, CustomerId = customer.CustomerId };
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        _context.OrderDetails.Add(new OrderDetail { 
            OrderID = order.OrderId, 
            ProductID = product.ProductID, 
            Quantity = 1 
        });
        await _context.SaveChangesAsync();

        // Load lại để thấy thành quả
        return RedirectToAction(nameof(Details), new { id = id });
    }

    return View(customer);
}

        // 3. Create (GET)
        public IActionResult Create() => View();

        // 4. Create (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // 5. Edit (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // 6. Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
        {
            if (id != customer.CustomerId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Customers.Any(e => e.CustomerId == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // 7. Delete (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // 8. Delete (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null) _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}