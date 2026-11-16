using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Validation;

namespace BlazorDemoComponents;

[ValidatableType]
public sealed class BookingValidationModel
{
  [CustomValidation(typeof(BookingValidationModel), nameof(ValidateCustomer))]
  public Guid CustomerId { get; set; }

  [CustomValidation(typeof(BookingValidationModel), nameof(ValidateRoom))]
  public Guid RoomId { get; set; }

  [CustomValidation(typeof(BookingValidationModel), nameof(ValidateStart))]
  public DateTimeOffset StartAt { get; set; } = new(DateTime.Today.AddDays(1).AddHours(9));

  [CustomValidation(typeof(BookingValidationModel), nameof(ValidateEnd))]
  public DateTimeOffset EndAt { get; set; } = new(DateTime.Today.AddDays(1).AddHours(10));

  [MaxLength(500, ErrorMessage = "Notes too long (500 character limit).")]
  public string? Notes { get; set; }

  [SkipValidation]
  public Dictionary<Guid, string> AvailableItems { get; set; } = [];

  [CustomValidation(typeof(BookingValidationModel), nameof(ValidateBookingItems))]
  public List<BookingItemValidationModel> BookingItems { get; set; } = [new()];

  public static ValidationResult? ValidateCustomer(Guid value) =>
    value == Guid.Empty
      ? new("Customer is required.", [nameof(CustomerId)])
      : ValidationResult.Success;

  public static ValidationResult? ValidateRoom(Guid value) =>
    value == Guid.Empty
      ? new("Room is required.", [nameof(RoomId)])
      : ValidationResult.Success;

  public static ValidationResult? ValidateStart(DateTimeOffset value) =>
    value == DateTimeOffset.MinValue
      ? new("Start date is required.", [nameof(StartAt)])
      : ValidationResult.Success;

  public static ValidationResult? ValidateEnd(DateTimeOffset value, ValidationContext context)
  {
    if (value == DateTimeOffset.MinValue)
      return new("End date is required.", [nameof(EndAt)]);

    var booking = (BookingValidationModel)context.ObjectInstance;
    return booking.StartAt != DateTimeOffset.MinValue && value <= booking.StartAt
      ? new("End must be after Start.", [nameof(EndAt)])
      : ValidationResult.Success;
  }

  public static ValidationResult? ValidateBookingItems(List<BookingItemValidationModel> value) =>
    value.Count == 0
      ? new("At least one booking item is required.", [nameof(BookingItems)])
      : ValidationResult.Success;
}

public sealed class BookingItemValidationModel
{
  [Required]
  public string? Name { get; set; }

  [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
  public int Quantity { get; set; }

  [CustomValidation(typeof(BookingItemValidationModel), nameof(ValidateStaff))]
  public Guid StaffId { get; set; }

  public static ValidationResult? ValidateStaff(Guid value) =>
    value == Guid.Empty
      ? new("Staff member is required.", [nameof(StaffId)])
      : ValidationResult.Success;
}
