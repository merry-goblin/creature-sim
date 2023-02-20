
using System;
using System.Collections.Generic;

namespace FeedForwardNeuralNetwork
{
    public class Neuron
    {
        public float weightedSum = float.NaN;
        public float outputValue;
        public List<Synapse> dendrites; // input
        public List<Synapse> axons; // output
        public IActivation activation;

        public Neuron(ref IActivation activation)
        {
            this.dendrites = new List<Synapse>();
            this.axons = new List<Synapse>();
            this.activation = activation;
        }

        public void Connect(Neuron otherNeuron, bool initSynaspesRandomly)
        {
            Synapse synapse = new Synapse(initSynaspesRandomly);
            synapse.axon = this;
            synapse.dendrite = otherNeuron;

            this.axons.Add(synapse);
            otherNeuron.dendrites.Add(synapse);
        }


        public void CalculateOutput()
        {
            //  Input value
            this.weightedSum = 0.0f;
            for (int i = 0, nb = this.dendrites.Count; i < nb; i++)
            {
                this.weightedSum += this.dendrites[i].weight * this.dendrites[i].axon.outputValue;
            }

            //  Output value
            this.outputValue = this.activation.Filter(this.weightedSum);
        }

        public void ExportWeights(ref List<float> exportList)
        {
            for (int i = 0, nb = this.axons.Count; i < nb; i++)
            {
                exportList.Add(this.axons[i].weight);
            }
        }

        public void ImportWeights(ref List<float> importList, ref int currentIndex)
        {
            for (int i = 0, nb = this.axons.Count; i < nb; i++)
            {
                this.axons[i].weight = importList[currentIndex];
                currentIndex++;
            }
        }

        public Dictionary<string, float> ExportLabelledWeights(string mainLabel)
        {
            Dictionary<string, float> exportList = new Dictionary<string, float>();
            string label;

            for (int i = 0, nb = this.axons.Count; i < nb; i++)
            {
                label = mainLabel + "l" + i;
                exportList.Add(label, this.axons[i].weight);
            }

            return exportList;
        }
    }
}