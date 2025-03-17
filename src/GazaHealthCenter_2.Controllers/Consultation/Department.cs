using GazaHealthCenter_2.Components.Security;
using GazaHealthCenter_2.Objects.Models.Consultation;
using GazaHealthCenter_2.Services;
using GazaHealthCenter_2.Services.Consultation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Controllers.Consultation
{
    [Area(nameof(Area.Consultation))]
    [Route("Departments")]
    public class Department : ServicedController<DepartmentService>
    {
        private readonly ConsultationService _consultationService;
        private readonly DoctorService _doctorService;

        public Department(DepartmentService service, ConsultationService consultationService, DoctorService doctorService) : base(service)
        {
            _consultationService = consultationService;
            _doctorService = doctorService;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<DepartmentModel> departments = Service.GetAllDepartments();
            return View(departments);
        }

        [HttpGet("ShowDepartment")]
        public IActionResult ShowDepartment()
        {
            List<DepartmentModel> departments = Service.GetAllDepartments();
            return View(departments);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(DepartmentModel model, IFormFile? ImageFile)
        {

            if (ImageFile != null)
            {
                string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(uploadDir, fileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    ImageFile.CopyTo(fileStream);

                model.ImageUrl = "/uploads/" + fileName;
            }

            Service.AddDepartment(model);
            return RedirectToAction("Index");

        }


        [HttpGet("{id}/Consultations")]
        public IActionResult Consultations(long id)
        {
            DepartmentModel? department = Service.GetDepartmentById(id);
            if (department == null)
                return NotFound();

            List<ConsultationRequestModel> consultations = _consultationService.GetConsultationsByDepartment(id);
            ViewBag.DepartmentName = department.Name;
            ViewBag.DepartmentId = id;
            return View(consultations);
        }

        [HttpGet("{id}/ConsultationsShow")]
        public IActionResult ConsultationsShow(long id)
        {
            DepartmentModel? department = Service.GetDepartmentById(id);
            if (department == null)
                return NotFound();

            List<ConsultationRequestModel> consultations = _consultationService.GetConsultationsByDepartment(id);
            ViewBag.DepartmentName = department.Name;
            ViewBag.DepartmentId = id;
            return View(consultations);
        }

        [HttpGet("{departmentId}/ConsultationRequest")]
        public IActionResult CreateConsultation(long departmentId)
        {
            DepartmentModel? department = Service.GetDepartmentById(departmentId);
            if (department == null)
                return NotFound();

            ViewBag.DepartmentId = departmentId;
            ConsultationRequestModel model = new ConsultationRequestModel { DepartmentId = departmentId };
            return View(model);
        }


        [HttpPost("{departmentId}/ConsultationRequest")]
        public IActionResult CreateConsultation(long departmentId, ConsultationRequestModel model)
        {

            _consultationService.SubmitRequest(model);
            return RedirectToAction("Consultations", new { id = departmentId });

        }


        [HttpGet("{departmentId}/ConsultationRequestShow")]
        public IActionResult CreateConsultationShow(long departmentId)
        {
            DepartmentModel? department = Service.GetDepartmentById(departmentId);
            if (department == null)
                return NotFound();

            ViewBag.DepartmentId = departmentId;
            ConsultationRequestModel model = new ConsultationRequestModel { DepartmentId = departmentId };
            return View(model);
        }
        [HttpPost("{departmentId}/ConsultationRequestShow")]
        public IActionResult CreateConsultationShow(long departmentId, ConsultationRequestModel model)
        {

            _consultationService.SubmitRequest(model);
            return RedirectToAction("ConsultationsShow", new { id = departmentId });

        }

        [HttpGet("Consultations/{id}/Responses")]
        public IActionResult Responses(long id)
        {
            List<ConsultationResponseModel> responses = _consultationService.GetResponses(id);
            if (responses == null)
                return NotFound();
            ViewBag.ConsultationId = id;
            return View(responses);
        }

        [HttpGet("Consultations/{id}/ResponsesShow")]
        public IActionResult ResponsesShow(long id)
        {
            List<ConsultationResponseModel> responses = _consultationService.GetResponses(id);
            if (responses == null)
                return NotFound();
            ViewBag.ConsultationId = id;
            return View(responses);
        }


        [HttpGet("Consultations/{id}/Respond")]
        public IActionResult Respond(long id)
        {
            ConsultationRequestModel? consultation = _consultationService.GetConsultationById(id);
            if (consultation == null)
                return NotFound();

            List<DoctorModel> doctors = _doctorService.GetDoctorsByDepartmentId(consultation.DepartmentId); // Fetch doctors from database
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name");

            ConsultationResponseModel model = new ConsultationResponseModel { ConsultationRequestId = id };
            return View(model);
        }


        [HttpPost("Consultations/{id}/Respond")]
        public IActionResult Respond(long id, ConsultationResponseModel response)
        {
            ConsultationRequestModel? consultation = _consultationService.GetConsultationById(id);
            if (consultation == null)
                return NotFound();


            DoctorModel? doctor = _doctorService.GetDoctorById(response.DoctorId);
            if (doctor != null)
                response.Doctor = doctor;
            response.ConsultationRequestId = id;
            _consultationService.AddResponse(response);
            return RedirectToAction("Responses", new { id });
        }
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(long id)
        {
            DepartmentModel? department = Service.GetDepartmentById(id);
            if (department == null)
                return NotFound();
            return View(department);
        }

        [HttpPost("Edit/{id}")]
        public IActionResult Edit(long id, DepartmentModel model, IFormFile? ImageFile)
        {
            if (id != model.Id)
                return BadRequest();

            if (ImageFile != null)
            {
                string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                if (!Directory.Exists(uploadDir))
                    Directory.CreateDirectory(uploadDir);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(uploadDir, fileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    ImageFile.CopyTo(fileStream);

                model.ImageUrl = "/uploads/" + fileName;
            }

            Service.UpdateDepartment(model);
            return RedirectToAction("Index");
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            DepartmentModel? department = Service.GetDepartmentById(id);
            if (department == null)
                return NotFound();

            Service.DeleteDepartment(id);
            return RedirectToAction("Index");
        }

        [HttpGet("Consultations/Edit/{id}")]
        public IActionResult EditConsultation(long id)
        {
            ConsultationRequestModel? consultation = _consultationService.GetConsultationById(id);
            if (consultation == null)
                return NotFound();
            return View(consultation);
        }

        [HttpPost("Consultations/Edit/{id}")]
        public IActionResult EditConsultation(long id, ConsultationRequestModel model)
        {
            if (id != model.Id)
                return BadRequest();

            _consultationService.UpdateConsultation(model);
            return RedirectToAction("Consultations", new { id = model.DepartmentId });
        }

        [HttpPost("Consultations/Delete/{id}")]
        public IActionResult DeleteConsultation(long id)
        {
            ConsultationRequestModel? consultation = _consultationService.GetConsultationById(id);
            if (consultation == null)
                return NotFound();

            _consultationService.DeleteConsultation(id);
            return RedirectToAction("Consultations", new { id = consultation.DepartmentId });
        }
        // Edit Response (GET)
        [HttpGet("Consultations/Responses/Edit/{id}")]
        public IActionResult EditResponse(long id)
        {
            ConsultationResponseModel? response = _consultationService.GetResponseById(id);
            if (response == null)
                return NotFound();

            List<DoctorModel> doctors = _doctorService.GetDoctorsByDepartmentId(response.ConsultationRequest.DepartmentId);
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name", response.DoctorId);

            return View(response);
        }

        // Edit Response (POST)
        [HttpPost("Consultations/Responses/Edit/{id}")]
        public IActionResult EditResponse(long id, ConsultationResponseModel model)
        {
            if (id != model.Id)
                return BadRequest();

            _consultationService.UpdateResponse(model);
            return RedirectToAction("Responses", new { id = model.ConsultationRequestId });
        }

        // Delete Response
        [HttpPost("Consultations/Responses/Delete/{id}")]
        public IActionResult DeleteResponse(long id)
        {
            ConsultationResponseModel? response = _consultationService.GetResponseById(id);
            if (response == null)
                return NotFound();

            _consultationService.DeleteResponse(id);
            return RedirectToAction("Responses", new { id = response.ConsultationRequestId });
        }

    }
}
