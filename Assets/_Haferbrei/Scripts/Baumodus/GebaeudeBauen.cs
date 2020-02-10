//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei {
public class GebaeudeBauen : MonoBehaviour
{
    [SerializeField, BoxGroup("Info"), ReadOnly] private GameObject gebaeudePreview;
    [SerializeField, BoxGroup("Info"), ReadOnly] private bool buildingIsAllowed;
    [SerializeField, FoldoutGroup("References"), Required] private Transform previewParent;
    [SerializeField, FoldoutGroup("References"), Required] private RessourceContainer_ContainsCheck enoughRessourcesCheck;
    [SerializeField, FoldoutGroup("References"), Required] private RessourceContainer playerRessourceContainer;
    [SerializeField, BoxGroup("Atom Events"), Required] private BuildingsEvent onZuBauendesGebaeudeChanged;
    [SerializeField, BoxGroup("Atom Values"), Required] private BuildingsVariable zuBauendesGebaeude;


    private void OnEnable() => onZuBauendesGebaeudeChanged.Register(OnZuBauendesGebaeudeChanged);
    private void OnDisable() => onZuBauendesGebaeudeChanged.Unregister(OnZuBauendesGebaeudeChanged);


    private void OnZuBauendesGebaeudeChanged(Buildings _newBuilding)
    {
        if(gebaeudePreview != null) Destroy(gebaeudePreview);

        if (_newBuilding.previewPrefab == null) return;
        
        //set preview
        gebaeudePreview = Instantiate(   _newBuilding.previewPrefab,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).With(z: previewParent.transform.position.z),
            Quaternion.identity,
            previewParent);
            
        //set costs recipe
        enoughRessourcesCheck.SetRessourcesToCheckFor(zuBauendesGebaeude.Value.cost);
    }

    public void SetBuildingIsAllowed(bool _newValue)
    {
        if (_newValue == buildingIsAllowed) return;

        buildingIsAllowed = _newValue;
        OnBuildingIsAllowedChanged();
    }

    public bool GetBuildingIsAllowed() => buildingIsAllowed;

    private void OnBuildingIsAllowedChanged()
    {
        if(gebaeudePreview != null) gebaeudePreview.GetComponent<Building_Preview_SetBuildableMode>().SetBuildableMode(buildingIsAllowed);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && buildingIsAllowed)
        {
            BuildBuilding();
        }
    }

    private void BuildBuilding()
    {
        //pay for the building
        playerRessourceContainer.SubtractRessources(zuBauendesGebaeude.Value.cost);
        
        //instantiate building
        var newBuilding = Instantiate(  zuBauendesGebaeude.Value.instancePrefab,
                               Camera.main.ScreenToWorldPoint(Input.mousePosition).With(z: previewParent.transform.position.z),
                                        Quaternion.identity);
        
        INIT001_Initialize.InitializePrefab(newBuilding.transform);
    }
}
}