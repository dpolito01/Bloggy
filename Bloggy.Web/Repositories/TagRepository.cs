using Bloggy.Web.Data;
using Bloggy.Web.Models.Domain;
using Bloggy.Web.Models.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        //Injection of BD
        private readonly BloggieDbContext _bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }


        public async Task<Tag> AddAsync(Tag tag)
        {
            await _bloggieDbContext.Tags.AddAsync(tag);
            await _bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await _bloggieDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                _bloggieDbContext.Tags.Remove(existingTag);
                await _bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await _bloggieDbContext.Tags.ToListAsync();
            return tags;
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            return _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);   
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _bloggieDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;

        }
    }
}
