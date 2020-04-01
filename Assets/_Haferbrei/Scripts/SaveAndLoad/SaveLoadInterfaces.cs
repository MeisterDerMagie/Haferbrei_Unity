namespace Haferbrei{
public interface ISaveablePrefab
{
    //SaveableObjectData SaveData();
    //void LoadData(SaveableObjectData _loadedObjectData);
    //void InitSaveable();
    //void OnDestroy();
}

public interface ISaveableComponent
{
    SaveableData StoreData();
    void RestoreData(SaveableData _loadedData);
}

public interface ISaveableScriptableObject
{
}
}