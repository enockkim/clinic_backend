using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
  [Table("operation_room")]
  public partial class OperationRoom
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int orId
    {
      get;
      set;
    }
    public int orTypeId
    {
      get;
      set;
    }
    public OperationType OperationType { get; set; }
    public int appointmentId
    {
      get;
      set;
    }
    public Appointment Appointment { get; set; }
    public DateTime dateOfOperation
    {
      get;
      set;
    }
  }
}
