using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class FurItem : ScriptableObject
{
    /*---Necessary info for our fur items---*/
    public new string name;
    public int furID;
    public int cost;
    public bool isPurchased;
    
    public Sprite buttonImage;
    public Material furMaterial;
}
