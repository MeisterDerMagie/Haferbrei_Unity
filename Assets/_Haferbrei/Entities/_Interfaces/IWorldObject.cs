using System;
using UnityEngine;

namespace Haferbrei{
public interface IWorldObject
{
    Vector3 Position { get; set; }
    DateTime BirthDate { get; set; }
}
}