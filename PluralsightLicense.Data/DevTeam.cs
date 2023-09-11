using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PluralsightLicense.Data
{
    public class DevTeam
    {
        [Key]
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int DeveloperId { get; set; }
       public string DeveloperIds { get; set; }
    }
}
