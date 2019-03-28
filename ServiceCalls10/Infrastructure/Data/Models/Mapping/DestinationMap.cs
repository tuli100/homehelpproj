using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models.Mapping
{
    public class DestinationMap
    {
        public void BuildMapping(DbModelBuilder modelBuilder)
        {
            var configuration = modelBuilder.Entity<DestinationModel>();
            configuration.HasEntitySetName("VUMM_HH_DSTNS");
            configuration.ToTable("VUMM_HH_DSTNS", "DBTRANS");
            configuration.HasKey(m => new { m.code });

            #region Properties
            configuration.Property(m => m.code)
                .HasColumnName("DSTN_CODE")
                .IsRequired();

            configuration.Property(m => m.name)
                .HasColumnName("DSTN_NAME")
                .IsRequired();

            configuration.Property(m => m.cell_phone)
                .HasColumnName("CELL")
                .IsRequired();

            configuration.Property(m => m.email)
                .HasColumnName("EMAIL")
                .IsRequired();

            configuration.Property(m => m.apt)
                .HasColumnName("DIRA")
                .IsRequired();

            configuration.Property(m => m.sub_area)
                .HasColumnName("SALE_SUB_AREA")
                .IsRequired();
            
            configuration.Ignore(m => m.Id);
            #endregion Properties
        }
    }
}