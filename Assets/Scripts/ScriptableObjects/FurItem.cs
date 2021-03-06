﻿using System.Collections;
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
    public int sellPrice;
    public bool isPurchased;
    
    public Sprite buttonImage;
    public Material furMaterial;

    public FurManager.FuritemSave GetFurItemSave()
    {
        FurManager.FuritemSave newSave = new FurManager.FuritemSave();

        newSave.furID = furID;
        newSave.isPurchased = isPurchased;
        newSave.sellPrice = sellPrice;

        return newSave;
    }

    public void LoadFurSave(FurManager.FuritemSave save)
    {
        isPurchased = save.isPurchased;
        sellPrice = save.sellPrice;
    }
}
