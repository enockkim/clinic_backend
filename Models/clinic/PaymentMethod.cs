using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("payment_methods")]
  public partial class PaymentMethod
  {
    public string method
    {
      get;
      set;
    }
    [Key]
    public int methodId
    {
      get;
      set;
    }

        [JsonIgnore]
    public ICollection<Appointment> Appointments { get; set; }
        [JsonIgnore]
        public ICollection<AccountsReceivable> AccountsReceivables { set; get; }
        [JsonIgnore]

        public ICollection<AccountsPayable> AccountsPayable { set; get; }
    }

}
