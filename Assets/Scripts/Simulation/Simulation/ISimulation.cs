using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISimulation
{
    public delegate void NoMoreActiveWorldsDelegate();
    public event NoMoreActiveWorldsDelegate OnNoMoreActiveWorlds;

    void Load();

    void Update();
}
