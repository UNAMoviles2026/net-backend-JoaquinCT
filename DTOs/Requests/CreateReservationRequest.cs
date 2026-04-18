using System.ComponentModel.DataAnnotations;

namespace reservations_api.DTOs.Requests;

public class CreateReservationRequest
{
    [Required]
    public Guid ClassroomId { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    public TimeOnly EndTime { get; set; }
}