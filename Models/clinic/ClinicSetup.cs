using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
    [Table("clinic_setup")]
    public partial class ClinicSetup
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string pobox { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        //public string logo { get; set; }
        public int contact { get; set; }
        public int county { get; set; }
        public int subcounty { get; set; }
        public int ward { get; set; }
    }
}
