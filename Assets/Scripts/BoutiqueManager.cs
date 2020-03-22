using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutiqueManager : MonoBehaviour
{
    public GameObject FurSelection;

    public GameObject basicContainer;
    public GameObject specialContainer;
    public GameObject royalContainer;

    void Start()
    {
        FurSelection.SetActive(true);
        basicContainer.SetActive(true);
        specialContainer.SetActive(false);
        royalContainer.SetActive(false);
    }

    public void DisplayBasicFur()
    {
        basicContainer.SetActive(true);
        specialContainer.SetActive(false);
        royalContainer.SetActive(false);
    }

    public void DisplaySpecialFur()
    {
        basicContainer.SetActive(false);
        specialContainer.SetActive(true);
        royalContainer.SetActive(false);
    }

    public void DisplayRoyalFur()
    {
        basicContainer.SetActive(false);
        specialContainer.SetActive(false);
        royalContainer.SetActive(true);
    }

    public void GoToFarm()
    {
        SceneManager.LoadScene("Farm_test");
    }
}
