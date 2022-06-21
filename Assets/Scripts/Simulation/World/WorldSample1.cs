
using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldSample1 : World
{
    public WorldSample1() : base()
    {
    }

    public override void Load()
    {
        SubjectSample1 subject = new SubjectSample1();

        this.AddSubject(subject);

        base.Load();
    }
}
