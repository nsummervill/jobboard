using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.EF//.Metadata
{
    class AspNetUserMetadata
    {
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="* Required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "* Required")]
        public string LastName { get; set; }
        [Display(Name = "Resume File")]
        [Required(ErrorMessage = "* Required")]
        public string ResumeFile { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="* Required. Must use uppercase, lowercase, number, and special character")]
        public string Email { get; set; }
        [Required(ErrorMessage = "* Required. Must use uppercase, lowercase, number, and special character")]
        public bool EmailConfirmed { get; set; }
    }
    [MetadataType(typeof(AspNetUserMetadata))]
    public partial class AspNetUser { }
}
