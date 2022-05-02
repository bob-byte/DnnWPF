using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Dnn;
using System;

namespace DnnWPF.ViewModels
{
    abstract class RecognisingTypeOfRoadSign<T, U> 
        where T : struct, IColor
        where U : new()
    {
        protected const Int32 MAX_COLOR_COMPONENT_VALUE = 255;

        public abstract Object LoadModel(String pathToModel);

        public abstract Object NormalizedDataOfImage(Image<T, U> image);

        public abstract Array OutputOfNetwork<V>(Object modelObj, Object matObj) where V : new();

        public virtual Image<T, U> ProcessedImage(Image<T, U> image) =>
            image.Resize(
                width: 32, 
                height: 32, 
                interpolationType: Inter.Linear, 
                preserveScale: false
            );

        public virtual Int32 LayerNumWithMaxOutputValue(Array outputOfNetworkForward)
        {
            if (outputOfNetworkForward is Single[,] singleArray)
            {
                Int32 layerNumWithMaxOutput = 0;
                Single maxOutputOfLayers = 0;

                for (Int32 numLayer = 0; numLayer <= singleArray.GetUpperBound(dimension: 1); numLayer++)
                {
                    if (singleArray[0, numLayer] > maxOutputOfLayers)
                    {
                        maxOutputOfLayers = singleArray[0, numLayer];
                        layerNumWithMaxOutput = numLayer;
                    }
                }

                return layerNumWithMaxOutput;
            }
            else
            {
                throw new ArgumentException(message: "Can\'t convert to Single[,]", nameof(outputOfNetworkForward));
            }
        }

        public Int32 GetPredictedIdOfRoadSign<V>(Object modelObj, Image<T, U> image) 
            where V : new()
        {
            Image<T, U> processedImage = ProcessedImage(image);
            Object imageData = NormalizedDataOfImage(processedImage);
            Array preeds = OutputOfNetwork<V>(modelObj, imageData);
            Int32 predictedId = LayerNumWithMaxOutputValue(preeds);

            return predictedId;
        }

        internal Double ValueOfPixel(System.Drawing.Color color) => 
            (color.R + color.G + color.B) / 3.0;
    }
}
