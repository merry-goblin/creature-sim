using System;
using System.Collections.Generic;

namespace Learning.GeneticAlgorithm
{
    public class Population
    {
        public List<Subject> subjectList
        {
            get;
            set;
        }

        public Population()
        {
            this.subjectList = new List<Subject>();
        }

        public void AddSubject(Subject subject)
        {
            this.subjectList.Add(subject);
        }
    }
}
