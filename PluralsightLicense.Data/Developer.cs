using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PluralsightLicense.Data
{
    public class Developer
    {
        [Key]
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
        public bool IsPluralsightLicenseAssigned { get; set; } = false;
    }
}
