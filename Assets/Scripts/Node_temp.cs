using UnityEngine;

public class Node_temp : MonoBehaviour
{
    // Start is called before the first frame update


    public Color hoverColor;

    private Renderer rend;
    private Color startColor;

    void Start ()
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
