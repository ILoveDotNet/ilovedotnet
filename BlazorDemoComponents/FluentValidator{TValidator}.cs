﻿using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorDemoComponents;
//https://gist.github.com/SteveSandersonMS/090145d7511c5190f62a409752c60d00
public class FluentValidator<TValidator> : ComponentBase where TValidator : IValidator, new()
{
  private static readonly char[] _separators = new[] { '.', '[' };
  private TValidator _validator = default!;

  [CascadingParameter] private EditContext EditContext { get; set; } = default!;

  protected override void OnInitialized()
  {
    _validator = new TValidator();
    var messages = new ValidationMessageStore(EditContext);

    // Revalidate when any field changes, or if the entire form requests validation
    // (e.g., on submit)

    EditContext.OnFieldChanged += (sender, eventArgs)
        => ValidateModel((EditContext)sender!, messages);

    EditContext.OnValidationRequested += (sender, eventArgs)
        => ValidateModel((EditContext)sender!, messages);
  }

  private void ValidateModel(EditContext editContext, ValidationMessageStore messages)
  {
    var context = new ValidationContext<object>(editContext.Model);
    var validationResult = _validator.Validate(context);
    messages.Clear();
    foreach (var error in validationResult.Errors)
    {
      var fieldIdentifier = ToFieldIdentifier(editContext, error.PropertyName);
      messages.Add(fieldIdentifier, error.ErrorMessage);
    }
    editContext.NotifyValidationStateChanged();
  }

  private static FieldIdentifier ToFieldIdentifier(EditContext editContext, string propertyPath)
  {
    // This method parses property paths like 'SomeProp.MyCollection[123].ChildProp'
    // and returns a FieldIdentifier which is an (instance, propName) pair. For example,
    // it would return the pair (SomeProp.MyCollection[123], "ChildProp"). It traverses
    // as far into the propertyPath as it can go until it finds any null instance.

    var obj = editContext.Model;

    while (true)
    {
      var nextTokenEnd = propertyPath.IndexOfAny(_separators);
      if (nextTokenEnd < 0)
      {
        return new FieldIdentifier(obj, propertyPath);
      }

      var nextToken = propertyPath.Substring(0, nextTokenEnd);
      propertyPath = propertyPath.Substring(nextTokenEnd + 1);

      object newObj;
      if (nextToken.EndsWith("]"))
      {
        // It's an indexer
        // This code assumes C# conventions (one indexer named Item with one param)
        nextToken = nextToken.Substring(0, nextToken.Length - 1);
        var prop = obj.GetType().GetProperty("Item");
        var indexerType = prop!.GetIndexParameters()[0].ParameterType;
        var indexerValue = Convert.ChangeType(nextToken, indexerType);
        newObj = prop.GetValue(obj, new object[] { indexerValue })!;
      }
      else
      {
        // It's a regular property
        var prop = obj.GetType().GetProperty(nextToken);
        if (prop == null)
        {
          throw new InvalidOperationException($"Could not find property named {nextToken} on object of type {obj.GetType().FullName}.");
        }
        newObj = prop.GetValue(obj)!;
      }

      if (newObj == null)
      {
        // This is as far as we can go
        return new FieldIdentifier(obj, nextToken);
      }

      obj = newObj;
    }
  }
}
