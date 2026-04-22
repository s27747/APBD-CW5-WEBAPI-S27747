using APBD_CW5_S27747.Data;
using APBD_CW5_S27747.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW5_S27747.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> reservations = AppData.Reservations;

        if (date.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.Date == date.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            reservations = reservations.Where(reservation =>
                reservation.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(reservation => reservation.RoomId == roomId.Value);
        }

        return Ok(reservations);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetById(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(reservation => reservation.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create(Reservation reservation)
    {
        var room = AppData.Rooms.FirstOrDefault(roomItem => roomItem.Id == reservation.RoomId);

        if (room is null)
        {
            return NotFound(new { message = "Room does not exist." });
        }

        if (!room.IsActive)
        {
            return Conflict(new { message = "Cannot create reservation for an inactive room." });
        }

        if (HasTimeConflict(reservation))
        {
            return Conflict(new { message = "Reservation conflicts with an existing reservation." });
        }

        reservation.Id = AppData.Reservations.Count == 0
            ? 1
            : AppData.Reservations.Max(existingReservation => existingReservation.Id) + 1;

        AppData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Reservation> Update(int id, Reservation updatedReservation)
    {
        var existingReservation = AppData.Reservations.FirstOrDefault(reservation => reservation.Id == id);

        if (existingReservation is null)
        {
            return NotFound();
        }

        var room = AppData.Rooms.FirstOrDefault(roomItem => roomItem.Id == updatedReservation.RoomId);

        if (room is null)
        {
            return NotFound(new { message = "Room does not exist." });
        }

        if (!room.IsActive)
        {
            return Conflict(new { message = "Cannot create reservation for an inactive room." });
        }

        if (HasTimeConflict(updatedReservation, id))
        {
            return Conflict(new { message = "Reservation conflicts with an existing reservation." });
        }

        existingReservation.RoomId = updatedReservation.RoomId;
        existingReservation.OrganizerName = updatedReservation.OrganizerName;
        existingReservation.Topic = updatedReservation.Topic;
        existingReservation.Date = updatedReservation.Date;
        existingReservation.StartTime = updatedReservation.StartTime;
        existingReservation.EndTime = updatedReservation.EndTime;
        existingReservation.Status = updatedReservation.Status;

        return Ok(existingReservation);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = AppData.Reservations.FirstOrDefault(reservation => reservation.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        AppData.Reservations.Remove(reservation);

        return NoContent();
    }

    private static bool HasTimeConflict(Reservation candidateReservation, int? ignoredReservationId = null)
    {
        if (candidateReservation.Status.Equals("cancelled", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return AppData.Reservations.Any(existingReservation =>
            existingReservation.Id != ignoredReservationId &&
            existingReservation.RoomId == candidateReservation.RoomId &&
            existingReservation.Date == candidateReservation.Date &&
            !existingReservation.Status.Equals("cancelled", StringComparison.OrdinalIgnoreCase) &&
            candidateReservation.StartTime < existingReservation.EndTime &&
            candidateReservation.EndTime > existingReservation.StartTime);
    }
}