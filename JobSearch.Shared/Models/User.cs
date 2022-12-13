using JobSearch.Shared.Utility.Language;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace JobSearch.Shared.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(10, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E003")]
        public required string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [EmailAddress(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        public required string Email { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        [MinLength(6, ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E003")]
        public required string Password { get; set; }
    }
}
