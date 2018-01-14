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
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Resume File")]
        public string ResumeFile { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
    }
    [MetadataType(typeof(AspNetUserMetadata))]
    public partial class AspNetUser { }
}
