namespace Haferbrei{
public interface IModelReceiver<T> where T : IModel
{
    void SetModel(T _model);
}
}