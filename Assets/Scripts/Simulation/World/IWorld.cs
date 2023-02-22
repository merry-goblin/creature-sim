using System.Collections.Generic;

namespace CreatureSim
{
    public interface IWorld
    {
        public List<ISubject> subjectList
        {
            get;
        }
        public List<ISubject> inactiveSubjectList
        {
            get;
            set;
        }

        public delegate void WorldEndsDelegate();
        public event WorldEndsDelegate OnWorldEnds;

        public delegate void NoMoreActiveSubjectsDelegate();
        public event NoMoreActiveSubjectsDelegate OnNoMoreActiveSubjects;

        void Load();

        void Unload();

        void Update();

        public bool IsActive();
    }
}
