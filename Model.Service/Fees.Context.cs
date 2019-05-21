﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class OplatyEntities1 : DbContext
    {
        public OplatyEntities1()
            : base("name=OplatyEntities1")
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
        public virtual DbSet<Uslugi> Uslugi { get; set; }
        public virtual DbSet<FakturaSzczegoly> FakturaSzczegoly { get; set; }
        public virtual DbSet<Faktury_z_podziałem_na_Kategorie> Faktury_z_podziałem_na_Kategorie { get; set; }
        public virtual DbSet<Firmy_z_iloscia_faktur> Firmy_z_iloscia_faktur { get; set; }
        public virtual DbSet<IloscUslugFaktur> IloscUslugFaktur { get; set; }
        public virtual DbSet<Klienci_z_fakturami_podział_kateogrii> Klienci_z_fakturami_podział_kateogrii { get; set; }
        public virtual DbSet<Klienci_Z_Maks_Kwota_DO_Zaplaty> Klienci_Z_Maks_Kwota_DO_Zaplaty { get; set; }
        public virtual DbSet<Nie_Oplac_Fak> Nie_Oplac_Fak { get; set; }
        public virtual DbSet<Opłacone_Faktury> Opłacone_Faktury { get; set; }
        public virtual DbSet<Ranking_WG_Wartosci_Fak> Ranking_WG_Wartosci_Fak { get; set; }
    
        public virtual ObjectResult<Klienci_oplacone_terminowo_faktury_Result> Klienci_oplacone_terminowo_faktury(Nullable<System.DateTime> rOK)
        {
            var rOKParameter = rOK.HasValue ?
                new ObjectParameter("ROK", rOK) :
                new ObjectParameter("ROK", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Klienci_oplacone_terminowo_faktury_Result>("Klienci_oplacone_terminowo_faktury", rOKParameter);
        }
    
        public virtual ObjectResult<Klienci_z_wykazem_faktur_Result> Klienci_z_wykazem_faktur(Nullable<System.DateTime> rOK)
        {
            var rOKParameter = rOK.HasValue ?
                new ObjectParameter("ROK", rOK) :
                new ObjectParameter("ROK", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Klienci_z_wykazem_faktur_Result>("Klienci_z_wykazem_faktur", rOKParameter);
        }
    
        public virtual ObjectResult<FakturyWystawione_Result> FakturyWystawione(Nullable<System.DateTime> rOK)
        {
            var rOKParameter = rOK.HasValue ?
                new ObjectParameter("ROK", rOK) :
                new ObjectParameter("ROK", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FakturyWystawione_Result>("FakturyWystawione", rOKParameter);
        }
    
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
