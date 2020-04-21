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
        public int animalsBought;
        public int pensBought;
        public int shearLevel;
        
        public bool pen01Purchased;
        public bool pen02Purchased;
        public bool pen03Purchased;
        public bool pen04Purchased;
        
        public int pen01Animals;
        public int pen02Animals;
        public int pen03Animals;
        public int pen04Animals;

        public bool pen01IsFull;
        public bool pen02IsFull;
        public bool pen03IsFull;
        public bool pen04IsFull;

        public bool tutorialGift;
        public bool recievedWinGift;
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
        
        playerSave.pen01Animals = 0;
        playerSave.pen02Animals = 0;
        playerSave.pen03Animals = 0;
        playerSave.pen04Animals = 0;

        playerSave.pen01IsFull = false;
        playerSave.pen02IsFull = false;
        playerSave.pen03IsFull = false;
        playerSave.pen04IsFull = false;

        playerSave.tutorialGift = false;
        playerSave.recievedWinGift = false;

        playerSave.money = 0;
        playerSave.animalsBought = 0;
        playerSave.pensBought = 0;
        playerSave.shearLevel = 0;
        
        InventoryManager.instance.money = playerSave.money;
    }
}
