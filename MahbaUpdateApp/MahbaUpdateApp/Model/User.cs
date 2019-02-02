//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MahbaUpdateApp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.VersionClients = new HashSet<VersionClient>();
        }
    
        public int Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string NikName { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public string RoleCode { get; set; }
        public string StateCode { get; set; }
        public string Visible { get; set; }
        public bool isLogin { get; set; }
        public bool isGuest { get; set; }
        public Nullable<System.DateTime> Expire { get; set; }
        public string IPAddress { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VersionClient> VersionClients { get; set; }
    }
}
