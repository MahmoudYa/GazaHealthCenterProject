using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Objects.Models.MedicalLocation
{
    public class MedicalLocationModel : AModel
    {
        [Required, StringLength(128)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [Url]
        public string? Website { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? ImageUrl { get; set; }

        [Required, Url]
        public string GoogleMapsUrl { get; set; }

        [Required]
        public MedicalLocationType Type { get; set; }
    }

    public enum MedicalLocationType
    {
        Hospital,
        HealthCenter,
        MedicalPoint,
        Pharmacy
    }
}
