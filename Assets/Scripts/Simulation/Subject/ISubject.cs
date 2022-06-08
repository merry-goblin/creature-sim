using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISubject
{
    public List<float> GetOutput();

    public void Update();
}
