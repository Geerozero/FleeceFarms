using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Customizable : MonoBehaviour
{
    private Animal animal;
    private LevelManager.AnimalSave animalSave;
    public Button customizeButton;

    private bool isSelected;

    void Start()
    {
        /*---Gets Animal component and the Animal Save for this animal that is customizeable---*/
        animal = GetComponent<Animal>();
        animalSave = animal.GetAnimalSave();

        /*---If we are in the Farm scene, we look for the customizable button---*/
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Farm_test"))
            customizeButton = GameObject.Find("Customize").GetComponent<Button>();
    }

    void Update()
    {
        /*---Input code is temporary, i needed it to test animal selection from player input---*/
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Farm_test")) //<---------------------put actual farm scene here
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray;
                RaycastHit hit;

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.name);

                    if (hit.collider.gameObject == this.gameObject)
                    {
                        isSelected = true;
                        Debug.Log(gameObject.name + "is selected: " + isSelected);
                    }
                    else
                    {
                        isSelected = false;
                        Debug.Log(gameObject.name + "isSelected: " + isSelected);
                    }
                }

            }
        }

        if (isSelected == true)
        {
            /*---Calls CustomizeAnimal function when button is pressed---*/
            customizeButton.onClick.AddListener(CustomizeAnimal);
        }
    }

    public void CustomizeAnimal()
    {
        /*---Saves selectedAnimal and Loads the Boutique scene---*/
        BoutiqueManager.instance.selectedAnimal = animalSave;
        LevelManager.instance.SaveAnimalsToFile();
        SceneManager.LoadScene("Boutique");
    }
}
