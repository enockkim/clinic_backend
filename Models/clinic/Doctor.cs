using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("doctors")]
  public partial class Doctor
  {
    public int? doctorType
    {
      get;
      set;
    }
    public DoctorType DoctorType1 { get; set; }
    [Key]
    public int employeeId
    {
      get;
      set;
    }
    public Employee Employee { get; set; }
  }
}
