using System.Collections.Generic;

namespace CreatureSim
{
    public interface IWorld
    {
        public List<ISubject> subjectList
        {
            get;
        }

        public delegate void NoMoreActiveSubjectsDelegate();
        public event NoMoreActiveSubjectsDelegate OnNoMoreActiveSubjects;

        void Load();

        void Unload();

        void Update();

        public bool IsActive();
    }
}
