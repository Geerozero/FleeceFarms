using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutiqueFurButton : MonoBehaviour
{
    [Header("Associated Fur Item")]
    public FurItem furItem;

    [Header("Button Referenecs")]
    public Text buttonText;
    public Image itemImage;
    public Image lockedImage;
    
    /*---Private Variables---*/
    private Button button;
    private Material material;

    void Start()
    {
        /*---Connect fur item attributes to this button---*/
        button = GetComponent<Button>();
        buttonText.text = furItem.name;
        itemImage.sprite = furItem.buttonImage;
        material = furItem.furMaterial;
    }

    void Update()
    {
        /*---Unlock button if the item was purchased---*/
        if(furItem.isPurchased == false)
        {
            button.interactable = false;
            lockedImage.gameObject.SetActive(true);
        }
        else if(furItem.isPurchased)
        {
            button.interactable = true;
            lockedImage.gameObject.SetActive(false);
        }
    }

    public void ChangeFur()
    {
        /*---Changes furItem and changes animals material---*/
        BoutiqueManager.instance.animalInfo.fur = furItem;

        if(BoutiqueManager.instance.selectedAnimal.animalType == Animal.AnimalType.Alpaca)
        {
            BoutiqueManager.instance.animalInfo.wool.GetComponent<SkinnedMeshRenderer>().material = FurManager.instance.furs[furItem.furID].furMaterial;
        }
        else
        {
            BoutiqueManager.instance.animalInfo.wool.GetComponent<MeshRenderer>().material = FurManager.instance.furs[furItem.furID].furMaterial;
        }
    }
}
