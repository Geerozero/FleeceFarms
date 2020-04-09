using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<Animal> animals = new List<Animal>();
    public FurItem startingFurItem;
    public OutfitItem startingAccessory;
    public OutfitItem startingTorso;
    public OutfitItem startingShoe;
    public GameObject alpacaPrefab;
    public GameObject sheepPrefab;
    public GameObject rabbitPrefab;


    public Dictionary<int, FurItem> furs = new Dictionary<int, FurItem>();
    //int is furID pointing to furitem

    public Dictionary<int, AnimalSave> saves = new Dictionary<int, AnimalSave>();
    //int is animalID pointing to the animal save

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        /*---building furs dictionary, all furs in the Resources folder are loaded here on Awake---*/
        FurItem[] fursFound = Resources.LoadAll<FurItem>("Furs");
        for(int i = 0; i < fursFound.Length; i++)
        {
            furs.Add(fursFound[i].furID, fursFound[i]);
        }

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
        else if(scene.name == "Farm_design")
        {
            /*---Restores animals when in the Farm scene---*/
            Debug.Log("Loading Scene...");
            RestoreAnimals();
        }
    }

    [System.Serializable]
    public class AnimalSave
    {
        /*---Data container for animal traits---*/
        public string animalName;
        public int animalID;
        public Animal.AnimalType animalType;

        public int furID;
        public int slot01ClothID;
        public int slot02ClothID;
        public int slot03ClothID;

        public bool animalIsHappy;
        public int animalBond;
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

    public void SpawnNewAnimal(Animal.AnimalType animalType)
    {
        /* Spawns a new animal based on a enum parameter
         * initializes new animal with starting traits
         * and adds the new animals info to the animals List to be saved and loaded */
        GameObject newAnimal = null;

        if (animalType == Animal.AnimalType.Alpaca)
        {
            newAnimal = SpawnAnimalType(Animal.AnimalType.Alpaca);
        }
        else if (animalType == Animal.AnimalType.Sheep)
        {
            newAnimal = SpawnAnimalType(Animal.AnimalType.Sheep);
        }
        else if (animalType == Animal.AnimalType.Rabbit)
        {
            newAnimal = SpawnAnimalType(Animal.AnimalType.Rabbit);
        }

        Animal animalInfo = newAnimal.GetComponent<Animal>();

        InitializeNewAnimal(animalInfo, animalType);
        
        newAnimal.GetComponent<MeshRenderer>().material = furs[animalInfo.fur.furID].furMaterial;
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

        animalInfo.name = "NewAnimal";
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
            newOutfitItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if(animalType == Animal.AnimalType.Sheep)
        {
            GameObject newOutfitItem = Instantiate(ClothingManager.instance.clothes[clothingID].item);
            newOutfitItem.transform.localScale = ClothingManager.instance.clothes[clothingID].spawnScaleSheep;
            newOutfitItem.transform.parent = animal.transform;
            newOutfitItem.transform.localPosition = ClothingManager.instance.clothes[clothingID].spawnPositionSheep;
            newOutfitItem.transform.localRotation = Quaternion.Euler(0, 0, 0);
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
            GameObject newAnimal = SpawnAnimalType(saves[i].animalType);
            Animal animalInfo = newAnimal.GetComponent<Animal>();

            animalInfo.LoadAnimalSave(saves.ElementAt(i).Value);
            newAnimal.GetComponent<MeshRenderer>().material = furs[animalInfo.fur.furID].furMaterial;
            
            /*---filling in clothing slots with appropriate clothing ID---*/
            animalInfo.slot01 = ClothingManager.instance.clothes[saves.ElementAt(i).Value.slot01ClothID];
            animalInfo.slot02 = ClothingManager.instance.clothes[saves.ElementAt(i).Value.slot02ClothID];
            animalInfo.slot03 = ClothingManager.instance.clothes[saves.ElementAt(i).Value.slot03ClothID];

            /*---Begin spawning clothing on the animals---*/
            SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot01.clothingID);
            SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot02.clothingID);
            SpawnClothesOnAnimal(newAnimal, animalInfo.animalType, animalInfo.slot03.clothingID);

            animals.Add(animalInfo);
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
    }

    /*-------------------Test Functions-------------------*/

    public void GoToBoutique()
    {
        StoreAnimals();
        SceneManager.LoadScene("Boutique");
    }

    public void SpawnTestAnimal()
    {
        SpawnNewAnimal(Animal.AnimalType.Alpaca);
    }

    /*----------------------------------------------------*/
}
