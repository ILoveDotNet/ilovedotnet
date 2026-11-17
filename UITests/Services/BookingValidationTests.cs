using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using SharedModels;

namespace UITests.Services;

[ExcludeFromCodeCoverage]
public class BookingValidationTests
{
  [Theory]
  [InlineData(null, null, "Contact email is required.", "Contact phone is required.")]
  [InlineData("not-an-email", "123", "Enter a valid contact email.", "Contact phone must be 6 - 20 characters.")]
  public async Task ValidateAsync_ReportsNestedContactFields(
      string? email, string? phone, string emailError, string phoneError)
  {
    using var ctx = new BunitContext();
    ctx.Services.AddSharedModelValidation();
    var booking = CreateValidBooking();
    booking.Contact = new() { Email = email, Phone = phone };
    var editContext = new EditContext(booking);
    var form = RenderForm(ctx, editContext);

    var valid = await form.InvokeAsync(() => editContext.ValidateAsync());

    Assert.False(valid);
    Assert.Contains(emailError,
        editContext.GetValidationMessages(new FieldIdentifier(booking.Contact, nameof(booking.Contact.Email))));
    Assert.Contains(phoneError,
        editContext.GetValidationMessages(new FieldIdentifier(booking.Contact, nameof(booking.Contact.Phone))));
  }

  [Fact]
  public async Task ValidateAsync_SkipsIncompleteLookupItems()
  {
    using var ctx = new BunitContext();
    ctx.Services.AddSharedModelValidation();
    var booking = CreateValidBooking();
    booking.AvailableItems.Add(new() { Name = "Consultation" });
    var editContext = new EditContext(booking);
    var form = RenderForm(ctx, editContext);

    var valid = await form.InvokeAsync(() => editContext.ValidateAsync());

    Assert.True(valid);
    Assert.Empty(editContext.GetValidationMessages());
  }

  [Fact]
  public async Task ValidateAsync_ValidatesLookupItemAddedToBooking()
  {
    using var ctx = new BunitContext();
    ctx.Services.AddSharedModelValidation();
    var booking = CreateValidBooking();
    var item = new BookingItemValidationModel { Name = "Consultation" };
    booking.AvailableItems.Add(item);
    booking.BookingItems.Add(item);
    var editContext = new EditContext(booking);
    var form = RenderForm(ctx, editContext);

    var valid = await form.InvokeAsync(() => editContext.ValidateAsync());

    Assert.False(valid);
    Assert.Contains("Quantity must be between 1 and 100.",
        editContext.GetValidationMessages(new FieldIdentifier(item, nameof(item.Quantity))));
    Assert.Contains("Staff member is required.",
        editContext.GetValidationMessages(new FieldIdentifier(item, nameof(item.StaffId))));
  }

  private static IRenderedComponent<EditForm> RenderForm(BunitContext ctx, EditContext editContext) =>
    ctx.Render<EditForm>(parameters => parameters
        .Add(p => p.EditContext, editContext)
        .Add(p => p.ChildContent, _ => builder =>
        {
          builder.OpenComponent<DataAnnotationsValidator>(0);
          builder.CloseComponent();
        }));

  private static BookingValidationModel CreateValidBooking() => new()
  {
    CustomerId = Guid.NewGuid(),
    RoomId = Guid.NewGuid(),
    Contact = new() { Email = "taylor@example.com", Phone = "1234567890" },
    BookingItems = [new() { Name = "Consultation", Quantity = 1, StaffId = Guid.NewGuid() }]
  };
}
