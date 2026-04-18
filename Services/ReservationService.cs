using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;
using reservations_api.Mappers;
using reservations_api.Models.Entities;
using reservations_api.Repositories;

namespace reservations_api.Services;

public class ReservationService : IReservationService
{
  private readonly IReservationRepository _reservationRepository;

  public ReservationService(IReservationRepository reservationRepository)
  {
    _reservationRepository = reservationRepository;
  }

  public async Task<ReservationResponse> CreateAsync(CreateReservationRequest request)
  {
    if (request.StartTime >= request.EndTime)
    {
      throw new InvalidOperationException("StartTime must be less than EndTime");
    }

    var existingReservations = await _reservationRepository.GetByClassroomAndDateAsync(
        request.ClassroomId,
        request.Date);

    if (HasOverlap(request.StartTime, request.EndTime, existingReservations))
    {
      throw new InvalidOperationException("Time conflict with another reservation");
    }

    var reservation = ReservationMapper.ToEntity(request);
    var createdReservation = await _reservationRepository.AddAsync(reservation);

    return ReservationMapper.ToResponse(createdReservation);
  }

  public async Task DeleteAsync(Guid id)
  {
    var reservation = await _reservationRepository.GetByIdAsync(id);

    if (reservation is null)
    {
      throw new KeyNotFoundException($"Reservation with id {id} not found");
    }

    await _reservationRepository.DeleteAsync(reservation);
  }

  private static bool HasOverlap(TimeOnly startTime, TimeOnly endTime, List<Reservation> existingReservations)
  {
    return existingReservations.Any(r =>
        startTime < r.EndTime && endTime > r.StartTime);
  }
}