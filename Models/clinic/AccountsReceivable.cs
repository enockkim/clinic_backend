using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static log4net.Appender.RollingFileAppender;

namespace clinic.Models.clinic
{
    [Table("accounts_receivable")]
    public partial class AccountsReceivable
    {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }

    public int? AmountDue { get; set; }

    public int? AmountPaid { get; set; }

    public DateTime DateOfTransaction { get; set; }

    public int? PaymentMethod { get; set; }

    public int? CashType { get; set; }

    public int? BillDetailEntryNo { get; set; }

    [StringLength(45)]
    public string TransactionRefrence { get; set; }
        [JsonIgnore]
        public CashType CashType1 { get; set; }
        [JsonIgnore]
        public BillDetail BillDetail1 { get; set; }
        [JsonIgnore]
        public PaymentMethod PaymentMethod1 { get; set; }
        public string FormattedDateTime => DateOfTransaction.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public partial class RawAccountsReceivable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transactionId { get; set; }
        public int amountDue { get; set; }
        public int amountPaid { get; set; }
        public DateTime dateOfTransaction { get; set; }
        public int? paymentMethod { get; set; }
        public int? cashType { get; set; }
        public int billDetailEntryNo { get; set; }
        public string transactionRefrence { get; set; }
    }

    public class PaymentDetails
    {
        public int amountPaid { get; set; }
        public int? cashType { get; set; }
        public int billDetailEntryNo { get; set; }
        public string transactionRefrence { get; set; }
        public int paymentMethod { get; set; }
    }
}
