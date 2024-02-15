using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.Repositories.Base;

namespace AirbnbDiploma.DAL.Repositories.TagRepository;

public class TagRepository : RepositoryBase<Tag, int>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context)
    {
    }
}
