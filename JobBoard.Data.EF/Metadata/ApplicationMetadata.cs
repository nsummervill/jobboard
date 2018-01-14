using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.EF//.Metadata
{
    class ApplicationMetadata
    {
        [Display(Name ="Manager Notes")]
        public string ManagerNotes { get; set; }
        [Display(Name = "Decline")]
        public bool IsDeclined { get; set; }
        [Display(Name = "Resume File")]
        public string ResumeFile { get; set; }
        [Display(Name = "Application ID")]
        public int ApplicationID { get; set; }
        [Display(Name = "User ID")]
        public string UserID { get; set; }
        [Display(Name ="Application Date")]
        public System.DateTime ApplicationDate { get; set; }
    }
    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application { }
}
