using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BuildingGraphicSwitch : MonoBehaviour, IInitSelf
{
    public enum BuildingGraphicMode
    {
        ALL,
        Building,
        GroundPlane,
    }

    [SerializeField, OnValueChanged("OnGraphicModeChanged"), BoxGroup("Mode")]
    private BuildingGraphicMode graphicMode;

    public BuildingGraphicMode GraphicMode
    {
        get => graphicMode;
        set
        {
            if(graphicMode == value) return;
            graphicMode = value;
            OnGraphicModeChanged();}
    }

    [SerializeField, BoxGroup("References"), Required] private List<BuildingGraphic> graphics;

    public void InitSelf() => GraphicMode = BuildingGraphicMode.Building;

    private void OnGraphicModeChanged()
    {
        if (graphicMode == BuildingGraphicMode.ALL)
        {
            foreach (var graphic in graphics)
            {
                graphic.graphicObject.SetActive(true);
            }
            return;
        }
        
        foreach (var graphic in graphics)
        {
            graphic.graphicObject.SetActive(graphic.mode == graphicMode);
        }
    }

    [Serializable]
    private struct BuildingGraphic
    {
        public BuildingGraphicMode mode;
        [Required] public GameObject graphicObject;
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (BuildingGraphicMode mode in (BuildingGraphicMode[]) Enum.GetValues(typeof(BuildingGraphicMode)))
        {
            if (mode == BuildingGraphicMode.ALL) continue;
            
            bool isAlreadySet = false;
            foreach (var graphic in graphics)
            {
                if (graphic.mode == mode) isAlreadySet = true;
            }
            if(isAlreadySet) continue;
            
            graphics.Add(new BuildingGraphic{mode =  mode, graphicObject = null});
        }
        
        OnGraphicModeChanged();
    }
    #endif
}
