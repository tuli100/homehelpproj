using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeHelpCallsWebSite.Infrastructure.Data.Models.Mapping
{
   public interface IMapping
        {
            void BuildMapping(DbModelBuilder modelBuilder);
        }
}
