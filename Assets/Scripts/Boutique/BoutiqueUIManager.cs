using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutiqueUIManager : MonoBehaviour
{
    public GameObject furSelection;

    public GameObject basicContainer;
    public GameObject specialContainer;
    public GameObject royalContainer;

    void Start()
    {
        furSelection.SetActive(true);
        basicContainer.SetActive(true);
        specialContainer.SetActive(false);
        royalContainer.SetActive(false);
    }

    public void DisplayBasicFur()
    {
        basicContainer.SetActive(true);
        specialContainer.SetActive(false);
        royalContainer.SetActive(false);
    }

    public void DisplaySpecialFur()
    {
        basicContainer.SetActive(false);
        specialContainer.SetActive(true);
        royalContainer.SetActive(false);
    }

    public void DisplayRoyalFur()
    {
        basicContainer.SetActive(false);
        specialContainer.SetActive(false);
        royalContainer.SetActive(true);
    }

    public void GoToFarm()
    {
        /*---Saves furID for animal that was cusotmized before leaving the Boutique scene---*/
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].furID = BoutiqueManager.instance.animalInfo.fur.furID;
        //clothing items will be saved here as well

        SceneManager.LoadScene("Farm_test");
    }
}
