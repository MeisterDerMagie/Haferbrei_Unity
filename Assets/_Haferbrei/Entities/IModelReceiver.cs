namespace Haferbrei{
public interface IModelReceiver<T> where T : IIsModel
{
    void SetModel(T _model);
}
}