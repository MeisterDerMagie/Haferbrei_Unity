//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
[CreateAssetMenu(fileName = "BuildingModel", menuName = "Scriptable Objects/Buildings/BuildingModel", order = 0)]
public class BuildingModel : SerializedScriptableObject, IModel, ISaveableScriptableObject
{
    //--- Events ---
    #region Events
    public Action OnModelValuesChanged { get; set; }
    #endregion
    //--- ---
    
    //--- Properties ---
    #region Properties
    [SerializeField][Saveable] internal Building buildingType;
    public Building BuildingType => buildingType;

    [SerializeField][Saveable] private Vector3 position;
    public Vector3 Position
    {
        get => position;
        set { position = value; OnModelValuesChanged?.Invoke();}
    }

    [SerializeField][Saveable] private DateTime birthDate;
    public DateTime BirthDate
    {
        get => birthDate;
        set { birthDate = value; OnModelValuesChanged?.Invoke(); }
    }
    #endregion
    //--- ---

    //--- Instantiierung ---
    #region Initialization
    public void SetInitialValues(Building _buildingType, Vector3 _position, DateTime _birthDate)
    {
        buildingType = _buildingType;
        position = _position;
        birthDate = _birthDate;
    }
    #endregion
    //--- ---
}
}