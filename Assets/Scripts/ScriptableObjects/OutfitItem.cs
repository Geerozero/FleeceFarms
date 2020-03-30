using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OutfitItem : ScriptableObject
{
    /*---Necessary info for our outfit items---*/
    
    public enum ClothingType
    {
        Accessory, Torso, Shoe
    }
    
    public new string name;
    public int clothingID;
    public ClothingType clothingType;

    public int cost;
    public bool isPurchased;

    public Sprite buttonImage;
    public GameObject item;
    
    [Header("Position and Scale for an Alpaca item")]
    public Vector3 spawnPositionAlpaca;
    public Vector3 spawnScaleAlpaca;
    
    [Header("Position and Scale for a Sheep item")]
    public Vector3 spawnPositionSheep;
    public Vector3 spawnScaleSheep;
    
    [Header("Position and Scale for a Rabbit item")]
    public Vector3 spawnPositionRabbit;
    public Vector3 spawnScaleRabbit;
}
