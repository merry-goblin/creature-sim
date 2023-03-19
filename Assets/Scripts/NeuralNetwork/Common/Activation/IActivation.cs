using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActivation
{
    float Filter(float value);

    float UnfilterDerivative(float value);
}
