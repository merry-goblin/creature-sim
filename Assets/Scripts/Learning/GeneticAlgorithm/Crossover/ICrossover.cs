using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public interface ICrossover
    {
        public void Cross(FloatChromosome male, FloatChromosome female, ref FloatChromosome child);
    }
}
