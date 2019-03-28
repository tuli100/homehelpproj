using System.Data.Entity;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models.Mapping
{
    public class CallMap : IMapping
    {
        public void BuildMapping(DbModelBuilder modelBuilder)
        {
            var configuration = modelBuilder.Entity<CallModel>();
            configuration.HasEntitySetName("VUMM_HH_OPEN_CALLS");
            configuration.ToTable("VUMM_HH_OPEN_CALLS", "DBTRANS");
            configuration.HasKey(m => new { m.doc_nbr });

            #region Properties
            configuration.Property(m => m.doc_nbr)
                .HasColumnName("DOC_NBR")
                .IsRequired();

            configuration.Property(m => m.line_evnt_date)
                .HasColumnName("LINE_EVNT_DATE")
                .IsRequired();

            configuration.Property(m => m.rqstd_ship_date)
                .HasColumnName("RQSTD_SHIP_DATE")
                .IsRequired();

            configuration.Property(m => m.strm_code)
                .HasColumnName("STRM_CODE")
                .IsRequired();

            configuration.Property(m => m.dst_code)
                .HasColumnName("DST_CODE")
                .IsRequired();

            configuration.Property(m => m.dscr)
                .HasColumnName("CALL_DSCR")
                .IsRequired();

            configuration.Property(m => m.apt)
                .HasColumnName("APT_CODE")
                .IsRequired();

            configuration.Property(m => m.stat)
                .HasColumnName("CALL_STAT_FULL")
                .IsRequired();

            configuration.Property(m => m.has_images)
               .HasColumnName("HAS_IMAGES")
               .IsRequired();
            
            configuration.Ignore(m => m.Id);
            #endregion Properties


            //#region Foreign Keys
            //configuration.HasMany(m => m.CallLines)
            //   .HasForeignKey(t => t.doc_nbr);
            //#endregion Foreign Keys
        }
    }
}