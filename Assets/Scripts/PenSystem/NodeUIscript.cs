using UnityEngine;
using UnityEngine.UI;

public class NodeUIscript : MonoBehaviour
{
    public GameObject ui;

    /* We won't be selling pens at all
     * upgrading pens might be cut as we don't have upgraded farm fences
     * and we have to associate the animals stats with which pen they are in
     * currently how this is set up does not allow for animal association
     * penSpeed and penLevel on the Node script don't have any association with changing animal stats
     * 
     * also nodes are not always present in the game
     * as they only indicated where the player can build a new pen
     * the pen prefab is what is built and stays active during gameplay */

    //public Text upgradeCost;
    //public Button upgradeButton;

    //public Text sellAmount;

    private Node target;

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            //upgradeCost.text = "$" + target.animalPenBlueprint.upgradeCost;
            //upgradeButton.interactable = true;

        }
        else
        {
            //upgradeCost.text = "DONE";
            //upgradeButton.interactable = false;
        }

        //sellAmount.text = "$" + target.animalPenBlueprint.GetSellAmount();


        ui.SetActive(true);
    }

    public void Hide ()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradePen();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellPen();
        BuildManager.instance.DeselectNode();
    }
}
