using System;
using System.Collections.ObjectModel;

namespace DnnWPF.Models.Domain
{
    public class TypesRoadSigns
    {
        public TypesRoadSigns()
        {
            ValidSignsOfImagesForTest = new ObservableCollection<ImagesForTests>();
            TestedImages_PredictedRoadSigns = new ObservableCollection<TestedImages>();
            TestedImages_ValidRoadSigns = new ObservableCollection<TestedImages>();
        }

        public Byte ClassId { get; set; }
        public virtual ObservableCollection<ImagesForTests> ValidSignsOfImagesForTest { get; set; }
        public virtual ObservableCollection<TestedImages> TestedImages_PredictedRoadSigns { get; set; }
        public virtual ObservableCollection<TestedImages> TestedImages_ValidRoadSigns { get; set; }
        public String Name { get; set; }
        public String PathToTemplateImage { get; set; }
        public Double? PrecisionRecognising { get; set; }
        public Int32 CountTest { get; set; }

        public void AddTestImage(ImagesForTests image) =>
            ValidSignsOfImagesForTest.Add(image);
    }
}