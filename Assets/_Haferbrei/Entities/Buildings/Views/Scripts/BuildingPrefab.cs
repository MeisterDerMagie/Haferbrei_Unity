﻿//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class BuildingPrefab : MonoBehaviour, IModelReceiver<BuildingModel>
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
    }

    private void OnDisable()
    {
        model.OnModelValuesChanged -= OnModelValuesChanged;
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