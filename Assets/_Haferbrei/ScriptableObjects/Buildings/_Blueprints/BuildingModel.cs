//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "BuildingModel", menuName = "Scriptable Objects/Buildings/BuildingModel", order = 0)]
public class BuildingModel : BaseModel
{
    //--- Properties ---
    #region Properties
    [SerializeField] private Building buildingType;
    public Building BuildingType => buildingType;

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
    #endregion
    //--- ---

    //--- Instantiierung ---
    #region Instantiierung
    public static BuildingModel Instantiate(Building _buildingType)
    {
        var so = CreateInstance<BuildingModel>();
        so.Initialize(_buildingType);
        return so;
    }
    private void Initialize(Building _buildingType)
    {
        buildingType = _buildingType;
    }
    #endregion
    //--- ---
}
}