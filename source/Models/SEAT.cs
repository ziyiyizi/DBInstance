namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.SEATS")]
    public partial class SEAT
    {
        [StringLength(5)]
        public string SEATID { get; set; }

        public byte? FLOOR { get; set; }

        [StringLength(10)]
        public string MEMBERID { get; set; }

        public virtual MEMBER MEMBER { get; set; }
    }
}
