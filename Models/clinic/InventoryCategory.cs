using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
    [Table("inventory_category")]
    public partial class InventoryCategory
    {
        [Key]
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        [JsonIgnore]
        public ICollection<Inventory> Inventories { get; set; }
    }
}
