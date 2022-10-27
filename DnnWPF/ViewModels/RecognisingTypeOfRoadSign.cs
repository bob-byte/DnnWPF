using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Dnn;
using System;

namespace DnnWPF.ViewModels
{
    abstract class RecognisingTypeOfRoadSign<TColor, TDepth> 
        where TColor : struct, IColor
        where TDepth : new()
    {
        protected const Int32 MAX_COLOR_COMPONENT_VALUE = 255;

        public abstract Object LoadModel(String pathToModel);

        public Int32 GetPredictedIdOfRoadSign(Object modelObj, Image<TColor, TDepth> image)
        {
            Image<TColor, TDepth> processedImage = ProcessedImage(image);
            Object imageData = NormalizedDataOfImage(processedImage);
            Array preeds = OutputOfNetwork(modelObj, imageData);
            Int32 predictedId = NeuronNumWithMaxOutputValue(preeds);

            return predictedId;
        }

        protected abstract Object NormalizedDataOfImage(Image<TColor, TDepth> image);

        protected abstract Array OutputOfNetwork(Object modelObj, Object matObj);

        protected virtual Image<TColor, TDepth> ProcessedImage(Image<TColor, TDepth> image) =>
            image.Resize(
                width: 32, 
                height: 32, 
                interpolationType: Inter.Linear, 
                preserveScale: false
            );

        protected virtual Int32 NeuronNumWithMaxOutputValue(Array outputOfNetworkForward)
        {
            if (outputOfNetworkForward is Single[,] networkOutputTwoDimArray)
            {
                Int32 neuronNumWithMaxOutput = 0;
                Single maxOutputOfNeurons = 0;
                Int32 neuronCount = networkOutputTwoDimArray.GetUpperBound(dimension: 1) + 1;

                for (Int32 numNeuron = 0; numNeuron < neuronCount; numNeuron++)
                {
                    if (networkOutputTwoDimArray[0, numNeuron] > maxOutputOfNeurons)
                    {
                        maxOutputOfNeurons = networkOutputTwoDimArray[0, numNeuron];
                        neuronNumWithMaxOutput = numNeuron;
                    }
                }

                return neuronNumWithMaxOutput;
            }
            else
            {
                throw new ArgumentException(message: "Can\'t convert to Single[,]", nameof(outputOfNetworkForward));
            }
        }
    }
}
