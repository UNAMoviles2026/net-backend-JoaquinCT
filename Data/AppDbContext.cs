using Microsoft.EntityFrameworkCore;
using reservations_api.Models.Entities;

namespace reservations_api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
}