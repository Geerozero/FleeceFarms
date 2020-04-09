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
        Load(); //loads game on start
    }

    void Update()
    {

    }

    public void Save()
    {
        /*---Saves level data---*/
        LevelManager.instance.SaveAnimalsToFile();
        //player data should be saved the same way

        Debug.Log("Saved!");
    }

    public void Load()
    {
        /*---Loads level data---*/
        LevelManager.instance.LoadAnimalsFromFile();
        //player data should be loaded the same way

        Debug.Log("Loaded!");
    }
}
