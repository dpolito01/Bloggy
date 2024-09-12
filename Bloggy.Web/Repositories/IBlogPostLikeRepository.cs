using Bloggy.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bloggy.Web.Repositories
{
    public interface IBlogPostLikeRepository
    {
        Task<int> GetTotalLikes(Guid blogPostId);
        Task<BlogPostLike> AddLikeForBlog (BlogPostLike blogPostLike);
        Task<IEnumerable<BlogPostLike>> GetLikesForBlogForUser(Guid blogPostId);
    }
}
