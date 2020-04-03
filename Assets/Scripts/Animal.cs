 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    /*--------------------Animal Traits-------------------*/

    public enum AnimalType
    {
        Sheep, Alpaca, Rabbit
    }
    
    [Header("Animal Identification")]
    public new string name;
    public int animalID;
    public AnimalType animalType;

    [Header("Animal Appearance")]
    public FurItem fur;
    public OutfitItem slot01;
    public OutfitItem slot02;
    public OutfitItem slot03;

    [Header("Animal Stats")]
    public bool isHappy;
    public int bond;
    public int hunger;
    public int furGrowth;

    /*--------------------Animal Stats-------------------*/

    private int animalFurInventoryIndex = 0; //index within inventory array

    [Header("Watching stats")]
    [SerializeField]
    private int animalClean = 0;
    [SerializeField]
    private int animalFood = 0;

    [SerializeField]
    private int animalFurGrowth = 0;

    [Header("Bond to increase every feed/clean")]
    public int animalBondIncreasePerInteraction;

    //stats tick delay should be shared with ALL objects
    [Header("Delay between ticks for stats")]
    public float statsTickDelayInput;

    private static float statsTickDelay;
    private float tickPreviousCheckTime;
    public static int furGrowthModifier = 20;
    
    /*------------------------------Animal Data Management----------------------------*/

    public LevelManager.AnimalSave GetAnimalSave()
    {
        /* Add additional animal traits here
         * you must also add the new trait to the LevelManager.AnimalSave class as well */

        LevelManager.AnimalSave newSave = new LevelManager.AnimalSave();
        newSave.animalName = name;
        newSave.animalID = animalID;
        newSave.animalType = animalType;
        
        newSave.furID = fur.furID;
        newSave.slot01ClothID = slot01.clothingID;
        newSave.slot02ClothID = slot02.clothingID;
        newSave.slot03ClothID = slot03.clothingID;

        newSave.animalIsHappy = isHappy;
        newSave.animalBond = bond;
        newSave.animalHunger = hunger;
        newSave.animalFurGrowth = furGrowth;

        newSave.position = transform.position;
        newSave.rotation = transform.eulerAngles;

        return newSave;
    }

    public void LoadAnimalSave(LevelManager.AnimalSave save)
    {
        /* Loads animal with save info
         * Save info contains the last saved fur, clothing items, and animal stats
         * and gives them original position and rotation in Farm Scene */

        name = save.animalName;
        animalID = save.animalID;
        animalType = save.animalType;
        
        fur = LevelManager.instance.furs[save.furID];
        slot01 = ClothingManager.instance.clothes[save.slot01ClothID];
        slot02 = ClothingManager.instance.clothes[save.slot02ClothID];
        slot03 = ClothingManager.instance.clothes[save.slot03ClothID];

        isHappy = save.animalIsHappy;
        bond = save.animalBond;
        hunger = save.animalHunger;
        furGrowth = save.animalFurGrowth;

        transform.position = save.position;
        transform.eulerAngles = save.rotation;
    }

    /*------------------------------Animal Stat Management----------------------------*/

    void Start()
    {
        statsTickDelay = statsTickDelayInput;
    }

    void Update()
    {
        /*
        * Tick system to constantly update the status of animals
        * Basically as time passes animal becomes hungry, gets dirty, and grows wool
        */

        if (Time.time - tickPreviousCheckTime > statsTickDelay)
        {
            Debug.Log("Stats tick");

            tickPreviousCheckTime = Time.time;

            //tick all stats
            TickAnimalStats();

            //check happiness always
            CheckAnimalHappy();
        }
    }

    public string GetAnimalName()
    {
        return name;
    }

    public void ChangeAnimalName(string newName)
    {
        name = newName;
    }

    //change Animal Fur Inventory Index
    public void SetAnimalFurInventoryIndex(int newFurInventoryIndex)
    {
        animalFurInventoryIndex = newFurInventoryIndex;
    }

    public int GetAnimalFurInventoryIndex()
    {
        return animalFurInventoryIndex;
    }

    /*--------------------Animal Interaction System-------------------*/

    private void CheckAnimalHappy()
    {
        if (animalClean > 0 && animalFood > 0)
        {
            isHappy = true;
            Debug.Log("Happy animal!");
        }

        //check clean status
        else if (animalClean <= 0)
        {
            isHappy = false;
            Debug.Log("Unhappy: dirty");
        }

        //check hunger status
        else if (animalFood <= 0)
        {
            isHappy = false;
            Debug.Log("Unhappy: hungry");
        }
    }

    private void TickAnimalStats()
    {
        //tick fur growth only if animal is happy
        if (isHappy)
        {
            Debug.Log("Fur growing");
            ChangeAnimalFurGrowth(furGrowthModifier * GetAnimalBond());
        }


        //always tick hunger and cleanliness
        ChangeAnimalClean(-1);
        ChangeAnimalFood(-1);

    }
    //adjusting animal statistics
    //only want to ADJUST statistics of animal, not set fully
    //can pass in negative or positive integer

    public void ChangeAnimalClean(int cleanValueChange)
    {
        CheckToIncreaseBond(cleanValueChange);
        animalClean += cleanValueChange;

        //set lower and upper bounds
        if (animalClean > 100)
        {
            animalClean = 100;
        }

        if (animalClean < 0)
        {
            animalClean = 0;
        }
    }

    public int GetAnimalClean()
    {
        return animalClean;
    }

    //animal hunger should always just be added
    public void ChangeAnimalFood(int hungerValueChange)
    {
        CheckToIncreaseBond(hungerValueChange);
        animalFood += hungerValueChange;

        //set lower and upper bounds
        if (animalFood > 100)
        {
            animalFood = 100;
        }

        if (animalFood < 0)
        {
            animalFood = 0;
        }
    }

    public int GetAnimalHunger()
    {
        return animalFood;
    }

    //fur growth
    public void ChangeAnimalFurGrowth(int furGrowthValueChange)
    {
        animalFurGrowth += furGrowthValueChange;

        //set lower and upper bounds
        if (animalFurGrowth > 100)
        {
            animalFurGrowth = 100;
        }

        if (animalFurGrowth < 0)
        {
            animalFurGrowth = 0;
        }
    }

    //get fur growth
    public int GetAnimalFurGrowth()
    {
        return animalFurGrowth;
    }

    //checks if FurGrowht is at 100, returns true if it is, false if it is NOT
    public bool CheckIfAnimalSheerable()
    {
        if (animalFurGrowth >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /*--------------------Animal Bonding System-------------------*/
    //animal Bond is general affection for player that increases as player interacts with animal
    //bond can constantly increase up to a max of 1000
    //bond make Fur grow faster
    
    public void ChangeAnimalBond(int bondValueChange)
    {
        bond += bondValueChange;

        //set lower and upper bounds
        if (bond > 1000)
        {
            bond = 1000;
        }

        if (bond < 0)
        {
            bond = 0;
        }
    }

    //call whenever feeding or cleaning to check if a positive value is being added, if so add bond points
    private void CheckToIncreaseBond(int valueChangeToCheck)
    {
        if (valueChangeToCheck > 0)
        {
            ChangeAnimalBond(animalBondIncreasePerInteraction);
        }
    }

    //return Animal Bond divided by 100
    public int GetAnimalBond()
    {
        return (bond / 100);
    }
}
