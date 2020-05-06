//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class TEST_BuildingModelReceiver : MonoBehaviour, IModelReceiver<BuildingModel>
{
    //--- Model ---
    #region Model
    [SerializeField, BoxGroup("Model")] private BuildingModel model;

    public void SetModel(BuildingModel _model)
    {
        model = _model;
        if (!Application.isPlaying) return;
        
        //-- OnReceivedModel --
        
        //-- --
    }

    private void OnEnable()
    {
        model.OnModelValuesChanged += OnModelValuesChanged;
        model.OnModelDestroyed += OnModelDestroyed;
    }

    private void OnDisable()
    {
        model.OnModelValuesChanged -= OnModelValuesChanged;
        model.OnModelDestroyed -= OnModelDestroyed;
    }

    private void OnModelValuesChanged()
    {
        throw new NotImplementedException();
    }

    private void OnModelDestroyed()
    {
        throw new NotImplementedException();
    }
    #endregion
    //--- ---
}
}