//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "BuildingModel", menuName = "Scriptable Objects/Buildings/BuildingModel", order = 0)]
public class BuildingModel : SerializedScriptableObject, IIsModel, ISaveableScriptableObject
{
    //--- Events ---
    #region Events
    public Action OnModelValuesChanged { get; set; }
    public Action OnModelDestroyed { get; set; }
    #endregion
    //--- ---
    
    //--- Properties ---
    #region Properties
    [SerializeField][Saveable] private Building buildingType;
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
    #region Instantiierung
    public static BuildingModel Instantiate(Building _buildingType, BuildingModel _template = null)
    {
        BuildingModel so = (_template == null) ? CreateInstance<BuildingModel>() : Instantiate(_template);
        so.Initialize(_buildingType);
        return so;
    }
    public static void Destroy(BuildingModel _model)
    {
        _model.OnModelDestroyed?.Invoke();
        ScriptableObject.Destroy(_model);
    }
    private void Initialize(Building _buildingType)
    {
        buildingType = _buildingType;
        birthDate = IngameDateTime.Now;
    }
    #endregion
    //--- ---
}
}