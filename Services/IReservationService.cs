using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;

namespace reservations_api.Services;

public interface IReservationService
{
  Task<ReservationResponse> CreateAsync(CreateReservationRequest request);
  Task<List<ReservationResponse>> GetByDateAsync(DateOnly date);
}