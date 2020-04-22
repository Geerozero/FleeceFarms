using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alpaca : MonoBehaviour
{
    public void OnMouseDown()
    {
        FindObjectOfType<AudioManager>().Play("Alpaca");

    }
}

