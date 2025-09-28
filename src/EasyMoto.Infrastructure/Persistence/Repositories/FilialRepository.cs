using EasyMoto.Domain.Entities;
using EasyMoto.Domain.Repositories;

namespace EasyMoto.Infrastructure.Persistence.Repositories
{
    public class FilialRepository : RepositoryBase<Filial>, IFilialRepository
    {
        public FilialRepository(AppDbContext context) : base(context)
        {
        }
    }
}