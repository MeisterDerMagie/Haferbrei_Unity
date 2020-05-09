//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Haferbrei {
[ExecuteInEditMode]
public class TEST_ChildIndexToTextmehPro : MonoBehaviour
{
    public TextMeshProUGUI text;

    private void Update()
    {
        text.text = transform.GetSiblingIndex().ToString();
        gameObject.name = "GameObject " + transform.GetSiblingIndex();
    }
}
}