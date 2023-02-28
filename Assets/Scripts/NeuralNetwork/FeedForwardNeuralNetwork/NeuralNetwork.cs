
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FeedForwardNeuralNetwork
{
    public class NeuralNetwork : INeuralNetwork
    {
        public Layer inputLayer;
        public List<Layer> hiddenLayers;
        public Layer outputLayer;

        private int nbInputNeurons;
        private int nbOutputNeurons;
        private int nbHiddenLayers;
        private int nbHiddenNeuronsByLayer;

        private IActivation activation; // One object shared by each Neuron objects

        public NeuralNetwork(int nbInputNeurons, int nbOutputNeurons, int nbHiddenLayers, int nbHiddenNeuronsByLayer, bool initSynaspesRandomly, ref IActivation activation)
        {
            if (nbInputNeurons <= 0)
            {
                throw new Exception("nbInputNeurons has to be positive");
            }
            if (nbOutputNeurons <= 0)
            {
                throw new Exception("nbOutputNeurons has to be positive");
            }
            if (nbHiddenLayers <= 0)
            {
                throw new Exception("nbHiddenLayers has to be positive");
            }
            if (nbHiddenNeuronsByLayer <= 0)
            {
                throw new Exception("nbHiddenNeuronsByLayer has to be positive");
            }

            this.nbInputNeurons = nbInputNeurons;
            this.nbOutputNeurons = nbOutputNeurons;
            this.nbHiddenLayers = nbHiddenLayers;
            this.nbHiddenNeuronsByLayer = nbHiddenNeuronsByLayer;

            this.activation = activation;

            this.InitLayers();
            this.InitSynapses(initSynaspesRandomly);
        }

        public void ApplyInputValues(List<float> values)
        {
            int nbValues = values.Count;
            for (int i = 0, nb = this.inputLayer.neurons.Count; i < nb; i++)
            {
                if (i >= nbValues)
                {
                    break;
                }
                this.inputLayer.neurons[i].outputValue = values[i];
            }
        }

        /**
         * Cross this neural network from inputs to outputs by doing Weighted sums and filtered the result with an activation function.
         * When calling this method inputs have to be setted
         */
        public void CalculateOutput()
        {
            this.inputLayer.CheckInputs(); // Input are initialized

            for (int i = 0, nb = this.hiddenLayers.Count; i < nb; i++)
            {
                this.hiddenLayers[i].CalculateOutput();
            }

            this.outputLayer.CalculateOutput();
        }

        public List<float> GetOutputValues()
        {
            List<float> outputValues = new List<float>();

            for (int i = 0, nb = this.outputLayer.neurons.Count; i < nb; i++)
            {
                outputValues.Add(this.outputLayer.neurons[i].outputValue);
            }

            return outputValues;
        }

        public string Debug()
        {
            string debug = "test";/*"bias.outputValue: " + this.bias.outputValue + ". " +
                "bias.synapse.weight: " + this.bias.axons[0].weight + ". " +
                "bias.synapse.neuron1.weightedSum: " + this.bias.axons[0].dendrite.weightedSum + ". "
            ;*/

            return debug;
        }

        private void InitLayers()
        {
            //  Input layer
            this.inputLayer = new Layer(this.nbInputNeurons, ref this.activation);

            //  Hidden layers
            this.hiddenLayers = new List<Layer>();
            for (int layerIndex = 0; layerIndex < this.nbHiddenLayers; layerIndex++)
            {
                this.hiddenLayers.Add(new Layer(this.nbHiddenNeuronsByLayer, ref this.activation));
            }

            //  Output layer
            this.outputLayer = new Layer(this.nbOutputNeurons, ref this.activation, false);
        }

        private void InitSynapses(bool initSynaspesRandomly)
        {
            //  Input
            this.inputLayer.Connect(this.hiddenLayers[0], initSynaspesRandomly);

            //  Hidden layers
            int lastHiddenLayerIndex = this.hiddenLayers.Count - 1;
            for (int hiddenIndex = 0; hiddenIndex < lastHiddenLayerIndex; hiddenIndex++)
            {
                this.hiddenLayers[hiddenIndex].Connect(this.hiddenLayers[hiddenIndex + 1], initSynaspesRandomly);
            }

            //  Last hidden layer
            this.hiddenLayers[lastHiddenLayerIndex].Connect(this.outputLayer, initSynaspesRandomly);
        }

        /**
         * Export with one level array of floats
         */
        public List<float> ExportWeights()
        {
            List<float> exportList = new List<float>();

            //  Input
            this.inputLayer.ExportWeights(ref exportList);

            //  Hidden layers
            for (int hiddenIndex = 0, nb = this.hiddenLayers.Count; hiddenIndex < nb; hiddenIndex++)
            {
                this.hiddenLayers[hiddenIndex].ExportWeights(ref exportList);
            }

            return exportList;
        }

        /**
         * Import with one level array of floats
         */
        public List<float> ImportWeights(List<float> importList)
        {
            int currentIndex = 0;

            //  Input
            this.inputLayer.ImportWeights(ref importList, ref currentIndex);

            //  Hidden layers
            for (int hiddenIndex = 0, nb = this.hiddenLayers.Count; hiddenIndex < nb; hiddenIndex++)
            {
                this.hiddenLayers[hiddenIndex].ImportWeights(ref importList, ref currentIndex);
            }

            return importList;
        }

        /**
         * Export with three level array of labelled floats
         * @todo: Since there is labels is it really needed to have multiple levels?
         *        Initially I build this method without labels but with multiple levels in order to allow reproduction between not totally uniformed neural networks
         */
        public Dictionary<string, Dictionary<string, Dictionary<string, float>>> ExportLabelledWeights()
        {
            Dictionary<string, Dictionary<string, Dictionary<string, float>>> exportList = new Dictionary<string, Dictionary<string, Dictionary<string, float>>>();
            string label;

            //  Input
            label = "l0";
            exportList.Add(label, this.inputLayer.ExportLabelledWeights(label));

            //  Hidden layers
            for (int hiddenIndex = 0, nb = this.hiddenLayers.Count; hiddenIndex < nb; hiddenIndex++)
            {
                label = "l" + hiddenIndex + 1;
                exportList.Add(label, this.hiddenLayers[hiddenIndex].ExportLabelledWeights(label));
            }

            return exportList;
        }
    }
}
