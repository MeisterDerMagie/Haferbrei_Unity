﻿//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {
public class SpielstandWirklichLoeschenCallback : MonoBehaviour
{
    public void Yes()
    {
        SaveFileListUI.onDeleteCurrentlySelectedSpielstand?.Invoke();
    }
}
}