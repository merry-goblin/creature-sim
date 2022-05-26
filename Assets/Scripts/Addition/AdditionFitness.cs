
using System;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

class AdditionFitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
		var fc = chromosome as FloatingPointChromosome;

		var values = fc.ToFloatingPoints();
		var x = values[0];
		var y = values[1];

		var total = x + y;
		var diff = (total > 25) ? total - 25 : 25 - total;
		var eval = 25 - diff;

		return eval;
	}
}
