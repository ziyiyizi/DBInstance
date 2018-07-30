namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.RECOMMENDEDBOOKS")]
    public partial class RECOMMENDEDBOOKS
    {
        [Key]
        [StringLength(13)]
        public string ISBN { get; set; }

        [StringLength(20)]
        public string BOOKNAME { get; set; }

        public byte ISPURCHASED { get; set; }
    }
}
