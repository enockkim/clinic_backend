using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("pharmacy_Inventory")]
  public partial class PharmacyInventory
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int itemId
    {
      get;
      set;
    }


    public ICollection<Prescription> Prescriptions { get; set; }
    public string itemName
    {
      get;
      set;
    }
    public string itemBrand
    {
      get;
      set;
    }
    public string itemDescription
    {
      get;
      set;
    }
    public int itemPrice
    {
      get;
      set;
    }
  }
}
