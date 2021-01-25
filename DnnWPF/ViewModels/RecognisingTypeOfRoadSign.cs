using Emgu.CV;
using Emgu.CV.Dnn;
using System;

namespace DnnWPF.ViewModels
{
    abstract class RecognisingTypeOfRoadSign<T, U> 
        where T : struct, IColor
        where U : new()
    {
        public abstract Object LoadModel(String pathToModel);

        public abstract Object GetNormalizedDataOfImage(Image<T, U> image);

        public abstract Array ProcessModel<V>(Object modelObj, Object matObj) where V : new();

        public virtual Image<T, U> ProcessImage(Image<T, U> image) =>
            image.Resize(32, 32, Emgu.CV.CvEnum.Inter.Linear, false);

        public virtual Int32 GetMax(Array array)
        {
            Single[,] singleArray = array as Single[,];

            if (singleArray != null)
            {
                Int32 i = 0;
                Single max = 0;

                for (Int32 j = 0; j <= singleArray.GetUpperBound(1); j++)
                {
                    if (singleArray[0, j] > max)
                    {
                        max = singleArray[0, j];
                        i = j;
                    }
                }

                return i;
            }
            else
            {
                throw new InvalidCastException("Can\'t convert array to Single[,]");
            }
        }

        public Int32 GetPredictedIdOfRoadSign<V>(Object modelObj, Image<T, U> image) where V : new()
        {
            Object model = modelObj as Net;

            if (model == null)
            {
                model = modelObj as SharpCV.Net;
            }
            if (model == null)
            {
                throw new Exception("Can\'t convert modelObj to Net");
            }

            try
            {
                var processedImage = ProcessImage(image);
                Object imageData = GetNormalizedDataOfImage(processedImage);
                var preeds = ProcessModel<V>(model, imageData);
                Int32 predictedId = GetMax(preeds);

                return predictedId;
            }
            catch
            {
                throw;
            }
        }

        internal Double GetValueOfPixel(System.Drawing.Color color) => 
            (color.R + color.G + color.B) / 3.0;
    }
}
