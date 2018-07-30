namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.ROOMOCCUPATION")]
    public partial class ROOMOCCUPATION
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MEMBERID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(4)]
        public string ROOMID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime STARTTIME { get; set; }

        public DateTime? DEADLINE { get; set; }

        public DateTime? RETURNTIME { get; set; }

        public virtual MEMBER MEMBER { get; set; }

        public virtual STUDYROOM STUDYROOM { get; set; }
    }
}
