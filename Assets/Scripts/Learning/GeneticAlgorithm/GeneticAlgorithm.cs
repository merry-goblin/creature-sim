using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.GeneticAlgorithm
{
    public class GeneticAlgorithm
    {
        protected Population population;
        protected ISelection selection;
        protected ICrossover crossover;

        public GeneticAlgorithm(Population population, ISelection selection, ICrossover crossover)
        {
            this.population = population;
            this.selection = selection;
            this.crossover = crossover;
        }

        public List<float> BreedANewSubject()
        {
            //  Selection
            (Subject male, Subject female) parents = this.selection.Select(this.population);

            //  Crossover
            FloatChromosome child = new FloatChromosome();
            this.crossover.Cross(parents.male.chromosome, parents.female.chromosome, ref child);

            return child.genes;
        }
    }
}
