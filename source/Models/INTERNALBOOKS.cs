namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.INTERNALBOOKS")]
    public partial class INTERNALBOOKS
    {
        public byte BOOKSTATE { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        public short? SHELFNUMBER { get; set; }

        public byte? FLOOR { get; set; }

        [Key]
        public int BOOKID { get; set; }

        public virtual BOOK BOOK { get; set; }
    }
}
