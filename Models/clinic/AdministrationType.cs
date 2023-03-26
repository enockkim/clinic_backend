using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("administration_type")]
  public partial class AdministrationType
  {
    [Key]
    public int administrationTypeId { get; set; }
    [Column("administrationType")]
        [JsonPropertyName("administrationType")]
    public string administrationType1 { get; set; }
        [JsonIgnore]
        public ICollection<Inventory> Inventories { get; set; }
    }
}
