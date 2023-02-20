
using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public class FloatChromosome : IChromosome
    {
        public List<float> genes
        {
            get;
            set;
        }

        public FloatChromosome()
        {
            this.genes = new List<float>();
        }

        public FloatChromosome(List<float> genes)
        {
            this.genes = genes;
        }

        public void AddGenes(List<float> genes)
        {
            this.genes.AddRange(genes);
        }
    }
}
