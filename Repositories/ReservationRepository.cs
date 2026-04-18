using Microsoft.EntityFrameworkCore;
using reservations_api.Data;
using reservations_api.Models.Entities;

namespace reservations_api.Repositories;

public class ReservationRepository : IReservationRepository
{
  private readonly AppDbContext _context;

  public ReservationRepository(AppDbContext context)
  {
    _context = context;
  }

  public async Task<Reservation> AddAsync(Reservation reservation)
  {
    await _context.Reservations.AddAsync(reservation);
    await _context.SaveChangesAsync();
    return reservation;
  }

  public async Task<List<Reservation>> GetByClassroomAndDateAsync(Guid classroomId, DateOnly date)
  {
    return await _context.Reservations
        .AsNoTracking()
        .Where(r => r.ClassroomId == classroomId && r.Date == date)
        .ToListAsync();
  }
}