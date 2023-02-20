using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public interface IChromosome
    {
        public List<float> genes
        {
            get;
            set;
        }

        public void AddGenes(List<float> genes);
    }
}
