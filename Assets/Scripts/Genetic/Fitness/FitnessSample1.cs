
using System;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

public class FitnessSample1 : IFitness
{
	public double Evaluate(IChromosome chromosome)
	{
		double eval = (double)chromosome.Fitness;

		return eval;
	}
}
