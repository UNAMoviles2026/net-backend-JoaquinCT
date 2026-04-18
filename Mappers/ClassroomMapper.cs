using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;
using reservations_api.Models.Entities;

namespace reservations_api.Mappers;

public static class ClassroomMapper
{
    public static Classroom ToEntity(CreateClassroomRequest request)
    {
        return new Classroom
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Capacity = request.Capacity,
            Location = request.Location.Trim()
        };
    }

    public static ClassroomResponse ToResponse(Classroom classroom)
    {
        return new ClassroomResponse
        {
            Id = classroom.Id,
            Name = classroom.Name,
            Capacity = classroom.Capacity,
            Location = classroom.Location
        };
    }
}