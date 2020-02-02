//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wichtel.Extensions;

namespace Haferbrei {
public class GameObjectFollowPointer : MonoBehaviour
{
    private Camera mainCamera;
    private void Awake() => mainCamera = Camera.main;
    private void Update() => transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition).With(z: transform.position.z);
}
}