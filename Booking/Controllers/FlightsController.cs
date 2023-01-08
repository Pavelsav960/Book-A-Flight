using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.Data;
using Booking.Models;
using Microsoft.AspNetCore.Identity;

namespace Booking.Controllers
{
    public class FlightsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public static List<Flight> FilterFlight = new List<Flight>();
        public static Search search = new Search();


        public async Task<IActionResult> GetAllFlights(int id)
        {
            if(id == 1)
            {
                return _context.Flights != null ?
                 View(FilterFlight.Where(p => p.AvailableSeats > 0)
                        .Where(a => a.FlightDate >= DateTime.Now).OrderByDescending(x => x.Price).ToList()) :
                Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
            }
            if(id == 2) 
            {
                return _context.Flights != null ?
                 View(FilterFlight.Where(p => p.AvailableSeats > 0)
                        .Where(a => a.FlightDate >= DateTime.Now).OrderBy(x => x.Price).ToList()) :
                Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
            }
            if(id == 3)
            {
                FilterFlight =  await _context.Flights.ToListAsync();
                return _context.Flights != null ?
                    View(await _context.Flights.Where(p => p.AvailableSeats > 0)
                        .Where(a => a.FlightDate >= DateTime.Now).ToListAsync()) :
                 Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
            }
            if(id == 4)
            {
                return _context.Flights != null ?
                 View(FilterFlight.Where(p => p.AvailableSeats > 0)
                        .Where(a => a.FlightDate >= DateTime.Now).OrderBy(x => x.OriginCountry).ToList()) :
                Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
            }

            return _context.Flights != null ?
          View(await _context.Flights.Where(p => p.AvailableSeats > 0)
                        .Where(a => a.FlightDate >= DateTime.Now).ToListAsync()) :
                 Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
        }
        public async Task<IActionResult> Index(Search SearchFilter,int id)
        {
           
            FilterFlight = await _context.Flights.Where(x => x.DestinationCountry == SearchFilter.To)
                        .Where(y => y.OriginCountry == SearchFilter.From)
                        .Where(d => d.FlightDate >= SearchFilter.FlightDate && d.FlightDate <= SearchFilter.ReturnDate)
                        .Where(p => p.AvailableSeats > 0)
                        .Where(a => a.FlightDate >= DateTime.Now)
                        .Where(b => b.Price <= SearchFilter.PriceMax && b.Price >= SearchFilter.PriceMin)
                        .ToListAsync();
            return _context.Flights != null ?
                        View(await _context.Flights.Where(x => x.DestinationCountry == SearchFilter.To)
                        .Where(y => y.OriginCountry == SearchFilter.From)
                        .Where(d => d.FlightDate >= SearchFilter.FlightDate && d.FlightDate <= SearchFilter.ReturnDate)
                        .Where(p => p.AvailableSeats > 0)
                        .Where(a => a.FlightDate >= DateTime.Now)
                        .Where(b => b.Price <= SearchFilter.PriceMax && b.Price >= SearchFilter.PriceMin)
                        .ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
        }

        // GET: Flights/Details/5
        //public async Task<IActionResult> BookNow(int? id)
        //{
        //    if (id == null || _context.Flights == null)
        //    {
        //        return NotFound();
        //    }

        //    var flight = await _context.Flights
        //        .FirstOrDefaultAsync(m => m.FlightId == id);
        //    if (flight == null)
        //    {
        //        return NotFound();
        //    }
        //    var email = User.Identity.Name;

        //    BookingModel newBook = new BookingModel()
        //    {

        //        BookingEmail = email,
        //        DepartingFlightId = flight.FlightId,
        //        ReturnFlightId = flight.FlightId,
        //        DepartingSeatNumber = flight.AvailableSeats
        //    };

        //    _context.Add(newBook);
        //    flight.SeatTaken();
        //    await _context.SaveChangesAsync();
        //    return View(flight);
        //}

        // GET: Flights/Create
        //public IActionResult GetAllFlights()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Book(int? id,int numOfTickets)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            var email = User.Identity.Name;

            BookingModel newBook = new BookingModel()
            {

                BookingEmail = email,
                DepartingFlightId = flight.FlightId,
                ReturnFlightId = flight.FlightId,
                DepartingSeatNumber = flight.AvailableSeats
            };

            _context.Add(newBook);
            flight.SeatTaken();
            await _context.SaveChangesAsync();
            return View(flight);
        }



        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            return View(flight);
        }

        private bool FlightExists(int id)
        {
          return (_context.Flights?.Any(e => e.FlightId == id)).GetValueOrDefault();
        }
    }
}
