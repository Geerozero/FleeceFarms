using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothingManager : MonoBehaviour
{
    public static ClothingManager instance;
    
    public Dictionary<int, OutfitItem> clothes = new Dictionary<int, OutfitItem>();

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        /*---Adds all outfit items from the Resource folder to the clothes dictionary---*/
        OutfitItem[] clothesFound = Resources.LoadAll<OutfitItem>("Clothing");
        for (int i = 0; i < clothesFound.Length; i++)
        {
            clothes.Add(clothesFound[i].clothingID, clothesFound[i]);
        }
    }
}
