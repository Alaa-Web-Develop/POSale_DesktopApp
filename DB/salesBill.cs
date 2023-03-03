//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POS_Demo.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class salesBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public salesBill()
        {
            this.salesBillDetails = new HashSet<salesBillDetails>();
        }
    
        public int id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string notes { get; set; }
        public Nullable<int> cust_id { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<decimal> totalafterdiscount { get; set; }
        public Nullable<int> userId { get; set; }
    
        public virtual Customers Customers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<salesBillDetails> salesBillDetails { get; set; }
        public virtual Users Users { get; set; }
    }
}
