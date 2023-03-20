using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("insurance_details")]
  public partial class InsuranceDetail
  {
    [Key]
    public int insuranceRecordId
    {
      get;
      set;
    }
    public int? insuranceProvider
    {
      get;
      set;
    }
    public InsuranceProvider InsuranceProvider1 { get; set; }
    public int? memberNo
    {
      get;
      set;
    }
    public int? patientId
    {
      get;
      set;
    }
    public Patient Patient { get; set; }
  }
}
