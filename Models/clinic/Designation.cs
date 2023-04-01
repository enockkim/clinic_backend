using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("designation")]
  public partial class Designation
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int designationId { get; set; }
    public string type { get; set; }
        [JsonIgnore]
        public ICollection<Employee> Staffs { get; set; }
    }
}
