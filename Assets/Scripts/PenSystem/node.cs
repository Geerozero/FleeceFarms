using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {


    public Color hoverColor;
    
    public Vector3 positionOffset;
    
    public Color notEnoughMoneyColor;
    
    public GameObject penPurchase;

    [HideInInspector]
    public GameObject animalPen;
    [HideInInspector]
    public bool isUpgraded = false;
    [HideInInspector]
    public AnimalPenBlueprint animalPenBlueprint;

    private Color startColor;
    
    private Renderer rend;

    private PenPurchasing penPurchasing;

    BuildManager buildManager;

    public GameObject player;
    private bool wasPurchased;
    public GameObject inventory;
    private InventoryManager inventoryManager;

    //pen traits
    public int penLevel = 0;
    public int penCost = 0;
    public int penSpeed = 0;
    public int temp = 100;

    void Start ()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
        penPurchasing = penPurchase.GetComponent<PenPurchasing>();
        inventoryManager = inventory.GetComponent<InventoryManager>();
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    
    void Update()
    {
        /* OnMouseDown created problems with how we recieve input from the player
         * you had to click certain spots on the nodes that were far away for a pen to spawn
         * so it better to create a ray from the camera to where the mouse is pointing
         * and checking if the selected object is this object
         * 
         * However, it is not ideal to have input being generated on every node
         * but with a week left... this what we're doing lol */

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if(hit.transform.gameObject == this.gameObject)
                {
                    /* NOTE: it looks like there is supposed to be a material change when the mouse hovers over a node
                     * in the OnMouseDown method
                     * however this is not working, i'm not sure why
                     * we don't necessarily need this feedback as the buttons become interactable when a pen can be purchased
                     * so feel free to fix this functionality if you'd like */

                    BuildAnimalPen(buildManager.GetPenToBuild());
                    inventoryManager.SubtractMoney(penCost);
                    wasPurchased = true;
                    
                }
            }
        }
    }

    public bool GetWasPurchased()
    {
        return wasPurchased;
    }

    /*void OnMouseDown ()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (animalPen != null)
        {
            buildManager.SelectPen(this);
            Debug.Log("cant build here - to do display on screen");
            
            return;
        }
        if (!buildManager.CanBuild)
            return;
        //current script below removed for test
        BuildAnimalPen(buildManager.GetPenToBuild());

        //build animalpen
        //GameObject penToBuild = BuildManager.instance.GetPenToBuild();
      //  animalPen = (GameObject)Instantiate(penToBuild, transform.position + positionOffset, transform.rotation);
    }*/
    
    public void BuildAnimalPen (AnimalPenBlueprint blueprint)
    {
        if (InventoryManager.money < blueprint.cost)
        {
            Debug.Log("you poor");
            return;
        }

        InventoryManager.money -= blueprint.cost;
        GameObject _pen = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        animalPen = _pen;

        animalPenBlueprint = blueprint;
        penLevel = 0;
        //add effect

        penPurchasing.HidePossiblePens();

        Debug.Log("pen has built");
    }


    public void UpgradePen()
    {
        switch (penLevel)
        {
            case 0:
                if (InventoryManager.money < animalPenBlueprint.upgradeCost)
                {
                    Debug.Log("no upgrade for you");
                    return;
                }else
                {
                    InventoryManager.money -= animalPenBlueprint.upgradeCost;
                    //goodbye old pen
                    Destroy(animalPen);

                    //build pen 2.0
                    GameObject _pen = (GameObject)Instantiate(animalPenBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
                    animalPen = _pen;

                    //add effect

                    isUpgraded = true;
                    penLevel += 1;
                    penCost = penCost + (penCost * 2); //changed penCost to int as every other cost item is an int,
                                                       //our player money is in whole numbers
                    penSpeed = penSpeed * 2;

                    Debug.Log("level:" + penLevel);
                }
                break;

            case 1:
                if (InventoryManager.money < animalPenBlueprint.upgradeCost)
                {
                    Debug.Log("no upgrade for you");
                    return;
                }
                else
                {
                    InventoryManager.money -= animalPenBlueprint.upgradeCost;
                    //goodbye old pen
                    Destroy(animalPen);

                    //build pen 2.0
                    GameObject _pen = (GameObject)Instantiate(animalPenBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
                    animalPen = _pen;

                    //add effect

                    isUpgraded = true;
                    penLevel += 1;
                    penCost = penCost + (penCost * 2);
                    penSpeed = penSpeed * 2;

                    Debug.Log("level:" + penLevel);
                }

                break;

            case 2:
                if (InventoryManager.money < animalPenBlueprint.upgradeCost)
                {
                    Debug.Log("no upgrade for you");
                    return;
                }
                else
                {
                    InventoryManager.money -= animalPenBlueprint.upgradeCost;
                    //goodbye old pen
                    //Destroy(animalPen);

                    //build pen 2.0
                    GameObject _pen = (GameObject)Instantiate(animalPenBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
                    animalPen = _pen;

                    //add effect

                    isUpgraded = true;
                    penLevel += 1;
                    penCost = penCost + (penCost * 2);
                    penSpeed = penSpeed * 2;

                    Debug.Log("level:" + penLevel);
                }

                break;

            case 3:
                if (InventoryManager.money < animalPenBlueprint.upgradeCost)
                {
                    Debug.Log("no upgrade for you");
                    return;
                }
                else
                {
                    InventoryManager.money -= animalPenBlueprint.upgradeCost;
                    //goodbye old pen
                   // Destroy(animalPen);

                    //build pen 3.0
                    GameObject _pen = (GameObject)Instantiate(animalPenBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
                    animalPen = _pen;

                    //add effect

                    isUpgraded = true;
                    penLevel += 1;
                    penCost = penCost + (penCost * 3);
                    penSpeed = penSpeed * 2;

                    Debug.Log("level:" + penLevel);
                }
                print("max level");
                //Destroy(Canvas);
                break;                   
        
        }
    }

    public void SellPen()
    {
        InventoryManager.money += animalPenBlueprint.GetSellAmount();

        //add effect

        Destroy(animalPen);
        animalPenBlueprint = null;
    }
    
    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
        
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
