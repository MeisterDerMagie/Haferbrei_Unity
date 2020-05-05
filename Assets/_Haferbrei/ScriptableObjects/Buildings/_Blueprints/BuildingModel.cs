//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "BuildingModel", menuName = "Scriptable Objects/Buildings/BuildingModel", order = 0)]
public class BuildingModel : BaseModel, ISaveableScriptableObject
{
    //--- Events ---
    #region Events
    public static Action<BuildingModel> OnNewBuildingModel;
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
        set { position = value; onModelChanged?.Invoke();}
    }

    [SerializeField][Saveable] private DateTime birthDate;
    public DateTime BirthDate
    {
        get => birthDate;
        set { birthDate = value; onModelChanged?.Invoke(); }
    }
    #endregion
    //--- ---

    //--- Instantiierung ---
    #region Instantiierung
    public static BuildingModel Instantiate(Building _buildingType, BuildingModel _template = null)
    {
        BuildingModel so = (_template == null) ? CreateInstance<BuildingModel>() : Instantiate(_template);
        so.Initialize(_buildingType);
        OnNewBuildingModel?.Invoke(so);
        return so;
    }
    public static void Destroy(BuildingModel _model)
    {
        _model.onModelDestroyed?.Invoke();
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