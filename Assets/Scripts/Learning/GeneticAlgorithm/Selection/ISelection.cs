using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public interface ISelection
    {
        public (Subject male, Subject female) Select(Population population);
    }
}
