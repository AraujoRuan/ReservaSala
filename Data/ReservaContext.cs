using Microsoft.EntityFrameworkCore;
using ReservaSalaAPI.Models;

namespace ReservaSalaAPI.Data;

public class ReservaContext : DbContext
{
    public ReservaContext(DbContextOptions<ReservaContext> options) : base(options)
    {
    }

    public DbSet<Reserva> Reservas { get; set; }
}
