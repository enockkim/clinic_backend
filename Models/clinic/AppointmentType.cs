using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("appointment_type")]
  public partial class AppointmentType
  {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int typeId { get; set; }
        public string type { get; set; }
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; }

        public int patientType { get; set; }
        [JsonIgnore]
        public PatientType PatientType { get; set; }
    }
}
