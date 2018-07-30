namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.ADMINISTRATORS")]
    public partial class ADMINISTRATOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADMINISTRATOR()
        {
            BOOKMAINTAININGs = new HashSet<BOOKMAINTAINING>();
            MAGAZINEMAINTAININGs = new HashSet<MAGAZINEMAINTAINING>();
            MAGAZINEARCHIVINGs = new HashSet<MAGAZINEARCHIVING>();
        }

        [Key]
        [StringLength(10)]
        public string STAFFID { get; set; }

        [StringLength(15)]
        public string PASSWORDS { get; set; }

        [StringLength(15)]
        public string STAFFNAME { get; set; }

        [StringLength(10)]
        public string STAFFPOSITION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKMAINTAINING> BOOKMAINTAININGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAGAZINEMAINTAINING> MAGAZINEMAINTAININGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAGAZINEARCHIVING> MAGAZINEARCHIVINGs { get; set; }
    }
}
