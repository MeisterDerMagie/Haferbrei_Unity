using UnityEngine;

namespace Haferbrei{
public class PrefabCollectionsSingletons : MonoBehaviour
{
    public PrefabCollection allPrefabs;
    
    //--- Singleton Behaviour ---
    #region Singleton
    private static PrefabCollectionsSingletons instance_;
    public static PrefabCollectionsSingletons Instance
        => instance_ == null ? FindObjectOfType<PrefabCollectionsSingletons>() : instance_;

    public void Awake()
    {
        if (instance_ == null)
        {
            instance_ = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    #endregion
    //--- ---
}
}