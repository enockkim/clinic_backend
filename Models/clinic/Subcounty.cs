using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("subcounty")]
  public partial class Subcounty
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
        public int county_id { get; set; }
        [JsonIgnore]
        public County County { get; set; }
    public string constituency_name { get; set; }
        public string ward { get; set; }
        [JsonIgnore]

        public ICollection<Ward> Ward { get; set; }
   }

    public class subcounties
    {
        public int subcountyId { get; set; }
        public int countyId { get; set; }
        public string subcountyName { get; set; }

    }

    public class wards
    {
        public int id { get; set; }
        public int subcountyId { get; set; }
        public string ward { get; set; }
    }
}
