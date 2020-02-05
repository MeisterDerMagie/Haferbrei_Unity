//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class BlockBauPreviewWhenOverUI : MonoBehaviour
{
    [ReadOnly] public bool blockedByUI => MouseInputUIBlocker.BlockedByUI;
    
    [SerializeField, FoldoutGroup("References"), Required] private GameObject bauPreview;

    private void Update()
    {
        bauPreview.SetActive(!MouseInputUIBlocker.BlockedByUI);
    }
}
}