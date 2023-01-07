using Booking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<Flight> Flights { get; set; }  
        public DbSet<BookingModel> Bookings { get; set; }    
    }
}
