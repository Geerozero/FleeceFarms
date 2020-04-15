using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    [SerializeField]
    private int maxAnimals;
    [SerializeField]
    private int currentAnimals;

    void Awake()
    {
        maxAnimals = 20;
        currentAnimals = 0;
    }

    void Update()
    {
        if(gameObject.name == "Pen01" && CheckIfPenIsFull())
        {
            PlayerManager.instance.playerSave.pen01IsFull = true;
        }
        if (gameObject.name == "Pen02" && CheckIfPenIsFull())
        {
            PlayerManager.instance.playerSave.pen02IsFull = true;
        }
        if (gameObject.name == "Pen03" && CheckIfPenIsFull())
        {
            PlayerManager.instance.playerSave.pen03IsFull = true;
        }
        if (gameObject.name == "Pen04" && CheckIfPenIsFull())
        {
            PlayerManager.instance.playerSave.pen04IsFull = true;
        }
    }

    public void AddAnimalToPen()
    {
        currentAnimals += 1;
    }

    public int GetCurrentAnimals()
    {
        return currentAnimals;
    }

    public void SetCurrentAniamls(int numberOfAnimals)
    {
        currentAnimals = numberOfAnimals;
    }

    public bool CheckIfPenIsFull()
    {
        if(currentAnimals >= maxAnimals)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
