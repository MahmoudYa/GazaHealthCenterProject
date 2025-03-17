using GazaHealthCenter_2.Components.Security;
using GazaHealthCenter_2.Objects.Models.PsychologicalSession;
using GazaHealthCenter_2.Services.PsychologicalSessionService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Controllers.PsychologicalSession
{
    [Area(nameof(Area.PsychologicalSession))]
    public class PsychologicalSession : ServicedController<PsychologicalSessionService>
    {
        public PsychologicalSession(PsychologicalSessionService service) : base(service) { }

        [HttpGet]
        public IActionResult Index()
        {
            List<PsychologicalSessionModel> sessions = Service.GetAllSessions();
            return View(sessions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PsychologicalSessionModel session)
        {
            
                session.IsBooked = false; 
                Service.AddSession(session);
                TempData["SuccessMessage"] = "Session added successfully!";
                return RedirectToAction(nameof(Index));
            
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            Service.DeleteSession(id);
            TempData["SuccessMessage"] = "Session deleted successfully!";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(long id, string patientName, string whatsappNumber, string otherNotes)
        {
            if (ModelState.IsValid)
            {
                Service.BookSession(id, patientName, whatsappNumber, otherNotes);
                TempData["SuccessMessage"] = "Your session has been booked successfully!";
                return RedirectToAction(nameof(Index));
            }

            TempData["ErrorMessage"] = "An error occurred while booking.!";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelBooking(long id)
        {
            Service.CancelBooking(id);
            TempData["SuccessMessage"] = "Your reservation has been successfully cancelled.!";
            return RedirectToAction(nameof(Index));
        }
    }
}
