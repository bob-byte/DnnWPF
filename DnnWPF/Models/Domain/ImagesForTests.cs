using System;

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