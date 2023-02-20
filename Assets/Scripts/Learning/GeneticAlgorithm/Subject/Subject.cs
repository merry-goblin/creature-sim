
namespace Learning.GeneticAlgorithm
{
    public class Subject: ISubject
    {
        public float score = 0;
        public FloatChromosome chromosome
        {
            get;
            set;
        }

        public Subject()
        {

        }
    }
}
