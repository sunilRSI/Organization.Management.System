using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Repository.Repository.DbContext.Command
{
    public interface IDbContextCommandRepository
    {
        public Task Initilize();
    }
}
