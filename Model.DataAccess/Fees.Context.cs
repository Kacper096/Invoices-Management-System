﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OplatyEntities : DbContext
    {
        public OplatyEntities()
            : base("name=OplatyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Faktury> Faktury { get; set; }
        public virtual DbSet<Firmy> Firmy { get; set; }
        public virtual DbSet<Kategorie> Kategorie { get; set; }
        public virtual DbSet<Klienci> Klienci { get; set; }
        public virtual DbSet<Liczebniki> Liczebniki { get; set; }
        public virtual DbSet<Uslugi> Uslugi { get; set; }
        public virtual DbSet<FakturaSzczegoly> FakturaSzczegoly { get; set; }
    
        public virtual int ZaplacFakture(Nullable<decimal> kwota, Nullable<int> nRfaktury)
        {
            var kwotaParameter = kwota.HasValue ?
                new ObjectParameter("Kwota", kwota) :
                new ObjectParameter("Kwota", typeof(decimal));
    
            var nRfakturyParameter = nRfaktury.HasValue ?
                new ObjectParameter("NRfaktury", nRfaktury) :
                new ObjectParameter("NRfaktury", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("ZaplacFakture", kwotaParameter, nRfakturyParameter);
        }
    }
}
