using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.GeneticAlgorithm
{
    public class Chromosome
    {
        protected string genes; // ex: "110001"

        public Chromosome()
        {
            this.genes = "";
        }

        public void AddGenes(string genes)
        {
            this.genes += genes;
        }
    }
}
