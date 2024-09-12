using Bloggy.Web.Models.Domain;

namespace Bloggy.Web.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}
