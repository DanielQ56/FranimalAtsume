using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] GameObject inventoryGrid;
    [SerializeField] Button prev;
    [SerializeField] Button next;

    int currentIndex = 0;
    List<Toy> ownedToys;
        
    public void OpenInventory(List<Toy> ownedToys)
    {
        currentIndex = 0;
        this.ownedToys = ownedToys;
        foreach(Transform t in inventoryGrid.transform)
        {
            if (currentIndex < ownedToys.Count + 1)
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponent<ToyInInventory>().SetInfo((currentIndex > 0 ? ownedToys[currentIndex - 1] : null));
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
                t.gameObject.GetComponent<ToyInInventory>().SetInfo(ownedToys[currentIndex - 1]);
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
                t.gameObject.GetComponent<ToyInInventory>().SetInfo((currentIndex > 0 ? ownedToys[currentIndex - 1] : null));
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


    public void CloseInventory()
    {
        currentIndex = 0;
        this.gameObject.SetActive(false);
        GManager.instance.PauseGame();
    }
}
