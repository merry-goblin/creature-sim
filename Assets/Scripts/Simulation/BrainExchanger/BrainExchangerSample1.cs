
using System.Collections.Generic;
using GA = Learning.GeneticAlgorithm;

namespace CreatureSim
{
    public class BrainExchangerSample1 : AbstractBrainExchanger, IBrainExchanger
    {
        protected GA.GeneticAlgorithm geneticAlgorithm;
        protected bool isInitialized = false;

        public BrainExchangerSample1() : base()
        {
            this.InitGeneticAlgorithm();
        }

        protected void InitGeneticAlgorithm()
        {
            GA.Population gaPopulation = new GA.Population();
            GA.RouletteWheelSelection gaSelection = new GA.RouletteWheelSelection();
            GA.Crossover gaCrossover = new GA.Crossover();
            GA.Mutation gaMutation = new GA.Mutation();

            this.geneticAlgorithm = new GA.GeneticAlgorithm(gaPopulation, gaSelection, gaCrossover, gaMutation);
        }

        /**
         * Convert a simulation subject into a genetic algorithm subject
         */
        public void AddSimSubject(SubjectSample1 simSubject)
        {
            GA.FloatChromosome gaChromosome = new GA.FloatChromosome(simSubject.neuralNetwork.ExportWeights());

            GA.Subject gaSubject = new GA.Subject();
            gaSubject.chromosome = gaChromosome;
            gaSubject.score = this.CalculateFitness(simSubject);

            this.geneticAlgorithm.AddSubject(gaSubject);
            this.isInitialized = true;
        }

        protected float CalculateFitness(SubjectSample1 simSubject)
        {
            return simSubject.score + 1.0f;
        }

        public void BuildSimSubjectBrain(ref SubjectSample1 simSubject)
        {
            if (this.isInitialized)
            {
                List<float> brainToImport = this.geneticAlgorithm.BreedANewSubject();
                simSubject.neuralNetwork.ImportWeights(brainToImport);
            }
        }
    }
}