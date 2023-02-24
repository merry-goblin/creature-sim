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
            Subject male = null;
            Subject female = null;
            if (population.subjectList.Count == 0)
            {
                throw new Exception("Population is empty which prevents selection");
            } 
            else if (population.subjectList.Count == 1)
            {
                male = population.subjectList[0];
                female = population.subjectList[0];
            }
            else // Population has enough subjects to apply a normal breeding
            {
                float totalScores = this.CalculateTotalSubjectScores(population);
                int selectedMaleIndex = this.SpinRouletteWheel(population, totalScores, null);
                totalScores -= population.subjectList[selectedMaleIndex].score;
                int selectedFemaleIndex = this.SpinRouletteWheel(population, totalScores, selectedMaleIndex);

                male = population.subjectList[selectedMaleIndex];
                female = population.subjectList[selectedFemaleIndex];
            }

            return (male: male, female: female);
        }

        protected float CalculateTotalSubjectScores(Population population)
        {
            float totalScores = 0;
            for (int i = 0, nb = population.subjectList.Count; i < nb; i++)
            {
                totalScores += population.subjectList[i].score;
            }

            return totalScores;
        }

        /**
         * Retrieves index of selected subject in population
         */
        protected int SpinRouletteWheel(Population population, float totalScores, int? ignoredIndex)
        {
            int selectedIndex = 0;
            float randomScore = ToolBox.GetRandomFloat(0.0f, totalScores);
            float calculatedScore = 0;

            for (int subjectIndex = 0, nb = population.subjectList.Count; subjectIndex < nb; subjectIndex++)
            {
                if (ignoredIndex != null && ignoredIndex == subjectIndex)
                {
                    continue; // This subject is ignored and won't be selected
                }
                //  Does randomScore have selected this subject?
                if (randomScore >= calculatedScore)
                {
                    calculatedScore += population.subjectList[subjectIndex].score;
                    if (randomScore < calculatedScore)
                    {
                        selectedIndex = subjectIndex; // This subject has been selected
                        break;
                    }
                }
            }

            return selectedIndex;
        }
    }
}
