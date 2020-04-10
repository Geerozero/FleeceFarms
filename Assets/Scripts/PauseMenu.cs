using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject gameSaved;
    public GameObject confirm;
    private int option;

    public void ResumeGame()
    {
        confirm.SetActive(false);
        gameSaved.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void SaveGame()
    {
        DataManager.instance.Save();
        gameSaved.SetActive(true); //then resume game
    }

    public void LoadGame()
    {
        option = 1;
        confirm.SetActive(true);
    }

    public void GoToMainMenu()
    {
        option = 2;
        confirm.SetActive(true);
    }

    public void QuitGame()
    {
        option = 3;
        confirm.SetActive(true);
    }

    public void Cancel()
    {
        confirm.SetActive(false);
        option = 0;
    }

    public void ConfirmOption()
    {
        if(option == 1)
        {
            option = 0;
            DataManager.instance.Load();
            ResumeGame();

        }
        else if(option == 2)
        {
            option = 0;
            DataManager.instance.Load();
            SceneManager.LoadScene("Title");
        }
        else if(option == 3)
        {
            Application.Quit();
        }
    }


}
