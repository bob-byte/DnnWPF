using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnnWPF.Models.Domain
{
    public class ImagesForTests
    {
        public Int32 Id { get; set; }
        public virtual TestedImages TestedImage { get; set; }
        public Byte ValidId { get; set; }
        public virtual TypesRoadSigns TypeValidSign { get; set; }
        public String PathToImage { get; set; }
    }
}