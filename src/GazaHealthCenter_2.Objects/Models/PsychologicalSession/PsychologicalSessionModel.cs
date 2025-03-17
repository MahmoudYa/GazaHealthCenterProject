using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Objects.Models.PsychologicalSession
{
    public class PsychologicalSessionModel : AModel
    {
        public string? PatientName { get; set; }
        public string? PatientWhatsApp { get; set; } 
        public string? PatientNotes { get; set; }  

        public string PsychologistName { get; set; }
        public DateTime SessionDate { get; set; }
        public string Notes { get; set; }
        public bool IsBooked { get; set; }
    }
}
