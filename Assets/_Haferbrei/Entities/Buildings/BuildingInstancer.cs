using Lean.Pool;
using UnityEngine;

namespace Haferbrei{
public class BuildingInstancer
{
    public static BuildingModel Instantiate(Building _buildingType, Vector3 _position)
    {
        //-- instantiate Model --
        var model = BuildingModel.Instantiate(_buildingType);

        //-- instantiate View --
        var viewPrefab = LeanPool.Spawn(model.BuildingType.instancePrefab, _position, Quaternion.identity);
        viewPrefab.GetComponent<ModelDistributor_Building>().DistributeModel(model);
            //alle möglichen Particles, Sounds, ......

        return model;
    }
}
}