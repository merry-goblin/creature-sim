
using System.Collections.Generic;

public class World : IWorld
{
    public List<ISubject> subjectList;

    public World()
    {
        this.subjectList = new List<ISubject>();
    }

    public void AddSubject(ISubject subject)
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
