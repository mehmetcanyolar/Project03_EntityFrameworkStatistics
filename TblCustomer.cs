//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project03_EntityFrameworkStatistics
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblCustomer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblCustomer()
        {
            this.TblOrders = new HashSet<TblOrder>();
        }
    
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCountry { get; set; }
        public string CustomerCity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
