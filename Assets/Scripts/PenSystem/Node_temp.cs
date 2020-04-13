using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_temp : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
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
