using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class FarmUIManager : MonoBehaviour
{
    [Header("Feedback text")]
    public TextMeshProUGUI farmAnnounceText;
    public Text moneyText;
    bool announcingText;

    [Header("Gift")]
    public int giftMoney;

    [Header("UI References")]
    public GameObject FarmUIContainer;
    public GameObject marketButton;
    public Button customizeButton;
    public Button shearButton;
    public GameObject pauseMenu;
    public GameObject buyPenButton;
    public GameObject gift;
    public Text editNameButtonText;
    public TextMeshProUGUI bondPointText;

    [Header("Pen UI Things")]
    public GameObject viewPens;
    public Button viewPensButton;
    public Text viewPensButtonText;
    public Button pen01Ready;
    public Button pen02Ready;
    public Button pen03Ready;
    public Button pen04Ready;

    [Header("Animal Name text")]
    public TextMeshProUGUI animalNameText;

    //camera script for resetting camera to view
    [Header("Camera reference")]
    public CameraFarmMovement cameraScript;

    [Header("Inventory Manager")]
    //public InventoryManager inventory;

    //boolean for other functions to check
    [SerializeField]
    private bool isInteracting;

    //reference for Animal being interacted with for working with and calling functions
    private GameObject animalReference;
    private Animal animalStats;

    private string animalName;
    [Header("Name field")]
    public GameObject nameInputTextField;

    //variable for messing with coroutine logic
    private Coroutine lastCoroutine;

    private LevelManager.AnimalSave animalSave;
    private bool checkPens;

    private void Start()
    {
        isInteracting = false;

        if(PlayerManager.instance.playerSave.tutorialGift == false)
        {
            gift.SetActive(true);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }

        if(isInteracting)
        {
            buyPenButton.SetActive(false);
            viewPensButton.gameObject.SetActive(false);
        }
        else
        {
            viewPensButton.gameObject.SetActive(true);
            buyPenButton.SetActive(true);
        }

        if(PlayerManager.instance.playerSave.shearLevel >= 3)
        {
            viewPensButton.interactable = true;
            viewPensButton.image.rectTransform.sizeDelta = new Vector2(100, 35);
            viewPensButtonText.text = "View Pens";
        }
        else if(PlayerManager.instance.playerSave.shearLevel < 3)
        {
            viewPensButton.interactable = false;
            viewPensButton.image.rectTransform.sizeDelta = new Vector2(145, 35);
            viewPensButtonText.text = "Level 3 Required";
        }

        if(PlayerManager.instance.playerSave.pensBought <= 0)
        {
            marketButton.GetComponent<Button>().interactable = false;
        }
        else if(PlayerManager.instance.playerSave.pensBought > 0)
        {
            marketButton.GetComponent<Button>().interactable = true;
        }

        if(checkPens)
        {
            UpdateShearAllButtons();
        }

        UpdateMoneyText();
    }

    //SCENE TRANSITIONS

    public void LoadMarket()
    {
        LevelManager.instance.SaveAnimalsToFile();
        PlayerManager.instance.SavePlayerDataToFile();
        SceneManager.LoadScene("Market");
    }

    public void LoadBoutique()
    {
        BoutiqueManager.instance.selectedAnimal = animalSave;
        LevelManager.instance.SaveAnimalsToFile();
        PlayerManager.instance.SavePlayerDataToFile();
        SceneManager.LoadScene("Boutique");
    }

    public void SetIsInteracting(bool newInteractingStatus)
    {
        Debug.Log("Set interacting!");
        isInteracting = newInteractingStatus;
    }

    public bool GetIsInteracting()
    {
        return isInteracting;
    }

    //animal has been clicked on, show UI
    public void ActivateUI()
    {
        FarmUIContainer.SetActive(true);
        marketButton.SetActive(false);
        //set name of animal in UI
        animalNameText.SetText(animalStats.name); //get current Animal's name
        //UpdateBondPointDisplay();
    }

    //  ////////////////////
    //back on UI hit
    public void BackCallDeactivateUI()
    {
        //set interacting to false
        SetIsInteracting(false);
        //reset camera position
        cameraScript.DefaultCameraPositionSet();
        //deactivate UI
        FarmUIContainer.SetActive(false);
        marketButton.SetActive(true);
        //
    }

    //call this after each Feed/Brush as those modify bond point values
    public void UpdateBondPointDisplay()
    {
        bondPointText.SetText(animalStats.GetAnimalBond().ToString()); //get bond point of animal for UI
    }

    //events to be called through UI Buttons
    public void FeedCall()
    {
        //0 is hungry, 100 is full
        animalStats.ChangeAnimalFood(20);
        FindObjectOfType<AudioManager>().Play("Feed");
        SetAnnounceText(animalName + " is fed, hunger is at: " + animalStats.GetAnimalHunger());
        //UpdateBondPointDisplay();
    }

    public void BrushCall()
    {
        animalStats.ChangeAnimalClean(20);
        FindObjectOfType<AudioManager>().Play("Brush");
        SetAnnounceText(animalName + " is brushed, clean is at: " + animalStats.GetAnimalClean());
        //UpdateBondPointDisplay();
    }

    public void ShearCall()
    {
        //check if animal is sheerable
        if(animalStats.CheckIfAnimalSheerable())
        {
            //SetAnnounceText(animalName + " is sheared. Got 1 fur");
            animalStats.ChangeAnimalFurGrowth(-100);
            //Adds money to player inventory based on a current sell price of a fur (just gets it from
            //first fur in fur dictionary since they all have the same sell price)
            InventoryManager.instance.money += FurManager.instance.furs.ElementAt(0).Value.sellPrice;
            
            FindObjectOfType<AudioManager>().Play("Shear");

            //inventory.AddToFurInventory(animalStats.GetAnimalFurInventoryIndex(), 1);
        }

        else
        {
            //SetAnnounceText(animalName + " is not ready to be sheared, currently at " + animalStats.GetAnimalFurGrowth() + "% fur growth");
        }
    }

    // ////////////INPUT TEXT FIELD START
    //change animal name
    public void ChangeAnimalName(string newName)
    {
        //change in animal stats
        animalStats.ChangeAnimalName(newName);

        //change in UI reference
        animalName = newName;
    }

    public void ShowOrHideNameFieldInput()
    {
        if(!nameInputTextField.activeSelf)
        {
            animalNameText.SetText("");
            nameInputTextField.SetActive(true);
            editNameButtonText.text = "Save Name";
        }
        else
        {
            nameInputTextField.SetActive(false);
            animalNameText.SetText(animalName);
            editNameButtonText.text = "Edit Name";
        }
    }

    public void HideNameFieldInput()
    {
        nameInputTextField.SetActive(false);
        animalNameText.SetText(animalName);
    }

    // //////////////// INPUT TEXT FIELD END

    //function to call to set announcement text that appears on screen for two seconds, should reset timer if clicked while running
    private void SetAnnounceText(string textToDisplay)
    {
        if(announcingText)
        {
            Debug.Log("Already displaying");
            StopCoroutine(lastCoroutine);
            announcingText = false;
        }

        //see text display below
        lastCoroutine = StartCoroutine(AnnounceTextTimer(textToDisplay));
    }

    //timer yield return enumerator
    IEnumerator AnnounceTextTimer(string announceText)
    {
        announcingText = true;
        farmAnnounceText.SetText(announceText);

        yield return new WaitForSeconds(1f);

        farmAnnounceText.SetText("");

        announcingText = false;
    }

    //set reference to Animal to apply UI actions to
    public void GetReferenceOfAnimalBeingInteractedWith(GameObject animalGameObject)
    {
        Debug.Log("Setting to reference: " + animalGameObject);
        animalReference = animalGameObject;
        animalStats = animalReference.GetComponent<Animal>();
        animalSave = animalStats.GetAnimalSave();
        animalName = animalStats.GetAnimalName();
    }

    public void ResetReferenceOfAnimalBeingInteractedWith()
    {
        Debug.Log("Resetting animal reference");
        animalReference = null;
    }

    public void UpdateMoneyText()
    {
        moneyText.text = InventoryManager.instance.money.ToString();
    }

    public bool CheckIfAnimalsAreReady(string penName, int count, int animalsReady)
    {
        for(int i = 0; i < LevelManager.instance.animals.Count; i++)
        {
            if(LevelManager.instance.animals.ElementAt(i).pen == penName)
            {
                if(LevelManager.instance.animals.ElementAt(i).GetIsFleeceGrown())
                {
                    animalsReady++;
                }

                count++;
            }
        }

        if (count == animalsReady && count != 0)
            return true;
        else
            return false;
    }

    public void UpdateShearAllButtons()
    {
        if(CheckIfAnimalsAreReady("Pen01", 0, 0))
        {
            pen01Ready.interactable = true;
        }
        if(!CheckIfAnimalsAreReady("Pen01", 0, 0))
        {
            pen01Ready.interactable = false;
        }

        if (CheckIfAnimalsAreReady("Pen02", 0, 0))
        {
            pen02Ready.interactable = true;
        }
        if (!CheckIfAnimalsAreReady("Pen02", 0, 0))
        {
            pen02Ready.interactable = false;
        }

        if (CheckIfAnimalsAreReady("Pen03", 0, 0))
        {
            pen03Ready.interactable = true;
        }
        if (!CheckIfAnimalsAreReady("Pen03", 0, 0))
        {
            pen03Ready.interactable = false;
        }

        if (CheckIfAnimalsAreReady("Pen04", 0, 0))
        {
            pen04Ready.interactable = true;
        }
        if (!CheckIfAnimalsAreReady("Pen04", 0, 0))
        {
            pen04Ready.interactable = false;
        }
    }

    public void DisplayPens()
    {
        if(!checkPens)
        {
            viewPens.SetActive(true);
            checkPens = true;
            viewPensButtonText.text = "Hide Pens";
        }
        else if(checkPens)
        {
            checkPens = false;
            viewPens.SetActive(false);
            viewPensButtonText.text = "View Pens";
        }
    }

    public void ShearAnimalsInPen(string penName)
    {
        for (int i = 0; i < LevelManager.instance.animals.Count; i++)
        {
            if (LevelManager.instance.animals.ElementAt(i).pen == penName)
            {
                LevelManager.instance.animals.ElementAt(i).ChangeAnimalFurGrowth(-100);
                InventoryManager.instance.money += FurManager.instance.furs.ElementAt(0).Value.sellPrice;
            }
        }
    }

    public void TutorialGiftMoney()
    {
        InventoryManager.instance.money += giftMoney;
        PlayerManager.instance.playerSave.money = InventoryManager.instance.money;
        PlayerManager.instance.playerSave.tutorialGift = true;
        gift.SetActive(false);
    }
}
