using Microsoft.AspNetCore.Mvc;
using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;
using reservations_api.Services;

namespace reservations_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
  private readonly IReservationService _reservationService;

  public ReservationsController(IReservationService reservationService)
  {
    _reservationService = reservationService;
  }

  [HttpGet]
  [ProducesResponseType(typeof(List<ReservationResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> GetByDate([FromQuery] DateOnly? date)
  {
    if (date is null)
    {
      return Problem(
          detail: "The 'date' query parameter is required.",
          statusCode: StatusCodes.Status400BadRequest,
          title: "Missing required parameter");
    }

    var reservations = await _reservationService.GetByDateAsync(date.Value);
    return Ok(reservations);
  }

  [HttpPost]
  [ProducesResponseType(typeof(ReservationResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public async Task<IActionResult> Create([FromBody] CreateReservationRequest request)
  {
    if (!ModelState.IsValid)
    {
      return ValidationProblem(ModelState);
    }

    try
    {
      var createdReservation = await _reservationService.CreateAsync(request);
      return CreatedAtAction(
          nameof(Create),
          createdReservation);
    }
    catch (InvalidOperationException ex)
    {
      if (ex.Message.Contains("StartTime"))
      {
        return BadRequest(new { message = ex.Message });
      }

      if (ex.Message.Contains("Time conflict"))
      {
        return Conflict(new { message = ex.Message });
      }

      throw;
    }
  }
}