using Project.Data.Infrastructure;
using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Model.Models;

namespace Project.Data.Repositories
{
    public class GadgetRepository : RepositoryBase<Gadget>, IGadgetRepository
    {
        public GadgetRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IGadgetRepository : IRepository<Gadget>
    {

    }
}
