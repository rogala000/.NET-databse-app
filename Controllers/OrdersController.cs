using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RogalskiJaroslaw.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrdersContext _context;

        public OrdersController(OrdersContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.idSortParam = String.IsNullOrEmpty(sortOrder) ? "id" : "";
            ViewBag.DateSortParm = "Date";
            ViewBag.NumberSortParam = "number";
            ViewBag.CommentsSortParam = "comments";
            ViewBag.OriginSortParam = "origin";
            ViewBag.DeliverySortParam = "delivery";

            var order = from s in _context.Orders
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                order = order.Where(s => s.OrderComments.Contains(searchString)
                                       || s.OrderOrigin.Contains(searchString) 
                                       || s.OrderNumber.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id":
                    order = order.OrderByDescending(s => s.OrderId);
                    break;
                case "number":
                    order = order.OrderBy(s => s.OrderNumber);
                    break;
                case "Date":
                    order = order.OrderByDescending(s => s.OrderDate);
                    break;
                case "comments":
                    order = order.OrderBy(s => s.OrderComments);
                    break;
                case "origin":
                    order = order.OrderBy(s => s.OrderOrigin);
                    break;
                case "delivery":
                    order = order.OrderByDescending(s => s.EstimatedDelivery);
                    break;

                default:
                    order = order.OrderByDescending(s => s.OrderId);
                    break;
            }
            return View(order.ToList());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderNumber,OrderDate,OrderComments,OrderOrigin,EstimatedDelivery")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderNumber,OrderDate,OrderComments,OrderOrigin,EstimatedDelivery")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
