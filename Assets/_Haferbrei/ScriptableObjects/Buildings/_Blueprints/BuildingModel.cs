//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "BuildingModel", menuName = "Scriptable Objects/Buildings/BuildingModel", order = 0)]
public class BuildingModel : BaseModel
{
    [SerializeField] private Vector3 position;
    public Vector3 Position
    {
        get => position;
        set { position = value; onModelChanged?.Invoke();}
    }

    [SerializeField] private DateTime birthDate;
    public DateTime BirthDate
    {
        get => birthDate;
        set { birthDate = value; onModelChanged?.Invoke(); }
    }
}
}