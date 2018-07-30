namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.EBOOKCOLLECTING")]
    public partial class EBOOKCOLLECTING
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EBOOKID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MEMBERID { get; set; }

        public virtual EBOOK EBOOK { get; set; }
    }
}
