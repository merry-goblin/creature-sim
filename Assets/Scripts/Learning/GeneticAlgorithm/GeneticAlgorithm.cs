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
        protected IMutation mutation;

        public GeneticAlgorithm(Population population, ISelection selection, ICrossover crossover, IMutation mutation)
        {
            this.population = population;
            this.selection = selection;
            this.crossover = crossover;
            this.mutation = mutation;
        }

        public void AddSubject(Subject subject)
        {
            this.population.subjectList.Add(subject);
        }

        public List<float> BreedANewSubject()
        {
            //  Selection
            (Subject male, Subject female) parents = this.selection.Select(this.population);

            //  Crossover
            FloatChromosome child = new FloatChromosome();
            this.crossover.Cross(parents.male.chromosome, parents.female.chromosome, ref child);

            //  Mutation
            this.mutation.Mutate(ref child);

            return child.genes;
        }
    }
}
