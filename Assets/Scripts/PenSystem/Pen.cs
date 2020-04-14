using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    public bool wasPurchased;

    public PlayerManager.PenSave GetPenSave()
    {
        PlayerManager.PenSave newSave = new PlayerManager.PenSave();

        newSave.position = transform.position;
        newSave.wasPurchased = wasPurchased;

        return newSave;
    }

    public void LoadPenSave(PlayerManager.PenSave save)
    {
        transform.position = save.position;
        wasPurchased = save.wasPurchased;
    }
}
