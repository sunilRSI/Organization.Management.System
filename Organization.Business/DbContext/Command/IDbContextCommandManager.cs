using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization.Business.DbContext.Command
{
    public interface IDbContextCommandManager
    {
        public Task Initialize();
    }
}
