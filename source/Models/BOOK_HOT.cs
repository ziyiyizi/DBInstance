namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOK_HOT")]
    public partial class BOOK_HOT
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string BOOKNAME { get; set; }

        public decimal? RENTNUM { get; set; }
    }
}
