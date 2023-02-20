using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.GeneticAlgorithm
{
    public class RouletteWheelSelection: ISelection
    {
        public RouletteWheelSelection()
        {

        }

        public (Subject male, Subject female) Select(Population population)
        {
            return (male: population.subjectList[0], female: population.subjectList[0]);
        }
    }
}
