using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Data.EF//.Metadata
{
    class StatusMetadata
    {
        [Display(Name ="Status")]
        public string StatusName { get; set; }
    }
    [MetadataType(typeof(StatusMetadata))]
    public partial class Status { }
}
