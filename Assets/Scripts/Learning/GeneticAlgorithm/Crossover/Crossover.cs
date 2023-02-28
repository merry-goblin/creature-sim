using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.GeneticAlgorithm
{
    public class Crossover: ICrossover
    {
        private Random random = null;
        private static readonly int MaxNumberOfGenesToCopyBeforeSwitching = 50;

        public Crossover()
        {

        }

        public void Cross(FloatChromosome male, FloatChromosome female, ref FloatChromosome child)
        {
            //  Check number of genes validity and retrieve it
            int nbGenes = this.GetNbGenes(male, female);

            int crossIndex = 0;
            bool isMale = false;
            float childGene;
            int nbGenesBeforeSwitch = GetRandomGeneIndexBeforeSwitch(crossIndex);
            while (crossIndex < nbGenes)
            {
                if (nbGenesBeforeSwitch <= 0)
                {
                    nbGenesBeforeSwitch = GetRandomGeneIndexBeforeSwitch(crossIndex);
                    isMale = !isMale;
                }

                childGene = (isMale) ? male.genes[crossIndex] : female.genes[crossIndex];
                child.genes.Add(childGene);
                nbGenesBeforeSwitch--;
                crossIndex++;
            }
        }

        private int GetNbGenes(FloatChromosome male, FloatChromosome female)
        {
            int maleNbGenes = male.genes.Count;
            int femaleNbGenes = female.genes.Count;
            if (maleNbGenes <= 0 || maleNbGenes != femaleNbGenes)
            {
                throw new Exception("Male & female aren't compatible. Crossover failed");
            }

            return maleNbGenes;
        }

        /**
         * Number of genes to copy on one parent before switching of genes of the other parents
         */
        private int GetRandomGeneIndexBeforeSwitch(int currentIndex)
        {
            if (this.random == null)
            {
                this.random = new Random();
            }

            return random.Next(1, MaxNumberOfGenesToCopyBeforeSwitching + 1);
        }
    }
}
