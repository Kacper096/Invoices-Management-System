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
    
    public partial class Faktury
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faktury()
        {
            this.FakturaSzczegoly = new HashSet<FakturaSzczegoly>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FakturaSzczegoly> FakturaSzczegoly { get; set; }
        public virtual Firmy Firmy { get; set; }
        public virtual Kategorie Kategorie { get; set; }
        public virtual Klienci Klienci { get; set; }
    }
}
