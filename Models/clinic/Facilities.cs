using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("facilities")]
  public partial class Facilities
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int facilityId { get; set; }

        [JsonIgnore]
    public ICollection<Appointment> Appointments { get; set; }

    public string facilityName { get; set; }
    }
}
