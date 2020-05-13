using System;

namespace Haferbrei{
public interface IModel
{
    Action OnModelValuesChanged { get; set; }
}
}