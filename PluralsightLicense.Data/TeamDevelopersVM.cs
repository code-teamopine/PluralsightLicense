using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PluralsightLicense.Data
{
    public class TeamDevelopersVM
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string DeveloperName { get; set; }
       
    }
}
