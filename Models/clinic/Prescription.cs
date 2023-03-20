using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("prescription")]
  public partial class Prescription
  {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int prescriptionId { get; set; }
        public int appointmentId { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }
        [JsonIgnore]
        public ICollection<Pharmacy> Pharmacies { get; set; }
        public int itemId { get; set; }
        [JsonIgnore]
        public Inventory Inventory { get; set; }
        public int dosageNumber { get; set; }
        public string remarks { get; set; }
        public int status { get; set; }
        public int? availability { get; set; }
    }
}
