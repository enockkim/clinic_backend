using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
    [Table("laboratory")]
    public partial class Laboratory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int labId { get; set; }
        public int labTypeId { get; set; }
        [JsonIgnore]
        public LaboratoryType LaboratoryType { get; set; }
        public int appointmentId { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }
        public string? labResults { get; set; }
        public DateTime? date { get; set; }
        public string? doctorRemarks { get; set; }
        public string? labTechRemarks { get; set; }
        public int status { get; set; }
    }
}
