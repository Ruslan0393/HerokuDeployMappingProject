using Mapping.Data.Contexts;
using Mapping.Data.IRepositories;
using Mapping.Domain.Entities;

namespace Mapping.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(MappingDbContext mappingDbContext) : base(mappingDbContext)
        {
        }
    }
}
