
using System.Collections.Generic;
using FeedForwardNeuralNetwork;

namespace Learning.GradientDescent
{
    public class GradientDescentAlgorithm
    {
        protected NeuralNetwork neuralNetwork;

        public GradientDescentAlgorithm(NeuralNetwork neuralNetwork)
        {
            this.neuralNetwork = neuralNetwork;
        }

        public void Train(List<List<float>> inputValueSet, List<List<float>> expectedOutputValueSet)
        {
            //  One loop is done on set of values but it should be more than that: this is a work in progress
            for (int i = 0, nb = inputValueSet.Count; i < nb; i++)
            {
                List<float> inputValues = inputValueSet[i];
                List<float> expectedOutputValues = expectedOutputValueSet[i];

                this.FeedForward(inputValues);
                this.Backpropagation(expectedOutputValues);
            }
        }

        protected void FeedForward(List<float> inputValues)
        {
            this.neuralNetwork.ApplyInputValues(inputValues);
            this.neuralNetwork.CalculateOutput();
        }

        protected void Backpropagation(List<float> expectedOutputValues)
        {
            //  @todo
        }
    }
}
