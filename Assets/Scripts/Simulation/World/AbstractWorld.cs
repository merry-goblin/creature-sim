
using System.Collections.Generic;

public abstract class AbstractWorld
{
    public List<ISubject> subjectList;
    
    public virtual void Load()
    {

    }

    protected void AddSubject(ISubject subject)
    {
        this.subjectList.Add(subject);
    }

    public void Update()
    {
        for (int i = 0, nb = subjectList.Count; i < nb; i++)
        {
            this.subjectList[i].Update();
        }
    }
}
