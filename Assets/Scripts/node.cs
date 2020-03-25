﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class node : MonoBehaviour {
    BuildManager buildManager;
    public Color hoverColor;
    public Vector3 positionOffset;
    public Color notEnoughMoneyColor;

    private GameObject animalPen;
    
    public bool isUpgraded = false;

    public AnimalPenBlueprint animalPenBlueprint;

    private Color startColor;
    
    private Renderer rend;

void Start ()
{
    rend = GetComponent<Renderer>();
    startColor = rend.material.color;

    buildManager = BuildManager.instance;
}

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }
    void OnMouseDown()
    {
        //if (UnityEventQueueSystem.current.IsPointerOverGameObject())
        //    return;


        if (animalPen != null)
        {
            
            buildManager.selectPen(this);
            Debug.Log("cant build here - to do display on screen");
            
            return;


        }
        if (!buildManager.CanBuild)
            return;

        BuildAnimalPen(buildManager.GetPenToBuild());


        //build animalpen
        //GameObject penToBuild = BuildManager.instance.GetPenToBuild();
        //animalPen = (GameObject)Instantiate(penToBuild, transform.position + positionOffset, transform.rotation);

    }
    void BuildAnimalPen (AnimalPenBlueprint blueprint)
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

        //add effect

        Debug.Log("pen has built");
    }

    public void UpgradePen()
    {
        if (InventoryManager.money < animalPenBlueprint.upgradeCost)
        {
            Debug.Log("no upgrade for you");
            return;
        }
        InventoryManager.money -= animalPenBlueprint.upgradeCost;
        //goodbye old pen
        Destroy(animalPen);

        //build pen 2.0
        GameObject _pen = (GameObject)Instantiate(animalPenBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        animalPen = _pen;

        //add effect

        isUpgraded = true;

        Debug.Log("pen 2.0");

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
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}