﻿
public interface IWorld
{
    public delegate void NoMoreActiveSubjectsDelegate();
    public event NoMoreActiveSubjectsDelegate OnNoMoreActiveSubjects;

    void Load();

    void Unload();

    void Update();

    public bool IsActive();
}
