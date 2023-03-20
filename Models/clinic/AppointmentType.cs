using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("appointment_type")]
  public partial class AppointmentType
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int typeId
    {
      get;
      set;
    }
    public string type
    {
      get;
      set;
    }
        public ICollection<Appointment> Appointments { get; set; }

        public int patientType { get; set; }
        public PatientType PatientType { get; set; }
    }
}
