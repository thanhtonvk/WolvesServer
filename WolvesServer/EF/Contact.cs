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

    public partial class Contact
    {

        public int Id { get; set; }
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        [DisplayName("Email")]
        public string Gmail { get; set; }
        [DisplayName("SĐT")]
        public string PhoneNumber { get; set; }
  
        public string STK { get; set; }
        [DisplayName("Tên chủ tài khoản")]
        public string NameBank { get; set; }
        [DisplayName("Ngân hàng")]
        public string Bank { get; set; }

        public string Website { get; set; }
   
        public string Telegram { get; set; }
    }
}
