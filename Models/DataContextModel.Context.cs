﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoffeePricingMgt.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCategory> tblCategories { get; set; }
        public virtual DbSet<tblProduct> tblProducts { get; set; }
        public virtual DbSet<tblShippingPeriod> tblShippingPeriods { get; set; }
        public virtual DbSet<tblUser> tblUsers { get; set; }
        public virtual DbSet<tblProductPricing> tblProductPricings { get; set; }
        public virtual DbSet<tblCrop> tblCrops { get; set; }
        public virtual DbSet<tblTerm> tblTerms { get; set; }
    
        public virtual ObjectResult<pFOBOfferList_Result> pFOBOfferList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<pFOBOfferList_Result>("pFOBOfferList");
        }
    }
}
