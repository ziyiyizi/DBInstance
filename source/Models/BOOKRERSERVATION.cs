namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOKRERSERVATION")]
    public partial class BOOKRERSERVATION
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ISBN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MEMBERID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime RESERVETIME { get; set; }

        public DateTime? DEADLINE { get; set; }

        public byte? RESERVESTATE { get; set; }

        public virtual BOOK BOOK { get; set; }

        public virtual MEMBER MEMBER { get; set; }
    }
}
