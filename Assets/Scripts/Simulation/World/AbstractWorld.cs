
using System.Collections.Generic;

public abstract class AbstractWorld
{
    public List<ISubject> subjectList;
    public List<IElement> elementList;

    public AbstractWorld()
    {
        this.subjectList = new List<ISubject>();
        this.elementList = new List<IElement>();
    }

    /**
     * Tip: Override this method to add one or more subjects & elemetns in it then call base.Load()
     */
    public virtual void Load()
    {
        for (int i = 0, nb = this.subjectList.Count; i < nb; i++)
        {
            this.subjectList[i].Load();
        }

        for (int i = 0, nb = this.elementList.Count; i < nb; i++)
        {
            this.elementList[i].Load();
        }
    }

    protected void AddSubject(ISubject subject)
    {
        this.subjectList.Add(subject);
    }
    protected void AddElement(IElement element)
    {
        this.elementList.Add(element);
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
