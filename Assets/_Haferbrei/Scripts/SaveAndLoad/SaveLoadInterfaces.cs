namespace Haferbrei{
public interface ISaveable
{
    SaveableData SaveData();
    void LoadData(SaveableData _loadedData);
    void InitSaveable();
    void OnDestroy();
}

public interface IStoreable
{
    SaveableComponentData StoreData();
    void RestoreData(SaveableComponentData _loadedData);
    void OnDestroy();
}

public interface ISaveableScriptableObject
{
    SaveableData SaveData();
    void LoadData(SaveableData _loadedData);
    void InitSaveable();
}
}