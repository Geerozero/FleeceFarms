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

    void Start()
    {

    }

    public LevelManager.AnimalSave GetAnimalSave()
    {
        /* Add additional animal traits here
         * you must also add the new trait to the LevelManager.AnimalSave class as well */

        LevelManager.AnimalSave newSave = new LevelManager.AnimalSave();
        newSave.animalID = animalID;
        newSave.animalType = animalType;
        newSave.furID = fur.furID;
        newSave.position = transform.position;
        newSave.rotation = transform.eulerAngles;

        return newSave;
    }

    public void LoadAnimalSave(LevelManager.AnimalSave save)
    {
        /* Loads animal with save info
         * and gives them original position and rotation in Farm Scene */

        animalID = save.animalID;
        animalType = save.animalType;
        fur = LevelManager.instance.furs[save.furID];

        transform.position = save.position;
        transform.eulerAngles = save.rotation;
    }
}
