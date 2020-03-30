using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.Tags;
using UnityEngine;

public class Building_ShowGroundPlaneOnCollisionWithPreview : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEnter! HasTag = " + other.gameObject.HasTag("BuildingPreview"));
        if (!other.gameObject.HasTag("BuildingPreview")) return;

        other.GetComponent<BuildingGraphicSwitch>().GraphicMode = BuildingGraphicSwitch.BuildingGraphicMode.GroundPlane;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.HasTag("BuildingPreview")) return;

        other.GetComponent<BuildingGraphicSwitch>().GraphicMode = BuildingGraphicSwitch.BuildingGraphicMode.Building;
    }
}
