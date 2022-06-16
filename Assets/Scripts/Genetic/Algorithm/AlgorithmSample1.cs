
using System;

using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Fitnesses;
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
    private IFitness fitness;
    private ISelection selection;
    private ICrossover crossover;
    private IMutation mutation;
    private ITermination termination;
    private GeneticAlgorithm geneticAlgorithm;

    public AlgorithmSample1()
    {
        this.InitChromosome();
        this.InitPopulation();
        this.InitFitness();
        this.InitSelection();
        this.InitCrossover();
        this.InitMutation();
        this.InitTermination();

        this.geneticAlgorithm = new GeneticAlgorithm(
            population,
            fitness,
            selection,
            crossover,
            mutation
        );
        this.geneticAlgorithm.Termination = termination;

    }

    public double Start()
    {
        this.geneticAlgorithm.Start();

        ChromosomeSample1 bestChromosome = this.geneticAlgorithm.BestChromosome as ChromosomeSample1;
        double bestFitness = bestChromosome.Fitness.Value;

        return bestFitness;
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

        this.chromosome = new ChromosomeSample1(
            minArray,
            maxArray,
            bitArray,
            decimalArray,
            null
        );
    }

    private void InitPopulation()
    {
        this.population = new Population(50, 50, this.chromosome);
    }

    private void InitFitness()
    {
        this.fitness = new FitnessSample1();
    }

    private void InitSelection()
    {
        this.selection = new RouletteWheelSelection();
    }

    private void InitCrossover()
    {
        this.crossover = new UniformCrossover();
    }

    private void InitMutation()
    {
        this.mutation = new FlipBitMutation();
    }

    private void InitTermination()
    {
        this.termination = new GenerationNumberTermination(1);
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
