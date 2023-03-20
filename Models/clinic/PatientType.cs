using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("patient_type")]
  public partial class PatientType
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int patientTypeId
        {
      get;
      set;
    }


    public ICollection<Appointment> Appointments { get; set; }
    public string patientType
        {
      get;
      set;
    }

        public ICollection<AppointmentType> AppointmentType { get; set; }

    }
}
