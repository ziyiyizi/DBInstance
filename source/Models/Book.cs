namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.BOOKS")]
    public partial class BOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BOOK()
        {
            BOOKRERSERVATIONs = new HashSet<BOOKRERSERVATION>();
            INTERNALBOOKS = new HashSet<INTERNALBOOKS>();
            MEMBERS = new HashSet<MEMBER>();
           
        }

        [Key]
        [StringLength(13)]
        public string ISBN { get; set; }

        [StringLength(20)]
        public string BOOKNAME { get; set; }

        [StringLength(10)]
        public string MAJOR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKRERSERVATION> BOOKRERSERVATIONs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INTERNALBOOKS> INTERNALBOOKS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER> MEMBERS { get; set; }

    
    }
}
