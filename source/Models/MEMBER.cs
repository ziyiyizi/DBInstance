namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.MEMBERS")]
    public partial class MEMBER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MEMBER()
        {
            BOOKRENTINGs = new HashSet<BOOKRENTING>();
            BOOKRERSERVATIONs = new HashSet<BOOKRERSERVATION>();
            EBOOKHISTORies = new HashSet<EBOOKHISTORY>();
            ROOMRESERVATIONs = new HashSet<ROOMRESERVATION>();
            ROOMOCCUPATIONs = new HashSet<ROOMOCCUPATION>();
            SEATS = new HashSet<SEAT>();
            BOOKS = new HashSet<BOOK>();
            RECOMMENDEDBOOKS = new HashSet<RECOMMENDEDBOOKS>();
            EBOOKS = new HashSet<EBOOK>();
            
        }

        [StringLength(10)]
        public string MEMBERID { get; set; }

        [Required]
        [StringLength(15)]
        public string PASSWORDS { get; set; }

        [StringLength(15)]
        public string MEMBERNAME { get; set; }

        public byte? UPPERLIMIT { get; set; }

        public byte? MEMBERSTATE { get; set; }

        public byte? RESERVELIMIT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKRENTING> BOOKRENTINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKRERSERVATION> BOOKRERSERVATIONs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EBOOKHISTORY> EBOOKHISTORies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROOMRESERVATION> ROOMRESERVATIONs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ROOMOCCUPATION> ROOMOCCUPATIONs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SEAT> SEATS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOK> BOOKS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RECOMMENDEDBOOKS> RECOMMENDEDBOOKS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EBOOK> EBOOKS { get; set; }

        
    }
}
