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

        public virtual Image<T, U> ProcessImage(Image<T, U> image)
        {
            var processedImage = image.Resize(32, 32, Emgu.CV.CvEnum.Inter.Linear, false);
            return processedImage;
        }

        public abstract Object GetNormalizedDataOfImage(Image<T, U> image);

        public abstract Array ProcessModel<V>(Object modelObj, Object matObj) where V : new();

        //Int32 GetPredictedIdOfRoadSign<T>(String pathToModel, T image) where T : class;
        //public Int32 GetPredictedIdOfRoadSign<T, U>(, Image<T, U> image) 
        //    where T: struct, IColor 
        //    where U : new() 
        //{
        //    Emgu.CV.Dnn.Net file = ;
        //    //Image<Gray, Byte> grayImage = image.Convert<Gray, Byte>();

        //    image = image.Resize(32, 32, Emgu.CV.CvEnum.Inter.Linear, false);
        //    //CvInvoke.CLAHE(grayImage, 0.1, new System.Drawing.Size(32, 32), image);

        //    MatND<Double> matND = new MatND<Double>(1, 32, 32, 3);
        //    Double[,,,] imageData = new Double[1, 32, 32, 3];
        //    for (Int32 i = 0; i <= imageData.GetUpperBound(1); i++)
        //    {
        //        for (Int32 j = 0; j <= imageData.GetUpperBound(2); j++)
        //        {
        //            for (Int32 k = 0; k <= imageData.GetUpperBound(3); k++)
        //            {
        //                if (k == 0)
        //                {
        //                    imageData[0, j, i, k] = image.Bitmap.GetPixel(i, j).R / 255.0 / 255.0;
        //                }
        //                else if (k == 1)
        //                {
        //                    imageData[0, j, i, k] = image.Bitmap.GetPixel(i, j).G / 255.0 / 255.0;
        //                }
        //                else
        //                {
        //                    imageData[0, j, i, k] = image.Bitmap.GetPixel(i, j).B / 255.0 / 255.0;
        //                }
        //            }
        //        }
        //    }
        //    matND.ManagedArray = imageData;
        //    file.SetInput(matND);

        //    var preeds = file.Forward().GetData();
        //    //using (StreamWriter writer = new StreamWriter("data of variable preeds.txt"))
        //    //{
        //    //    Single[,] arrayPreeds = (Single[,])preeds;
        //    //    for (Int32 i = 0; i <= arrayPreeds.GetUpperBound(1); i++)
        //    //    {
        //    //        writer.WriteLine($"[0, {i}] = {arrayPreeds[0, i]}");
        //    //    }
        //    //}
        //    predictedId = GetMax((Single[,])preeds);

        //    return predictedId;
        //}

        public virtual Int32 GetMax(Array array)
        {
            Single[,] singleArray = array as Single[,];
            if(singleArray != null)
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

        //void WithEmguCV2()
        //{
        //    Emgu.CV.Dnn.Net file = DnnInvoke.ReadNetFromONNX("trafficsignnet.onnx");

        //    CvInvoke.CvtColor(image, image, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
        //    var image2 = image.Resize(32, 32, Emgu.CV.CvEnum.Inter.Linear, false);
        //    CvInvoke.CvtColor(image2, image2, Emgu.CV.CvEnum.ColorConversion.Gray2Bgr);

        //    var mat = image2.Mat;
        //    CvInvoke.Resize(mat, mat, new System.Drawing.Size(32, 32));
        //    file.SetInput(mat);
        //    Emgu.CV.Mat preeds = file.Forward();
        //}



        //Double[,] Get2DimensionalArray(Image<Bgr, Byte> image)
        //{
        //    Double[,] imageData = new Double[1, 1024];
        //    Int32 i = 0;
        //    for (Int32 k = 0; k < 32; k++)
        //    {
        //        for (Int32 j = 0; j < 32; j++)
        //        {
        //            var pixel = image.Bitmap.GetPixel(k, j);
        //            imageData[0, i] = GetValueOfPixel(pixel) / 255;
        //            i++;
        //        }
        //    }

        //    return imageData;
        //}

        internal Double GetValueOfPixel(System.Drawing.Color color)
        {
            Double result = (color.R + color.G + color.B) / 3.0;
            return result;
        }
    }
}
