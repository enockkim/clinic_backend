using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("subcounty")]
  public partial class Subcounty
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int subcountyId
        {
      get;
      set;
    }
    public int countyId
    {
      get;
      set;
    }
        public County County { get; set; }
    public string subcounty
        {
      get;
      set;
    }

        public ICollection<Ward> Ward { get; set; }
    }
}
