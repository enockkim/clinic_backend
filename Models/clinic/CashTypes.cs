    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("cash_types")]
  public partial class CashType
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? cashTypeId { get; set; }
    public string cashType { get; set; }
    [JsonIgnore]
    public ICollection<AccountsReceivable> AccountsReceivables { get; set; }
  }
}
