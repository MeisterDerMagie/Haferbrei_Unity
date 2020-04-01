//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using Wichtel.Extensions;
using BindingFlags = System.Reflection.BindingFlags;

namespace Haferbrei {
[ExecuteInEditMode]
[HideMonoScript]
public class SaveableComponent : MonoBehaviour, ISaveableComponent
{
    [InfoBox("Missing Initializer in Parent!", InfoMessageType.Error, "missingInitializer")]
    [InfoBox("Missing SaveablePrefab in Parent!", InfoMessageType.Error, "missingSaveablePrefab")]
    [SerializeField, DisplayAsString, LabelText("Guid")] private string componentGuid;

    [SerializeField] protected Component componentToSave;
    
    private bool missingInitializer => (!this.IsAssetOnDisk()) && !Application.isPlaying && (GetComponentsInParent<INIT001_Initialize>(true).Length == 0); //für Odin
    private bool missingSaveablePrefab => ( this.IsAssetOnDisk() && GetSaveablePrefab() == null); //für Odin
    
    public SaveableData StoreData() => new SaveableData(componentToSave, componentGuid, "on gameObject: " + gameObject.name);

    public void RestoreData(SaveableData _loadedData) => _loadedData.PopulateObject(componentToSave);

    public void SetComponentGuid(Guid _guid)
    {
        componentGuid = _guid.ToString();
    }
    public Guid GetComponentGuid() => Guid.Parse(componentGuid);
    
    private SaveablePrefab GetSaveablePrefab()
    {
        SaveablePrefab saveablePrefab = null;

        if (GetComponent<SaveablePrefab>() != null)
        {
            return GetComponent<SaveablePrefab>();
        }
        if(GetComponentsInParent<SaveablePrefab>().Length > 0)
        {
            return GetComponentsInParent<SaveablePrefab>()[0];
        }

        return null;
    }
    
    #if UNITY_EDITOR
    public virtual void OnValidate()
    {
        if (Application.isPlaying) return;
        Guid checkGuid = Guid.Empty;
        Guid.TryParse(componentGuid, out checkGuid);
        if (checkGuid != Guid.Empty) return; 
        Guid newGuid = (this.IsAssetOnDisk()) ? Guid.Empty : Guid.NewGuid();
        SetComponentGuid(newGuid);
    }
    #endif
}
}