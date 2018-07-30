namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.MAGAZINEARCHIVING")]
    public partial class MAGAZINEARCHIVING
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ISSN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MAGAZINEID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string STAFFID { get; set; }

        public DateTime? ARCHIVETIME { get; set; }

        public byte? ARCHIVELOCATION { get; set; }

        public virtual ADMINISTRATOR ADMINISTRATOR { get; set; }

        public virtual MAGAZINE MAGAZINE { get; set; }
    }
}
