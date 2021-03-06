﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using System.Xml;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<Animal> animals = new List<Animal>();
    public List<GameObject> pens = new List<GameObject>();
    public FurItem startingFurItem;
    public OutfitItem startingAccessory;
    public OutfitItem startingTorso;
    public OutfitItem startingShoe;
    public GameObject alpacaPrefab;
    public GameObject sheepPrefab;
    public GameObject rabbitPrefab;

    public int sheepToCreate;
    public int alpacaToCreate;

    public GameObject penPrefab;

    public Dictionary<int, AnimalSave> saves = new Dictionary<int, AnimalSave>();
    //int is animalID pointing to the animal save

    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        /*---listens for when the scene has changed---*/
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Boutique")
        {
            /*---Calls BoutiqueManager to spawn animal that needs to be cutomizzed, when in the Boputique scene---*/
            BoutiqueManager.instance.SpawnAnimalToCustomize();
        }
        else if(scene.name == "Market")
        {
            //do something
        }
        else if(scene.name == "Title")
        {
            //do something
        }
        else if(scene.name == "Farm_design")
        {
            /*---Restores animals when in the Farm scene---*/
            Debug.Log("Loading Scene...");

            RestoreAnimals();
            RestorePens();
        }
    }

    [System.Serializable]
    public class AnimalSave
    {
        /*---Data container for animal traits---*/
        public string animalName;
        public int animalID;
        public Animal.AnimalType animalType;
        public string assignedPen;

        public int furID;
        public int slot01ClothID;
        public int slot02ClothID;
        public int slot03ClothID;

        public bool isFurGrown;
        public bool animalIsHappy;
        public int animalClean;
        public int animalHunger;
        public int animalFurGrowth;

        public SerializableVector3 position;
        public SerializableVector3 rotation;
    }

    /*----------------Animal Creation------------------------------------------------------*/

    public GameObject SpawnAnimalType(Animal.AnimalType animalType)
    {
        /* Returns an animal prefab based on a enum parameter
         * instantiates animal with placeholder position and rotation */

        GameObject newAnimal;

        if (animalType == Animal.AnimalType.Alpaca)
        {
            newAnimal = Instantiate(alpacaPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            
        }
        else if (animalType == Animal.AnimalType.Sheep)
        {
            newAnimal = Instantiate(sheepPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else if (animalType == Animal.AnimalType.Rabbit)
        {
            newAnimal = Instantiate(rabbitPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            newAnimal = null;
        }

        return newAnimal;
    }

    public void SpawnAnimalTypeWithLocation(Animal.AnimalType animalType, GameObject location)
    {
        /* Returns an animal prefab based on a enum parameter
         * instantiates animal with placeholder position and rotation */

        GameObject newAnimal;

        if (animalType == Animal.AnimalType.Alpaca)
        {
            newAnimal = Instantiate(alpacaPrefab, location.transform.position, Quaternion.identity);
        }
        else if (animalType == Animal.AnimalType.Sheep)
        {
            newAnimal = Instantiate(sheepPrefab, location.transform.position, Quaternion.identity);
        }
        else if (animalType == Animal.AnimalType.Rabbit)
        {
            newAnimal = Instantiate(rabbitPrefab, location.transform.position, Quaternion.identity);
        }
        else
        {
            newAnimal = null;
        }

        Animal animalInfo = newAnimal.GetComponent<Animal>();

        InitializeNewAnimal(animalInfo, animalType);

        //have to adjust MeshRenderer to SKinnedMeshRenderer for Alpaca

        if(animalType == Animal.AnimalType.Alpaca)
        {
            animalInfo.wool.GetComponent<SkinnedMeshRenderer>().material = FurManager.instance.furs[animalInfo.fur.furID].furMaterial;

        }
        else
        {
            animalInfo.wool.GetComponent<MeshRenderer>().material = FurManager.instance.furs[animalInfo.fur.furID].furMaterial;
        }

        SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot01.clothingID);
        SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot02.clothingID);
        SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot03.clothingID);

        animalInfo.pen = location.gameObject.name;

        animals.Add(animalInfo);
    }

    public void SpawnNewAnimal(Animal.AnimalType animalType)
    {
        /* Spawns a new animal based on a enum parameter
         * initializes new animal with starting traits
         * and adds the new animals info to the animals List to be saved and loaded */
        GameObject newAnimal = null;

        if (animalType == Animal.AnimalType.Alpaca)
        {
            newAnimal = SpawnAnimalType(Animal.AnimalType.Alpaca);
            FindObjectOfType<AudioManager>().Play("Alpaca");
        }
        else if (animalType == Animal.AnimalType.Sheep)
        {
            newAnimal = SpawnAnimalType(Animal.AnimalType.Sheep);
            FindObjectOfType<AudioManager>().Play("Sheep");
        }
        else if (animalType == Animal.AnimalType.Rabbit)
        {
            newAnimal = SpawnAnimalType(Animal.AnimalType.Rabbit);
            FindObjectOfType<AudioManager>().Play("Rabbit");
        }

        Animal animalInfo = newAnimal.GetComponent<Animal>();

        InitializeNewAnimal(animalInfo, animalType);
        
        if(animalInfo.animalType == Animal.AnimalType.Alpaca)
        {
            animalInfo.wool.GetComponent<SkinnedMeshRenderer>().material = FurManager.instance.furs[animalInfo.fur.furID].furMaterial;
        }
        else
        {
            animalInfo.wool.GetComponent<MeshRenderer>().material = FurManager.instance.furs[animalInfo.fur.furID].furMaterial;
        }
        SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot01.clothingID);
        SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot02.clothingID);
        SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot03.clothingID);

        animals.Add(animalInfo);
        //other animals to be added later(sheep, rabbit)
    }

    public void InitializeNewAnimal(Animal animalInfo, Animal.AnimalType animalType)
    {
        /* Initializes a new animal with un-upgraded starting traits
         * assigns them a unique ID
         * assigns their type */

        animalInfo.animalID = saves.Count;
        
        while(saves.ContainsKey(animalInfo.animalID))
        {
            animalInfo.animalID += 1;
        }

        animalInfo.name = GenerateRandomName();
        animalInfo.animalType = animalType;
        animalInfo.fur = startingFurItem;
        animalInfo.slot01 = startingAccessory;
        animalInfo.slot02 = startingTorso;
        animalInfo.slot03 = startingShoe;

        saves.Add(animalInfo.animalID, animalInfo.GetAnimalSave());

        return;
    }

    public void SpawnClothesOnAnimal(GameObject animal, Animal.AnimalType animalType, int clothingID)
    {
        /*---Spawns clothing according to what animal type the item is intended for---*/

        if(animalType == Animal.AnimalType.Alpaca)
        {
            GameObject newOutfitItem = Instantiate(ClothingManager.instance.clothes[clothingID].item);
            newOutfitItem.transform.localScale = ClothingManager.instance.clothes[clothingID].spawnScaleAlpaca;
            newOutfitItem.transform.parent = animal.transform;
            newOutfitItem.transform.localPosition = ClothingManager.instance.clothes[clothingID].spawnPositionAlpaca;
            newOutfitItem.transform.localRotation = Quaternion.Euler(ClothingManager.instance.clothes[clothingID].spawnRotationAlpaca);
        }
        else if(animalType == Animal.AnimalType.Sheep)
        {
            GameObject newOutfitItem = Instantiate(ClothingManager.instance.clothes[clothingID].item);
            newOutfitItem.transform.localScale = ClothingManager.instance.clothes[clothingID].spawnScaleSheep;
            newOutfitItem.transform.parent = animal.transform;
            newOutfitItem.transform.localPosition = ClothingManager.instance.clothes[clothingID].spawnPositionSheep;
            newOutfitItem.transform.localRotation = Quaternion.Euler(ClothingManager.instance.clothes[clothingID].spawnRotationSheep);
        }
        else if(animalType == Animal.AnimalType.Rabbit)
        {
            GameObject newOutfitItem = Instantiate(ClothingManager.instance.clothes[clothingID].item);
            newOutfitItem.transform.localScale = ClothingManager.instance.clothes[clothingID].spawnScaleRabbit;
            newOutfitItem.transform.parent = animal.transform;
            newOutfitItem.transform.localPosition = ClothingManager.instance.clothes[clothingID].spawnPositionRabbit;
            newOutfitItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public string GenerateRandomName()
    {
        string[] names = {"Anastasia", "Sia", "Austin", "Brent", "Cheyenne", "Chris",
                           "Danielle", "Hannah", "Josh", "Kaitlin", "Katie", "Kate", "Selena",
                           "Rick", "Dave", "Sebastian", "Sam", "Samantha", "Greg", "Mickey",
                           "Carly", "Carlos", "Milly", "Sally", "Sandro", "Lizzy", "Mike",
                           "Aaron", "Joe", "Jo Jo", "Martha", "Gertrude", "Mary", "Cary",
                           "Alex", "Alexis", "Marhta", "Yoda", "Luke", "Leia", "Han", "Chewy",
                           "Chloe", "Tony", "Zach", "Cody", "Lara", "Max", "Sampson", "Marley",
                           "Bob", "Liam", "Noah", "William", "Prince", "Princess", "Queen", "King",
                           "Legend", "James", "Oliver", "Benjamin", "Benjie", "Elijah", "Lucas", "Mason",
                           "Logan", "Jake", "Emma", "Olivia", "Ava", "Isabelle", "Sophia", "Charlotte",
                           "Mia", "Maya", "Harper", "Evelyn", "Colleen", "Mickey", "Dylan", "Luna", "Penelope",
                           "Ellie", "Stella", "Natalie", "Zoey", "Xavier", "Anna", "Elena", "Christie", "Terra",
                           "Jasmine", "Ariana", "Ethan", "Hila", "Felix", "Marzia", "Jackson", "Jack", "Jackie",
                           "Hunter", "Jon", "Jonathon", "Steve", "Ty", "Tyler", "Santi", "Santiago", "Rox", "Ren"};

        int random = UnityEngine.Random.Range(0, names.Length);
        
        return names[random];
    }


    /*----------------Animal Management------------------------------------------------------*/

    void StoreAnimals()
    {
        /* Clears saves dictionary to allow for a new save
         * Then adds animals that currently exist into the saves Dictionary */

        saves.Clear();

        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i] != null)
            {
                saves.Add(animals[i].animalID, animals[i].GetAnimalSave());
            }
        }
    }

    void RestoreAnimals()
    {
        /* Restores the animals that were last saved
         * and spawns them into the scene */

        /*---Destroys all spawned animals in the scene---*/
        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i] != null)
            {
                Destroy(animals[i].gameObject);
            }
        }

        animals.Clear();

        /*---Spawns animals that were last saved */

        for (int i = 0; i < saves.Count; i++)
        {
            GameObject newAnimal = SpawnAnimalType(saves.ElementAt(i).Value.animalType);
            Animal animalInfo = newAnimal.GetComponent<Animal>();

            animalInfo.LoadAnimalSave(saves.ElementAt(i).Value);

            if(animalInfo.animalType == Animal.AnimalType.Alpaca)
            {
                animalInfo.wool.GetComponent<SkinnedMeshRenderer>().material = FurManager.instance.furs[animalInfo.fur.furID].furMaterial;
            }
            else
            {
                animalInfo.wool.GetComponent<MeshRenderer>().material = FurManager.instance.furs[animalInfo.fur.furID].furMaterial;
            }

            /*---filling in clothing slots with appropriate clothing ID---*/
            animalInfo.slot01 = ClothingManager.instance.clothes[saves.ElementAt(i).Value.slot01ClothID];
            animalInfo.slot02 = ClothingManager.instance.clothes[saves.ElementAt(i).Value.slot02ClothID];
            animalInfo.slot03 = ClothingManager.instance.clothes[saves.ElementAt(i).Value.slot03ClothID];

            /*---Begin spawning clothing on the animals---*/
            SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot01.clothingID);
            SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot02.clothingID);
            SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot03.clothingID);

            NavMeshAgent animalNavMesh = newAnimal.GetComponent<NavMeshAgent>();

            animalNavMesh.Warp(saves.ElementAt(i).Value.position);
            newAnimal.transform.rotation = Quaternion.Euler(saves.ElementAt(i).Value.rotation);

            animals.Add(animalInfo);
        }
    }

    void RestorePens()
    {
        for(int i = 0; i < pens.Count; i++)
        {
            if(pens[i] != null )
            {
                Destroy(pens[i].gameObject);
                Debug.Log("Deleting: " + pens[i].gameObject.name);
            }
        }

        pens.Clear();
        
        PlayerManager.instance.LoadPlayerDataFromFile();

        GameObject pen01 = GameObject.Find("Pen01");
        GameObject pen02 = GameObject.Find("Pen02");
        GameObject pen03 = GameObject.Find("Pen03");
        GameObject pen04 = GameObject.Find("Pen04");

        if (PlayerManager.instance.playerSave.pen01Purchased)
        {
            GameObject newPen = Instantiate(penPrefab);
            newPen.transform.position = new Vector3(-60.8f, 0f, 20.1f);
            pen01.GetComponent<Pen>().SetCurrentAniamls(PlayerManager.instance.playerSave.pen01Animals);
            pens.Add(newPen);
        }
        if (PlayerManager.instance.playerSave.pen02Purchased)
        {
            GameObject newPen = Instantiate(penPrefab);
            newPen.transform.position = new Vector3(-246.2f, 0f, 20.1f);
            pen02.GetComponent<Pen>().SetCurrentAniamls(PlayerManager.instance.playerSave.pen02Animals);
            pens.Add(newPen);
        }
        if (PlayerManager.instance.playerSave.pen03Purchased)
        {
            GameObject newPen = Instantiate(penPrefab);
            newPen.transform.position = new Vector3(-60.8f, 0f, 188.4f);
            pen03.GetComponent<Pen>().SetCurrentAniamls(PlayerManager.instance.playerSave.pen03Animals);
            pens.Add(newPen);
        }
        if (PlayerManager.instance.playerSave.pen04Purchased)
        {
            GameObject newPen = Instantiate(penPrefab);
            newPen.transform.position = new Vector3(-243.5f, 0f, 188.4f);
            pen04.GetComponent<Pen>().SetCurrentAniamls(PlayerManager.instance.playerSave.pen04Animals);
            pens.Add(newPen);
        }
    }

    /*----------------Animal File Management-----------------------------------------------*/

    public void SaveAnimalsToFile()
    {
        /*---Stores animals in saves Dictionary and puts them in a file---*/
        StoreAnimals();
        FileHelper.SaveDataFile(saves, "AnimalData");
    }

    public void LoadAnimalsFromFile()
    {
        /*---Gets data from file and then restores animal list---*/
        saves = FileHelper.LoadDataFile<Dictionary<int, AnimalSave>>("AnimalData");
        if (saves == null)
            saves = new Dictionary<int, AnimalSave>();
        RestoreAnimals();
        RestorePens();
    }

    public void LoadAnimalsFromSaves()
    {
        saves = FileHelper.LoadDataFile<Dictionary<int, AnimalSave>>("AnimalData");
        if (saves == null)
            saves = new Dictionary<int, AnimalSave>();
    }

    /*-------------------Test Functions-------------------*/

    public void GoToBoutique()
    {
        StoreAnimals();
        SceneManager.LoadScene("Boutique");
    }

    //public void SpawnTestAnimal()
    //{
        //SpawnNewAnimal(Animal.AnimalType.Alpaca, spawnPen01);
    //}

    /*----------------------------------------------------*/
}
