using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public List<PenSave> pens = new List<PenSave>();

    public List<PenSave> penSaves = new List<PenSave>();


    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class PenSave
    {
        public Vector3 position;
        public bool wasPurchased;
    }

    [System.Serializable]
    public class PlayerSave
    {
        public int playerMoney;
    }

    void StorePlayerData()
    {
        penSaves.Clear();

        for(int i = 0; i < pens.Count; i++)
        {
            penSaves.Add(pens.ElementAt(i));
        }
    }

    void RestorePlayerData()
    {

    }

    public void SavePlayerDataToFile()
    {

    }

    public void LoadPlayerDataFromFile()
    {

    }
}
