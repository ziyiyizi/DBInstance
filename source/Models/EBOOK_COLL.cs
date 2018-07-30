namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.EBOOK_COLL")]
    public partial class EBOOK_COLL
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EBOOKID { get; set; }

        [StringLength(20)]
        public string EBOOKTYPE { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string EBOOKNAME { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(10)]
        public string MEMBERID { get; set; }
    }
}
