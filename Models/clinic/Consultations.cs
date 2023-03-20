using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
    [Table("consultation_room")]
    public partial class Consultations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int consultationId { get; set; }
        public int appointmentId { get; set; }

        public Appointment Appointment { get; set; }

        public string? remarks { get; set; }

        public int status { get; set; }
    }
}