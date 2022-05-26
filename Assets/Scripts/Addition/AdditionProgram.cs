
using UnityEngine;
using System;

using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Terminations;

public class AdditionProgram
{
    public AdditionProgram()
    {

    }

    public void Start()
    {
        float maxWidth = 25f;
        float maxHeight = 25f;

        var chromosome = new FloatingPointChromosome(
            new double[] { 0, 0 },
            new double[] { maxWidth, maxHeight },
            new int[] { 10, 10 },
            new int[] { 0, 0 });
        var population = new Population(50, 100, chromosome);
        var fitness = new AdditionFitness();
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
