using Covid_Project.Persistence.Context;

namespace Covid_Project.Persistence.Repositories
{
    public class BaseRepository
    {
        private readonly AppDbContext _context;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}