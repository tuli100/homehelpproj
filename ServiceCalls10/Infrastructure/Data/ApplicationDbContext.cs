using HomeHelpCallsWebSite.Infrastructure.Data.Models.Mapping;
using MaaganMichael.Core.Extensions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Security;

namespace HomeHelpCallsWebSite.Infrastructure.Data
{
    public class ApplicationDbContext : System.Data.Entity.DbContext
    {
        private Lazy<DbProviderFactory> _dbProviderFactory;

        public virtual DbSet<MM_HH_LGSTC_FRCST> MM_HH_LGSTC_FRCST { get; set; }
        public virtual DbSet<MM_HH_USERS> MM_HH_USERS { get; set; }
        public virtual DbSet<VUMM_HH_HNDL_CALLS> VUMM_HH_HNDL_CALLS { get; set; }
        public virtual DbSet<VUMM_HH_OPEN_CALLS> VUMM_HH_OPEN_CALLS { get; set; }
        public virtual DbSet<VUMM_HH_PARTS> VUMM_HH_PARTS { get; set; }
        public virtual DbSet<VUMM_HH_CALLS_LINES> VUMM_HH_CALLS_LINES { get; set; }
        public virtual DbSet<VUMM_HH_WORK_PARTS> VUMM_HH_WORK_PARTS{ get; set; }
        public virtual DbSet<VUMM_HH_STRMS_USERS> VUMM_HH_STRMS_USERS { get; set; }
        public virtual DbSet<VUMM_HH_STATUS_LIST> VUMM_HH_STATUS_LIST { get; set; }
        public virtual DbSet<VUMM_HH_IMAGES_LINKS> VUMM_HH_IMAGES_LINKS { get; set; }
        public virtual DbSet<MM_DOCS_FILES> MM_DOCS_FILE { get; set; }

        public DbProviderFactory DbProviderFactory { get { return _dbProviderFactory.Value; } }

        public ApplicationDbContext()
            : base("OracleDbContext")
        {
            _dbProviderFactory = new Lazy<DbProviderFactory>(() => this.Database.Connection.GetPropertyValue<DbProviderFactory>("DbProviderFactory", ReflectionExtesions.NON_PUBLIC));
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("DBTRANS");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<MM_HH_LGSTC_FRCST>().HasKey(c => c.DOC_NBR);
            modelBuilder.Entity<VUMM_HH_HNDL_CALLS>().HasKey(c => c.DOC_NBR);
            modelBuilder.Entity<VUMM_HH_OPEN_CALLS>().HasKey(c => c.DOC_NBR);
            modelBuilder.Entity<VUMM_HH_PARTS>().HasKey(c => c.PART_CODE);
            modelBuilder.Entity<VUMM_HH_WORK_PARTS>().HasKey(c => c.WORK_PART_ID);
            modelBuilder.Entity<VUMM_HH_CALLS_LINES>().HasKey(c => c.LINE_ID);
            modelBuilder.Entity<MM_HH_USERS>().HasKey(c => c.USER_NAME);
            modelBuilder.Entity<VUMM_HH_STRMS_USERS>().HasKey(c => c.STRM_CODE);
            modelBuilder.Entity<VUMM_HH_STATUS_LIST>().HasKey(c => c.STEP_CODE);
            modelBuilder.Entity<VUMM_HH_IMAGES_LINKS>().HasKey(c => c.DOC_NBR);
            modelBuilder.Entity<MM_DOCS_FILES>().HasKey(c => c.DOC_NBR);

            
            typeof(DbContext).Assembly.GetTypes().Where(t => typeof(IMapping).IsAssignableFrom(t)).ForEach(t =>
            {
                var mapping = Activator.CreateInstance(t) as IMapping;
                mapping.BuildMapping(modelBuilder);
            });
        }

        public IEnumerable<T> SqlQuery<T>(string sqlQuery, params object[] parameters)
        {
            return this.Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public Task<IEnumerable<T>> SqlQueryAsync<T>(string sqlQuery, params object[] parameters)
        {
            return Task.Run(() => this.SqlQuery<T>(sqlQuery, parameters));
        }

        public T ExecuteFunction<T>(string functionName, params object[] parameters)
        {
            if (!parameters.All(p => p is DbParameter))
            {
                parameters = parameters.Select((p, i) => new OracleParameter(string.Format(":{0}", i), p)).ToArray();
            }
            var results = this.SqlQuery<T>(string.Format("select {0} ({1}) from dual", functionName, GetParametersFormat(parameters.Length)), parameters);
            return results.First();
        }

        public Task<T> ExecuteFunctionAsync<T>(string functionName, params object[] parameters)
        {
            return Task.Run(() => this.ExecuteFunction<T>(functionName, parameters));
        }

        public void ExecuteStoreProcedure(string procedureName, params object[] parameters)
        {
            if (!parameters.All(p => p is DbParameter))
            {
                parameters = parameters.Select((p, i) => new OracleParameter(string.Format(":{0}", i), p)).ToArray();
            }
            this.Database.ExecuteSqlCommand(string.Format("begin\n{0} ({1});\nend;", procedureName, GetParametersFormat(parameters.Length)), parameters);
        }

        public Task ExecuteStoreProcedureAsync(string procedureName, params object[] parameters)
        {
            return Task.Run(() => this.ExecuteStoreProcedure(procedureName, parameters));
        }

        private string GetParametersFormat(int parameterCount)
        {
            var parametersBuilder = new StringBuilder();
            for (int i = 0; i < parameterCount; i++)
            {
                parametersBuilder.AppendFormat(":{0},", i);
            }
            return parametersBuilder.ToString().TrimEnd(',');
        }

        public override int SaveChanges()
        {
            //OnSavingChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            //OnSavingChanges();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            //OnSavingChanges();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void OnSavingChanges()
        {
            //this.ChangeTracker.DetectChanges();
            //this.ChangeTracker.Entries<LineViewModel>().ForEach(t =>
            //{
            //    if ("forms".Equals(Thread.CurrentPrincipal.Identity.AuthenticationType, StringComparison.OrdinalIgnoreCase))
            //    {
            //        //var user = Membership.Provider.GetUser(Thread.CurrentPrincipal.Identity.Name, true);
            //        if (t.CurrentValues["Status"] != t.OriginalValues["Status"])
            //        {
            //            this.ExecuteStoreProcedure("MM_HH.mm_hh_insert_lft", t.Entity.TransactionLineId, (int)t.Entity.Status, user.ProviderUserKey);
            //        }
            //    }
            //});
        }
    }
}