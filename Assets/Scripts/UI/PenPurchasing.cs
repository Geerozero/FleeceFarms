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

    private InventoryManager inventoryManager;
    private BasicInteractions basicInteractions;

    void Start()
    {
        pen01Info = pen01Selection.GetComponent<Node>();
        pen02Info = pen02Selection.GetComponent<Node>();
        pen03Info = pen03Selection.GetComponent<Node>();
        pen04Info = pen04Selection.GetComponent<Node>();

        inventoryManager = localManagers.GetComponent<InventoryManager>();
        basicInteractions = player.GetComponent<BasicInteractions>();
    }

    void Update()
    {
        CheckAllPensPurchased();

        if(inventoryManager.GetMoney() >= costOfNewPen && !allPurchased)
        {
            purchaseButton.interactable = true;
        }
        else
        {
            purchaseButton.interactable = false;
        }
    }

    public void DisplayPossiblePens()
    {
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

    public void HidePossiblePens()
    {
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
