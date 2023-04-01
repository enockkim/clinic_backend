using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("counties")]
  public partial class County
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
        public string county_name { get; set; }
        [JsonIgnore]
        public ICollection<Subcounty> Subcounties { get; set; }
    }
}
