using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemCategory
{
    Material,
    Craftable,
    Weapon,
    Consumable
}

public class ItemSO : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite icon;
    public ItemCategory itemType;
}
