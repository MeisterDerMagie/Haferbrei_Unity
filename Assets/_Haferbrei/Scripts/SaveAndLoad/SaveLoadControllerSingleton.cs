using UnityEngine;

namespace Haferbrei{
public class SaveLoadControllerSingleton : MonoBehaviour
{
    public SaveLoadController SaveLoadController;
    
    //--- Singleton Behaviour ---
    #region Singleton
    private static SaveLoadControllerSingleton instance_;
    public static SaveLoadControllerSingleton Instance
        => instance_ == null ? FindObjectOfType<SaveLoadControllerSingleton>() : instance_;

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