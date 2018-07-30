namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOKCOMMERCIAL")]
    public partial class BOOKCOMMERCIAL
    {
        [Key]
        [StringLength(13)]
        public string ISBN { get; set; }

        [StringLength(20)]
        public string BOOKNAME { get; set; }

        [StringLength(40)]
        public string PUBLISHER { get; set; }

        [StringLength(20)]
        public string PAGENUM { get; set; }

        [StringLength(20)]
        public string PRICE { get; set; }

        [StringLength(20)]
        public string PUBLISHTIME { get; set; }

        public string DETAIL { get; set; }

        [StringLength(20)]
        public string RATE { get; set; }

        [StringLength(202)]
        public string URL { get; set; }

        [StringLength(20)]
        public string WRITER { get; set; }
    }
}
