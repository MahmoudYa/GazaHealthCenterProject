using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects.Models.Consultation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Services.Consultation
{
    public class DoctorService : AService
    {
        public DoctorService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public void AddDoctor(DoctorModel doctor)
        {
            UnitOfWork.Insert(doctor);
            UnitOfWork.Commit();
        }

        public DoctorModel? GetDoctorById(long id)
        {
            return UnitOfWork.Select<DoctorModel>().FirstOrDefault(d => d.Id == id);
        }

        public List<DoctorModel> GetAllDoctors() => UnitOfWork.Select<DoctorModel>().ToList();

        public List<DoctorModel> GetDoctorsByDepartmentId(long departmentId)
        {
            return UnitOfWork.Select<DoctorModel>().Where(d => d.DepartmentId == departmentId).ToList();
        }

        public void UpdateDoctor(DoctorModel doctor)
        {
            UnitOfWork.Update(doctor);
            UnitOfWork.Commit();
        }

        public void DeleteDoctor(long id)
        {
            DoctorModel? doctor = GetDoctorById(id);
            if (doctor != null)
            {
                // Delete all responses linked to the doctor first
                List<ConsultationResponseModel> responses = UnitOfWork.Select<ConsultationResponseModel>().Where(r => r.DoctorId == id).ToList();
                foreach (ConsultationResponseModel response in responses)
                {
                    UnitOfWork.Delete(response);
                }

                // Now delete the doctor
                UnitOfWork.Delete(doctor);
                UnitOfWork.Commit();
            }
        }

    }
}
