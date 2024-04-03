using Microsoft.EntityFrameworkCore;
using RealWorldApp.Core.Tags;
using RealWorldApp.Infrastructure.DAL;

namespace RealWorldApp.Infrastructure.DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _tagDbContext;

        public TagRepository(DataContext tagDbContext)
        {

            _tagDbContext = tagDbContext;
        }

        public async Task AddTag(Tag tag)
        {
            await _tagDbContext.Tags.AddAsync(tag);
            await _tagDbContext.SaveChangesAsync();
        }

        public Task<List<Tag>> GetAllTagsDescByNames(int start = 1000, int skip = 0)
        {
            return _tagDbContext.Tags.OrderByDescending(x => x.Name).Skip(skip).Take(start).ToListAsync();
        }

        public Task<List<Tag>> GetAllTagsAscByNames(int start = 1000, int skip = 0)
        {
            return _tagDbContext.Tags.OrderBy(x => x.Name).Skip(skip).Take(start).ToListAsync();
        }

        public Task<List<Tag>> GetAllTagsDescByCount(int start = 1000, int skip = 0)
        {
            return _tagDbContext.Tags.OrderByDescending(x => x.Count).Skip(skip).Take(start).ToListAsync();
        }

        public Task<List<Tag>> GetAllTagsAscByCount(int start = 1000, int skip = 0)
        {
            return _tagDbContext.Tags.OrderBy(x => x.Count).Skip(skip).Take(start).ToListAsync();
        }

        public int GetPopulation()
        {
            return  _tagDbContext.Tags.Where(x => x.Count > 0).Sum(x => x.Count);
        }

        public async Task<Tag> GetTagByName(string name) 
            => await _tagDbContext.Tags.FirstOrDefaultAsync(x => x.Name == name);

        public async Task UpdateTag(Tag tag)
        {
            _tagDbContext.Update(tag);
            await _tagDbContext.SaveChangesAsync();
        }


        //public async Task<User> GetByIdAsync(UserId userId) => await _tagDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
    }
}
