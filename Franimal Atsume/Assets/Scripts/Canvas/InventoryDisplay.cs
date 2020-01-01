using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject inventoryGrid;
    [SerializeField] Button prev;
    [SerializeField] Button next;
    [SerializeField] TextMeshProUGUI description;

    ToyInDisplay toy;

    int currentIndex = 0;
    List<Toy> ownedToys;
        
    public void OpenInventory(List<Toy> oT)
    {
        currentIndex = 0;
        this.ownedToys = oT;
        foreach(Transform t in inventoryGrid.transform)
        {
            if (currentIndex < ownedToys.Count + 1)
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponent<ToyInDisplay>().SetInfo((currentIndex > 0 ? ownedToys[currentIndex - 1] : null));
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            currentIndex += 1;
        }
        next.interactable = (ownedToys.Count > 5);
        prev.interactable = false;
    }

    public void NextPage()
    {
        foreach (Transform t in inventoryGrid.transform)
        {
            if (currentIndex < ownedToys.Count + 1)
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponent<ToyInDisplay>().SetInfo(ownedToys[currentIndex - 1]);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            currentIndex += 1;
        }
        next.interactable = (currentIndex + 6 < ownedToys.Count + 1);
        prev.interactable = true;
    }

    public void PrevPage()
    {
        currentIndex -= 6;
        foreach (Transform t in inventoryGrid.transform)
        {
            if (currentIndex < ownedToys.Count + 1)
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponent<ToyInDisplay>().SetInfo((currentIndex > 0 ? ownedToys[currentIndex - 1] : null));
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            currentIndex += 1;
        }
        next.interactable = true;
        prev.interactable = (currentIndex - 6 >= 0);
    }

    public void UpdateDescription(string desc, ToyInDisplay t)
    {
        if(toy != null)
        {
            toy.Deselect();
        }
        toy = t;
        description.text = desc;
        
    }


    public void CloseInventory()
    {
        currentIndex = 0;
        this.gameObject.SetActive(false);
        GManager.instance.PauseGame();
    }
}
