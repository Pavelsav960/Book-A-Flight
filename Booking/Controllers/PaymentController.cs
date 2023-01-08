using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.Data;
using Booking.Models;

namespace Booking.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;
     

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            return _context.payments != null ?
                        View(await _context.payments.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.payments'  is null.");
        }

        // GET: Payment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.payments == null)
            {
                return NotFound();
            }

            var payment = await _context.payments
                .FirstOrDefaultAsync(m => m.IdNumber == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNumber,Email,CardHolderName,CardNumber,ExparationMonth,ExparationYear,SaveDetails")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                if (payment.SaveDetails == true)
                {
                    _context.Add(payment);
                }
                await _context.SaveChangesAsync();
                ViewData["Success"] = "Payment was successful , Thank you for booking with us !";
                return RedirectToAction("Index","Home");
            }
            return View(payment);
        }

      

        private bool PaymentExists(int id)
        {
            return (_context.payments?.Any(e => e.IdNumber == id)).GetValueOrDefault();
        }
    }
}
