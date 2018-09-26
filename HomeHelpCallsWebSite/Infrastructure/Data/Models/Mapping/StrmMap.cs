using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models.Mapping
{
    public class StrmMap
    {
        public void BuildMapping(DbModelBuilder modelBuilder)
        {
            var configuration = modelBuilder.Entity<StrmModel>();
            configuration.HasEntitySetName("VUMM_HH_STRMS");
            configuration.ToTable("VUMM_HH_STRMS", "DBTRANS");
            configuration.HasKey(m => new { m.code });

            #region Properties
            configuration.Property(m => m.code)
                .HasColumnName("STRM_CODE")
                .IsRequired();

            configuration.Property(m => m.name)
                .HasColumnName("STRM_NAME")
                .IsRequired();

            configuration.Property(m => m.parent_code)
                .HasColumnName("PARENT_STRM_CODE")
                .IsRequired();

            configuration.Property(m => m.parent_name)
                .HasColumnName("PARENT_STRM_NAME")
                .IsRequired();

            configuration.Ignore(m => m.Id);
            #endregion Properties
        }
    }
}