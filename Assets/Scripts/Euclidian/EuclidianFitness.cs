
using System;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

class EuclidianFitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
		var fc = chromosome as FloatingPointChromosome;

		var values = fc.ToFloatingPoints();
		var x1 = values[0];
		var y1 = values[1];
		var x2 = values[2];
		var y2 = values[3];

		return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
	}
}
