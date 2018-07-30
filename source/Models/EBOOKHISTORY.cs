namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.EBOOKHISTORY")]
    public partial class EBOOKHISTORY
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EBOOKID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MEMBERID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime BROWSETIME { get; set; }

        public virtual EBOOK EBOOK { get; set; }

        public virtual MEMBER MEMBER { get; set; }
    }
}
