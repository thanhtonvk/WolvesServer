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

    public partial class ThongKe
    {
        public int Id { get; set; }
        [DisplayName("Đơn vị")]
        public string Money { get; set; }
        [DisplayName("Ngày")]
        public Nullable<System.DateTime> Date { get; set; }
        [DisplayName("Pip Cũ")]
        public Nullable<int> PipCu { get; set; }
        [DisplayName("Pip mới")]
        public Nullable<int> PipMoi { get; set; }

        public Nullable<double> SL { get; set; }
        public Nullable<double> TP { get; set; }
    }
}
