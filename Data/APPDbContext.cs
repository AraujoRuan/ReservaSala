using Microsoft.EntityFrameworkCore;
using ReservaSalaAPI.Models;

namespace ReservaSalaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Reserva> Reservas { get; set; }
    }
}
