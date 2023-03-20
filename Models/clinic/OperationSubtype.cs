using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
  [Table("operation_subtypes")]
  public partial class OperationSubtype
  {
    [Key]
    public int operationSubTypeId { get; set; }
    public int? operationTypeId { get; set; }

    [Column("operationSubType")]
    [JsonPropertyName("operationSubType")]
    public string operationSubType1 { get; set; }
    public int? cost { get; set; }
    [JsonIgnore]
    public OperationType OperationType { get; set; }
    [JsonIgnore]
    public ICollection<OperationRequest> OperationRequests { get; set; }
    }
}
