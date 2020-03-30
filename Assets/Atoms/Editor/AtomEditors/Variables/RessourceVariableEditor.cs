using UnityEditor;
using UnityAtoms.Editor;
using Haferbrei;


namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `Ressource`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(RessourceVariable))]
    public sealed class RessourceVariableEditor : AtomVariableEditor<Ressource, RessourcePair> { }
}
