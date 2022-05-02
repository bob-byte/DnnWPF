using Emgu.CV;
using Emgu.CV.Dnn;
using System;

namespace DnnWPF.ViewModels
{
    class RecognisingEmguCvNoClahe<T, U> : RecognisingTypeOfRoadSign<T, U>
        where T : struct, IColor
        where U : new() 
    {
        public override Object LoadModel(String pathToModel) =>
            (Object)DnnInvoke.ReadNetFromONNX(pathToModel);

        public override Object NormalizedDataOfImage(Image<T, U> image)
        {
            var imageData = new Double[1, 32, 32, 3];

            for (Int32 widthIndex = 0; widthIndex <= imageData.GetUpperBound(1); widthIndex++)
            {
                for (Int32 heightIndex = 0; heightIndex <= imageData.GetUpperBound(2); heightIndex++)
                {
                    for (Int32 numColorComponent = 0; numColorComponent <= imageData.GetUpperBound(3); numColorComponent++)
                    {
                        switch (numColorComponent)
                        {
                            case 0:
                                {
                                    imageData[0, heightIndex, widthIndex, numColorComponent] = image.Bitmap.GetPixel(widthIndex, heightIndex).R / Math.Pow(MAX_COLOR_COMPONENT_VALUE, 2);
                                    break;
                                }

                            case 1:
                                {
                                    imageData[0, heightIndex, widthIndex, numColorComponent] = image.Bitmap.GetPixel(widthIndex, heightIndex).G / Math.Pow(MAX_COLOR_COMPONENT_VALUE, 2);
                                    break;
                                }
                            default:
                                {
                                    imageData[0, heightIndex, widthIndex, numColorComponent] = image.Bitmap.GetPixel(widthIndex, heightIndex).B / Math.Pow(MAX_COLOR_COMPONENT_VALUE, 2);
                                    break;
                                }
                        }
                    }
                }
            }

            var matND = new MatND<Double>(1, 32, 32, 3)
            {
                ManagedArray = imageData
            };
            return matND;
        }

        public override Array OutputOfNetwork<V>(Object modelObj, Object matObj)
        {
            if (modelObj is Net model)
            {
                if (matObj is MatND<V> mat)
                {
                    //set input data for neural network
                    model.SetInput(mat);

                    //run forward pass for the whole network
                    Mat outputMatrix = model.Forward();

                    //get copy of output to convert it to array
                    Array outputAsArray = outputMatrix.GetData();
                    return outputAsArray;
                }
                else
                {
                    throw new ArgumentException(message: $"Can\'t convert {nameof(matObj)} to {typeof(MatND<V>).FullName}");
                }
            }
            else
            {
                throw new ArgumentException($"Can\'t convert {nameof(modelObj)} to {typeof(Net).FullName}");
            }
        }
    }
}
