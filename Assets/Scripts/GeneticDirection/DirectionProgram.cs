
using UnityEngine;
using System;

using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Terminations;

public class DirectionProgram
{
    public DirectionProgram()
    {

    }

    public void Start()
    {
        double minRotationX = -179;
        double minRotationY = -179;
        double maxRotationX = 179;
        double maxRotationY = 179;

        var chromosome = new FloatingPointChromosome(
            new double[] { minRotationX, minRotationY },
            new double[] { maxRotationX, maxRotationY },
            new int[] { 64, 64 },
            new int[] { 0, 0 });
        var population = new Population(50, 100, chromosome);
        var fitness = new DirectionFitness();
        var selection = new EliteSelection();
        var crossover = new UniformCrossover(0.5f);
        var mutation = new FlipBitMutation();
        var termination = new FitnessStagnationTermination(100);

        var ga = new GeneticAlgorithm(
        population,
        fitness,
        selection,
        crossover,
        mutation);

        ga.Termination = termination;

        Debug.Log("Generation: (x, y) = addition");

        var latestFitness = 0.0;

        ga.GenerationRan += (sender, e) =>
        {
            var bestChromosome = ga.BestChromosome as FloatingPointChromosome;
            var bestFitness = bestChromosome.Fitness.Value;

            if (bestFitness != latestFitness)
            {
                latestFitness = bestFitness;
                var phenotype = bestChromosome.ToFloatingPoints();

                Debug.Log(String.Format(
                    "Generation {0,2}: ({1},{2}) = {3}",
                    ga.GenerationsNumber,
                    phenotype[0],
                    phenotype[1],
                    bestFitness
                ));
            }
        };

        ga.Start();
    }
}
