//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KetabGard.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Amanat
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Expiredate { get; set; }
        public decimal Userid { get; set; }
        public decimal Bookid { get; set; }
        public Nullable<int> Expireday { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<bool> Backed { get; set; }
    }
}
