using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("patients")]
  public partial class Patient
  {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int patientId { get; set; }
        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; }
        [JsonIgnore]
        public ICollection<InsuranceDetail> InsuranceDetails { get; set; }
        public string surname { get; set; }
        public string otherName { get; set; }   
        public int gender { get; set; } 
        [JsonIgnore]
        public Gender Gender1 { get; set; }
        public int contact { get; set; }
        public DateTime dob { get; set; } = DateTime.Now;
        public string nokName { get; set; }
        public int nokContact { get; set; }
        public int nokRelationship { get; set; }
        [JsonIgnore]
        public Relationship Relationship { get; set; }
        public string nationalIdNumber { get; set; }
        public int county { get; set; }
        public int subcounty { get; set; }
        public int ward { get; set; }
        public int status { get; set; }
        public string? email { get; set; }
    }

    public class CreatePatient
    {
        public Patient patientData { get; set; } 
        public ApplicationUser userData { get; set; }
    }  
}
