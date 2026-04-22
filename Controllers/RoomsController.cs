using APBD_CW5_S27747.Data;
using APBD_CW5_S27747.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_CW5_S27747.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Room>> GetAll(
        [FromQuery] int? minCapacity,
        [FromQuery] bool? hasProjector,
        [FromQuery] bool? activeOnly)
    {
        IEnumerable<Room> rooms = AppData.Rooms;

        if (minCapacity.HasValue)
        {
            rooms = rooms.Where(room => room.Capacity >= minCapacity.Value);
        }

        if (hasProjector.HasValue)
        {
            rooms = rooms.Where(room => room.HasProjector == hasProjector.Value);
        }

        if (activeOnly == true)
        {
            rooms = rooms.Where(room => room.IsActive);
        }

        return Ok(rooms);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Room> GetById(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(room => room.Id == id);

        if (room is null)
        {
            return NotFound();
        }

        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public ActionResult<IEnumerable<Room>> GetByBuildingCode(string buildingCode)
    {
        var rooms = AppData.Rooms
            .Where(room => room.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(rooms);
    }

    [HttpPost]
    public ActionResult<Room> Create(Room room)
    {
        room.Id = AppData.Rooms.Count == 0 ? 1 : AppData.Rooms.Max(existingRoom => existingRoom.Id) + 1;

        AppData.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Room> Update(int id, Room updatedRoom)
    {
        var existingRoom = AppData.Rooms.FirstOrDefault(room => room.Id == id);

        if (existingRoom is null)
        {
            return NotFound();
        }

        existingRoom.Name = updatedRoom.Name;
        existingRoom.BuildingCode = updatedRoom.BuildingCode;
        existingRoom.Floor = updatedRoom.Floor;
        existingRoom.Capacity = updatedRoom.Capacity;
        existingRoom.HasProjector = updatedRoom.HasProjector;
        existingRoom.IsActive = updatedRoom.IsActive;

        return Ok(existingRoom);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var room = AppData.Rooms.FirstOrDefault(room => room.Id == id);

        if (room is null)
        {
            return NotFound();
        }

        var hasRelatedReservations = AppData.Reservations.Any(reservation => reservation.RoomId == id);

        if (hasRelatedReservations)
        {
            return Conflict(new { message = "Cannot delete room with related reservations." });
        }

        AppData.Rooms.Remove(room);

        return NoContent();
    }
}