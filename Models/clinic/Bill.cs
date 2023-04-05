using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("bill")]
  public partial class Bill
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int billNo { get; set; }
    public int? appointmentId { get; set; }
    public int status { get; set; }
    [JsonIgnore]
    public Appointment Appointment { get; set; }
    [JsonIgnore]
    public ICollection<BillDetail> BillDetails { get; set; }
  }

    public class BillData
    {
        public int billNo { get; set; }
        public int? appointmentId { get; set; }
        public int patientId { get; set; }
        public int patientIdNumber { get; set; }
        public string patientName { get; set; }
        public int status { get; set; }
    }
}
