using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("diagnositc_imaging_subtypes")]
  public partial class DiagnositcImagingSubtype
    {
        [Key]
        public int imagingSubTypeId { get; set; }
        public string imagingSubType { get; set; }
        public int? imagingTypeId { get; set; }
        public int cost { get; set; }
        [JsonIgnore]
        public DiagnosticImagingType DiagnosticImagingType { get; set; }
        [JsonIgnore]
        public ICollection<DiagnosticImagingRequest> DiagnosticImagingRequests { get; set; }
  }
}
