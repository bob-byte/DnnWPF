using DnnWPF.ViewModels;
using Emgu.CV;
using System;
using System.IO;

namespace DnnWPF.Models
{
    class Test<T, U, V>
        where T : struct, IColor
        where U : new()
        where V : struct
    {
        internal void Testing(RecognisingTypeOfRoadSign<T, U> recognising, Object model, 
            String pathToDirectoryWithImagesForTests, SearchOption searchOption, String searchPattern = null)
        
        {
            FileInfo[] images;
            DirectoryInfo directory = new DirectoryInfo(pathToDirectoryWithImagesForTests);

            if (searchPattern != null)
            {
                images = directory.GetFiles(searchPattern, searchOption);
            }
            else
            {
                images = directory.GetFiles();
            }

            if(images.Length > 200)
            {
                throw new Exception("Images can\'t be than 200 for right test");
            }

            using(Query query = new Query())
            {
                foreach (var image in images)
                {
                    try
                    {
                        String pathToImage = image.Name;
                        Image<T, U> imageEmgu = new Image<T, U>(image.FullName);

                        Byte validId = query.GetValidId(pathToImage);
                        Byte predictedId = (Byte)recognising.GetPredictedIdOfRoadSign<V>(model, imageEmgu);

                        query.AddImage(pathToImage, validId, predictedId, true);
                        query.UpdateTypesRoadSigns(validId);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
