using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Objects.Models.Consultation
{
    public class ConsultationRequestModel : AModel
    {
        [Required]
        public long DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual DepartmentModel Department { get; set; }

        [Required, StringLength(512)]
        public string Question { get; set; }

        [Required]
        public long PatientId { get; set; }

        public bool IsPublic { get; set; }

        public virtual ICollection<ConsultationResponseModel> Responses { get; set; }
    }


}
