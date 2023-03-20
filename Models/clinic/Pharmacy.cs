using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("pharmacy")]
  public partial class Pharmacy
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int issueId
    {
      get;
      set;
    }
    public int prescriptionId
    {
      get;
      set;
    }
        [JsonIgnore]
    public Prescription Prescription { get; set; }
  }
}
