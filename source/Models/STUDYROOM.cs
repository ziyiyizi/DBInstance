namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.STUDYROOMS")]
    public partial class STUDYROOM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public STUDYROOM()
        {
            ROOMOCCUPATIONs = new HashSet<ROOMOCCUPATION>();
            ROOMRESERVATIONs = new HashSet<ROOMRESERVATION>();
        }

        [Key]
        [StringLength(4)]
        public string ROOMID { get; set; }

        public byte? FLOOR { get; set; }

        public byte? ROOMCAPACITY { get; set; }

        public byte? ROOMSTATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROOMOCCUPATION> ROOMOCCUPATIONs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROOMRESERVATION> ROOMRESERVATIONs { get; set; }
    }
}
