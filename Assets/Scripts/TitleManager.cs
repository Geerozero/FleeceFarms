using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Current name of Farm Scene")]
    public string FarmSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(FarmSceneName);
    }


    public void Settings()
    {
        Debug.Log("Sound settings/Window scale here");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
