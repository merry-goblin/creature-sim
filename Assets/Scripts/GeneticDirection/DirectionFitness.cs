
using System;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;

class DirectionFitness : IFitness
{
    public double Evaluate(IChromosome chromosome)
    {
		var fc = chromosome as FloatingPointChromosome;

		var values = fc.ToFloatingPoints();
		var x = values[0];
		var y = values[1];

		double xGoal = 75.0;
		double yGoal = -75.0;

		double xDiff = (x > xGoal) ? x - xGoal : xGoal - x;
		double yDiff = (y > yGoal) ? y - yGoal : yGoal - y;

		var eval = xGoal - xDiff + yGoal -  yDiff; // the closer of the goal the better is the score

		return eval;
	}
}
