namespace WebApplication3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBTA.EBOOKS")]
    public partial class EBOOK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EBOOK()
        {
            EBOOKCOLLECTINGs = new HashSet<EBOOKCOLLECTING>();
            EBOOKHISTORies = new HashSet<EBOOKHISTORY>();
            MEMBERs = new HashSet<MEMBER>();
        }

        [StringLength(20)]
        public string EBOOKTYPE { get; set; }

        [Required]
        [StringLength(20)]
        public string EBOOKNAME { get; set; }

        [Key]
        public int EBOOKID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EBOOKCOLLECTING> EBOOKCOLLECTINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EBOOKHISTORY> EBOOKHISTORies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEMBER> MEMBERs { get; set; }
    }
}
