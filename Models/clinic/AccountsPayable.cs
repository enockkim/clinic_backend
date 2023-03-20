using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
    [Table("accounts_payable")]
    public partial class AccountsPayable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int transactionId { get; set; }
        public int amountDue { get; set; }
        public int amountPaid { get; set; }
        public DateTime dateOfTransaction { get; set; } = DateTime.Now;
        public DateTime dateOfRecord { get; set; } = DateTime.Now;
        public int? paymentMethod { get; set; }
        public PaymentMethod PaymentMethod1 { get; set; }
        public string transactionRefrence { get; set; }
        public int supplierId { get; set; }
        public Supplier Supplier { get; set; }
        public int itemId { get; set; }
        public Inventory Inventory { get; set; }
        public int quantity { get; set; }
        public string addedBy { get; set; }
    }
}
