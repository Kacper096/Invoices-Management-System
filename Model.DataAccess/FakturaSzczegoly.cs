//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class FakturaSzczegoly
    {
        public int FakturaID { get; set; }
        public int UslugaID { get; set; }
        public short Ilosc { get; set; }
        public decimal CenaJednostkowa { get; set; }
        public decimal WartoscNetto { get; set; }
        public string StawkaVAT { get; set; }
        public decimal WartoscVAT { get; set; }
        public decimal WartoscBrutto { get; set; }
    
        public virtual Faktury Faktury { get; set; }
        public virtual Uslugi Uslugi { get; set; }
    }
}
