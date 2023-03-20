using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("relationship")]
  public partial class Relationship
  {
    [Key]
    public int relationshipId { get; set; }
    [JsonIgnore]
    public ICollection<Patient> Patients { get; set; }

    [Column("relationship")]
    [JsonPropertyName("relationshipName")]
    public string relationship1 { get; set; }
    }
}
