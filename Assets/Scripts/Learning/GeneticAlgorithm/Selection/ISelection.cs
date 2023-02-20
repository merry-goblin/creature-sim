using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public interface ISelection
    {
        public List<Subject> Select();
    }
}
