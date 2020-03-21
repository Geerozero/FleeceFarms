using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour {
    BuildManager buildManager;
    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject animalPen;
    
    private Color startColor;
    
    private Renderer rend;

void Start ()
{
    rend = GetComponent<Renderer>();
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


        //build animalpen
        GameObject penToBuild = BuildManager.instance.GetPenToBuild();
        animalPen = (GameObject)Instantiate(penToBuild, transform.position + positionOffset, transform.rotation);

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
