using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    None,
    Weapon,
    Healing,
    Consumable
}

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public ItemType itemType;
    public int quantity = 1;
    public Transform player;
    private TMP_Text quantityText;

    public int healAmount;
    public int stamRegen;
    public int damage;
    public float attackCooldown = 1f;

    private void Awake()
    {
        quantityText = GetComponentInChildren<TMP_Text>();
        UpdateQuantityDisplay();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogWarning("Player object not found in scene for item: " + Name);
        }
    }

    public void AddToStack(int amount = 1)
    {
        quantity += amount;
        UpdateQuantityDisplay();
    }

    public int RemoveFromStack(int amount = 1)
    {
        int removed = Mathf.Min(amount, quantity);
        quantity -= removed;
        UpdateQuantityDisplay();
        return removed;
    }

    public GameObject CloneItem(int newQuantity)
    {
        GameObject clone = Instantiate(gameObject);
        Item cloneItem = clone.GetComponent<Item>();
        cloneItem.quantity = newQuantity;
        cloneItem.UpdateQuantityDisplay();
        return clone;
    }

    public void UpdateQuantityDisplay()
    {
        if (quantityText != null)
        {
            quantityText.text = quantity > 1 ? quantity.ToString() : "";
        }
    }

    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<SpriteRenderer>().sprite;
        if(ItemPickupUIController.Instance != null)
        {
            ItemPickupUIController.Instance.ShowItemPickup(Name, itemIcon);
        }
    }

    public virtual void UseItem()
    {
        Debug.Log("Using item " + Name);
        switch (itemType)
        {
            case ItemType.Healing:
                if (quantity > 0)
                {
                    HealUser();
                    quantity--;
                    if (quantity <= 0) Destroy(gameObject);
                    else UpdateQuantityDisplay();
                }
                break;
            case ItemType.Weapon:
                player.GetComponent<PlayerController>().EquipWeapon(this);
                break;
            case ItemType.Consumable:
                player.GetComponent<PlayerMovement>().Regen(stamRegen);
                break;
            default:
                Debug.Log(Name + " has no use function.");
                break;
        }
    }

    void HealUser()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not assigned for item " + Name);
            return;
        }

        HealthController health = player.GetComponent<HealthController>();

        if (health != null)
        {
            health.Heal(healAmount);
        }
        else
        {
            Debug.LogWarning("No HealthController found on user.");
        }
    }
}