﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomeHelpCallsWebSite
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MaaleDBEntities : DbContext
    {
        public MaaleDBEntities()
            : base("name=MaaleDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MM_HH_LGSTC_FRCST> MM_HH_LGSTC_FRCST { get; set; }
        public virtual DbSet<MM_HH_USERS> MM_HH_USERS { get; set; }
        public virtual DbSet<VUMM_HH_HNDL_CALLS> VUMM_HH_HNDL_CALLS { get; set; }
        public virtual DbSet<VUMM_HH_OPEN_CALLS> VUMM_HH_OPEN_CALLS { get; set; }
        public virtual DbSet<VUMM_HH_PARTS> VUMM_HH_PARTS { get; set; }
        public virtual DbSet<VUMM_HH_CALLS_LINES> VUMM_HH_CALLS_LINES { get; set; }
        public virtual DbSet<VUMM_HH_WORK_PARTS> VUMM_HH_WORK_PARTS { get; set; }
        public virtual DbSet<VUMM_HH_STRMS_USERS> VUMM_HH_STRMS_USERS { get; set; }
        public virtual DbSet<VUMM_HH_STATUS_LIST> VUMM_HH_STATUS_LIST { get; set; }
    }
}
