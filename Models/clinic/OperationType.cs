using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic.Models.clinic
{
    [Table("operation_type")]
    public partial class OperationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int operationTypeId { get; set; }
        public string operationName { get; set; }
        [JsonIgnore]
        public ICollection<OperationRoom> OperationRooms { get; set; }
        [JsonIgnore]
        public ICollection<OperationSubtype> OperationSubtypes { get; set; }
    }

    public class OpType
    {
        public int operationTypeId { get; set; }
        public string operationName { get; set; }
        public List<OperationSubtype> subtypes { get; set; }
    }
}
