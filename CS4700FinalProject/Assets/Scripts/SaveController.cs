using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    private HotbarController hotbarController;
    private PlayerMovement playerMovement;
    private HealthController healthController;

    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        inventoryController = FindObjectOfType<InventoryController>();
        hotbarController = FindObjectOfType<HotbarController>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        healthController = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
        LoadGame();
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            inventorySaveData = inventoryController.GetInventoryItems(),
            hotbarSaveData = hotbarController.GetHotbarItems(),
            healthSaveData = healthController.GetHealth(),
            staminaSaveData = playerMovement.GetStamina()
        };
        Debug.Log(saveData.healthSaveData);
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log(saveLocation);
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            inventoryController.SetInventoryItem(saveData.inventorySaveData);
            hotbarController.SetHotbarItem(saveData.hotbarSaveData);
            Debug.Log(saveData.healthSaveData);
            healthController.currentHealth = saveData.healthSaveData;
            healthController.Start();
            playerMovement.SetStamina(saveData.staminaSaveData);
        }
        else
        {
            SaveGame();
        }
    }
}