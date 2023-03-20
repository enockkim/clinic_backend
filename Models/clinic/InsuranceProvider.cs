using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("insurance_providers")]
  public partial class InsuranceProvider
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int providerId
    {
      get;
      set;
    }


    public ICollection<InsuranceDetail> InsuranceDetails { get; set; }
    public string providerName
    {
      get;
      set;
    }
    public int? phoneNumber
    {
      get;
      set;
    }
    public string email
    {
      get;
      set;
    }
    public string address
    {
      get;
      set;
    }
  }
}
