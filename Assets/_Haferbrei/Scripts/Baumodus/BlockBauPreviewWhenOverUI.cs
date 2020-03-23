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
    private bool blockedByUIBefore;
    private bool baumodusIsActive;

    [SerializeField, FoldoutGroup("References"), Required] private GameObject bauPreview;

    private void Update()
    {
        if (blockedByUI == blockedByUIBefore || !baumodusIsActive) return;
        bauPreview.SetActive(!MouseInputUIBlocker.BlockedByUI);
        blockedByUIBefore = blockedByUI;
    }

    public void OnEnterBaumodus()
    {
        baumodusIsActive = true;
    }

    public void OnLeaveBaumodus()
    {
        baumodusIsActive = false;
        bauPreview.SetActive(false);
    }
}
}