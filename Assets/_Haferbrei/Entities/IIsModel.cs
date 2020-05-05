using System;

namespace Haferbrei{
public interface IIsModel
{
    Action OnModelValuesChanged { get; set; }
    Action OnModelDestroyed { get; set; }
}
}