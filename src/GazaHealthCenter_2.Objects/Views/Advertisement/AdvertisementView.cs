using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Objects.Views.Advertisement
{
    public class AdvertisementView : AView
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public IFormFile? MediaFile { get; set; }
    }
}
