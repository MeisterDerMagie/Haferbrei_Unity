using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using MEC;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace Haferbrei{
public class INIT001_Initialize : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] public bool initOnSceneStart = true;
    [SerializeField, BoxGroup("Settings")] public bool initOnAwake = false;
    [SerializeField, ReadOnly] public List<IInitSingletons>   singletonInits = new List<IInitSingletons>();
    [SerializeField, ReadOnly] public List<ISaveable>         saveableInits  = new List<ISaveable>();
    //[SerializeField, ReadOnly] public List<IStoreable>        storeableInits = new List<IStoreable>();
    [SerializeField, ReadOnly] public List<IInitSelf>         selfInits      = new List<IInitSelf>();
    [SerializeField, ReadOnly] public List<IInitDependencies> dependentInits = new List<IInitDependencies>();

    [SerializeField, BoxGroup("OnInitializeFinished"), Required] private UnityEvent onInitializeFinished;
    [SerializeField, ReadOnly] private bool isInitialized;
    
    private void Awake()
    {
        if (!isInitialized)
        {
            gameObject.SetActive(false); //wird der Initializer hier inaktiv gesetzt, wird kein Awake, Start, OnEnable der Kinder aufgerufen. Nichtmal OnDisable!
            //Man könnte also hier die Hierarchy inaktiv setzen, dann alles initialisieren (bei Spielstart) und dann erst zu einem späteren Zeitpunkt aktivieren.
        }

        if (initOnAwake) StartInitialization();
    }

    [Button, DisableInEditorMode]
    public void StartInitialization()
    {
        Debug.Log("start initializing in scene " + gameObject.scene.name, gameObject);
        Timing.RunCoroutine(_Initialize());
    }
    
    private IEnumerator<float> _Initialize()
    {
        ClearLists();

        GetSingletons(transform);
        InitSingletons();

        yield return Timing.WaitForOneFrame;
        
        GetSaveables(transform, saveableInits);
        InitSaveables(saveableInits);
        
        yield return Timing.WaitForOneFrame;
        
        GetSelfAndDependent(transform, selfInits, dependentInits);
        InitSelfAndDepentents(selfInits,dependentInits);
        
        yield return Timing.WaitForOneFrame;

        //System.GC.Collect(); //Force Garbage Collection

        gameObject.SetActive(true); //Initialization is finished --> activate Scene

        //Alle children detachen, damit sie im root sind und nicht unnötige Transform updates senden, wenn sie sich bewegen
        transform.DetachChildren();

        isInitialized = true;
        onInitializeFinished.Invoke();

        Destroy(gameObject);
    }
    
    //--- Init Singletons ---
    #region Init Singletons
    private void GetSingletons(Transform _root)
    {
        IInitSingletons[] _singleton = _root.GetComponents<IInitSingletons>();
        singletonInits.AddRange(_singleton);

        foreach(Transform t in _root)
        {
            if(t == _root) continue;  //make sure you don't initialize the existing transform
            GetSingletons(t);        //initialize this Transform's children recursively
        }
    }
    private void InitSingletons()
    {
        foreach(IInitSingletons _singleton in singletonInits)
        {
            _singleton.InitSingleton();
        }
    }
    #endregion
    //--- ---
    
    //--- Init Saveables ---
    #region Init Saveables
    private static void GetSaveables(Transform _root, List<ISaveable> _saveablesList, bool _getRoot = false)
    {
        ISaveable[] _saveable  = _root.GetComponents<ISaveable>();
        _saveablesList.AddRange(_saveable);

        foreach(Transform t in _root)
        {
            if(t == _root && !_getRoot) continue;      //make sure you don't initialize the existing transform
            GetSaveables(t, _saveablesList, _getRoot); //get this Transform's children recursively
        }
    }    
    private static void InitSaveables(List<ISaveable> _saveablesList)
    {
        foreach(ISaveable _saveable in _saveablesList)
        {
            _saveable.InitSaveable();
        }
    }
    #endregion
    //--- ---

    //--- Init Self and Dependents ---
    #region Init Self and Dependents
    private static void GetSelfAndDependent(Transform _root, List<IInitSelf> _initSelvesList, List<IInitDependencies> _initDependenciesList, bool _getRoot = false)
    {
        IInitSelf[]         _self      = _root.GetComponents<IInitSelf>();
        IInitDependencies[] _dependent = _root.GetComponents<IInitDependencies>();

        _initSelvesList.AddRange(_self);
        _initDependenciesList.AddRange(_dependent);

        foreach(Transform t in _root)
        {
            if(t == _root && !_getRoot) continue;  //make sure you don't initialize the existing transform
            GetSelfAndDependent(t, _initSelvesList, _initDependenciesList, _getRoot);      //initialize this Transform's children recursively
        }
    }
    private static void InitSelfAndDepentents(List<IInitSelf> _initSelvesList, List<IInitDependencies> _initDependenciesList)
    {
        foreach(IInitSelf _self in _initSelvesList)
        {
            _self.InitSelf();
        }
        foreach(IInitDependencies _dependent in _initDependenciesList)
        {
            _dependent.InitDependencies();
        }
    }
    #endregion
    //--- ---
    
    //--- Initialize newly generated Prefab ---
    public static void InitializePrefab(Transform _gameObjectToInitialize)
    {
        var saveableInitsInPrefab = new List<ISaveable>();
        var selfInitsInPrefab = new List<IInitSelf>();
        var dependentInitsInPrefab = new List<IInitDependencies>();

        GetSaveables(_gameObjectToInitialize, saveableInitsInPrefab, true);
        InitSaveables(saveableInitsInPrefab);
        
        GetSelfAndDependent(_gameObjectToInitialize, selfInitsInPrefab, dependentInitsInPrefab, true);
        InitSelfAndDepentents(selfInitsInPrefab, dependentInitsInPrefab);
    }
    //--- ---

    private void ClearLists()
    {
        singletonInits.Clear();
        selfInits.Clear();
        dependentInits.Clear();
        saveableInits.Clear();
    }
    
    #if UNITY_EDITOR
    private void OnValidate()
    {
        ClearLists();
        GetSingletons(transform);
        GetSelfAndDependent(transform, selfInits, dependentInits);
    }
    #endif
}
}