using Microsoft.AspNetCore.Mvc.ModelBinding;
using GazaHealthCenter_2.Components.Notifications;

namespace GazaHealthCenter_2.Validators;

public interface IValidator : IDisposable
{
    Alerts Alerts { get; set; }
    ModelStateDictionary ModelState { get; set; }
}
