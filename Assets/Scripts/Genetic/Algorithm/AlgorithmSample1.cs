
using System;

using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Terminations;


public class AlgorithmSample1
{
    private int nbInputNeurons = 2;
    private int nbOutputNeurons = 2;
    private int nbHiddenLayers = 4; // must be > 0
    private int nbHiddenNeuronsByLayer = 5;

    private IChromosome chromosome;
    private IPopulation population;

    public AlgorithmSample1()
    {
        this.InitChromosome();
        this.InitPopulation();

    }

    private void InitChromosome()
    {
        int nbSynapses = this.getNbSynapses();

        double[] minArray = new double[nbSynapses];
        double[] maxArray = new double[nbSynapses];
        int[] bitArray = new int[nbSynapses];
        int[] decimalArray = new int[nbSynapses];

        for (int i = 0; i < nbSynapses; i++)
        {
            minArray[i] = -1.0;
            maxArray[i] = 1.0;
            bitArray[i] = 64;
            decimalArray[i] = 2;
        }

        this.chromosome = new FloatingPointChromosome(
            minArray,
            maxArray,
            bitArray,
            decimalArray
        );
    }

    private void InitPopulation()
    {
        this.population = new Population(50, 50, this.chromosome);
    }

    private int getNbSynapses()
    {
        double nbHiddenSynapses = Math.Pow(5, this.nbHiddenLayers - 1) * 5;
        int nbSynapses = this.nbInputNeurons * this.nbHiddenNeuronsByLayer +
                        System.Convert.ToInt32(System.Math.Floor(nbHiddenSynapses)) +
                        this.nbHiddenNeuronsByLayer * this.nbOutputNeurons;

        return nbSynapses;
    }
}
