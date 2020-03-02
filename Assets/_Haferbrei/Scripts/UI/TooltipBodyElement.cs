using System;
using I2.Loc;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Haferbrei{
[Serializable]
public class TooltipBodyElement
{
    public enum ElementType { Text, RessourceContainer, Recipe, ModRecipe, Prefab }

    public ElementType elementType;
    
    [ShowIf("typeIsText")] public LocalizedString text;
    [ShowIf("typeIsContainer")] public RessourceContainer ressourceContainer;
    [ShowIf("typeIsRecipe")] public RessourceRecipe recipe;
    [ShowIf("typeIsModRecipe")] public ModRessourceRecipe modRecipe;
    [ShowIf("typeIsPrefab")] public GameObject prefab;
    [ShowIf("typeIsRessources")] public bool compareToPlayerRessources = true;
    
    //-- for Odin --
    private bool typeIsText => (elementType == ElementType.Text);
    private bool typeIsContainer => (elementType == ElementType.RessourceContainer);
    private bool typeIsRecipe => (elementType == ElementType.Recipe);
    private bool typeIsModRecipe => (elementType == ElementType.ModRecipe);
    private bool typeIsPrefab => (elementType == ElementType.Prefab);
    private bool typeIsRessources => (elementType == ElementType.RessourceContainer || elementType == ElementType.Recipe || elementType == ElementType.ModRecipe);
    //-- --
}
}