
using System.Collections.Generic;

public class Simulation : ISimulation
{
    public const int AutoPlayMode = 1;
    public const int ManualPlayMode = 2;

    protected int playMode;

    public List<ISubject> subjectList;

    public Simulation(int playMode = Simulation.AutoPlayMode)
    {
        this.playMode = playMode;

        this.subjectList = new List<ISubject>();
    }

    public void AddSubject(ISubject subject)
    {
        this.subjectList.Add(subject);
    }

    public void Update()
    {
        for (int i=0, nb=subjectList.Count; i < nb; i++)
        {
            this.subjectList[i].Update();
        }
    }
}
