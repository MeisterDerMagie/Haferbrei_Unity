//(c) copyright by Martin M. Klöckener
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace Haferbrei {
public class ModelCollection : SerializedMonoBehaviour, IInitSingletons
{
    [OdinSerialize] private readonly Dictionary<Type, List<IModel>> allModels = new Dictionary<Type, List<IModel>>();
    
    //--- Singleton Behaviour ---
    #region Singleton
    private static ModelCollection instance_;
    public static ModelCollection Instance
        => instance_ == null ? FindObjectOfType<ModelCollection>() : instance_;

    public void InitSingleton()
    {
        if (instance_ == null)
            instance_ = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(this);
    }
    #endregion
    //--- ---

    public static void RegisterModel(IModel _model)
    {
        Type modelType = _model.GetType();
        
        if(!Instance.allModels.ContainsKey(modelType)) Instance.allModels.Add(modelType, new List<IModel>());
        Instance.allModels[modelType].Add(_model);
    }

    public static void UnregisterModel(IModel _model)
    {
        if (Instance == null) return;
        
        Type modelType = _model.GetType();
        
        if(Instance.allModels[modelType].Contains(_model)) Instance.allModels[modelType].Remove(_model);
    }
    
    public static List<T> GetModels<T>() where T : IModel
    {
        Type modelType = typeof(T);

        if (!Instance.allModels.ContainsKey(modelType)) Instance.allModels.Add(modelType, new List<IModel>());
        return Instance.allModels[modelType].OfType<T>().ToList();
    }
}
}