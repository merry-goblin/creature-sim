
using UnityEngine;
using System;

using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Terminations;

public class EuclidianProgram
{
    public EuclidianProgram()
    {

    }

    public void Start()
    {
        float maxWidth = 998f;
        float maxHeight = 680f;

        var chromosome = new FloatingPointChromosome(
            new double[] { 0, 0, 0, 0 },
            new double[] { maxWidth, maxHeight, maxWidth, maxHeight },
            new int[] { 10, 10, 10, 10 },
            new int[] { 0, 0, 0, 0 });
        var population = new Population(50, 100, chromosome);
        var fitness = new EuclidianFitness();
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

        Debug.Log("Generation: (x1, y1), (x2, y2) = distance");

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
                    "Generation {0,2}: ({1},{2}),({3},{4}) = {5}",
                    ga.GenerationsNumber,
                    phenotype[0],
                    phenotype[1],
                    phenotype[2],
                    phenotype[3],
                    bestFitness
                ));
            }
        };

        ga.Start();
    }
}
