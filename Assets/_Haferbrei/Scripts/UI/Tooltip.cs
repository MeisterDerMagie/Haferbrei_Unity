//(c) copyright by Martin M. Klöckener
using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using MEC;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Haferbrei {
public class Tooltip : MonoBehaviour, IInitSingletons
{
    [SerializeField, BoxGroup("Settings"), Required] private float showDelay, hideDelay;
    [SerializeField, BoxGroup("References"), Required] private GameObject tooltipObject, titleObject, bodyObject, dividerObject;
    [SerializeField, BoxGroup("References"), Required] private Image icon;
    [SerializeField, BoxGroup("References"), Required] private TextMeshProUGUI title;
    [SerializeField, BoxGroup("References"), Required] private LeanGameObjectPool bodyElementSpanwer_Text, bodyElementSpanwer_Ressources;
    [SerializeField, BoxGroup("References"), Required] private RessourceContainer defaultContainer;
    [SerializeField, BoxGroup("References"), Required] private TooltipPosition positionScript;

    [SerializeField, BoxGroup("Info"), ReadOnly] private bool isEnabled;
    [SerializeField, BoxGroup("Info"), ReadOnly] private bool hasTitle;
    [SerializeField, BoxGroup("Info"), ReadOnly] private bool hasBody;
    [SerializeField, BoxGroup("Info"), ReadOnly] private CoroutineHandle showCountdown, hideCountdown;
    [SerializeField, BoxGroup("Info"), ReadOnly] private HasTooltip currentTooltipSource;
    [SerializeField, BoxGroup("Info"), ReadOnly] private List<GameObject> bodyElements = new List<GameObject>();

    //--- Singleton Behaviour ---
    #region Singleton
    private static Tooltip instance_;
    public static Tooltip Instance
        => instance_ == null ? FindObjectOfType<Tooltip>() : instance_;

    public void InitSingleton()
    {
        if (instance_ == null)
            instance_ = this;
        else
            Destroy(gameObject);
    }
    #endregion
    //--- ---

    public void ShowTooltip(HasTooltip _source)
    {
        Timing.KillCoroutines(hideCountdown);
        currentTooltipSource = _source;
        if(!isEnabled) showCountdown = Timing.CallDelayed(showDelay, EnableTooltip);
        else EnableTooltip();
    }

    public void HideTooltip()
    {
        Timing.KillCoroutines(showCountdown);
        hideCountdown = Timing.CallDelayed(hideDelay, DisableTooltip);
    }

    private void EnableTooltip()
    {
        tooltipObject.SetActive(true);
        ClearTooltip();
        
        //Title
        SetTitle();
        
        //Body
        SetBody();
        
        //Divider
        dividerObject.SetActive(hasTitle && hasBody);

        //Position
        positionScript.UpdatePosition(currentTooltipSource);
        
        isEnabled = true;
    }

    private void DisableTooltip()
    {
        currentTooltipSource = null;
        tooltipObject.SetActive(false);

        isEnabled = false;
    }

    private void ClearTooltip()
    {
        hasTitle = false;
        hasBody = false;
        titleObject.SetActive(false);
        bodyObject.SetActive(false);

        //clear body elements
        for (int i = bodyElements.Count-1; i >= 0; i--)
        {
            LeanPool.Despawn(bodyElements[i]);
            bodyElements.RemoveAt(i);
        }
    }
    
    private void SetTitle()
    {
        if(currentTooltipSource == null) return;
        icon.sprite = currentTooltipSource.tooltipIcon;
        title.text = currentTooltipSource.tooltipTitle;
        
        icon.gameObject.SetActive(icon.sprite != null);
        titleObject.SetActive(!string.IsNullOrEmpty(title.text));
        hasTitle = true;
    }
    
    private void SetBody()
    {
        foreach (var element in currentTooltipSource.bodyElements)
        {
            switch (element.elementType)
            {
                case TooltipBodyElement.ElementType.Text:
                    AddBodyElement(element.text);
                    break;
                case TooltipBodyElement.ElementType.RessourceContainer:
                    AddBodyElement(element.ressourceContainer);
                    break;
                case TooltipBodyElement.ElementType.Recipe:
                    AddBodyElement(element.recipe);
                    break;
                case TooltipBodyElement.ElementType.ModRecipe:
                    AddBodyElement(element.modRecipe);
                    break;
                case TooltipBodyElement.ElementType.Prefab:
                    AddBodyElement(element.prefab);
                    break;
            }
        }
        
        bodyObject.SetActive(hasBody);
    }

    private void AddBodyElement(string _text)
    {
        var newElement = bodyElementSpanwer_Text.Spawn(bodyObject.transform.position, Quaternion.identity, bodyObject.transform);
        bodyElements.Add(newElement);
        newElement.GetComponent<TextMeshProUGUI>().text = _text;
        
        hasBody = true;
    }

    private void AddBodyElement(RessourceContainer _container)
    {
        var newElement = bodyElementSpanwer_Ressources.Spawn(bodyObject.transform.position, Quaternion.identity, bodyObject.transform);
        bodyElements.Add(newElement);
        newElement.GetComponent<RessourceBar>().SetContainer(_container);

        hasBody = true;
    }

    private void AddBodyElement(RessourceRecipe _recipe)
    {
        var newElement = bodyElementSpanwer_Ressources.Spawn(bodyObject.transform.position, Quaternion.identity, bodyObject.transform);
        bodyElements.Add(newElement);
        defaultContainer.SetRessources(_recipe);
        newElement.GetComponent<RessourceBar>().SetContainer(defaultContainer);
        
        hasBody = true;
    }

    private void AddBodyElement(ModRessourceRecipe _recipe)
    {
        var newElement = bodyElementSpanwer_Ressources.Spawn(bodyObject.transform.position, Quaternion.identity, bodyObject.transform);
        bodyElements.Add(newElement);
        defaultContainer.SetRessources(_recipe);
        newElement.GetComponent<RessourceBar>().SetContainer(defaultContainer);
        
        hasBody = true;
    }

    private void AddBodyElement(GameObject _prefab)
    {
        var newElement = LeanPool.Spawn(_prefab, bodyObject.transform);
        bodyElements.Add(newElement);
        
        hasBody = true;
    }
}
}