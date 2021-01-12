using System;

namespace DnnWPF.Models.Domain
{
    public class TestedImages
    {
        public Int32 Id { get; set; }
        public virtual ImagesForTests ImageForTest { get; set; }
        public String PathToTestedImage { get; set; }
        public Byte ValidSignId { get; set; }
        public virtual TypesRoadSigns TypeValidSign { get; set; }
        public Byte PredictedSignId { get; set; }
        public virtual TypesRoadSigns TypePredictedSign { get; set; }
        public Byte PrecisionRecognising { get; set; }
    }
}