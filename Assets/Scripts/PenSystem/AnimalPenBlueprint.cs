using UnityEngine;
using System.Collections;


[System.Serializable]
public class AnimalPenBlueprint : MonoBehaviour
{
    public GameObject prefab;
    public int cost;
    public int outPut;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }

}
