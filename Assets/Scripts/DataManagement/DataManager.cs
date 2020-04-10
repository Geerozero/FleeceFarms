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
        /*---Saves level data---*/
        LevelManager.instance.SaveAnimalsToFile();
        ClothingManager.instance.SaveClothesToFile();
        FurManager.instance.SaveFursToFile();
        //player data should be saved the same way

        Debug.Log("Saved!");
    }

    public void Load()
    {
        /*---Loads level data---*/
        FurManager.instance.LoadFursFromFile();
        ClothingManager.instance.LoadClothesFromFile();
        LevelManager.instance.LoadAnimalsFromFile();
        //player data should be loaded the same way

        Debug.Log("Loaded!");
    }

    public void NewGame()
    {
        FileClearing.Clean();
        ClothingManager.instance.ResetClothes();
        FurManager.instance.ResetFurs();
        Load();
    }
}
