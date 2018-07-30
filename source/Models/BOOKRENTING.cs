namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOKRENTING")]
    public partial class BOOKRENTING
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOKRENTING()
        {
            BOOKBILLINGs = new HashSet<BOOKBILLING>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string BOOKID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MEMBERID { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime RENTTIME { get; set; }

        public DateTime? DEADLINE { get; set; }

        public bool? RENTSTATE { get; set; }

        public DateTime? RETURNTIME { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKBILLING> BOOKBILLINGs { get; set; }

        public virtual INTERNALBOOKS INTERNALBOOK { get; set; }

        public virtual MEMBER MEMBER { get; set; }
    }
}
