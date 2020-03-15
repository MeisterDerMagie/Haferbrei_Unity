namespace Haferbrei{
public interface ISaveable
{
    SaveableObjectData SaveData();
    void LoadData(SaveableObjectData _loadedObjectData);
    void InitSaveable();
    void OnDestroy();
}

public interface IStoreable
{
    SaveableData StoreData();
    void RestoreData(SaveableData _loadedData);
    void OnDestroy();
}

public interface ISaveableScriptableObject
{
    SaveableScriptableObjectData SaveData();
    void LoadData(SaveableScriptableObjectData _loadedObjectData);
    void InitSaveable();
}
}