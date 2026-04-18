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

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
  public async Task<IActionResult> Delete(Guid id)
  {
    try
    {
      await _reservationService.DeleteAsync(id);
      return NoContent();
    }
    catch (KeyNotFoundException ex)
    {
      return Problem(
          detail: ex.Message,
          statusCode: StatusCodes.Status404NotFound,
          title: "Reservation not found");
    }
  }
}