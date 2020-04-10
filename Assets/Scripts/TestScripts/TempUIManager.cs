using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempUIManager : MonoBehaviour
{
    /*---Temporary buttons for my farm_test scene---*/

    public void GoToMarket()
    {
        LevelManager.instance.SaveAnimalsToFile();
        SceneManager.LoadScene("Market");
    }

    public void GoToBoutique()
    {
        LevelManager.instance.SaveAnimalsToFile();
        SceneManager.LoadScene("Boutique");
    }

    //public void SpawnTest()
    //{
        //LevelManager.instance.SpawnNewAnimal(Animal.AnimalType.Sheep, LevelManager.instance.spawnPen01);
    //}

    public void LoadGame()
    {
        DataManager.instance.Load();
    }

    public void SaveGame()
    {
        DataManager.instance.Save();
    }
}
