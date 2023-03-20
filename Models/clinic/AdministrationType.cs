using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("administration_type")]
  public partial class AdministrationType
  {
    [Key]
    public int administrationTypeId
    {
      get;
      set;
    }


    public ICollection<Inventory> Inventories { get; set; }

    [Column("administrationType")]
    public string administrationType1
    {
      get;
      set;
    }
  }
}
