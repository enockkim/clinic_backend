using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
    [Table("diagnostic_imaging_requests")]
    public partial class DiagnosticImagingRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int imagingRequestId { get; set; }
        public int? appointmentId { get; set; }
        [JsonIgnore]
        public Appointment Appointment { get; set; }
        public DateTime? datetimeOfRequest { get; set; }
        public string? doctorRemarks { get; set; }
        public int? imagingSubType { get; set; }
        [JsonIgnore]
        public DiagnositcImagingSubtype DiagnositcImagingSubtype { get; set; }
        public string? result { get; set; }
        public int? status { get; set; }
        public string? technicianRemarks { get; set; }
    }
}
