using Emgu.CV;
using SharpCV;
using System;

namespace DnnWPF.ViewModels
{
    class RecognisingSharpCVNoCLAHE<T, U> : RecognisingTypeOfRoadSign<T, U>
        where T : struct, IColor
        where U : new()
    {
        public override Object LoadModel(String pathToModel)
        {
            Dnn dnn = new Dnn();
            Net model = dnn.ReadNetFromONNX(pathToModel);
            return model;
        }

        public override Object GetNormalizedDataOfImage(Image<T, U> processedImage)
        {
            var imageData = new Double[processedImage.Bitmap.Width, processedImage.Height];
            for (Int32 i = 0; i <= imageData.GetUpperBound(0); i++)
            {
                for (Int32 j = 0; j <= imageData.GetUpperBound(1); j++)
                {
                    var pixel = processedImage.Bitmap.GetPixel(i, j);
                    imageData[i, j] = GetValueOfPixel(pixel) / 255.0 / 255.0;
                }
            }
            var mat = new SharpCV.Mat(imageData);

            return mat;
        }

        public override Array ProcessModel<V>(Object modelObj, Object matObj)
        {
            Net model = modelObj as Net;
            if (model != null)
            {
                SharpCV.Mat mat = matObj as SharpCV.Mat;
                if (mat != null)
                {
                    try
                    {
                        model.setInput(mat);
                        var preeds = model.forward().GetData();

                        return (Array)preeds;
                    }
                    catch
                    {
                        throw;
                    }

                }
                else
                {
                    throw new Exception("Can\'t convert matObj to SharpCV.Mat");
                }
            }
            else
            {
                throw new Exception("Can\'t convert modelObj to SharpCV.Net");
            }
        }
    }
}
