using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects.Models.Consultation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Services.Consultation
{
    public class ConsultationService : AService
    {
        public ConsultationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public void SubmitRequest(ConsultationRequestModel request)
        {
            UnitOfWork.Insert(request);
            UnitOfWork.Commit();
        }

        public List<ConsultationRequestModel> GetConsultationsByDepartment(long departmentId)
        {
            return UnitOfWork.Select<ConsultationRequestModel>()
                .Where(c => c.DepartmentId == departmentId)
                .ToList();
        }

        public List<ConsultationResponseModel> GetResponses(long consultationId) =>
            UnitOfWork.Select<ConsultationResponseModel>()
                .Where(r => r.ConsultationRequestId == consultationId)
                .ToList();

        public void AddResponse(ConsultationResponseModel response)
        {
            UnitOfWork.Insert(response);
            UnitOfWork.Commit();
        }

        public ConsultationRequestModel? GetConsultationById(long id)
        {
            return UnitOfWork.Get<ConsultationRequestModel>(id);
        }
        public void UpdateConsultation(ConsultationRequestModel consultation)
        {
            UnitOfWork.Update(consultation);
            UnitOfWork.Commit();
        }

        public void DeleteConsultation(long id)
        {
            // Retrieve the consultation
            ConsultationRequestModel? consultation = GetConsultationById(id);
            if (consultation != null)
            {
                // 🔹 Delete all associated responses
                List<ConsultationResponseModel> responses = UnitOfWork.Select<ConsultationResponseModel>()
                                          .Where(r => r.ConsultationRequestId == id)
                                          .ToList();

                foreach (ConsultationResponseModel? response in responses)
                {
                    UnitOfWork.Delete(response); // Delete each response
                }

                // 🔹 Now delete the consultation
                UnitOfWork.Delete(consultation);
                UnitOfWork.Commit(); // Commit the changes to the database
            }
        }
        public ConsultationResponseModel? GetResponseById(long id)
        {
            return UnitOfWork.Get<ConsultationResponseModel>(id);
        }

        public void UpdateResponse(ConsultationResponseModel response)
        {
            UnitOfWork.Update(response);
            UnitOfWork.Commit();
        }

        public void DeleteResponse(long id)
        {
            ConsultationResponseModel? response = GetResponseById(id);
            if (response != null)
            {
                UnitOfWork.Delete(response);
                UnitOfWork.Commit();
            }
        }

    }


}
