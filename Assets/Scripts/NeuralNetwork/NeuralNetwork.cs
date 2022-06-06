
using System;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork
{
    public Neuron[] inputLayer;
    public Neuron[][] hiddenLayers;
    public Neuron[] outputLayer;

    private Neuron bias;

    private int nbInputNeurons;
    private int nbOutputNeurons;
    private int nbHiddenLayers;
    private int nbHiddenNeuronsByLayer;

    private TanhActivation activation; // One object shared by each Neuron objects

    public NeuralNetwork(int nbInputNeurons, int nbOutputNeurons, int nbHiddenLayers, int nbHiddenNeuronsByLayer, bool initSynaspesRandomly)
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

        this.activation = new TanhActivation();

        this.InitBias();
        this.InitLayers();
        this.InitSynapses(initSynaspesRandomly);
    }

    /**
     * Cross the neural network from inputs to outputs by doing Weighted sums and filtered the result with an activation function.
     * When calling this method inputs have to be setted
     */
    public void CalculateOutput()
    {
        this.CheckInputs(); // Input are initialized

        for (int i=0, nb=this.hiddenLayers.Length; i < nb; i++)
        {
            this.CalculateLayerOutput(ref this.hiddenLayers[i]);
        }

        this.CalculateLayerOutput(ref this.outputLayer);
    }

    public List<float> GetOutputValues()
    {
        List<float> outputValues = new List<float>();

        for (int i=0, nb=this.outputLayer.Length; i < nb; i++)
        {
            outputValues.Add(this.outputLayer[i].outputValue);
        }

        return outputValues;
    }

    public string Debug()
    {
        string debug = "bias.outputValue: " + this.bias.outputValue + ". " +
            "bias.synapse.weight: " + this.bias.axons[0].weight + ". " +
            "bias.synapse.neuron1.weightedSum: " + this.bias.axons[0].dendrite.weightedSum + ". "
        ;

        return debug;
    }

    private void InitBias()
    {
        this.bias = new Neuron(this.activation);
        this.bias.outputValue = 1.0f;
    }

    private void InitLayers()
    {
        this.inputLayer = BuildLayer(this.nbInputNeurons);
        this.hiddenLayers = BuildLayers(this.nbHiddenLayers, this.nbHiddenNeuronsByLayer);
        this.outputLayer = BuildLayer(this.nbOutputNeurons);
    }

    private void InitSynapses(bool initSynaspesRandomly)
    {
        //  Connections between the bias and the first hidden layer
        this.AddSynapsesBetweenOneNeuronAndALayer(ref this.bias, ref this.hiddenLayers[0], initSynaspesRandomly);

        //  Connections between the input layer and the first hidden layer
        this.AddSynapsesBetweenTwoLayers(ref this.inputLayer, ref this.hiddenLayers[0], initSynaspesRandomly);

        //  Connections between hidden layers
        for (int i = 0, nb = hiddenLayers.Length-1; i < nb; i++)
        {
            this.AddSynapsesBetweenTwoLayers(ref this.hiddenLayers[i], ref this.hiddenLayers[i+1], initSynaspesRandomly);
        }

        //  Connections between the last hidden layer and the output layer
        this.AddSynapsesBetweenTwoLayers(ref this.hiddenLayers[hiddenLayers.Length-1], ref this.outputLayer, initSynaspesRandomly);
    }

    private Neuron[] BuildLayer(int nbNeurons)
    {
        List<Neuron> layerList = new List<Neuron>();
        for (int i = 0; i<nbNeurons; i++)
        {
            layerList.Add(new Neuron(this.activation));
        }

        return layerList.ToArray();
    }

    private Neuron[][] BuildLayers(int nbLayers, int nbNeuronsByLayer)
    {
        List<Neuron[]> layerList = new List<Neuron[]>();
        for (int i = 0; i < nbNeuronsByLayer; i++)
        {
            layerList.Add(this.BuildLayer(nbNeuronsByLayer));
        }

        return layerList.ToArray();
    }

    private void AddSynapsesBetweenTwoLayers(ref Neuron[] layer1, ref Neuron[] layer2, bool initSynaspesRandomly)
    {
        for (int i = 0, nb = layer1.Length; i < nb; i++)
        {
            this.AddSynapsesBetweenOneNeuronAndALayer(ref layer1[i], ref layer2, initSynaspesRandomly);
        }
    }

    private void AddSynapsesBetweenOneNeuronAndALayer(ref Neuron neuron, ref Neuron[] layer, bool initSynaspesRandomly)
    {
        for (int i = 0, nb = layer.Length; i < nb; i++)
        {
            Synapse synapse = new Synapse(initSynaspesRandomly);
            synapse.axon = neuron;
            synapse.dendrite = layer[i];

            neuron.axons.Add(synapse);
            layer[i].dendrites.Add(synapse);
        }
    }

    private void CheckInputs()
    {
        //  Does input are setted
        for (int i = 0, nb = this.inputLayer.Length; i < nb; i++)
        {
            //  
            if (float.IsNaN(this.inputLayer[i].outputValue))
            {
                throw new Exception("input value of neural network at index " + i + " isn't initialized");
            }
        }
    }

    private void CalculateLayerOutput(ref Neuron[] layer)
    {
        for (int i = 0, nb = layer.Length; i < nb; i++)
        {
            this.CalculateNeuronOutput(layer[i]);
        }
    }

    private void CalculateNeuronOutput(Neuron neuron)
    {
        //  Input value
        neuron.weightedSum = 0.0f;
        for (int i = 0, nb = neuron.dendrites.Count; i < nb; i++)
        {
            neuron.weightedSum += neuron.dendrites[i].weight * neuron.dendrites[i].axon.outputValue;
        }

        //  Output value
        neuron.outputValue = neuron.activation.Filter(neuron.weightedSum);
    }
}
