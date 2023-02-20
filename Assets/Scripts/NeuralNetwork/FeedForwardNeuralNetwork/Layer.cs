using System;
using System.Collections.Generic;

namespace FeedForwardNeuralNetwork
{
    public class Layer
    {
        public Neuron bias;
        public List<Neuron> neurons;

        public Layer(int nbNeurons, ref IActivation activation, bool addABias = true)
        {
            this.InitBias(ref activation);
            this.InitNeurons(nbNeurons, ref activation);
        }

        public void Connect(Layer otherLayer, bool initSynaspesRandomly)
        {
            //  Loop on neurons of other layer
            for (int otherNeuronIndex = 0, otherNb = otherLayer.neurons.Count; otherNeuronIndex < otherNb; otherNeuronIndex++)
            {
                //  Bias
                this.bias.Connect(otherLayer.neurons[otherNeuronIndex], initSynaspesRandomly);

                //  Loop on neurons of current layer
                for (int neuronIndex = 0, nb = this.neurons.Count; neuronIndex < nb; neuronIndex++)
                {
                    this.neurons[neuronIndex].Connect(otherLayer.neurons[otherNeuronIndex], initSynaspesRandomly);
                }
            }
        }

        public void CheckInputs()
        {
            //  Does input be setted
            for (int neuronIndex = 0, nb = this.neurons.Count; neuronIndex < nb; neuronIndex++)
            {
                //  
                if (float.IsNaN(this.neurons[neuronIndex].outputValue))
                {
                    throw new Exception("input value of neural network at index " + neuronIndex + " isn't initialized");
                }
            }
        }

        public void CalculateOutput()
        {
            for (int neuronIndex = 0, nb = this.neurons.Count; neuronIndex < nb; neuronIndex++)
            {
                this.neurons[neuronIndex].CalculateOutput();
            }
        }

        public void ExportWeights(ref List<float> exportList)
        {
            //  Bias
            this.bias.ExportWeights(ref exportList);

            //  Neurons
            for (int neuronIndex = 0, nb = this.neurons.Count; neuronIndex < nb; neuronIndex++)
            {
                this.neurons[neuronIndex].ExportWeights(ref exportList);
            }
        }

        public void ImportWeights(ref List<float> importList, ref int currentIndex)
        {
            //  Bias
            this.bias.ImportWeights(ref importList, ref currentIndex);

            //  Neurons
            for (int neuronIndex = 0, nb = this.neurons.Count; neuronIndex < nb; neuronIndex++)
            {
                this.neurons[neuronIndex].ImportWeights(ref importList, ref currentIndex);
            }
        }

        public Dictionary<string, Dictionary<string, float>> ExportLabelledWeights(string mainLabel)
        {
            Dictionary<string, Dictionary<string, float>> exportList = new Dictionary<string, Dictionary<string, float>>();
            string label;

            //  Bias
            label = mainLabel + "b";
            exportList.Add(label, this.bias.ExportLabelledWeights(label));

            //  Neurons
            for (int neuronIndex = 0, nb = this.neurons.Count; neuronIndex < nb; neuronIndex++)
            {
                label = mainLabel + "l" + neuronIndex;
                exportList.Add(label, this.neurons[neuronIndex].ExportLabelledWeights(label));
            }

            return exportList;
        }

        private void InitBias(ref IActivation activation)
        {
            this.bias = new Neuron(ref activation);
            this.bias.outputValue = 1.0f;
        }

        private void InitNeurons(int nbNeurons, ref IActivation activation)
        {
            this.neurons = new List<Neuron>();
            for (int i = 0; i < nbNeurons; i++)
            {
                this.neurons.Add(new Neuron(ref activation));
            }
        }
    }
}
