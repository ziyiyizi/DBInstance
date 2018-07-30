namespace WebApplication3.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=TestConfig")
        {
        }
        public virtual DbSet<BOOKCOMMERCIAL> BOOKCOMMERCIALs { get; set; }
        public virtual DbSet<BOOK_HOT> BOOK_HOT { get; set; }
        public virtual DbSet<ADMINISTRATOR> ADMINISTRATORS { get; set; }
        public virtual DbSet<BOOKBILLING> BOOKBILLINGs { get; set; }
        public virtual DbSet<BOOKMAINTAINING> BOOKMAINTAININGs { get; set; }
        public virtual DbSet<BOOKRENTING> BOOKRENTINGs { get; set; }
        public virtual DbSet<BOOKRERSERVATION> BOOKRERSERVATIONs { get; set; }
        public virtual DbSet<BOOK> BOOKS { get; set; }
        public virtual DbSet<EBOOKHISTORY> EBOOKHISTORies { get; set; }
        public virtual DbSet<EBOOK> EBOOKS { get; set; }
        public virtual DbSet<INTERNALMAGAZINE> INTERNALMAGAZINES { get; set; }
        public virtual DbSet<MAGAZINEARCHIVING> MAGAZINEARCHIVINGs { get; set; }
        public virtual DbSet<MAGAZINEMAINTAINING> MAGAZINEMAINTAININGs { get; set; }
        public virtual DbSet<MAGAZINE> MAGAZINES { get; set; }
        public virtual DbSet<MEMBER> MEMBERS { get; set; }
        public virtual DbSet<RECOMMENDEDBOOKS> RECOMMENDEDBOOKS { get; set; }
        public virtual DbSet<ROOMOCCUPATION> ROOMOCCUPATIONs { get; set; }
        public virtual DbSet<ROOMRESERVATION> ROOMRESERVATIONs { get; set; }
        public virtual DbSet<SEAT> SEATS { get; set; }
        public virtual DbSet<STUDYROOM> STUDYROOMS { get; set; }
        public virtual DbSet<BOOKRECOMMENDING> BOOKRECOMMENDING { get; set; }
        public virtual DbSet<RECOM_BOOK_USER> RECOM_BOOK_USER { get; set; }
        public virtual DbSet<INTERNALBOOKS> INTERNALBOOKS { get; set; }
        public virtual DbSet<BOOK_REG> BOOK_REG { get; set; }
        public virtual DbSet<RECOM_BOOK_INFO> RECOM_BOOK_INFO { get; set; }
        public virtual DbSet<EBOOKCOLLECTING> EBOOKCOLLECTINGs { get; set; }
        public virtual DbSet<EBOOK_COLL> EBOOK_COLL { get; set; }
        public virtual DbSet<EBOOK_HIS> EBOOK_HIS { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.BOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.PUBLISHER)
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.PAGENUM)
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.PRICE)
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.PUBLISHTIME)
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.DETAIL)
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.RATE)
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.URL)
                .IsUnicode(false);

            modelBuilder.Entity<BOOKCOMMERCIAL>()
                .Property(e => e.WRITER)
                .IsUnicode(false);

            modelBuilder.Entity<EBOOK_COLL>()
               .Property(e => e.EBOOKTYPE)
               .IsUnicode(false);

            modelBuilder.Entity<EBOOK_COLL>()
                .Property(e => e.EBOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<EBOOK_COLL>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EBOOK_HIS>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EBOOK_HIS>()
                .Property(e => e.EBOOKTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<EBOOK_HIS>()
                .Property(e => e.EBOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<BOOK_HOT>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOK_HOT>()
                .Property(e => e.BOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<BOOK_HOT>()
                .Property(e => e.RENTNUM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RECOM_BOOK_INFO>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RECOM_BOOK_INFO>()
                .Property(e => e.RECOM_NUM)
                .HasPrecision(38, 0);

            modelBuilder.Entity<RECOM_BOOK_INFO>()
                .Property(e => e.BOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<INTERNALBOOKS>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOK_REG>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOK_REG>()
                .Property(e => e.BOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<ADMINISTRATOR>()
                .Property(e => e.STAFFID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ADMINISTRATOR>()
                .Property(e => e.PASSWORDS)
                .IsUnicode(false);

            modelBuilder.Entity<ADMINISTRATOR>()
                .Property(e => e.STAFFNAME)
                .IsUnicode(false);

            modelBuilder.Entity<ADMINISTRATOR>()
                .Property(e => e.STAFFPOSITION)
                .IsUnicode(false);

            modelBuilder.Entity<ADMINISTRATOR>()
                .HasMany(e => e.BOOKMAINTAININGs)
                .WithRequired(e => e.ADMINISTRATOR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ADMINISTRATOR>()
                .HasMany(e => e.MAGAZINEMAINTAININGs)
                .WithRequired(e => e.ADMINISTRATOR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ADMINISTRATOR>()
                .HasMany(e => e.MAGAZINEARCHIVINGs)
                .WithRequired(e => e.ADMINISTRATOR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BOOKBILLING>()
                .Property(e => e.BOOKID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKBILLING>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKBILLING>()
                .Property(e => e.BILLNUMBER)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKMAINTAINING>()
                .Property(e => e.BOOKID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKMAINTAINING>()
                .Property(e => e.STAFFID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKRENTING>()
                .Property(e => e.BOOKID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKRENTING>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKRENTING>()
                .HasMany(e => e.BOOKBILLINGs)
                .WithRequired(e => e.BOOKRENTING)
                .HasForeignKey(e => new { e.BOOKID, e.MEMBERID, e.RENTTIME })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BOOKRERSERVATION>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKRERSERVATION>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOK>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOK>()
                .Property(e => e.BOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<BOOK>()
                .Property(e => e.MAJOR)
                .IsUnicode(false);

            modelBuilder.Entity<BOOK>()
                .HasMany(e => e.BOOKRERSERVATIONs)
                .WithRequired(e => e.BOOK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BOOK>()
                .HasMany(e => e.MEMBERS)
                .WithMany(e => e.BOOKS)
                .Map(m => m.ToTable("BOOKCOLLECTING", "DBTA").MapLeftKey("ISBN").MapRightKey("MEMBERID"));

            modelBuilder.Entity<EBOOKCOLLECTING>()
                 .Property(e => e.MEMBERID)
                 .IsFixedLength()
                 .IsUnicode(false);

            modelBuilder.Entity<EBOOKHISTORY>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<EBOOK>()
                .Property(e => e.EBOOKTYPE)
                .IsUnicode(false);

            modelBuilder.Entity<EBOOK>()
                .Property(e => e.EBOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<EBOOK>()
                .HasMany(e => e.EBOOKCOLLECTINGs)
                .WithRequired(e => e.EBOOK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EBOOK>()
                .HasMany(e => e.EBOOKHISTORies)
                .WithRequired(e => e.EBOOK)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<INTERNALMAGAZINE>()
                .Property(e => e.BOOKID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<INTERNALMAGAZINE>()
                .Property(e => e.ISSN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<INTERNALMAGAZINE>()
                .Property(e => e.MAGAZINEID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<INTERNALMAGAZINE>()
                .HasMany(e => e.MAGAZINEMAINTAININGs)
                .WithRequired(e => e.INTERNALMAGAZINE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MAGAZINEARCHIVING>()
                .Property(e => e.ISSN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MAGAZINEARCHIVING>()
                .Property(e => e.MAGAZINEID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MAGAZINEARCHIVING>()
                .Property(e => e.STAFFID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MAGAZINEMAINTAINING>()
                .Property(e => e.BOOKID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MAGAZINEMAINTAINING>()
                .Property(e => e.STAFFID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MAGAZINE>()
                .Property(e => e.ISSN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MAGAZINE>()
                .Property(e => e.MAGAZINEID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MAGAZINE>()
                .Property(e => e.MAGAZINENAME)
                .IsUnicode(false);

            modelBuilder.Entity<MAGAZINE>()
                .HasMany(e => e.INTERNALMAGAZINES)
                .WithOptional(e => e.MAGAZINE)
                .HasForeignKey(e => new { e.ISSN, e.MAGAZINEID });

            modelBuilder.Entity<MAGAZINE>()
                .HasMany(e => e.MAGAZINEARCHIVINGs)
                .WithRequired(e => e.MAGAZINE)
                .HasForeignKey(e => new { e.ISSN, e.MAGAZINEID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MEMBER>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MEMBER>()
                .Property(e => e.PASSWORDS)
                .IsUnicode(false);

            modelBuilder.Entity<MEMBER>()
                .Property(e => e.MEMBERNAME)
                .IsUnicode(false);

            modelBuilder.Entity<MEMBER>()
                .HasMany(e => e.BOOKRENTINGs)
                .WithRequired(e => e.MEMBER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MEMBER>()
                .HasMany(e => e.BOOKRERSERVATIONs)
                .WithRequired(e => e.MEMBER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MEMBER>()
                .HasMany(e => e.EBOOKHISTORies)
                .WithRequired(e => e.MEMBER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MEMBER>()
                .HasMany(e => e.ROOMRESERVATIONs)
                .WithRequired(e => e.MEMBER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MEMBER>()
                .HasMany(e => e.ROOMOCCUPATIONs)
                .WithRequired(e => e.MEMBER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RECOMMENDEDBOOKS>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RECOMMENDEDBOOKS>()
                .Property(e => e.BOOKNAME)
                .IsUnicode(false);

            modelBuilder.Entity<ROOMOCCUPATION>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ROOMOCCUPATION>()
                .Property(e => e.ROOMID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ROOMRESERVATION>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ROOMRESERVATION>()
                .Property(e => e.ROOMID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SEAT>()
                .Property(e => e.SEATID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SEAT>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<STUDYROOM>()
                .Property(e => e.ROOMID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<STUDYROOM>()
                .HasMany(e => e.ROOMOCCUPATIONs)
                .WithRequired(e => e.STUDYROOM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<STUDYROOM>()
                .HasMany(e => e.ROOMRESERVATIONs)
                .WithRequired(e => e.STUDYROOM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<BOOKRECOMMENDING>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BOOKRECOMMENDING>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RECOM_BOOK_USER>()
                .Property(e => e.ISBN)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RECOM_BOOK_USER>()
                .Property(e => e.MEMBERID)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<RECOM_BOOK_USER>()
                .Property(e => e.BOOKNAME)
                .IsUnicode(false);
        }

        public System.Data.Entity.DbSet<WebApplication3.Models.BOOK_INFO> BOOK_INFO { get; set; }


    }
}
