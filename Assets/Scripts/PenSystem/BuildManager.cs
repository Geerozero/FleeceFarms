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
    
    private AnimalPenBlueprint penToBuild;
    
    private Node selectedPen;

    //this will need to change with value of pen
    //private int cost = 10;

    public NodeUIscript nodeUI;

    public bool CanBuild { get { return penToBuild != null; } }
    public bool HasMoney { get { return InventoryManager.money >= penToBuild.cost; } }


   //test below override
    //private GameObject penToBuild;


//idea
/*
    void Start()
    {
        penToBuild = standardPenPrefab;
    }

    */

    public void SelectPen(Node node)
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
        DeselectNode();

       
    }
    
    
    public AnimalPenBlueprint GetPenToBuild()
    {
        return penToBuild;
    }
    //add type of pen to select "level of pen"
    //selectedpen = null;
    
}
