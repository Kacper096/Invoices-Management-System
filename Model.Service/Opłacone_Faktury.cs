//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Service
{
    using System;
    using System.Collections.Generic;
    
    public partial class Opłacone_Faktury
    {
        public int FakturaID { get; set; }
        public Nullable<int> KlientID { get; set; }
        public int FirmaID { get; set; }
        public int KategoriaID { get; set; }
        public string Numer { get; set; }
        public System.DateTime DataWystawienia { get; set; }
        public string RodzajPlatnosci { get; set; }
        public Nullable<System.DateTime> DataZaplaty { get; set; }
        public System.DateTime TerminPlatnosci { get; set; }
        public string Waluta { get; set; }
        public decimal DoZaplaty { get; set; }
        public string DoZaplatySlownie { get; set; }
        public Nullable<decimal> Zaplacono { get; set; }
        public Nullable<decimal> PozostaloDoZaplaty { get; set; }
        public string Status { get; set; }
        public string Uwagi { get; set; }
    }
}
