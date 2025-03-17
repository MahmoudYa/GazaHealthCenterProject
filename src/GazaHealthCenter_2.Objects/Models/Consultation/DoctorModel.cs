using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Objects.Models.Consultation
{
    public class DoctorModel : AModel
    {
        [Required, StringLength(128)]
        public string Name { get; set; }

        [Required, StringLength(256)]
        public string Specialty { get; set; }

        [Required]
        public long DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual DepartmentModel Department { get; set; }
    }

}
