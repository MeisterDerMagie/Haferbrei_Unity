using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Haferbrei{
public class Ui_IngameDateTime : MonoBehaviour
{
    [SerializeField, BoxGroup("References"), Required] private TextMeshProUGUI dateTimeText;
    
    private void OnEnable()
    {
        IngameDateTime.OnNewMonth += UpdateView;
        UpdateView();
    }

    private void OnDestroy() => IngameDateTime.OnNewMonth -= UpdateView;

    private void UpdateView()
    {
        dateTimeText.text = IngameDateTime.Now.ToString("MMMM yyyy");
    }
}
}