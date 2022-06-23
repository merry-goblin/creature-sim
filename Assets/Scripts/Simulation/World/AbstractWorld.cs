
using System.Collections.Generic;

public abstract class AbstractWorld
{
    public List<ISubject> subjectList;
    public List<IElement> elementList;
    public List<ISubject> subjectListToLoad;
    public List<IElement> elementListToLoad;

    public AbstractWorld()
    {
        this.subjectList = new List<ISubject>();
        this.elementList = new List<IElement>();
        this.subjectListToLoad = new List<ISubject>();
        this.elementListToLoad = new List<IElement>();
    }

    /**
     * Tip: Override this method to add one or more subjects & elemetns in it then call base.Load()
     */
    public virtual void Load()
    {
        for (int i = 0, nb = this.subjectListToLoad.Count; i < nb; i++)
        {
            this.subjectListToLoad[i].Load();
            this.subjectList.Add(this.subjectListToLoad[i]);
        }
        this.subjectListToLoad.Clear();

        for (int i = 0, nb = this.elementListToLoad.Count; i < nb; i++)
        {
            this.elementListToLoad[i].Load();
            this.elementList.Add(this.elementListToLoad[i]);
        }
        this.elementListToLoad.Clear();
    }

    protected void AddSubject(ISubject subject)
    {
        this.subjectListToLoad.Add(subject);
    }
    protected void AddElement(IElement element)
    {
        this.elementListToLoad.Add(element);
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
}
