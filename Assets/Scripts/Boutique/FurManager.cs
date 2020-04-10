using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FurManager : MonoBehaviour
{
    public static FurManager instance;

    public Dictionary<int, FurItem> furs = new Dictionary<int, FurItem>();

    public Dictionary<int, FuritemSave> saves = new Dictionary<int, FuritemSave>();

    void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;

        DontDestroyOnLoad(gameObject);

        /*---building furs dictionary, all furs in the Resources folder are loaded here on Awake---*/
        FurItem[] fursFound = Resources.LoadAll<FurItem>("Furs");
        for (int i = 0; i < fursFound.Length; i++)
        {
            furs.Add(fursFound[i].furID, fursFound[i]);
        }
    }

    [System.Serializable]
    public class FuritemSave
    {
        public int furID;
        public bool isPurchased;
    }

    void StoreFurs()
    {
        saves.Clear();

        for(int i = 0; i < furs.Count; i++)
        {
            saves.Add(furs.ElementAt(i).Value.furID, furs.ElementAt(i).Value.GetFurItemSave());
        }
    }

    void RestoreFurs()
    {
        for(int i = 0; i < saves.Count; i++)
        {
            furs[saves.ElementAt(i).Key].LoadFurSave(saves.ElementAt(i).Value);
        }
    }

    public void SaveFursToFile()
    {
        StoreFurs();
        FileHelper.SaveDataFile(saves, "FursData");
    }

    public void LoadFursFromFile()
    {
        saves = FileHelper.LoadDataFile<Dictionary<int, FuritemSave>>("FursData");
        if(saves == null)
            saves = new Dictionary<int, FuritemSave>();
        RestoreFurs();
    }

    public void ResetFurs()
    {
        for (int i = 0; i < furs.Count; i++)
        {
            if (furs.ElementAt(i).Value.name != "White Fur")
                furs.ElementAt(i).Value.isPurchased = false;
        }
    }
}
