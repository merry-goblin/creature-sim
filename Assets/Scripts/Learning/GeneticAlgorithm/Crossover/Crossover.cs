using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.GeneticAlgorithm
{
    public class Crossover: ICrossover
    {
        public Crossover()
        {

        }

        public void Cross(FloatChromosome male, FloatChromosome female, ref FloatChromosome child)
        {
            int maleNbGenes = male.genes.Count;
            int femaleNbGenes = female.genes.Count;
            if (maleNbGenes <= 0 || maleNbGenes != femaleNbGenes)
            {
                throw new Exception("Male & female aren't compatible. Crossover failed");
            }

            Random random = new Random();
            int crossIndex = 0;
            int nbGenesBeforeSwitch = 0;
            bool isMale = false;
            while (crossIndex < maleNbGenes)
            {
                if (nbGenesBeforeSwitch <= 0)
                {
                    nbGenesBeforeSwitch = random.Next(0 + 1, 6);
                    isMale = !isMale;
                }

                float childGene = (isMale) ? male.genes[crossIndex] : female.genes[crossIndex];
                child.genes.Add(childGene);
                
                nbGenesBeforeSwitch--;
                crossIndex++;
            }
        }
    }
}
