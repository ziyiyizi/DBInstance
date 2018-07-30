namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.MAGAZINES")]
    public partial class MAGAZINE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MAGAZINE()
        {
            INTERNALMAGAZINES = new HashSet<INTERNALMAGAZINE>();
            MAGAZINEARCHIVINGs = new HashSet<MAGAZINEARCHIVING>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string ISSN { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string MAGAZINEID { get; set; }

        [StringLength(25)]
        public string MAGAZINENAME { get; set; }

        public bool? ISARCHIVED { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INTERNALMAGAZINE> INTERNALMAGAZINES { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAGAZINEARCHIVING> MAGAZINEARCHIVINGs { get; set; }
    }
}
