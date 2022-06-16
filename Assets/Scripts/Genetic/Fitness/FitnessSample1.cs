
using System;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

public class FitnessSample1 : IFitness
{
	public double Evaluate(IChromosome chromosome)
	{
		ChromosomeSample1 c = chromosome as ChromosomeSample1;

		double eval = c.lifespan;

		return eval;
	}
}
