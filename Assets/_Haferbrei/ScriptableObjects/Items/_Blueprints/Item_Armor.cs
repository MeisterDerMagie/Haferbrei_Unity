//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Haferbrei {

[CreateAssetMenu(fileName = "Armor", menuName = "Scriptable Objects/Items/Armor", order = 0)]
public class Item_Armor : Item
{
    public int armorRating;
}
}