namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOK_REG")]
    public partial class BOOK_REG
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BOOKID { get; set; }

        
        [Column(Order = 1)]
        [StringLength(13)]
        public string ISBN { get; set; }

        
        [Column(Order = 2)]
        [StringLength(20)]
        public string BOOKNAME { get; set; }

        
        [Column(Order = 3)]
        public DateTime MAINTAINTIME { get; set; }
    }
}
