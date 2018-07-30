namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.RECOM_BOOK_USER")]
    public partial class RECOM_BOOK_USER
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(13)]
        public string ISBN { get; set; }

        
        [Column(Order = 1)]
        [StringLength(10)]
        public string MEMBERID { get; set; }

        [StringLength(20)]
        public string BOOKNAME { get; set; }

        
        [Column(Order = 2)]
        public byte ISPURCHASED { get; set; }
    }
}
