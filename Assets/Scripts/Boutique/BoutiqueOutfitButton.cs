using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutiqueOutfitButton : MonoBehaviour
{
    [Header("Associated Outfit Item")]
    public OutfitItem outfitItem;

    [Header("Button Referenecs")]
    public Text buttonText;
    public Image itemImage;
    public Image lockedImage;

    /*---Private Variables---*/
    private Button button;
    private GameObject outfitObject;
    /*---These are varibles for the position and scaling for the clothing item's for each animal type---*/
    private Vector3 spawnA;
    private Vector3 scaleA;
    private Vector3 rotA;
    private Vector3 spawnS;
    private Vector3 scaleS;
    private Vector3 rotS;
    private Vector3 spawnR;
    private Vector3 scaleR;
    
    void Start()
    {
        /*---Connect fur item attributes to this button---*/
        button = GetComponent<Button>();
        buttonText.text = outfitItem.name;
        itemImage.sprite = outfitItem.buttonImage;
        
        outfitObject = outfitItem.item;
        
        spawnA = outfitItem.spawnPositionAlpaca;
        scaleA = outfitItem.spawnScaleAlpaca;
        rotA = outfitItem.spawnRotationAlpaca;
        
        spawnS = outfitItem.spawnPositionSheep;
        scaleS = outfitItem.spawnScaleSheep;
        rotS = outfitItem.spawnRotationSheep;
        
        spawnR = outfitItem.spawnPositionRabbit;
        scaleR = outfitItem.spawnScaleRabbit;
    }

    void Update()
    {
        /*---Unlock button if the item was purchased---*/
        if (outfitItem.isPurchased == false)
        {
            button.interactable = false;
            lockedImage.gameObject.SetActive(true);
        }
        else if (outfitItem.isPurchased)
        {
            button.interactable = true;
            lockedImage.gameObject.SetActive(false);
        }
    }

    public void DetermineClothingSlot()
    {
        /*---Determines what clothing slot the outfit item should go in---*/

        if (outfitItem.clothingType == OutfitItem.ClothingType.Accessory)
        {
            BoutiqueManager.instance.animalInfo.slot01 = ClothingManager.instance.clothes[outfitItem.clothingID];
        }
        else if (outfitItem.clothingType == OutfitItem.ClothingType.Torso)
        {
            BoutiqueManager.instance.animalInfo.slot02 = ClothingManager.instance.clothes[outfitItem.clothingID];
        }
        else if (outfitItem.clothingType == OutfitItem.ClothingType.Shoe)
        {
            BoutiqueManager.instance.animalInfo.slot03 = ClothingManager.instance.clothes[outfitItem.clothingID];
        }
    }

    public void ApplyOutfitItem()
    {
        /* Spawns outfit item
         * scales it to the appropriate size for each animal type
         * makes outfit item a child of the animal
         * and puts it in the appropriate position
         * and fills in the animals save slots for clothing with the appropriate clothing IDs */

        Debug.Log("Putting outfit item on...");
        if(BoutiqueManager.instance.selectedAnimal.animalType == Animal.AnimalType.Alpaca)
        {
            DeletePreviousOutfitItem();

            GameObject newOutfitItem = Instantiate(outfitObject);
            
            newOutfitItem.transform.localScale = scaleA;
            newOutfitItem.transform.parent = BoutiqueManager.instance.animal.transform;
            newOutfitItem.transform.localPosition = spawnA;
            newOutfitItem.transform.localRotation = Quaternion.Euler(rotA);

            DetermineClothingSlot();
        }
        else if (BoutiqueManager.instance.selectedAnimal.animalType == Animal.AnimalType.Sheep)
        {
            DeletePreviousOutfitItem();

            GameObject newOutfitItem = Instantiate(outfitObject);

            newOutfitItem.transform.localScale = scaleS;
            newOutfitItem.transform.parent = BoutiqueManager.instance.animal.transform;
            newOutfitItem.transform.localPosition = spawnS;
            newOutfitItem.transform.localRotation = Quaternion.Euler(rotS);

            DetermineClothingSlot();
        }
        else if (BoutiqueManager.instance.selectedAnimal.animalType == Animal.AnimalType.Rabbit)
        {
            DeletePreviousOutfitItem();

            GameObject newOutfitItem = Instantiate(outfitObject);

            newOutfitItem.transform.localScale = scaleR;
            newOutfitItem.transform.parent = BoutiqueManager.instance.animal.transform;
            newOutfitItem.transform.localPosition = spawnR;

            DetermineClothingSlot();
        }
        else
        {
            Debug.Log("Invalid animal type... sorry :(");
        }
    }

    public void DeletePreviousOutfitItem()
    {
        for (int i = 0; i < BoutiqueManager.instance.animal.transform.childCount; i++)
        {
            if (outfitItem.clothingType == OutfitItem.ClothingType.Accessory && BoutiqueManager.instance.animal.transform.GetChild(i).gameObject.CompareTag("Accessory"))
            {
                Debug.Log("destroying: " + BoutiqueManager.instance.animal.transform.GetChild(i).gameObject);
                Destroy(BoutiqueManager.instance.animal.transform.GetChild(i).gameObject);
            }
            else if (outfitItem.clothingType == OutfitItem.ClothingType.Torso && BoutiqueManager.instance.animal.transform.GetChild(i).gameObject.CompareTag("Torso"))
            {
                Debug.Log("destroying: " + BoutiqueManager.instance.animal.transform.GetChild(i).gameObject);
                Destroy(BoutiqueManager.instance.animal.transform.GetChild(i).gameObject);
            }
            else if (outfitItem.clothingType == OutfitItem.ClothingType.Shoe && BoutiqueManager.instance.animal.transform.GetChild(i).gameObject.CompareTag("Shoe"))
            {
                Debug.Log("destroying: " + BoutiqueManager.instance.animal.transform.GetChild(i).gameObject);
                Destroy(BoutiqueManager.instance.animal.transform.GetChild(i).gameObject);
            }
        }
    }
}
