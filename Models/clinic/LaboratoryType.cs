using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("laboratory_type")]
  public partial class LaboratoryType
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int labTypeID { get; set; }
    [JsonIgnore]
    public ICollection<Laboratory> Laboratories { get; set; }
    public string labTypeName { get; set; }
    public int cost { get; set; }
  }
}
