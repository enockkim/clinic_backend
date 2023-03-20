using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("gender")]
  public partial class Gender
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public ICollection<Patient> Patients { get; set; }
    public ICollection<Employee> Staffs { get; set; }
    [Column("gender")]
    [JsonPropertyName("genderName")]
    public string gender1 { get; set; }
    }
}
