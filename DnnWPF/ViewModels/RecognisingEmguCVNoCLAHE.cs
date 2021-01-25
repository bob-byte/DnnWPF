using Emgu.CV;
using Emgu.CV.Dnn;
using System;

namespace DnnWPF.ViewModels
{
    class RecognisingEmguCVNoCLAHE<T, U> : RecognisingTypeOfRoadSign<T, U>
        where T : struct, IColor
        where U : new() 
    {
        public override Object LoadModel(String pathToModel) =>
            (Object)DnnInvoke.ReadNetFromONNX(pathToModel);

        public override Object GetNormalizedDataOfImage(Image<T, U> image)
        {
            MatND<Double> matND = new MatND<Double>(1, 32, 32, 3);
            Double[,,,] imageData = new Double[1, 32, 32, 3];

            for (Int32 i = 0; i <= imageData.GetUpperBound(1); i++)
            {
                for (Int32 j = 0; j <= imageData.GetUpperBound(2); j++)
                {
                    for (Int32 k = 0; k <= imageData.GetUpperBound(3); k++)
                    {
                        if (k == 0)
                        {
                            imageData[0, j, i, k] = image.Bitmap.GetPixel(i, j).R / Math.Pow(255, 2);
                        }
                        else if (k == 1)
                        {
                            imageData[0, j, i, k] = image.Bitmap.GetPixel(i, j).G / Math.Pow(255, 2);
                        }
                        else
                        {
                            imageData[0, j, i, k] = image.Bitmap.GetPixel(i, j).B / Math.Pow(255, 2);
                        }
                    }
                }
            }

            matND.ManagedArray = imageData;

            return matND;
        }

        public override Array ProcessModel<V>(Object modelObj, Object matObj)
        {
            Net model = modelObj as Net;
            MatND<V> mat = matObj as MatND<V>;

            if (model != null)
            {
                if (mat != null)
                {
                    model.SetInput(mat);
                    Array preeds = model.Forward().GetData();
                    
                    return preeds;
                }
                else
                {
                    throw new Exception("Can\'t convert matObj to Emgu.CV.Dnn.MatND");
                }
            }
            else
            {
                throw new Exception("Can\'t convert modelObj to Emgu.CV.Dnn.Net");
            }
        }
    }
}
