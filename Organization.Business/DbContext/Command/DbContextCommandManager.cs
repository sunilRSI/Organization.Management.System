using Organization.Repository.Repository.DbContext.Command; 

namespace Organization.Business.DbContext.Command
{
    public class DbContextCommandManager : IDbContextCommandManager
    {
        private readonly IDbContextCommandRepository _dbContextCommandRepository;

        public DbContextCommandManager(IDbContextCommandRepository dbContextCommandRepository)
        {
            _dbContextCommandRepository = dbContextCommandRepository;
        }
        public async Task Initialize()
        {
            await _dbContextCommandRepository.Initilize();
        }
    }
}
