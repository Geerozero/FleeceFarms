using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialFurButton : MonoBehaviour
{
    [Header("Associated Fur Item")]
    public FurItem furItemAlpaca;
    public FurItem furItemSheep;

    [Header("Button Referenecs")]
    public Text buttonText;
    public Image itemImage;
    public Image lockedImage;

    /*---Private Variables---*/
    private Button button;
    private Material material;

    void Start()
    {
        /*---Connect fur item attributes to this button---*/
        button = GetComponent<Button>();
        buttonText.text = furItemAlpaca.name;
        itemImage.sprite = furItemSheep.buttonImage;
        material = furItemAlpaca.furMaterial;
    }

    void Update()
    {
        /*---Unlock button if the item was purchased---*/
        if (furItemAlpaca.isPurchased == false)
        {
            button.interactable = false;
            lockedImage.gameObject.SetActive(true);
        }
        else if (furItemAlpaca.isPurchased)
        {
            button.interactable = true;
            lockedImage.gameObject.SetActive(false);
        }
    }

    public void ChangeFur()
    {
        /*---Changes furItem and changes animals material---*/
        if(BoutiqueManager.instance.selectedAnimal.animalType == Animal.AnimalType.Alpaca)
        {
            BoutiqueManager.instance.animalInfo.fur = furItemAlpaca;
            BoutiqueManager.instance.animalInfo.wool.GetComponent<SkinnedMeshRenderer>().material = FurManager.instance.furs[furItemAlpaca.furID].furMaterial;
        }
        else if(BoutiqueManager.instance.selectedAnimal.animalType == Animal.AnimalType.Sheep)
        {
            BoutiqueManager.instance.animalInfo.fur = furItemSheep;
            BoutiqueManager.instance.animalInfo.wool.GetComponent<SkinnedMeshRenderer>().material = FurManager.instance.furs[furItemSheep.furID].furMaterial;
        }
    }
}
