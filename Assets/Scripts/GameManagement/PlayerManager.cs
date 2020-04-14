using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [System.Serializable]
    public class PlayerData
    {
        public int money;
        public bool pen01Purchased;
        public bool pen02Purchased;
        public bool pen03Purchased;
        public bool pen04Purchased;
    }

    public static PlayerManager instance;

    public PlayerData playerSave = new PlayerData();

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void StorePlayerData()
    {
        playerSave.money = InventoryManager.instance.money;
    }

    void RestorePlayerData()
    {
        InventoryManager.instance.money = playerSave.money;
    }

    public void SavePlayerDataToFile()
    {
        StorePlayerData();
        FileHelper.SaveDataFile(playerSave, "PlayerData");
    }


    public void LoadPlayerDataFromFile()
    {
        playerSave = FileHelper.LoadDataFile<PlayerData>("PlayerData");
        if (playerSave == null)
            playerSave = new PlayerData ();
        RestorePlayerData();
    }

    public void ResetPlayer()
    {
        playerSave.pen01Purchased = false;
        playerSave.pen02Purchased = false;
        playerSave.pen03Purchased = false;
        playerSave.pen04Purchased = false;
        playerSave.money = 0;
        InventoryManager.instance.money = playerSave.money;
    }
}
