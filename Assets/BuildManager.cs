using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one instance");
        }
        instance = this;
        
    }

    public GameObject standardPenPrefab;
    private node selectedPen;

    public nodeUIscript nodeUI;

    void Start()
    {
        penToBuild = standardPenPrefab;
    }
    private GameObject penToBuild;

    public GameObject GetPenToBuild ()
    {
        return penToBuild;
    }

    public void selectPen(node node)
    {
        selectedPen = node;
        //penToBuild = null;
        nodeUI.SetTarget(node);
    }

    //add type of pen to select "level of pen"
    //selectedpen = null;
}
