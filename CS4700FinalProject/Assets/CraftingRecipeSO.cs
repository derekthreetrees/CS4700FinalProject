using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public ItemSO item;
    public int amount;
}

public class CraftingRecipeSO : ScriptableObject
{
    public ItemSO outputItem;
    public int outputAmount = 1;

    public List<Ingredient> ingredients;
}
