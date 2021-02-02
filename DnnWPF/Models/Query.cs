using DnnWPF.Models.Domain;
using Emgu.CV;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace DnnWPF.Models
{
    class Query : IDisposable
    {
        private RoadSignsDbContext context = new RoadSignsDbContext();

        private Boolean disposedValue = false; // Для определения избыточных вызовов

        internal TestedImages AddImage(String nameImage, Byte validId, Byte predictedId, Boolean whetherUpdateDataOfImage)
        {
            Boolean hasDuplicate = context.TestedImages.Any(c => c.PathToTestedImage.Contains(nameImage));

            if (hasDuplicate && !whetherUpdateDataOfImage)
            {
                throw new DuplicateWaitObjectException("Image in table has such value in column \"PathToTestedImage\"");
            }

            TestedImages image;

            if (hasDuplicate && whetherUpdateDataOfImage)
            {
                var imageForTest = context.ImagesForTests.SingleOrDefault(c => c.PathToImage.Contains(nameImage));

                if (imageForTest == null)
                {
                    throw new Exception("This image doesn\'t exist in database");
                }

                var validRoadSign = context.TypesRoadSigns.Single(c => c.ClassId == validId);
                validRoadSign.CountTest--;

                image = context.TestedImages.Single(c => c.Id == imageForTest.Id);
                image.TypeValidSign = validRoadSign;
                image.TypePredictedSign = context.TypesRoadSigns.Single(c => c.ClassId == predictedId);
                image.PrecisionRecognising = GetPrecisionOfImage(validId, predictedId);
            }
            else
            {
                if (context.ImagesForTests.Where(c => c.PathToImage.Contains(nameImage)).Count() <= 1)
                {
                    Byte precision = GetPrecisionOfImage(validId, predictedId);

                    var imageForTest = context.ImagesForTests.SingleOrDefault(c => c.PathToImage.Contains(nameImage));
                    if (imageForTest == null)
                    {
                        imageForTest = context.ImagesForTests.Add(new ImagesForTests
                        {
                            PathToImage = nameImage,
                            TypeValidSign = context.TypesRoadSigns.Single(c => c.ClassId == validId),
                            ValidId = validId
                        });
                    }

                    image = new TestedImages
                    {
                        ImageForTest = imageForTest,
                        PathToTestedImage = nameImage,
                        TypeValidSign = context.TypesRoadSigns.Single(c => c.ClassId == validId),
                        PrecisionRecognising = precision,
                        TypePredictedSign = context.TypesRoadSigns.Single(c => c.ClassId == predictedId)
                    };

                    context.TestedImages.Add(image);
                }
                else
                {
                    throw new DuplicateWaitObjectException("Bad name of file. More than 1 record have such name");
                }
            }

            context.SaveChanges();
            return image;
        }

        internal String GetNameOfPredictedRoadSign(Byte id)
        {
            var roadSign = context.TypesRoadSigns.Single(c => c.ClassId == id);
            return roadSign.Name;
        }

        internal ObservableCollection<TestedImages> GetTestedImagesByValidId(Byte validId)
        {
            context.TestedImages.Where(c => c.ValidSignId == validId).Load();
            return context.TestedImages.Local;
        }

        internal ObservableCollection<TypesRoadSigns> GetAllTypesRoadSigns()
        {
            context.TypesRoadSigns.Load();
            return context.TypesRoadSigns.Local;
        }

        //Add possibility to add new record to table ImagesForTests
        internal String GetNameOfValidRoadSign(String nameImage)
        {
            if(context.ImagesForTests.Where(c => c.PathToImage.Contains(nameImage)).Count() <= 1)
            {
                var roadSign = context.ImagesForTests.SingleOrDefault(c => c.PathToImage.Contains(nameImage));
                if(roadSign == null)
                {
                    throw new Exception("This image doesn\'t exist in database");
                }

                return roadSign.TypeValidSign.Name;
            }
            else
            {
                throw new DuplicateWaitObjectException("Bad name of file. More than 1 record have such name");
            }
        }

        internal Byte GetValidId(String nameImage)
        {
            if (context.ImagesForTests.Where(c => c.PathToImage.Contains(nameImage)).Count() <= 1)
            {
                var roadSign = context.ImagesForTests.SingleOrDefault(c => c.PathToImage.Contains(nameImage));
                if (roadSign == null)
                {
                    throw new Exception("Can\'t get valid id of this image");
                }

                return roadSign.ValidId;
            }
            else
            {
                throw new DuplicateWaitObjectException("Bad name of file. More than 1 record have such name");
            }
        }

        internal void UpdateTypesRoadSigns(Byte validId)
        {
            var typeRoadSign = context.TypesRoadSigns.Single(c => c.ClassId == validId);

            typeRoadSign.CountTest++;
            typeRoadSign.PrecisionRecognising = GetPrecisionOfTypeRoadSign(typeRoadSign);

            context.SaveChanges();
        }

        private Double GetPrecisionOfTypeRoadSign(TypesRoadSigns roadSign)
        {
            Double precision = 0;

            if(roadSign.TestedImages_ValidRoadSigns.Count == roadSign.CountTest)
            {
                for (Int32 i = 0; i < roadSign.TestedImages_ValidRoadSigns.Count; i++)
                {
                    precision += roadSign.TestedImages_ValidRoadSigns[i].PrecisionRecognising;
                }
                precision /= roadSign.CountTest;

                return precision;
            }
            else
            {
                throw new Exception("Count of tests must be equal to TestedImages_ValidRoadSigns.Count");
            }
        }

        private Byte GetPrecisionOfImage(Byte validId, Byte predictedId)
        {
            Byte precision = (Byte)(validId == predictedId ? 1 : 0);
            return precision;
        }

        #region IDisposable Support

        protected virtual void Dispose(Boolean disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    GC.Collect();
                }

                context.Dispose();
                disposedValue = true;
            }
        }

        ~Query()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
