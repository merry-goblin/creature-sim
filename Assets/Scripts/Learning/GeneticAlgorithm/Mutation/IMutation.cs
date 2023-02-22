using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public interface IMutation
    {
        public void Mutate(ref FloatChromosome child);
    }
}
