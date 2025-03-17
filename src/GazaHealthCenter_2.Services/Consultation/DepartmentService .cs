using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects.Models.Consultation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Services.Consultation
{
    public class DepartmentService : AService
    {
        private readonly ConsultationService _consultationService;

        public DepartmentService(IUnitOfWork unitOfWork, ConsultationService consultationService) : base(unitOfWork)
        {
            _consultationService = consultationService;
        }
        public void AddDepartment(DepartmentModel department)
        {
            UnitOfWork.Insert(department);
            UnitOfWork.Commit();
        }

        public List<DepartmentModel> GetAllDepartments() => UnitOfWork.Select<DepartmentModel>().ToList();

        public DepartmentModel? GetDepartmentById(long id) => UnitOfWork.Get<DepartmentModel>(id);

        public void UpdateDepartment(DepartmentModel department)
        {
            UnitOfWork.Update(department);
            UnitOfWork.Commit();
        }

        public void DeleteDepartment(long id)
        {
            DepartmentModel? department = GetDepartmentById(id);
            if (department != null)
            {
                // Delete all related consultation requests first
                List<ConsultationRequestModel> consultationRequests = UnitOfWork.Select<ConsultationRequestModel>()
                    .Where(c => c.DepartmentId == id)
                    .ToList();

                foreach (ConsultationRequestModel? consultation in consultationRequests)
                {
                    _consultationService.DeleteConsultation(consultation.Id);
                }

                // Delete all related doctors first
                List<DoctorModel> doctors = UnitOfWork.Select<DoctorModel>()
                    .Where(d => d.DepartmentId == id)
                    .ToList();

                foreach (DoctorModel? doctor in doctors)
                {
                    UnitOfWork.Delete(doctor);
                }

                // Now delete the department
                UnitOfWork.Delete(department);
                UnitOfWork.Commit();
            }
        }


    }


}
