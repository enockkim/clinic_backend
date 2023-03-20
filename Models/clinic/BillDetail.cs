using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("bill_details")]
  public partial class BillDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int entryNo { get; set; }
    public int? billNo { get; set; }
    public int? cost { get; set; }
    public string details { get; set; }
    public int? facility { get; set; }
    public int status { get; set; }
    [JsonIgnore]
    public Bill Bill { get; set; }
    [JsonIgnore]
    public ICollection<AccountsReceivable> AccountsReceivables { set; get; }
    }
}
