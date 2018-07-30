namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOKRECOMMENDING")]
    public partial class BOOKRECOMMENDING
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MEMBERID { get; set; }
    }
}
