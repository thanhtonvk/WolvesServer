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

    public partial class TongQuat
    {
        public int Id { get; set; }
        [DisplayName("Tổng pip")]
        public Nullable<int> TongPip { get; set; }
        public Nullable<int> Trades { get; set; }
        public Nullable<double> WinRate { get; set; }
    }
}
