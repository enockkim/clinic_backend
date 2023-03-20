using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("diagnostic_imaging_types")]
  public partial class DiagnosticImagingType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int imagingTypeId { get; set; }
        public string imagingType { get; set; }
        [JsonIgnore]
        public ICollection<DiagnositcImagingSubtype> DiagnositcImagingSubtypes { get; set; }
  }

    public class ImagingType
    {
        public int imagingTypeId { get; set; }
        public string imagingType { get; set; }
        public List<DiagnositcImagingSubtype> subtypes { get; set; }
    }
}
