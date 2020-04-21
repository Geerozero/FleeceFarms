using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutiqueManager : MonoBehaviour
{
    public static BoutiqueManager instance;

    public LevelManager.AnimalSave selectedAnimal;

    public GameObject animal;
    public Animal animalInfo;
    public Vector3 spawnPos;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void SpawnAnimalToCustomize()
    {
        if(animal != null)
        {
            //Destroys animal if its already in the scene
            Destroy(animal.gameObject);
        }

        /*---Spawns animal based on selectedAnimals info---*/
        animal = LevelManager.instance.SpawnAnimalType(selectedAnimal.animalType);
        animalInfo = animal.GetComponent<Animal>();

        animalInfo.animalID = selectedAnimal.animalID;
        
        animalInfo.fur = FurManager.instance.furs[selectedAnimal.furID];
        animalInfo.fur.furID = selectedAnimal.furID;
        animalInfo.wool.gameObject.SetActive(true);

        if(selectedAnimal.animalType == Animal.AnimalType.Alpaca)
        {
            animalInfo.wool.GetComponent<SkinnedMeshRenderer>().material = FurManager.instance.furs[selectedAnimal.furID].furMaterial;
        }
        else
        {
            animalInfo.wool.GetComponent<MeshRenderer>().material = FurManager.instance.furs[selectedAnimal.furID].furMaterial;
        }

        animalInfo.slot01 = ClothingManager.instance.clothes[selectedAnimal.slot01ClothID];
        animalInfo.slot02 = ClothingManager.instance.clothes[selectedAnimal.slot02ClothID];
        animalInfo.slot03 = ClothingManager.instance.clothes[selectedAnimal.slot03ClothID];

        LevelManager.instance.SpawnClothesOnAnimal(animal, animalInfo.animalType, animalInfo.slot01.clothingID);
        LevelManager.instance.SpawnClothesOnAnimal(animal, animalInfo.animalType, animalInfo.slot02.clothingID);
        LevelManager.instance.SpawnClothesOnAnimal(animal, animalInfo.animalType, animalInfo.slot03.clothingID);

        //puts animal in postion to be customized
        animal.transform.position = spawnPos;
        animal.transform.rotation = Quaternion.Euler(new Vector3(0, 150, 0));
    }
}
