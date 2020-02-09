namespace Haferbrei{
public interface ISaveable
{
    //string ID { get; set; }
    SaveableData SaveData();
    void LoadData(SaveableGameObjectData _loadedData);
    void InitSaveable();
    void OnDestroy();
}

public interface IStoreable
{
    SaveableComponentData StoreData();
    void RestoreData(SaveableComponentData _loadedData);
    void InitStoreable();
    void OnDestroy();
}
}