using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic.Models.clinic
{
    [Table("unit_of_measure")]
    public partial class UnitOfMeasure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uomId { get; set; }
        public string uomName { get; set; }
        public string uomAbbreviation { get; set; }

        public ICollection<Inventory> Inventories { get; set; }
    }
}
