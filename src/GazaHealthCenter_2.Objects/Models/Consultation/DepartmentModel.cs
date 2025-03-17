using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Objects.Models.Consultation
{
    public class DepartmentModel : AModel
    {
        [Required, StringLength(128)]
        public string Name { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<DoctorModel> Doctors { get; set; }
    }

}
