using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;

namespace reservations_api.Services;

public interface IClassroomService
{
    Task<List<ClassroomResponse>> GetAllAsync();
    Task<ClassroomResponse?> GetByIdAsync(Guid id);
    Task<ClassroomResponse> CreateAsync(CreateClassroomRequest request);
}