using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {

    }

    void Update()
    {

    }

    public void Save()
    {
        /*---Saves game data---*/
        LevelManager.instance.SaveAnimalsToFile();
        ClothingManager.instance.SaveClothesToFile();
        FurManager.instance.SaveFursToFile();
        PlayerManager.instance.SavePlayerDataToFile();

        Debug.Log("Saved!");
    }

    public void Load()
    {
        /*---Loads game data---*/
        FurManager.instance.LoadFursFromFile();
        ClothingManager.instance.LoadClothesFromFile();
        LevelManager.instance.LoadAnimalsFromFile();
        PlayerManager.instance.LoadPlayerDataFromFile();

        Debug.Log("Loaded!");
    }

    public void LoadFromTitle()
    {
        FurManager.instance.LoadFursFromFile();
        ClothingManager.instance.LoadClothesFromFile();
        LevelManager.instance.LoadAnimalsFromSaves();
        PlayerManager.instance.LoadPlayerDataFromFile();
    }

    public void NewGame()
    {
        FileClearing.Clean();
        ClothingManager.instance.ResetClothes();
        FurManager.instance.ResetFurs();
        PlayerManager.instance.ResetPlayer();
        
        Load();
    }
}
