using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GazaHealthCenter_2.Components.Extensions;
using GazaHealthCenter_2.Components.Notifications;
using GazaHealthCenter_2.Components.Security;
using GazaHealthCenter_2.Resources;
using GazaHealthCenter_2.Services;
using GazaHealthCenter_2.Services.Advertisement;
using GazaHealthCenter_2.Objects.Models.Advertisment;

namespace GazaHealthCenter_2.Controllers;

[AllowUnauthorized]
public class Home : ServicedController<AccountService>
{
    private readonly AdvertisementService _advertisementService;

    public Home(AccountService accountService, AdvertisementService advertisementService)
        : base(accountService)
    {
        _advertisementService = advertisementService;
    }


    [HttpGet]
    public ActionResult Index()
    {
        if (!Service.IsActive(User.Id()))
            return RedirectToAction(nameof(Auth.Logout), nameof(Auth));


        List<AdvertisementModel> advertisements = _advertisementService.GetAllAdvertisements();
        return View(advertisements);
    }

    [HttpGet]
    [AllowAnonymous]
    public ActionResult Error()
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;

        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            Alerts.Add(new Alert
            {
                Id = "SystemError",
                Type = AlertType.Danger,
                Message = Resource.ForString("SystemError", HttpContext.TraceIdentifier)
            });

            return Json(new { alerts = Alerts });
        }

        return View();
    }

    [AllowAnonymous]
    public new ActionResult NotFound()
    {
        if (Service.IsLoggedIn(User) && !Service.IsActive(User.Id()))
            return RedirectToAction(nameof(Auth.Logout), nameof(Auth));

        Response.StatusCode = StatusCodes.Status404NotFound;

        return View();
    }

    [AllowUnauthorized]
    [HttpGet("AboutUs")]
    public IActionResult AboutUs()
    {
        return View();
    }

}
