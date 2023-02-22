using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.GeneticAlgorithm
{
    public class Mutation: IMutation
    {
        private Random random = null;
        private double mutationRate = 0.025; // 2.5%
        private double mutationChange = 0.1;

        public Mutation()
        {
            this.random = new Random();
        }

        public void Mutate(ref FloatChromosome child)
        {
            for (int i = 0, nb = child.genes.Count; i < nb; i++)
            {
                if (this.CheckMutation())
                {
                    child.genes[i] = this.GetNewGeneValue(child.genes[i]);
                }
            }
        }

        private bool CheckMutation()
        {
            double r = this.random.NextDouble();
            return (r <= this.mutationRate);
        }

        private float GetNewGeneValue(float currentGeneValue)
        {
            float newGeneValue = (float) ((this.random.NextDouble() * 2.0 - 1.0) * this.mutationChange);

            return currentGeneValue * newGeneValue;
        }
    }
}
