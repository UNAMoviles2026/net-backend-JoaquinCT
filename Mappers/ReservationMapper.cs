using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;
using reservations_api.Models.Entities;

namespace reservations_api.Mappers;

public static class ReservationMapper
{
  public static Reservation ToEntity(CreateReservationRequest request)
  {
    return new Reservation
    {
      Id = Guid.NewGuid(),
      ClassroomId = request.ClassroomId,
      Date = request.Date,
      StartTime = request.StartTime,
      EndTime = request.EndTime
    };
  }

  public static ReservationResponse ToResponse(Reservation reservation)
  {
    return new ReservationResponse
    {
      Id = reservation.Id,
      ClassroomId = reservation.ClassroomId,
      Date = reservation.Date,
      StartTime = reservation.StartTime,
      EndTime = reservation.EndTime
    };
  }
}