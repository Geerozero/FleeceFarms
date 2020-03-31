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
        animalInfo.fur = LevelManager.instance.furs[selectedAnimal.furID];
        animalInfo.fur.furID = selectedAnimal.furID;
        //will also need to add clothing later

        animal.GetComponent<MeshRenderer>().material = LevelManager.instance.furs[selectedAnimal.furID].furMaterial;

        //puts animal in postion to be customized
        animal.transform.position = spawnPos;
    }
}
