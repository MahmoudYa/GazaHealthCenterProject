using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects.Models.PsychologicalSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Services.PsychologicalSessionService
{
    public class PsychologicalSessionService : AService
    {
        public PsychologicalSessionService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public List<PsychologicalSessionModel> GetAllSessions()
        {
            return UnitOfWork.Select<PsychologicalSessionModel>().ToList();
        }

        public PsychologicalSessionModel? GetSessionById(long id)
        {
            return UnitOfWork.Get<PsychologicalSessionModel>(id);
        }

        public void AddSession(PsychologicalSessionModel session)
        {
            UnitOfWork.Insert(session);
            UnitOfWork.Commit();
        }

        public void DeleteSession(long id)
        {
            PsychologicalSessionModel? session = UnitOfWork.Get<PsychologicalSessionModel>(id);
            if (session != null)
            {
                UnitOfWork.Delete(session);
                UnitOfWork.Commit();
            }
        }

        public void BookSession(long id, string patientName, string whatsappNumber, string otherNotes)
        {
            PsychologicalSessionModel? session = UnitOfWork.Get<PsychologicalSessionModel>(id);
            if (session != null && !session.IsBooked)
            {
                session.IsBooked = true;
                session.PatientName = patientName;
                session.PatientWhatsApp = whatsappNumber;
                session.PatientNotes = otherNotes;

                UnitOfWork.Update(session);
                UnitOfWork.Commit();
            }
        }


        public void CancelBooking(long id)
        {
            PsychologicalSessionModel? session = UnitOfWork.Get<PsychologicalSessionModel>(id);
            if (session != null && session.IsBooked)
            {
                session.IsBooked = false;
                session.PatientName = string.Empty;
                UnitOfWork.Update(session);
                UnitOfWork.Commit();
            }
        }
    }
}
