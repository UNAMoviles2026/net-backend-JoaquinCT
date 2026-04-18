namespace reservations_api.Models.Entities;

public class Reservation
{
  public Guid Id { get; set; }
  public Guid ClassroomId { get; set; }
  public DateOnly Date { get; set; }
  public TimeOnly StartTime { get; set; }
  public TimeOnly EndTime { get; set; }
}