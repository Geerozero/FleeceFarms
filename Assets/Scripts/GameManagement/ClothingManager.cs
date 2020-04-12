using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ClothingManager : MonoBehaviour
{
    public static ClothingManager instance;
    
    public Dictionary<int, OutfitItem> clothes = new Dictionary<int, OutfitItem>();

    public Dictionary<int, OutfitSave> saves = new Dictionary<int, OutfitSave>();

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

    [System.Serializable]
    public class OutfitSave
    {
        public int clothingID;
        public bool isPurchased;
    }

    void StoreClothes()
    {
        saves.Clear();

        for(int i = 0; i < clothes.Count; i++)
        {
            saves.Add(clothes.ElementAt(i).Value.clothingID, clothes.ElementAt(i).Value.GetOutfitSave());
        }
    }

    void RestoreClothes()
    {
        for(int i = 0; i < saves.Count; i++)
        {
            clothes[saves.ElementAt(i).Key].LoadClothingSave(saves.ElementAt(i).Value);
        }
    }

    public void SaveClothesToFile()
    {
        StoreClothes();
        FileHelper.SaveDataFile(saves, "ClothesData");
    }

    public void LoadClothesFromFile()
    {
        saves = FileHelper.LoadDataFile<Dictionary<int, OutfitSave>>("ClothesData");
        if (saves == null)
            saves = new Dictionary<int, OutfitSave>();
        RestoreClothes();
    }

    public void ResetClothes()
    {
        for(int i = 0; i < clothes.Count; i++)
        {
            if(clothes.ElementAt(i).Value.name != "None")
                clothes.ElementAt(i).Value.isPurchased = false;
        }
    }
}
