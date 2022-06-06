using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISimulation
{
    void AddSubject(ISubject subject);

    void Calculate();
}
