
using System;
using System.Collections.Generic;
using FeedForwardNeuralNetwork;
using UnityEngine;

namespace Learning.GradientDescent
{
    public class GradientDescentAlgorithm
    {
        protected NeuralNetwork neuralNetwork;
        protected float learningRate;
        protected float errorMaxAllowed;
        protected float learningReductingRate;
        protected float nbIterationsToReduce;

        protected int counter;
        protected int decreaseCounter;
        protected float errorMax;

        public GradientDescentAlgorithm(NeuralNetwork neuralNetwork, float learningRate, float errorMaxAllowed, float learningReductingRate, int nbIterationsToReduce)
        {
            this.neuralNetwork = neuralNetwork;
            this.learningRate = learningRate;
            this.errorMaxAllowed = errorMaxAllowed;
            this.learningReductingRate = learningReductingRate;
            this.nbIterationsToReduce = nbIterationsToReduce;

            this.counter = 0;
        }

        public void Train(List<List<float>> inputValueSet, List<List<float>> expectedOutputValueSet)
        {
            for (int y = 0; y < 100000; y++)
            {
                this.errorMax = 0.0f;
                //  One loop is done on set of values but it should be more than that: this is a work in progress
                for (int i = 0, nb = inputValueSet.Count; i < nb; i++)
                {
                    List<float> inputValues = inputValueSet[i];
                    List<float> expectedOutputValues = expectedOutputValueSet[i];

                    this.FeedForward(inputValues);
                    this.Backpropagation(expectedOutputValues);
                }

                if (this.errorMax < this.errorMaxAllowed)
                {
                    Debug.Log(String.Concat("Break: ", y));
                    break;
                }

                this.DecreaseLearningRate();
            }

            for (int i = 0, nb = inputValueSet.Count; i < nb; i++)
            {
                this.FeedForward(inputValueSet[i]);
                this.DebugLog(String.Concat(" i1 => ", this.neuralNetwork.inputLayer.neurons[0].outputValue));
                this.DebugLog(String.Concat(" i2 => ", this.neuralNetwork.inputLayer.neurons[1].outputValue));
                this.DebugLog(String.Concat(" o1 => ", this.neuralNetwork.outputLayer.neurons[0].outputValue));
            }
        }

        protected void FeedForward(List<float> inputValues)
        {
            this.neuralNetwork.ApplyInputValues(inputValues);
            this.neuralNetwork.CalculateOutput();
        }

        protected void Backpropagation(List<float> expectedOutputValues)
        {
            this.CalculateErrors(expectedOutputValues);
            this.CalculateGradients();
            this.ModifyWeights();
        }

        protected void CalculateErrors(List<float> expectedOutputValues)
        {
            Neuron outputNeuron;
            float absError = 0.0f;
            for (int outputIndex = 0, nbNeurons = this.neuralNetwork.outputLayer.neurons.Count; outputIndex < nbNeurons; outputIndex++)
            {
                outputNeuron = this.neuralNetwork.outputLayer.neurons[outputIndex];
                outputNeuron.error = expectedOutputValues[outputIndex] - outputNeuron.outputValue;
                absError = Math.Abs(outputNeuron.error);
                if (absError > this.errorMax)
                {
                    this.errorMax = absError;
                }
            }
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
            this.ModifyWeightsOfHiddenLayers();
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

        protected void DecreaseLearningRate()
        {
        	this.decreaseCounter++;
        	if (this.decreaseCounter >= this.nbIterationsToReduce)
        	{
        		this.decreaseCounter = 0;
        		this.learningRate = this.learningRate * this.learningReductingRate;
        	}
        }

        protected void DebugLog(string print)
        {
            this.counter++;
            Debug.Log(String.Concat(this.counter, print));
        }

    }
}
