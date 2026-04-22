using System.ComponentModel.DataAnnotations;

namespace APBD_CW5_S27747.Models;

public class Reservation : IValidatableObject
{
    public int Id { get; set; }

    [Range(1, int.MaxValue)]
    public int RoomId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string OrganizerName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Topic { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Status { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Date == default)
        {
            yield return new ValidationResult("Date is required.", new[] { nameof(Date) });
        }

        if (EndTime <= StartTime)
        {
            yield return new ValidationResult("EndTime must be later than StartTime.", new[] { nameof(EndTime) });
        }
    }
}