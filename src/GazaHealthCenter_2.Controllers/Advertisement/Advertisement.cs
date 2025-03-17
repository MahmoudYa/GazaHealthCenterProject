using GazaHealthCenter_2.Components.Security;
using GazaHealthCenter_2.Objects.Models.Advertisment;
using GazaHealthCenter_2.Objects.Views.Advertisement;
using GazaHealthCenter_2.Services.Advertisement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Controllers.Advertisement
{
    [Area(nameof(Area.Advertisement))]
    public class Advertisement : ServicedController<AdvertisementService>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public Advertisement(AdvertisementService service, IWebHostEnvironment webHostEnvironment) : base(service)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // Display all advertisements
        [HttpGet]
        public IActionResult Index()
        {
            List<AdvertisementModel> advertisements = Service.GetAllAdvertisements();
            return View(advertisements);
        }


        // Display create advertisement page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new advertisement (without await)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdvertisementView advertisementView)
        {
            if (ModelState.IsValid)
            {
                string? filePath = null;

                if (advertisementView.MediaFile != null)
                {
                    String uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    String fileName = Path.GetFileNameWithoutExtension(advertisementView.MediaFile.FileName);
                    String extension = Path.GetExtension(advertisementView.MediaFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    filePath = Path.Combine(uploadDir, fileName);

                    // Save the file synchronously
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        advertisementView.MediaFile.CopyTo(fileStream);
                }

                AdvertisementModel advertisement = new AdvertisementModel
                {
                    Title = advertisementView.Title,
                    Content = advertisementView.Content,
                    MediaUrl = filePath,
                    DatePosted = DateTime.Now,
                    LikesCount = 0
                };

                Service.AddAdvertisement(advertisement);

                TempData["SuccessMessage"] = "تم إضافة الإعلان بنجاح!";
                return RedirectToAction(nameof(Index));
            }

            return View(advertisementView);
        }

        // Edit advertisement (without await)
        [HttpGet]
        public IActionResult Edit(long id)
        {
            AdvertisementModel? advertisement = Service.GetAdvertisementById(id);
            if (advertisement == null)
                return NotFound();

            AdvertisementView advertisementView = new AdvertisementView
            {
                Title = advertisement.Title,
                Content = advertisement.Content
            };

            return View(advertisementView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, AdvertisementView advertisementView)
        {
            if (id != advertisementView.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                string? filePath = null;
                if (advertisementView.MediaFile != null)
                {
                    String uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    String fileName = Path.GetFileNameWithoutExtension(advertisementView.MediaFile.FileName);
                    String extension = Path.GetExtension(advertisementView.MediaFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    filePath = Path.Combine(uploadDir, fileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                        advertisementView.MediaFile.CopyTo(fileStream);
                }

                AdvertisementModel? advertisement = Service.GetAdvertisementById(id);
                if (advertisement == null)
                    return NotFound();

                advertisement.Title = advertisementView.Title;
                advertisement.Content = advertisementView.Content;
                if (filePath != null)
                    advertisement.MediaUrl = filePath;

                Service.UpdateAdvertisement(advertisement);

                TempData["SuccessMessage"] = "تم تعديل الإعلان بنجاح!";
                return RedirectToAction(nameof(Index));
            }

            return View(advertisementView);
        }

        // Delete advertisement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            AdvertisementModel? advertisement = Service.GetAdvertisementById(id);
            if (advertisement == null)
                return NotFound();

            Service.DeleteAdvertisement(id);

            TempData["SuccessMessage"] = "تم حذف الإعلان بنجاح!";
            return RedirectToAction(nameof(Index));
        }
    }
}

