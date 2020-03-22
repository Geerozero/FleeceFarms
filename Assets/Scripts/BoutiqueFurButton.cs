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
    private Button _button;
    private Material _material;

    void Start()
    {
        /* make sure item is not unlocked at the start of game
         * item will be unlocked through merchant system */
        furItem.isPurchased = false;
        lockedImage.gameObject.SetActive(true);

        /*---Connect fur item attributes to this button---*/
        _button = GetComponent<Button>();
        buttonText.text = furItem.name;
        itemImage.sprite = furItem.buttonImage;
        _material = furItem.furMaterial;
    }

    void Update()
    {
        /*---Unlock button if the item was purchased---*/
        if(furItem.isPurchased == false)
        {
            _button.interactable = false;
            lockedImage.gameObject.SetActive(true);
        }
        else if(furItem.isPurchased)
        {
            _button.interactable = true;
            lockedImage.gameObject.SetActive(false);
        }
    }

    public void ChangeFur()
    {
        //changes material of animal based on furID
    }
}
