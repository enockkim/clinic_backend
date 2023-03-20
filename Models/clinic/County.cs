using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("counties")]
  public partial class County
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id
    {
      get;
      set;
    }
    public string county_name
    {
      get;
      set;
    }
        public ICollection<Subcounty> Subcounties { get; set; }
    }
}
