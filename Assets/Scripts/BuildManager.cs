using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    AudioSource audioSource;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one instance");
        }
        instance = this;
        
    }

    //add effect for build and sell


    //public GameObject standardPenPrefab;
    public AnimalPenBlueprint standardPen;
    private AnimalPenBlueprint penToBuild;
    
    private node selectedPen;
    

    public nodeUIscript nodeUI;

    public bool CanBuild { get { return penToBuild != null; } }
    public bool HasMoney { get { return InventoryManager.money >= penToBuild.cost; } }


   
    //private GameObject penToBuild;

    

    public void selectPen(node node)
    {
        if (selectedPen == node)
        {
            DeselectNode();
            return;

        }
        selectedPen = node;
        penToBuild = null;

        nodeUI.SetTarget(node);
    }
    public void DeselectNode()
    {
        selectedPen = null;
        nodeUI.Hide();
    }
    public void SelectAnimalPenToBuild (AnimalPenBlueprint animalPen)
    {
        penToBuild = animalPen;
        selectedPen = null;

        nodeUI.Hide();
    }

    public AnimalPenBlueprint GetPenToBuild()
    {
        return penToBuild;
    }
    //add type of pen to select "level of pen"
    //selectedpen = null;
}
