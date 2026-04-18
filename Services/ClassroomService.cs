using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;
using reservations_api.Mappers;
using reservations_api.Repositories;

namespace reservations_api.Services;

public class ClassroomService : IClassroomService
{
    private readonly IClassroomRepository _classroomRepository;

    public ClassroomService(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<List<ClassroomResponse>> GetAllAsync()
    {
        var classrooms = await _classroomRepository.GetAllAsync();
        return classrooms.Select(ClassroomMapper.ToResponse).ToList();
    }

    public async Task<ClassroomResponse?> GetByIdAsync(Guid id)
    {
        var classroom = await _classroomRepository.GetByIdAsync(id);
        return classroom is null ? null : ClassroomMapper.ToResponse(classroom);
    }

    public async Task<ClassroomResponse> CreateAsync(CreateClassroomRequest request)
    {
        var classroom = ClassroomMapper.ToEntity(request);
        var createdClassroom = await _classroomRepository.AddAsync(classroom);
        return ClassroomMapper.ToResponse(createdClassroom);
    }
}