using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("doctor_types")]
  public partial class DoctorType
  {
    public int? consultationCost
    {
      get;
      set;
    }

    [Column("doctorType")]
    public string doctorType1
    {
      get;
      set;
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int doctorTypeId
    {
      get;
      set;
    }


    public ICollection<Doctor> Doctors { get; set; }
  }
}
