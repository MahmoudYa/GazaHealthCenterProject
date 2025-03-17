using GazaHealthCenter_2.Components.Security;
using GazaHealthCenter_2.Objects.Models.MedicalLocation;
using GazaHealthCenter_2.Services.MedicalLocation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Controllers.MedicalLocation
{
    [Area(nameof(Area.MedicalLocation))]
    public class MedicalLocation : ServicedController<MedicalLocationService>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MedicalLocation(MedicalLocationService service, IWebHostEnvironment webHostEnvironment) : base(service)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<MedicalLocationModel> locations = Service.GetAllLocations();
            return View(locations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MedicalLocationModel location, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        ImageFile.CopyTo(fileStream);

                    location.ImageUrl = filePath;
                }

                Service.AddLocation(location);
                TempData["SuccessMessage"] = "تم إضافة الموقع الطبي بنجاح!";
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            MedicalLocationModel? location = Service.GetLocationById(id);
            if (location == null)
                return NotFound();
            return View(location);
        }

        [HttpPost]
        public IActionResult Edit(long id, MedicalLocationModel location, IFormFile? ImageFile)
        {
            if (id != location.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                MedicalLocationModel? existingLocation = Service.GetLocationById(id);
                if (existingLocation == null)
                    return NotFound();

                existingLocation.Name = location.Name;
                existingLocation.Address = location.Address;
                existingLocation.PhoneNumber = location.PhoneNumber;
                existingLocation.Type = location.Type;
                existingLocation.Website = location.Website;
                existingLocation.Email = location.Email;
                existingLocation.GoogleMapsUrl = location.GoogleMapsUrl;

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        ImageFile.CopyTo(fileStream);

                    if (!string.IsNullOrEmpty(existingLocation.ImageUrl))
                    {
                        string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingLocation.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                            System.IO.File.Delete(oldImagePath);
                    }

                    existingLocation.ImageUrl = "/uploads/" + fileName;
                }

                Service.UpdateLocation(existingLocation);
                TempData["SuccessMessage"] = "تم تعديل الموقع الطبي بنجاح!";
                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }


        [HttpPost]
        public IActionResult Delete(long id)
        {
            MedicalLocationModel? location = Service.GetLocationById(id);
            if (location == null)
                return NotFound();
            Service.DeleteLocation(id);
            TempData["SuccessMessage"] = "تم حذف الموقع الطبي بنجاح!";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(long id)
        {
            MedicalLocationModel? location = Service.GetLocationById(id);
            if (location == null)
                return NotFound();
            return View(location);
        }
        [HttpGet]
        public IActionResult DetailsHospitals(long id)
        {
            MedicalLocationModel? location = Service.GetLocationById(id);
            if (location == null)
                return NotFound();
            return View(location);
        }
        [HttpGet]
        public IActionResult DetailsHealthCenters(long id)
        {
            MedicalLocationModel? location = Service.GetLocationById(id);
            if (location == null)
                return NotFound();
            return View(location);
        }
        [HttpGet]
        public IActionResult DetailsMedicalPoints(long id)
        {
            MedicalLocationModel? location = Service.GetLocationById(id);
            if (location == null)
                return NotFound();
            return View(location);
        }
        [HttpGet]
        public IActionResult DetailsPharmacies(long id)
        {
            MedicalLocationModel? location = Service.GetLocationById(id);
            if (location == null)
                return NotFound();
            return View(location);
        }
        public IActionResult Hospitals()
        {
            List<MedicalLocationModel> hospitals = Service.GetAllLocations()
                .Where(l => l.Type == MedicalLocationType.Hospital)
                .ToList();

            return View("Hospitals", hospitals);
        }

        public IActionResult HealthCenters()
        {
            List<MedicalLocationModel> healthCenters = Service.GetAllLocations()
                .Where(l => l.Type == MedicalLocationType.HealthCenter)
                .ToList();

            return View("HealthCenters", healthCenters);
        }

        public IActionResult MedicalPoints()
        {
            List<MedicalLocationModel> medicalPoints = Service.GetAllLocations()
                .Where(l => l.Type == MedicalLocationType.MedicalPoint)
                .ToList();

            return View("MedicalPoints", medicalPoints);
        }

        public IActionResult Pharmacies()
        {
            List<MedicalLocationModel> pharmacies = Service.GetAllLocations()
                .Where(l => l.Type == MedicalLocationType.Pharmacy)
                .ToList();

            return View("Pharmacies", pharmacies);
        }

    }
}
