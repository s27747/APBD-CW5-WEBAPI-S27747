using APBD_CW5_S27747.Models;

namespace APBD_CW5_S27747.Data;

public static class AppData
{
    public static List<Room> Rooms { get; } = new();
    public static List<Reservation> Reservations { get; } = new();

    private static bool _initialized;

    public static void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        Rooms.AddRange(new List<Room>
        {
            new Room
            {
                Id = 1,
                Name = "Lab 101",
                BuildingCode = "A",
                Floor = 1,
                Capacity = 20,
                HasProjector = true,
                IsActive = true
            },
            new Room
            {
                Id = 2,
                Name = "Room 204",
                BuildingCode = "B",
                Floor = 2,
                Capacity = 30,
                HasProjector = true,
                IsActive = true
            },
            new Room
            {
                Id = 3,
                Name = "Conference 12",
                BuildingCode = "A",
                Floor = 3,
                Capacity = 15,
                HasProjector = false,
                IsActive = true
            },
            new Room
            {
                Id = 4,
                Name = "Workshop 7",
                BuildingCode = "C",
                Floor = 1,
                Capacity = 25,
                HasProjector = true,
                IsActive = false
            },
            new Room
            {
                Id = 5,
                Name = "Seminar 5",
                BuildingCode = "B",
                Floor = 4,
                Capacity = 40,
                HasProjector = false,
                IsActive = true
            }
        });

        Reservations.AddRange(new List<Reservation>
        {
            new Reservation
            {
                Id = 1,
                RoomId = 1,
                OrganizerName = "Anna Kowalska",
                Topic = "REST Basics",
                Date = new DateOnly(2026, 5, 10),
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(11, 0),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 2,
                RoomId = 2,
                OrganizerName = "Jan Nowak",
                Topic = "ASP.NET Workshop",
                Date = new DateOnly(2026, 5, 10),
                StartTime = new TimeOnly(12, 0),
                EndTime = new TimeOnly(14, 0),
                Status = "planned"
            },
            new Reservation
            {
                Id = 3,
                RoomId = 3,
                OrganizerName = "Maria Zielinska",
                Topic = "Consultation",
                Date = new DateOnly(2026, 5, 11),
                StartTime = new TimeOnly(10, 0),
                EndTime = new TimeOnly(11, 30),
                Status = "confirmed"
            },
            new Reservation
            {
                Id = 4,
                RoomId = 5,
                OrganizerName = "Piotr Wisniewski",
                Topic = "Architecture Review",
                Date = new DateOnly(2026, 5, 12),
                StartTime = new TimeOnly(8, 30),
                EndTime = new TimeOnly(10, 0),
                Status = "cancelled"
            },
            new Reservation
            {
                Id = 5,
                RoomId = 1,
                OrganizerName = "Katarzyna Maj",
                Topic = "HTTP Deep Dive",
                Date = new DateOnly(2026, 5, 11),
                StartTime = new TimeOnly(13, 0),
                EndTime = new TimeOnly(15, 0),
                Status = "confirmed"
            }
        });

        _initialized = true;
    }
}