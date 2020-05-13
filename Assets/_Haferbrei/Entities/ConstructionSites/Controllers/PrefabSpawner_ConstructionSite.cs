using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei {
public class PrefabSpawner_ConstructionSite : MonoBehaviour, IInitSelf
{
    public void InitSelf()
    {
        ConstructionSiteInstancer.onNewConstructionSite += SpawnPrefab;
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