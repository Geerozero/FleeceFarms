using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour {
    BuildManager buildManager;
    public Color hoverColor;
    public Vector3 positionOffset;

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
        if (InventoryManager.money < penToBuild.cost)
        {
            Debug.Log("you poor");
            return;
        }

        InventoryManager.money -= blueprint.cost;
        GameObject _pen = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaterion.identity);
        animalPen = _pen;

        AnimalPenBlueprint = blueprint;

        //add effect

        Debug.
    }

    
    void OnMouseEnter()
    {
        
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}
