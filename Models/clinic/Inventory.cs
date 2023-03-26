using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
    [Table("inventory")]
    public partial class Inventory
    {
        [Key]
        public int itemId { get; set; }
        public int? category { get; set; }
        public string brandName { get; set; }
        public string medication { get; set; }
        public int? administrationType { get; set; }
        [Column("unit")]
        [JsonPropertyName("unit")]
        public string unit1 { get; set; }
        public int unitCost { get; set; }
        public int stock { get; set; }
        public int UnitOfMeasure { get; set; }
        [JsonIgnore]
        public UnitOfMeasure UnitOfMeasure1 { get; set; }
        [JsonIgnore]
        public InventoryCategory InventoryCategory { get; set; }
        [JsonIgnore]
        public AdministrationType AdministrationType1 { get; set; }
        [JsonIgnore]
        public ICollection<AccountsPayable> AccountsPayable { get; set; }
        [JsonIgnore]
        public ICollection<Prescription> Prescriptions { get; set; }
    }

    public class AddStock
    {
        public int itemId { get; set; }
        public int stock { get; set; }
    }
}
