
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
            this.CalculateGradients();
            this.ModifyWeights();
            //this.CalculateNeuralNetworkErrors(expectedOutputValues);
            //this.CorrectNeuralNetworkWeights();

            /*//  Loop on each ouput neurons
            Neuron outputNeuron;
            for (int outputNeuronIndex = 0, nbOutputNeurons = this.neuralNetwork.outputLayer.neurons.Count; outputNeuronIndex < nbOutputNeurons; outputNeuronIndex++)
            {
                outputNeuron = this.neuralNetwork.outputLayer.neurons[outputNeuronIndex];

                this.CalculateError(outputNeuron, expectedOutputValues[outputNeuronIndex]);
                this.Descend(outputNeuron);
            }*/
        }

        protected void CalculateGradients()
        {
            this.CalculateOutputGradients();
            this.CalculateHiddenLayersGradients();
        }

        protected void CalculateOutputGradients()
        {
            for (int outputIndex = 0, nbNeurons = this.neuralNetwork.outputLayer.neurons.Count; outputIndex < nbNeurons; outputIndex++)
            {
                this.CalculateOutputGradient(this.neuralNetwork.outputLayer.neurons[outputIndex]);
            }
        }

        protected void CalculateOutputGradient(Neuron neuron)
        {
            neuron.gradient = neuron.error * neuron.activation.UnfilterDerivative(neuron.weightedSum);
        }

        protected void CalculateHiddenLayersGradients()
        {
            for (int layerIndex = this.neuralNetwork.hiddenLayers.Count - 1; layerIndex >= 0; layerIndex--)
            {
                this.CalculateHiddenGradients(this.neuralNetwork.hiddenLayers[layerIndex]);
            }
        }

        protected void CalculateHiddenGradients(Layer layer)
        {
            for (int outputIndex = 0, nbNeurons = layer.neurons.Count; outputIndex < nbNeurons; outputIndex++)
            {
                this.CalculateHiddenGradient(layer.neurons[outputIndex]);
            }
        }

        protected void CalculateHiddenGradient(Neuron neuron)
        {
            //  Only connections between our current neuron and neurons of the next layer are taken in account here
            float weightsByGradients = 0.0f;
            for (int nextNeuronIndex = 0, nbNeurons = neuron.axons.Count; nextNeuronIndex < nbNeurons; nextNeuronIndex++)
            {
                weightsByGradients += neuron.axons[nextNeuronIndex].weight * neuron.axons[nextNeuronIndex].dendrite.gradient;
            }
            neuron.gradient = weightsByGradients * neuron.activation.UnfilterDerivative(neuron.weightedSum);
        }

        protected void ModifyWeights()
        {
            this.ModifyWeightsOfOutputLayer();
        }

        protected void ModifyWeightsOfOutputLayer()
        {
            Neuron outputNeuron;
            Synapse synapse;
            for (int outputIndex = 0, nbNeurons = this.neuralNetwork.outputLayer.neurons.Count; outputIndex < nbNeurons; outputIndex++)
            {
                outputNeuron = this.neuralNetwork.outputLayer.neurons[outputIndex];
                for (int synapseIndex = 0, nbSynapses = outputNeuron.dendrites.Count; synapseIndex < nbSynapses; synapseIndex++)
                {
                    synapse = outputNeuron.dendrites[synapseIndex];
                    synapse.weight += this.learningRate * synapse.axon.outputValue * outputNeuron.gradient;
                }
            }
        }

        protected void ModifyWeightsOfHiddenLayers()
        {
            for (int layerIndex = this.neuralNetwork.hiddenLayers.Count - 1; layerIndex >= 0; layerIndex--)
            {
                this.ModifyWeightsOfHiddenLayer(this.neuralNetwork.hiddenLayers[layerIndex]);
            }
        }

        protected void ModifyWeightsOfHiddenLayer(Layer hiddenLayer)
        {
            Neuron hiddenNeuron;
            Synapse synapse;
            for (int hiddenIndex = 0, nbNeurons = hiddenLayer.neurons.Count; hiddenIndex < nbNeurons; hiddenIndex++)
            {
                hiddenNeuron = hiddenLayer.neurons[hiddenIndex];
                for (int synapseIndex = 0, nbSynapses = hiddenNeuron.dendrites.Count; synapseIndex < nbSynapses; synapseIndex++)
                {
                    synapse = hiddenNeuron.dendrites[synapseIndex];
                    synapse.weight += this.learningRate * synapse.axon.outputValue * hiddenNeuron.gradient;
                }
            }
        }

        /* *** */


        protected void CalculateNeuralNetworkErrors(List<float> expectedOutputValues)
        {
            CalculateLayerErrors(this.neuralNetwork.outputLayer, expectedOutputValues);
            for (int layerIndex = this.neuralNetwork.hiddenLayers.Count -1; layerIndex >= 0; layerIndex--)
            {
                CalculateDeepLayerErrors(this.neuralNetwork.hiddenLayers[layerIndex]);
            }
        }

        protected void CalculateLayerErrors(Layer layer, List<float> expectedValues)
        {
            for (int neuronIndex = 0, nbNeurons = layer.neurons.Count; neuronIndex < nbNeurons; neuronIndex++)
            {
                this.CalculateNeuronError(layer.neurons[neuronIndex], expectedValues[neuronIndex]);
            }
        }

        protected void CalculateNeuronError(Neuron neuron, float expectedValue)
        {


        }

        protected void CalculateDeepLayerErrors(Layer layer)
        {
            for (int neuronIndex = 0, nbNeurons = layer.neurons.Count; neuronIndex < nbNeurons; neuronIndex++)
            {
                this.CalculateDeepNeuronError(layer.neurons[neuronIndex]);
            }
        }

        protected void CalculateDeepNeuronError(Neuron neuron)
        {


        }

        protected void CorrectNeuralNetworkWeights()
        {

        }

        protected void CalculateError(Neuron neuron, float expectedValue)
        {
            float activationDerivative = neuron.activation.UnfilterDerivative(neuron.weightedSum);
            float difference = expectedValue - neuron.outputValue;

            neuron.error = activationDerivative* neuron.weightedSum * difference;
        }

        protected void Descend(Neuron neuron)
        {
            Synapse synapse;
            Neuron neuronOfPreviousLayer;
            for (int synapseIndex = 0, nbSynapses = neuron.dendrites.Count; synapseIndex < nbSynapses; synapseIndex++)
            {
                synapse = neuron.dendrites[synapseIndex];
                this.UpdateNeuronWeightWithError(synapse, neuron.error); // Update synapse weight

                neuronOfPreviousLayer = synapse.axon;
                float deepError = this.CalculateDeepError(neuron, neuron.error);
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
