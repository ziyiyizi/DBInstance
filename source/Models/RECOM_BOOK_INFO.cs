namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.RECOM_BOOK_INFO")]
    public partial class RECOM_BOOK_INFO
    {
        [Key]
        [StringLength(13)]
        public string ISBN { get; set; }

        public decimal? RECOM_NUM { get; set; }

        [StringLength(20)]
        public string BOOKNAME { get; set; }
    }
}
