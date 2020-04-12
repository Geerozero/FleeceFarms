
using UnityEngine;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update

    public AnimalPenBlueprint animalPen;

    BuildManager buildManager;
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardPen()
    {
        Debug.Log("standard pen selected");
        buildManager.SelectAnimalPenToBuild(animalPen);
    }
}
