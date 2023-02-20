using System;
using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public class Population
    {
        protected List<Subject> subjectList;

        public Population()
        {
            this.subjectList = new List<Subject>();
        }

        public void AddSubject(ref Subject subject)
        {
            this.subjectList.Add(subject);
        }
    }
}
