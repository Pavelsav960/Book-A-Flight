using Booking.Data;
using Booking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Booking.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public static Flight flightObject { get; set; }
        public static int PaymentTickets { get; set; }
        public static int PaymentflightId { get; set; }
        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            return _context.payments != null ?
                        View(await _context.payments.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.payments'  is null.");
        }

        // GET: Payments/Details/5
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

        // GET: Payments/Create
        public IActionResult Create(int flightID,int numOfTickets)
        {
            PaymentTickets = numOfTickets;
            PaymentflightId = flightID;
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNumber,Email,CardHolderName,CardNumber,ExparationMonth,ExparationYear,SaveDetails")] Payment payment)
        {
            if (!CardValidator.IsValidCardNumber(payment.CardNumber))
            {
                ModelState.AddModelError("CardNumber", "Invalid Card Number");

            }
            if (Int32.Parse(payment.ExparationYear) < Int32.Parse(DateTime.Now.Year.ToString()))
            {
                ModelState.AddModelError("ExparationYear", "Invalid Card Exparation Year");

            }
            if (Int32.Parse(payment.ExparationMonth) < Int32.Parse(DateTime.Now.Month.ToString()))
            {
                ModelState.AddModelError("ExparationMonth", "Invalid Card Exparation Month");

            }
            if (ModelState.IsValid)
            {
                if (payment.SaveDetails == false)
                {
                    payment.CardNumber = "";

                }
                else 
                {
                    var FindKey = await _context.Bookings
                    .FirstOrDefaultAsync(m => m.DepartingFlightId == 1);
                    string TempCardNumber = EncryptionClass.EncryptCardNumber(payment.CardNumber, FindKey.BookingEmail);
                    payment.CardNumber = TempCardNumber;
                    string DecryptCardNumber = EncryptionClass.DecryptCardNumber(TempCardNumber, FindKey.BookingEmail);
                }
                if (_context.Flights == null)
                {
                    return NotFound();
                }

                var flight = await _context.Flights
                    .FirstOrDefaultAsync(m => m.FlightId == PaymentflightId);
                if (flight == null)
                {
                    return NotFound();
                }
                payment.Email = User.Identity.Name;
                
                var email = User.Identity.Name;
                flight.AvailableSeats -= PaymentTickets;
                BookingModel newBook = new BookingModel()
                {
                    NumOfTickets = PaymentTickets,
                    BookingEmail = email,
                    DepartingFlightId = flight.FlightId,
                    ReturnFlightId = flight.FlightId,
                    DepartingSeatNumber = flight.AvailableSeats
                };
                _context.Add(newBook);
                _context.Add(payment);
                await _context.SaveChangesAsync();
                TempData["Success"] = true;
                return RedirectToAction("Index", "Home");
            }
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.payments == null)
            {
                return NotFound();
            }

            var payment = await _context.payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNumber,Email,CardHolderName,CardNumber,ExparationMonth,ExparationYear,SaveDetails")] Payment payment)
        {
            if (id != payment.IdNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.IdNumber))
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
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.payments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.payments'  is null.");
            }
            var payment = await _context.payments.FindAsync(id);
            if (payment != null)
            {
                _context.payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return (_context.payments?.Any(e => e.IdNumber == id)).GetValueOrDefault();
        }
    }
}
