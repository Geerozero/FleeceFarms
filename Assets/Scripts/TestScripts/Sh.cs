using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sh : MonoBehaviour
{
    void Update()
    {
        EasterEgg();
    }

    void EasterEgg()
    {
        string[] sams = { "Sam", "Sammy", "Sam Sam", "Samuel", "Sammy Sam", "Samu-san", "Mr. Sam", "Sam I Am", "I Am Sam", "SamCake", "Sammie" };

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.M))
        {
            for (int i = 0; i < LevelManager.instance.animals.Count; i++)
            {
                int random = UnityEngine.Random.Range(0, sams.Length);

                LevelManager.instance.animals[i].name = sams[random];
            }
        }
    }
}
