//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Wichtel.Extensions;

namespace Haferbrei {
public class DisableButtonWhenInputFieldIsEmpty : MonoBehaviour
{
    [SerializeField, BoxGroup("References"), Required] private TMP_InputField inputField;
    [SerializeField, BoxGroup("References"), Required] private Button button;

    private void OnEnable() => UpdateState();

    //call this on the "onValueChanged" of the inputField
    public void UpdateState()
    {
        button.interactable = !inputField.text.IsNullOrEmptyOrWhiteSpace();
    }
}
}