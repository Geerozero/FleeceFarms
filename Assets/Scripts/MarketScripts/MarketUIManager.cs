using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketUIManager : MonoBehaviour
{
    [Header("UI containers")]
    public GameObject BuyContainer;
    public GameObject SellContainer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DisplayBuy()
    {
        BuyContainer.SetActive(true);
        SellContainer.SetActive(false);
    }

    public void DisplaySell()
    {
        BuyContainer.SetActive(false);
        SellContainer.SetActive(true);
    }


}
