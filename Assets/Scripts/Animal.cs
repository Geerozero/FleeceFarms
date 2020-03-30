 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public enum AnimalType
    {
        Sheep, Alpaca, Rabbit
    }

    public int animalID;
    public AnimalType animalType;

    public FurItem fur;
    public OutfitItem slot01;
    public OutfitItem slot02;
    public OutfitItem slot03;

    public LevelManager.AnimalSave GetAnimalSave()
    {
        /* Add additional animal traits here
         * you must also add the new trait to the LevelManager.AnimalSave class as well */

        LevelManager.AnimalSave newSave = new LevelManager.AnimalSave();
        newSave.animalID = animalID;
        newSave.animalType = animalType;
        
        newSave.furID = fur.furID;
        newSave.slot01ClothID = slot01.clothingID;
        newSave.slot02ClothID = slot02.clothingID;
        newSave.slot03ClothID = slot03.clothingID;
        
        newSave.position = transform.position;
        newSave.rotation = transform.eulerAngles;

        return newSave;
    }

    public void LoadAnimalSave(LevelManager.AnimalSave save)
    {
        /* Loads animal with save info
         * Save info contains the last saved fur and clothing items
         * and gives them original position and rotation in Farm Scene */

        animalID = save.animalID;
        animalType = save.animalType;
        
        fur = LevelManager.instance.furs[save.furID];
        slot01 = ClothingManager.instance.clothes[save.slot01ClothID];
        slot02 = ClothingManager.instance.clothes[save.slot02ClothID];
        slot03 = ClothingManager.instance.clothes[save.slot03ClothID];

        transform.position = save.position;
        transform.eulerAngles = save.rotation;
    }
}
