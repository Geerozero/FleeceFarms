using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public int maxAnimals;
    public int currentAnimals;

    void Awake()
    {
        maxAnimals = 20;
        currentAnimals = 0;
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
