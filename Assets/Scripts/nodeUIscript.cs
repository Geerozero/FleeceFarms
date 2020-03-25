using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeUIscript : MonoBehaviour
{
    public GameObject ui;
    private node target;

    public void SetTarget (node _target)
    {
        target = _target;

        transform.position = target.positionOffset;
        //set above to buld posiotion 

        ui.SetActive(true);
    }

    public void Hide ()
    {
        ui.SetActive(false);
    }
}
