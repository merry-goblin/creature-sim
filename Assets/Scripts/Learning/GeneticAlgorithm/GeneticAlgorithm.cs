using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;

namespace Learning.GeneticAlgorithm
{
    public class GeneticAlgorithm
    {
        protected Population population;
        protected ISelection selection;
        protected ICrossover crossover;
        protected IMutation mutation;

        protected static int generationNum = 0;
        protected int subjectNum = 0;

        public GeneticAlgorithm(Population population, ISelection selection, ICrossover crossover, IMutation mutation)
        {
            this.population = population;
            this.selection = selection;
            this.crossover = crossover;
            this.mutation = mutation;

            GeneticAlgorithm.generationNum++;
            this.LogInText("New generation: " + GeneticAlgorithm.generationNum);
        }

        public void AddSubject(Subject subject)
        {
            this.subjectNum++;

            this.population.subjectList.Add(subject);
            this.LogInText("New subject: " + this.subjectNum);
            this.LogSubjectInText(subject);
            this.LogInText(" --- ");
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

        private void LogSubjectInText(Subject subject)
        {
            this.LogInText(" - score: " + subject.score);
            this.LogInText(" - genes: ");
            string genes = "   ";
            string gene = "";
            for (int i = 0, nb = subject.chromosome.genes.Count; i < nb; i++)
            {
                gene = "";
                if (subject.chromosome.genes[i] >= 0)
                {
                    gene += " ";
                }
                gene += subject.chromosome.genes[i].ToString("0.0000");
                genes += gene.PadRight(8);
            }
            this.LogInText(genes);
        }

        private void LogInText(string textToLog)
        {
            string path = Application.dataPath + "/Log.txt";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, textToLog + "\n");
            }
            else
            {
                File.AppendAllText(path, textToLog + "\n");
            }
        }
    }
}
