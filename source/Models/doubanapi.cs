using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doubanapi
{
        class bookinfo
        {
            private string name;
            private string author;
            private string imageurl;
            private string summary;
            private string isbn;
            private string pages;
            private string price;
            private string publisher;
            private string pubdate;

            public bookinfo()
            {
                this.name = string.Empty;
                this.author = string.Empty;
                this.imageurl = string.Empty;
                this.summary = string.Empty;
                this.isbn = string.Empty;
                this.pages = string.Empty;
                this.price = string.Empty;
                this.publisher = string.Empty;
                this.pubdate = string.Empty;
            }

            public string Name
            {
                get {
                    return this.name;
                }
                set {
                    this.name = value;
                }
            }

            public string Author
            {
                get
                {
                    return this.author;
                }
                set
                {
                    this.author = value;
                }
            }
    
            public string Imageurl
            {
                get
                {
                    return this.imageurl;
                }
                set
                {
                    this.imageurl = value;
                }
            }
    
            public string Summary
            {
                get
                {
                    return this.summary;
                }
                set
                {
                    this.summary = value;
                }
            }
    
            public string Isbn
            {
                get
                {
                    return this.isbn;
                }
                set
                {
                    this.isbn = value;
                }
            }
    
            public string Pages
            {
                get
                {
                    return this.pages;
                }
                set
                {
                    this.pages = value;
                }
            }
    
            public string Price
            {
                get
                {
                    return this.price;
               }
                set
                {
                    this.price = value;
                }
            }
    
            public string Publisher
            {
                get
                {
                    return this.publisher;
                }
                set
                {
                    this.publisher = value;
                }
            }
    
            public string Pubdate
            {
                get
                {
                    return this.pubdate;
                }
                set
                {
                    this.pubdate = value;
                }
            }
        }
    }
