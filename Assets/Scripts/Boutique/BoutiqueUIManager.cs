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
    public GameObject specialContainer;
    public GameObject royalContainer;

    /*---Outfit containers---*/
    public GameObject accessoryContainer;
    public GameObject clothesContainer;
    public GameObject shoesContainer;


    void Start()
    {
        DisplayFurs();
    }

    /*-----------Fur UI Displays----------*/

    public void DisplayFurs()
    {
        furSelection.SetActive(true);
        basicContainer.SetActive(true);

        specialContainer.SetActive(false);
        royalContainer.SetActive(false);

        outfitSelection.SetActive(false);
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

    /*----------Outfit UI Displays----------*/

    public void DisplayOutfits()
    {
        outfitSelection.SetActive(true);
        accessoryContainer.SetActive(true);
        
        clothesContainer.SetActive(false);
        shoesContainer.SetActive(false);
        
        furSelection.SetActive(false);
    }

    public void DisplayAcessories()
    {
        accessoryContainer.SetActive(true);
        clothesContainer.SetActive(false);
        shoesContainer.SetActive(false);
    }

    public void DisplayClothes()
    {
        accessoryContainer.SetActive(false);
        clothesContainer.SetActive(true);
        shoesContainer.SetActive(false);
    }

    public void DisplayShoes()
    {
        accessoryContainer.SetActive(false);
        clothesContainer.SetActive(false);
        shoesContainer.SetActive(true);
    }

    public void GoToFarm()
    {
        /*---Saves furID and clothindIDs for animal that was cusotmized before leaving the Boutique scene---*/
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].furID = BoutiqueManager.instance.animalInfo.fur.furID;
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].slot01ClothID = BoutiqueManager.instance.animalInfo.slot01.clothingID;
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].slot02ClothID = BoutiqueManager.instance.animalInfo.slot02.clothingID;
        LevelManager.instance.saves[BoutiqueManager.instance.animalInfo.animalID].slot03ClothID = BoutiqueManager.instance.animalInfo.slot03.clothingID;

        SceneManager.LoadScene("Farm_test");
    }
}
