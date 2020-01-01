using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToyInDisplay: MonoBehaviour
{

    public void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(ChangeToy);
    }

    #region Variables and Functions for this GameObject
    [SerializeField] TextMeshProUGUI toyName;
    [SerializeField] TextMeshProUGUI toyCost;
    [SerializeField] Image toyImage;
    [SerializeField] GameObject placed;
    [SerializeField] Button button;

    bool inShop;

    Toy toy;

    public void SetInfo(Toy t, bool inShop = false)
    {
        if(t != null)
        {
            toyImage.sprite = t.toySprite;
            toyName.text = t.toy;
            Debug.Log(t.hasBeenPlaced);
            placed.SetActive(t.hasBeenPlaced);
            button.interactable = (!t.hasBeenPlaced);
            toyCost.text = (inShop ? t.cost.ToString() : "");
        }
        else
        {
            toyName.text = "";
            placed.SetActive(false);
            button.interactable = true;
            toyCost.text = "";
        }
        this.inShop = inShop;
        toy = t;
    }
    #endregion


    #region Handles Button Clicks
    [SerializeField] InventoryDisplay invDisplay;
    [SerializeField] StoreDisplay storeDisplay;

    bool selected = false;

    void ChangeToy()
    {
        if (!inShop)
        {
            if (!selected)
            {
                button.Select();
                invDisplay.UpdateDescription((toy == null ? "" : toy.toyDescription), this);
                selected = true;
            }
            else
            {
                GManager.instance.ChangeToyAtLocation(toy);
                invDisplay.CloseInventory();

            }
        }
        else
        {
            if (!selected)
            {
                button.Select();
                storeDisplay.UpdateDescription(toy.toyDescription, this);
                selected = true;
            }
            else
            {
                if(GManager.instance.CanPurchaseToy(toy))
                {
                    GManager.instance.AddToyToInventory(toy);
                    storeDisplay.CloseStore();
                }
                else
                {
                    storeDisplay.DisplayErrorMessage();
                }
            }
        }
    }

    public void Deselect()
    {
        selected = false;
    }
    #endregion
}
