using GazaHealthCenter_2.Objects.Models.Consultation;
using GazaHealthCenter_2.Services.Consultation;
using Microsoft.AspNetCore.Mvc;

namespace GazaHealthCenter_2.Controllers.Consultation
{
    [Area(nameof(Area.Consultation))]
    [Route("Doctors")]
    public class Doctor : ServicedController<DoctorService>
    {
        private readonly DepartmentService _departmentService;

        public Doctor(DoctorService service, DepartmentService departmentService) : base(service)
        {
            _departmentService = departmentService;
        }

        // Make sure Index has a unique route
        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<DoctorModel> doctors = Service.GetAllDoctors();
            return View(doctors);
        }

        // Change the route for ShowDoctors to avoid ambiguity
        [HttpGet("ShowDoctors")]
        public IActionResult ShowDoctors()
        {
            List<DoctorModel> doctors = Service.GetAllDoctors();
            return View(doctors);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(DoctorModel model)
        {
            Service.AddDoctor(model);
            TempData["SuccessMessage"] = "تمت إضافة الطبيب بنجاح!";
            return RedirectToAction("Index");
        }

        // Edit Doctor
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(long id)
        {
            DoctorModel? doctor = Service.GetDoctorById(id);
            if (doctor == null)
                return NotFound();
            ViewBag.Departments = _departmentService.GetAllDepartments();
            return View(doctor);
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(long id, DoctorModel model)
        {
            if (id != model.Id)
                return BadRequest();

            Service.UpdateDoctor(model);
            TempData["SuccessMessage"] = "تم تعديل بيانات الطبيب بنجاح!";
            return RedirectToAction("Index");
        }

        // Delete Doctor
        [HttpPost("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            DoctorModel? doctor = Service.GetDoctorById(id);
            if (doctor == null)
                return NotFound();

            Service.DeleteDoctor(id);
            TempData["SuccessMessage"] = "تم حذف الطبيب بنجاح!";
            return RedirectToAction("Index");
        }
    }
}
