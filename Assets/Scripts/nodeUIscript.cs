using UnityEngine;
using UnityEngine.UI;

public class NodeUIscript : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;


    private Node target;

    public void SetTarget (Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.animalPenBlueprint.upgradeCost;
            upgradeButton.interactable = true;

        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.animalPenBlueprint.GetSellAmount();


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
