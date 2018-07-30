namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOKBILLING")]
    public partial class BOOKBILLING
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string BOOKID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MEMBERID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime RENTTIME { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(8)]
        public string BILLNUMBER { get; set; }

        public bool? BILLTYPE { get; set; }

        public byte? FIGURES { get; set; }

        public virtual BOOKRENTING BOOKRENTING { get; set; }
    }
}
