
using System.Collections.Generic;
using FeedForwardNeuralNetwork;

namespace Learning.GradientDescent
{
    public class GradientDescentAlgorithm
    {
        protected NeuralNetwork neuralNetwork;
        protected float learningRate;

        public GradientDescentAlgorithm(NeuralNetwork neuralNetwork, float learningRate)
        {
            this.neuralNetwork = neuralNetwork;
            this.learningRate = learningRate;
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
            //  Loop on each ouput neurons
            Neuron outputNeuron;
            for (int outputNeuronIndex = 0, nbOutputNeurons = this.neuralNetwork.outputLayer.neurons.Count; outputNeuronIndex < nbOutputNeurons; outputNeuronIndex++)
            {
                outputNeuron = this.neuralNetwork.outputLayer.neurons[outputNeuronIndex];

                float error = this.CalculateError(outputNeuron, expectedOutputValues[outputNeuronIndex]);
                this.Descend(outputNeuron, error);
            }
        }

        protected float CalculateError(Neuron neuron, float expectedValue)
        {
            float activationDerivative = neuron.activation.UnfilterDerivative(neuron.weightedSum);
            float difference = expectedValue - neuron.outputValue;

            return activationDerivative * neuron.weightedSum * difference;
        }

        protected void Descend(Neuron neuron, float error)
        {
            Synapse synapse;
            Neuron neuronOfPreviousLayer;
            for (int synapseIndex = 0, nbSynapses = neuron.dendrites.Count; synapseIndex < nbSynapses; synapseIndex++)
            {
                synapse = neuron.dendrites[synapseIndex];
                this.UpdateNeuronWeightWithError(synapse, error); // Update synapse weight

                neuronOfPreviousLayer = synapse.axon;
                float deepError = this.CalculateDeepError(neuron, error);
            }
        }

        protected void UpdateNeuronWeightWithError(Synapse synapse, float error)
        {
            synapse.weight -= this.learningRate * error * synapse.axon.outputValue;
        }

        protected float CalculateDeepError(Neuron neuron, float error)
        {
            return 0.0f;
        }

    }
}
