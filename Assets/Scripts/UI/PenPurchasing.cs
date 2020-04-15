using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PenPurchasing : MonoBehaviour
{
    public int costOfNewPen;
    //if you change the cost of the pens here
    //please also update them on the Node gameobjects as well
    //all pens should be the same price

    public GameObject pen01Selection;
    public GameObject pen02Selection;
    public GameObject pen03Selection;
    public GameObject pen04Selection;

    public GameObject player;
    public GameObject localManagers;
    public Button purchaseButton;
    public Button cancelButton;

    private Node pen01Info;
    private Node pen02Info;
    private Node pen03Info;
    private Node pen04Info;
    private bool allPurchased;
    public bool isBuilding;

    private BasicInteractions basicInteractions;

    void Start()
    {
        pen01Info = pen01Selection.GetComponent<Node>();
        pen02Info = pen02Selection.GetComponent<Node>();
        pen03Info = pen03Selection.GetComponent<Node>();
        pen04Info = pen04Selection.GetComponent<Node>();

        basicInteractions = player.GetComponent<BasicInteractions>();
    }

    void Update()
    {
        CheckAllPensPurchased();

        if(InventoryManager.instance.GetMoney() >= costOfNewPen && !allPurchased)
        {
            purchaseButton.interactable = true;
        }
        else
        {
            purchaseButton.interactable = false;
        }

        if(isBuilding)
        {
            DisplayPossiblePens();
        }
        else
        {
            HidePossiblePens();
        }
    }

    public void DisplayPossiblePens()
    {
        CheckPossibilities();
        cancelButton.gameObject.SetActive(true);

        if(!pen01Info.GetWasPurchased())
        {
            pen01Selection.SetActive(true);
        }
        if (!pen02Info.GetWasPurchased())
        {
            pen02Selection.SetActive(true);
        }
        if (!pen03Info.GetWasPurchased())
        {
            pen03Selection.SetActive(true);
        }
        if (!pen04Info.GetWasPurchased())
        {
            pen04Selection.SetActive(true);
        }

        basicInteractions.isPurchasing = true;
    }

    public void EnableBuilding()
    {
        isBuilding = true;
    }

    void CheckPossibilities()
    {
        //i know this is awful, dont yell at me

        if(PlayerManager.instance.playerSave.pen01Purchased)
        {
            pen01Info.SetWasPurchased(true);
        }
        if(!PlayerManager.instance.playerSave.pen01Purchased)
        {
            pen01Info.SetWasPurchased(false);
        }
        
        if (PlayerManager.instance.playerSave.pen02Purchased)
        {
            pen02Info.SetWasPurchased(true);
        }
        if (!PlayerManager.instance.playerSave.pen02Purchased)
        {
            pen02Info.SetWasPurchased(false);
        }
        
        if (PlayerManager.instance.playerSave.pen03Purchased)
        {
            pen03Info.SetWasPurchased(true);
        }
        if (!PlayerManager.instance.playerSave.pen03Purchased)
        {
            pen03Info.SetWasPurchased(false);
        }
        
        if (PlayerManager.instance.playerSave.pen04Purchased)
        {
            pen04Info.SetWasPurchased(true);
        }
        if (!PlayerManager.instance.playerSave.pen04Purchased)
        {
            pen04Info.SetWasPurchased(false);
        }
    }

    public void HidePossiblePens()
    {
        isBuilding = false;
        pen01Selection.SetActive(false);
        pen02Selection.SetActive(false);
        pen03Selection.SetActive(false);
        pen04Selection.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        basicInteractions.isPurchasing = false;
    }

    void CheckAllPensPurchased()
    {
        if(pen01Info.GetWasPurchased() && pen02Info.GetWasPurchased() && pen03Info.GetWasPurchased() && pen04Info.GetWasPurchased())
        {
            allPurchased = true;
            purchaseButton.GetComponentInChildren<Text>().text = "All Pens Unlocked";
        }
        else
        {
            allPurchased = false;
        }
    }    
}
