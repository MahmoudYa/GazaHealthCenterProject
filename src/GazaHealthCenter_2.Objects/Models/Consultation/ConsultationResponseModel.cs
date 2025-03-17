using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Objects.Models.Consultation
{
    public class ConsultationResponseModel : AModel
    {
        [Required]
        public long ConsultationRequestId { get; set; }

        [ForeignKey(nameof(ConsultationRequestId))]
        public virtual ConsultationRequestModel ConsultationRequest { get; set; }

        [Required]
        public long DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public virtual DoctorModel Doctor { get; set; }

        [Required, StringLength(1024)]
        public string ResponseText { get; set; }

        public DateTime ResponseDate { get; set; } = DateTime.UtcNow;
    }


}
