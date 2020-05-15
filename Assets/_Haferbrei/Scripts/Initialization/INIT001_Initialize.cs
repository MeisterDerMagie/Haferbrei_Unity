using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using MEC;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace Haferbrei{
[DisallowMultipleComponent]
public class INIT001_Initialize : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] public bool initOnSceneStart = true;
    [SerializeField, BoxGroup("Settings")] public bool initOnAwake = false;
    [SerializeField, BoxGroup("Settings")] public bool detachChildrenAndDestroySelf = true;
    [SerializeField, ReadOnly] public List<IInitSingletons> singletonInits = new List<IInitSingletons>();
    [SerializeField, ReadOnly] public List<SaveablePrefab> saveablePrefabs  = new List<SaveablePrefab>();
    [SerializeField, ReadOnly] public List<IInitSelf> selfInits = new List<IInitSelf>();
    [SerializeField, ReadOnly] public List<IInitDependencies> dependentInits = new List<IInitDependencies>();
    [SerializeField, ReadOnly] public List<IInitAfterLoading> afterLoadingInits = new List<IInitAfterLoading>();

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
        Debug.Log("start initializing in scene " + gameObject.scene.name + ", goName: " + gameObject.name, gameObject);
        Timing.RunCoroutine(_Initialize());
    }
    
    private IEnumerator<float> _Initialize()
    {
        ClearLists();

        GetSingletons(transform);
        InitSingletons();

        yield return Timing.WaitForOneFrame;
        
        GetSaveablePrefabs(transform, saveablePrefabs);
        InitSaveablePrefabs(saveablePrefabs);
        
        yield return Timing.WaitForOneFrame;
        
        GetSelfAndDependent(transform, selfInits, dependentInits);
        InitSelfAndDependents(selfInits,dependentInits);
        
        yield return Timing.WaitForOneFrame;

        GetLoadInits(transform, afterLoadingInits);
        InitAfterLoadings(afterLoadingInits);

        yield return Timing.WaitForOneFrame;
        
        //System.GC.Collect(); //Force Garbage Collection

        gameObject.SetActive(true); //Initialization is finished --> activate Scene

        //Alle children detachen, damit sie im root sind und nicht unnötige Transform updates senden, wenn sie sich bewegen
        if(detachChildrenAndDestroySelf) transform.DetachChildren();

        isInitialized = true;
        onInitializeFinished.Invoke();
        
        if (detachChildrenAndDestroySelf) Destroy(gameObject);
        else Destroy(this);
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
    private static void GetSaveablePrefabs(Transform _root, List<SaveablePrefab> _saveablesList, bool _getRoot = false)
    {
        SaveablePrefab[] _saveable  = _root.GetComponents<SaveablePrefab>();
        _saveablesList.AddRange(_saveable);

        foreach(Transform t in _root)
        {
            if(t == _root && !_getRoot) continue;      //make sure you don't initialize the existing transform
            GetSaveablePrefabs(t, _saveablesList, _getRoot); //get this Transform's children recursively
        }
    }
    private static void InitSaveablePrefabs(List<SaveablePrefab> _saveablesList)
    {
        foreach(SaveablePrefab saveablePrefab in _saveablesList)
        {
            saveablePrefab.InitSaveablePrefab();
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
    private static void InitSelfAndDependents(List<IInitSelf> _initSelvesList, List<IInitDependencies> _initDependenciesList)
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
    
    //--- Init After Loading ---
    #region Init After Loading
    private static void GetLoadInits(Transform _root, List<IInitAfterLoading> _afterLoadingList, bool _getRoot = false)
    {
        IInitAfterLoading[] initsAfterLoading = _root.GetComponents<IInitAfterLoading>();
        _afterLoadingList.AddRange(initsAfterLoading);

        foreach (Transform t in _root)
        {
            if (t == _root && !_getRoot) continue;
            GetLoadInits(t, _afterLoadingList, _getRoot);
        }
    }

    private static void InitAfterLoadings(List<IInitAfterLoading> _afterLoadingList)
    {
        foreach (var _init in _afterLoadingList)
        {
            _init.InitAfterLoading();
        }
    }
    #endregion
    //--- ---
    
    //--- Initialize newly generated Prefab ---
    public static void InitializePrefab(Transform _gameObjectToInitialize)
    {
        var saveableInitsInPrefab = new List<SaveablePrefab>();
        var selfInitsInPrefab = new List<IInitSelf>();
        var dependentInitsInPrefab = new List<IInitDependencies>();

        GetSaveablePrefabs(_gameObjectToInitialize, saveableInitsInPrefab, true);
        InitSaveablePrefabs(saveableInitsInPrefab);
        
        GetSelfAndDependent(_gameObjectToInitialize, selfInitsInPrefab, dependentInitsInPrefab, true);
        InitSelfAndDependents(selfInitsInPrefab, dependentInitsInPrefab);
    }
    //--- ---

    private void ClearLists()
    {
        singletonInits.Clear();
        selfInits.Clear();
        dependentInits.Clear();
        saveablePrefabs.Clear();
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