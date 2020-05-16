//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
[CreateAssetMenu(fileName = "ConstructionSiteModel", menuName = "Scriptable Objects/Models/ConstructionSiteModel", order = 0)]
public class ConstructionSiteModel : SerializedScriptableObject, IModel, ISaveableScriptableObject, IWorldObject
{
    //--- Events ---
    #region Events
    public Action OnModelValuesChanged { get; set; }
    public Action OnModelDestroyed { get; set; }
    #endregion
    //--- ---

    //--- Properties ---
    #region Properties
    [SerializeField][Saveable] private BuildingType buildingType;
    public BuildingType BuildingType => buildingType;
    
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
    
    [SerializeField][Saveable] private IngameTimer progress;
    public IngameTimer Progress => progress;

    #endregion
    //--- ---
    
    //--- Instantiierung ---
    #region Instantiierung
    public void Initialize(BuildingType _buildingType, Vector3 _position, DateTime _birthDate)
    {
        //external
        buildingType = _buildingType;
        position = _position;
        birthDate = _birthDate;
        
        //internal
        progress = new IngameTimer();
        progress.RunTimer(buildingType.constructionSiteBaseDuration, 0.5f);
        
        //register Model
        ModelCollection.RegisterModel(this);
    }

    public void Terminate()
    {
        //unregister Model
        ModelCollection.UnregisterModel(this);
    }
    
    private void OnDestroy() => Terminate();
    #endregion
    //--- ---
}
}