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

        public List<List<float>> ExportWeights()
        {
            List<List<float>> exportList = new List<List<float>>();

            //  Bias
            exportList.Add(this.bias.ExportWeights());

            //  Neurons
            for (int neuronIndex = 0, nb = this.neurons.Count; neuronIndex < nb; neuronIndex++)
            {
                exportList.Add(this.neurons[neuronIndex].ExportWeights());
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
