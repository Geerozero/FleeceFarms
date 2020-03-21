using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class FurItem : ScriptableObject
{
    /*---necessary info for our fur items---*/
    public new string name;
    public int cost;
    public bool isPurchased;
    
    public Sprite buttonImage;
    public Material furMaterial;
}
