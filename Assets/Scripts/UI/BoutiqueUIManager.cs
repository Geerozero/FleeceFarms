using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutiqueUIManager : MonoBehaviour
{
    public GameObject furSelection;
    public GameObject outfitSelection;

    /*---Fur containers---*/
    public GameObject basicContainer;

    /*---Outfit containers---*/
    public GameObject accessoryContainer;


    void Start()
    {
        DisplayFurs();
    }

    /*-----------Fur UI Displays----------*/

    public void DisplayFurs()
    {
        furSelection.SetActive(true);
        basicContainer.SetActive(true);

        outfitSelection.SetActive(false);
    }

    public void DisplayBasicFur()
    {
        basicContainer.SetActive(true);
    }

    public void DisplaySpecialFur()
    {
        basicContainer.SetActive(false);
    }

    public void DisplayRoyalFur()
    {
        basicContainer.SetActive(false);
    }

    /*----------Outfit UI Displays----------*/

    public void DisplayOutfits()
    {
        outfitSelection.SetActive(true);
        accessoryContainer.SetActive(true);
        
        furSelection.SetActive(false);
    }

    public void DisplayAcessories()
    {
        accessoryContainer.SetActive(true);
    }

    public void DisplayClothes()
    {
        accessoryContainer.SetActive(false);
    }

    public void DisplayShoes()
    {
        accessoryContainer.SetActive(false);
    }

    public void GoToFarm()
    {
        /*---Saves furID and clothindIDs for animal that was cusotmized before leaving the Boutique scene---*/
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].furID = BoutiqueManager.instance.animalInfo.fur.furID;
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].slot01ClothID = BoutiqueManager.instance.animalInfo.slot01.clothingID;
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].slot02ClothID = BoutiqueManager.instance.animalInfo.slot02.clothingID;
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].slot03ClothID = BoutiqueManager.instance.animalInfo.slot03.clothingID;

        SceneManager.LoadScene("Farm_design");
    }
}
