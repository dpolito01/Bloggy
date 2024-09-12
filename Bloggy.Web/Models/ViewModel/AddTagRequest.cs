using System.ComponentModel;

namespace Bloggy.Web.Models.ViewModel
{
    public class AddTagRequest
    {
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
    }
}
