//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class IngameTimerVisualizer : MonoBehaviour, IModelReceiver<ConstructionSiteModel>
{
    [SerializeField, BoxGroup("References"), Required] private Image fillImage;
    
    //--- Model ---
    #region Model
    [SerializeField, BoxGroup("Model")] private ConstructionSiteModel model;

    public void SetModel(ConstructionSiteModel _model)
    {
        if (model != null) model.Progress.onProgressChanged -= UpdateProgress;
        
        model = _model;
        if (!Application.isPlaying) return;
        
        //-- OnReceivedModel --
        model.Progress.onProgressChanged += UpdateProgress;
        //-- --
    }

    private void OnEnable()
    {
        if (model != null)
        {
            model.OnModelValuesChanged += OnModelValuesChanged;
        }
    }

    private void OnDestroy()
    {
        if (model != null)
        {
            model.OnModelValuesChanged -= OnModelValuesChanged;
        }
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

    private void UpdateProgress()
    {
        fillImage.fillAmount = model.Progress.Progress;
    }
}
}