namespace Haferbrei{
public interface ISaveable
{
    //string ID { get; set; }
    SaveableData SaveData();
    void LoadData();
    void InitSaveable();
    void OnDestroy();
}
}