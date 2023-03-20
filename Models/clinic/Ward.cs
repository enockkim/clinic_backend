using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
    [Table("ward")]
    public partial class Ward
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int wardId { get; set; }
        public int subcountyId { get; set; }
        public string ward { get; set; }
        public Subcounty Subcounty { get; set; }
    }
}