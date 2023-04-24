﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WolvesServer.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            this.LoadWolves = new HashSet<LoadWolf>();
            this.Invites = new HashSet<Invite>();
            this.VIPs = new HashSet<VIP>();
        }
    
        public int Id { get; set; }
        [DisplayName("SĐT")]
        public string PhoneNumber { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Họ")]
        public string FirstName { get; set; }
        [DisplayName("Tên")]
        public string LastName { get; set; }
        [DisplayName("Ngày sinh")]
        public System.DateTime DateOfBirth { get; set; }
        [DisplayName("Avater")]
        public string Avatar { get; set; }
        [DisplayName("Wol")]
        public Nullable<int> Wolves { get; set; }
        public Nullable<int> Type { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LoadWolf> LoadWolves { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invite> Invites { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIP> VIPs { get; set; }
    }
}