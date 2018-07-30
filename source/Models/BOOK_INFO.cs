namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOK_INFO")]
    public partial class BOOK_INFO
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string ISBN { get; set; }

        public int? STOCK { get; set; }

        [StringLength(10)]
        public string MAJOR { get; set; }

        [Column(Order = 1)]
        [StringLength(20)]
        public string BOOKNAME { get; set; }
    }
}
