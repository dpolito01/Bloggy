using Bloggy.Web.Models.Domain;

namespace Bloggy.Web.Models.ViewModel
{
    public class EditTagRequest
    {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string DisplayName { get; set; }
    }
}
