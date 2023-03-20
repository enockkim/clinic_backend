using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("designation")]
  public partial class Designation
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int designationId
    {
      get;
      set;
    }


    public ICollection<Employee> Staffs { get; set; }
    public string type
    {
      get;
      set;
    }
  }
}
