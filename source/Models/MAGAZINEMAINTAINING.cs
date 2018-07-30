namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.MAGAZINEMAINTAINING")]
    public partial class MAGAZINEMAINTAINING
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string BOOKID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string STAFFID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime MAINTAINTIME { get; set; }

        public bool? MAINTAINTYPE { get; set; }

        public virtual ADMINISTRATOR ADMINISTRATOR { get; set; }

        public virtual INTERNALMAGAZINE INTERNALMAGAZINE { get; set; }
    }
}
