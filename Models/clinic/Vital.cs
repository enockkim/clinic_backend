using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("vitals")]
  public partial class Vital
  {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int vRecordId { get; set; }
        public DateTime timeTaken { get; set; } = DateTime.Now;
        public decimal? temperature { get; set; }
        public string pressure { get; set; }
        public decimal? pulseRate { get; set; }
        public int? respirationRate { get; set; }
        public decimal? weight { get; set; }
        public decimal? height { get; set; }
        public int appointmentId { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }
        public int status { get; set; } = 1;
    }
}
