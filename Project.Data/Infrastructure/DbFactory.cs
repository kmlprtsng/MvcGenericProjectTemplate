using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        ProjectContext dbContext;

        public ProjectContext Init()
        {
            return dbContext ?? (dbContext = new ProjectContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
