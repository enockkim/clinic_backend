using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("employee")]
  public partial class Employee
  {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }
        public string surname { get; set; }
        public string otherName { get; set; }
        public int? contact { get; set; }
        public string address { get; set; }
        public int? gender { get; set; }
        [JsonIgnore]
        public Gender Gender1 { get; set; }
        public int designationId { get; set; }
        [JsonIgnore]
        public Designation Designation { get; set; }
        public string addedBy { get; set; }
        public DateTime? dateAdded { get; set; } 
        public string email { get; set; }
        public int? employmentStatus { get; set; }
        public DateTime? startDate { get; set; } = DateTime.Now;
        public DateTime? dateOfBirth { get; set; } = DateTime.Now;
        public string nokName { get; set; }
        public int? nokContact { get; set; }
        public int nokRelationship { get; set; }
        public int? idNumber { get; set; }
        public int? employmentType { get; set; }

        [JsonIgnore]
        public ICollection<Appointment> Appointments { get; set; }
        [JsonIgnore]
        public ICollection<Doctor> Doctors { get; set; }
        [JsonIgnore]
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }


    public class CreateEmployee
    {
        public Employee employeeData { get; set; }
        public ApplicationUser userData { get; set; }
    }

    public class EmploymentType
    {
        public int employmentTypeId { get; set; }
        public string employmentType { get; set; }
    }
}
