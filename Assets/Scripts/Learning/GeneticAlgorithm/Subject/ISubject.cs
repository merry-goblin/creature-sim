using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public interface ISubject
    {
        public FloatChromosome chromosome
        {
            get;
            set;
        }
    }
}
