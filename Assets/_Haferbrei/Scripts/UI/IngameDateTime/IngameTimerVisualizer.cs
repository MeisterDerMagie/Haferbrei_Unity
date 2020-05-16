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
        UnsubscribeFromModelEvents();
        
        model = _model;
        if (!Application.isPlaying) return;
        
        //-- OnReceivedModel --
        SubscribeToModelEvents();
        //-- --
    }

    private void OnDestroy()
    {
        UnsubscribeFromModelEvents();
    }

    private void UnsubscribeFromModelEvents()
    {
        if (model == null) return;
        model.Progress.onProgressChanged -= UpdateProgress;
    }

    private void SubscribeToModelEvents()
    {
        if (model == null) return;
        model.Progress.onProgressChanged += UpdateProgress;
    }
    #endregion
    //--- ---

    private void UpdateProgress()
    {
        fillImage.fillAmount = model.Progress.Progress;
    }
}
}