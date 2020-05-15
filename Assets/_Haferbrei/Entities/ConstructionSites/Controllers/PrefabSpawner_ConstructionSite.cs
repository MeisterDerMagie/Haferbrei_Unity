using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

namespace Haferbrei {
public class PrefabSpawner_ConstructionSite : MonoBehaviour, IInitSelf, IInitAfterLoading
{
    public void InitSelf()
    {
        ConstructionSiteInstancer.onNewConstructionSite += SpawnPrefab;
    }
    
    public void InitAfterLoading()
    {
        //Spawn construction site prefabs here
        
        /*
        foreach (var model in allConstructionSiteModels)
        {
            SpawnPrefab(model);
        }
        */

        Debug.Log("Initafterloading");
    }

    private void OnDestroy()
    {
        ConstructionSiteInstancer.onNewConstructionSite -= SpawnPrefab;
    }
    
    private void SpawnPrefab(ConstructionSiteModel _model)
    {
        var constructionSitePrefab = _model.BuildingType.constructionSitePrefab;
        var prefab = LeanPool.Spawn(constructionSitePrefab, _model.Position, Quaternion.identity);
        prefab.GetComponent<ModelDistributor_ConstructionSite>().DistributeModel(_model);
    }
}
}