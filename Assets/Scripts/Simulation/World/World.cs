
using System.Collections.Generic;

public class World : IWorld
{
    public List<ISubject> subjectList;
    public List<IElement> elementList;

    public World()
    {
        this.subjectList = new List<ISubject>();
        this.elementList = new List<IElement>();
    }

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
