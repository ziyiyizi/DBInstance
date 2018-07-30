namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.INTERNALMAGAZINES")]
    public partial class INTERNALMAGAZINE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public INTERNALMAGAZINE()
        {
            MAGAZINEMAINTAININGs = new HashSet<MAGAZINEMAINTAINING>();
        }

        [Key]
        [StringLength(10)]
        public string BOOKID { get; set; }

        [StringLength(8)]
        public string ISSN { get; set; }

        [StringLength(5)]
        public string MAGAZINEID { get; set; }

        public short? SHELFNUMBER { get; set; }

        public byte? FLOOR { get; set; }

        public virtual MAGAZINE MAGAZINE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAGAZINEMAINTAINING> MAGAZINEMAINTAININGs { get; set; }
    }
}
