using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToyInInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI toyName;
    [SerializeField] Image toyImage;
    [SerializeField] GameObject placed;
    [SerializeField] LocationInformation locationToUpdate;
    [SerializeField] Button button;

    Toy toy;

    public void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(ChangeToy);
    }

    public void SetInfo(Toy t)
    {
        if(t != null)
        {
            toyImage.sprite = t.toySprite;
            toyName.text = t.toy;
            placed.SetActive(t.hasBeenPlaced);
            button.interactable = (!t.hasBeenPlaced);
        }
        else
        {
            toyName.text = "";
            placed.SetActive(false);
            button.interactable = true;
        }
        toy = t;
    }

    void ChangeToy()
    {
        locationToUpdate.UpdateToyAtLocation(toy);
    }
}
