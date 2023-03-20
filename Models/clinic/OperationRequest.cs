
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
    [Table("operation_requests")]
    public partial class OperationRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int operationRequestId { get; set; }
        public int? operationSubType { get; set; }
        [JsonIgnore]
        public OperationSubtype OperationSubtype1 { get; set; }
        public int? appointmentId { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }
        public DateTime? datetimeOfRequest { get; set; } = DateTime.Now;
        public string? doctorRemarks { get; set; }
        public int? status { get; set; }
        public string? operatorRemarks { get; set; }
    }
}
