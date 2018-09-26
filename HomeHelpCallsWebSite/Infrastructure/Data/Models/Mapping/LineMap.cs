using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models.Mapping
{
    public class LineMap
    {
        public void BuildMapping(DbModelBuilder modelBuilder)
        {
            var configuration = modelBuilder.Entity<LineModel>();
            configuration.HasEntitySetName("VUMM_HH_CALLS_LINES");
            configuration.ToTable("VUMM_HH_CALLS_LINES", "DBTRANS");
            configuration.HasKey(m => new { m.doc_nbr, m.line_nbr });

            #region Properties
            configuration.Property(m => m.doc_nbr)
                .HasColumnName("DOC_NBR")
                .IsRequired();

            configuration.Property(m => m.line_nbr)
                .HasColumnName("LINE_NBR")
                .IsRequired();

            configuration.Property(m => m.part_code)
                .HasColumnName("PART_CODE")
                .IsRequired();

            configuration.Property(m => m.qnty)
                .HasColumnName("QNTY")
                .IsRequired();

            configuration.Property(m => m.rmrk)
                .HasColumnName("TXT_DSCR")
                .IsRequired();

            configuration.Ignore(m => m.Id);
            #endregion Properties
          
            
            #region Foreign Keys
            //configuration.HasOptional(m => m.Part)
             //   .HasForeignKey(t => t.part_code);
            #endregion Foreign Keys
        }


    }
}