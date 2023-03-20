using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
    [Table("appointment")]
    public partial class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int appointmentId { get; set; }
        [JsonIgnore]
        public ICollection<Bill> Bills { get; set; }
        public int patientId { get; set; }
        [JsonIgnore]
        public Patient Patient { get; set; }
        public int? employeeId { get; set; }
        [JsonIgnore]
        public Employee Employee { get; set; }
        public DateTime dateOfAppointment { get; set; } = DateTime.Now;
        public string? remarks { get; set; }
        public int appointmentStatus { get; set; }
        [JsonIgnore]
        public Facilities Facilities { get; set; }
        public int? paymentMethod { get; set; }
        [JsonIgnore]
        public PaymentMethod PaymentMethod1 { get; set; }
        public int? appointmentType { get; set; }
        [JsonIgnore]
        public AppointmentType AppointmentType1 { get; set; }
        public DateTime dateOfCreation { get; set; }
        public string createdBy { get; set; }
        public int patientType { get; set; }
        [JsonIgnore]
        public PatientType PatientType { get; set; }
        public int previousFacility { get; set; }

        [JsonIgnore]
        public ICollection<Laboratory> Laboratories { get; set; }
        [JsonIgnore]
        public ICollection<OperationRoom> OperationRooms { get; set; }
        [JsonIgnore]
        public ICollection<Vital> Vitals { get; set; }
        [JsonIgnore]
        public ICollection<Consultations> Consultations { get; set; }
        [JsonIgnore]
        public ICollection<DiagnosticImagingRequest> DiagnosticImagingRequests { get; set; }
        [JsonIgnore]
        public ICollection<Prescription> Prescriptions { get; set; }
        [JsonIgnore]
        public ICollection<OperationRequest> OperationRequests { get; set; }
    }
    public partial class AppointmentData
    {
        public int appointmentId { get; set; }
        public int patientId { get; set; }
        public string patientName { get; set; }
        public int? employeeId { get; set; }
        public string? employeeName { get; set; }
        public DateTime dateOfAppointment { get; set; }
        public string? remarks { get; set; }
        public int appointmentStatus { get; set; }
        public string facilityName { get; set; }
        public int? paymentMethod { get; set; }
        public string? paymentMethodName { get; set; }
        public int? appointmentType { get; set; }
        public string? appointmentTypeName { get; set; }
        public DateTime dateOfCreation { get; set; }
        public string createdBy { get; set; }
        public int patientType { get; set; }
        public string patientTypeName { get; set; }
        public int previousFacility { get; set; }
        public string previousFacilityName { get; set; }
    }
}
 