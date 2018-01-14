using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Data.EF//.Metadata
{
    class PositionMetadata
    {
        [Display(Name ="Position ID")]
        public int PositionID { get; set; }
        [Display(Name ="Job Description")]
        public string JobDescription { get; set; }
    }
}
