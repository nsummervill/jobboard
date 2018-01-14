using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.EF//.Metadata
{
    class LocationMetadata
    {
        [Display(Name ="Store Number")]
        public string StoreNumber { get; set; }
    }
    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }
}
