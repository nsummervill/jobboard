using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Data.EF//.Metadata
{
    class OpenPositionMetadata
    {
        [Display(Name ="OpenPosition ID")]
        public int OpenPositionID { get; set; }
    }
    [MetadataType(typeof(OpenPositionMetadata))]
    public partial class OpenPosition { }
}
