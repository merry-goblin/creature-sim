
using System.Collections.Generic;

namespace CreatureSim
{
    public abstract class AbstractWorld
    {
        public List<ISubject> subjectList
        {
            get;
            set;
        }
        public List<IElement> elementList;
        public List<ISubject> subjectListToLoad;
        public List<IElement> elementListToLoad;

        public event IWorld.NoMoreActiveSubjectsDelegate OnNoMoreActiveSubjects;

        protected bool active
        {
            get;
            set;
        }

        public AbstractWorld()
        {
            this.subjectList = new List<ISubject>();
            this.elementList = new List<IElement>();
            this.subjectListToLoad = new List<ISubject>();
            this.elementListToLoad = new List<IElement>();

            this.active = false;
        }

        /**
         * Tip: Override this method to add one or more subjects & elements in it then call base.Load()
         * It's the expected behavior since AddSubject & AddElement are protected
         */
        public virtual void Load()
        {
            for (int i = 0, nb = this.subjectListToLoad.Count; i < nb; i++)
            {
                this.subjectListToLoad[i].Load();
                this.subjectList.Add(this.subjectListToLoad[i]);
                this.subjectListToLoad[i].OnLifeEnds += this.OnSubjectIsNoMoreActive; // We will be informed any time one of this world's subject dies
            }
            this.subjectListToLoad.Clear();

            for (int i = 0, nb = this.elementListToLoad.Count; i < nb; i++)
            {
                this.elementListToLoad[i].Load();
                this.elementList.Add(this.elementListToLoad[i]);
            }
            this.elementListToLoad.Clear();

            //  World is active if at least one subject is active
            if (this.subjectList.Count > 0)
            {
                this.active = true;
            }
        }

        public virtual void Unload()
        {
            this.active = false;

            for (int i = 0, nb = this.subjectList.Count; i < nb; i++)
            {
                this.subjectList[i].Unload();
            }

            for (int i = 0, nb = this.elementList.Count; i < nb; i++)
            {
                this.elementList[i].Unload();
            }
        }

        public virtual void Update()
        {
            for (int i = 0, nb = this.subjectList.Count; i < nb; i++)
            {
                this.subjectList[i].Update();
            }

            for (int i = 0, nb = this.elementList.Count; i < nb; i++)
            {
                this.elementList[i].Update();
            }
        }

        /**
         * End simulation for current subjects of this world
         * To use when no more subject are active in this world
         * Will raise an event
         */
        public void EndSimulationForThisWorldSubjects()
        {
            this.active = false;
            if (this.OnNoMoreActiveSubjects != null)
            {
                this.OnNoMoreActiveSubjects(); // Event
            }
        }

        public bool IsActive()
        {
            return this.active;
        }

        /**
         * One subject of this world is no more active
         * This method checks if there is still some subjets active
         * If not we call EndSimulationForThisWorldSubjects()
         * Could be overridden if needed by child class
         */
        protected virtual void OnSubjectIsNoMoreActive()
        {
            bool oneSubjectIsStillActive = false;
            for (int i = 0, nb = this.subjectList.Count; i < nb; i++)
            {
                if (this.subjectList[i].IsActive())
                {
                    oneSubjectIsStillActive = true;
                    break;
                }
            }

            if (!oneSubjectIsStillActive)
            {
                this.EndSimulationForThisWorldSubjects();
            }
        }

        protected void AddSubject(ISubject subject)
        {
            this.subjectListToLoad.Add(subject);
        }
        protected void AddElement(IElement element)
        {
            this.elementListToLoad.Add(element);
        }
    }
}
