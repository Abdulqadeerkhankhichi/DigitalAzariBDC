﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DigitalAzariBDC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DigitalAzariEntities : DbContext
    {
        public DigitalAzariEntities()
            : base("name=DigitalAzariEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tblAccessLevel> tblAccessLevels { get; set; }
        public DbSet<tblBrand> tblBrands { get; set; }
        public DbSet<tblDealership> tblDealerships { get; set; }
        public DbSet<tblDealershipUser> tblDealershipUsers { get; set; }
        public DbSet<tblMenu> tblMenus { get; set; }
        public DbSet<tblRole> tblRoles { get; set; }
        public DbSet<tblSetting> tblSettings { get; set; }
        public DbSet<tblUser> tblUsers { get; set; }
    }
}
