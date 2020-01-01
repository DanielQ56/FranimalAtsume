using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoreDisplay : MonoBehaviour
{
    [SerializeField] GameObject storeGrid;
    [SerializeField] Button prev;
    [SerializeField] Button next;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI seedsOwned;
    [SerializeField] GameObject errorPanel;
    [SerializeField] GameObject successPanel;

    ToyInDisplay toy;

    int currentIndex = 0;
    List<Toy> unownedToys = new List<Toy>();

    public void OpenStore(List<Toy> uT, int seeds)
    {
        currentIndex = 0;
        unownedToys = uT;
        foreach (Transform t in storeGrid.transform)
        {
            if (currentIndex < unownedToys.Count)
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponent<ToyInDisplay>().SetInfo(unownedToys[currentIndex], true);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            currentIndex += 1;
        }
        next.interactable = (unownedToys.Count > 4);
        prev.interactable = false;
        seedsOwned.text = string.Format("Seeds: {0}", seeds);
        description.text = "";
    }

    public void NextPage()
    {
        foreach (Transform t in storeGrid.transform)
        {
            if (currentIndex < unownedToys.Count)
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponent<ToyInDisplay>().SetInfo(unownedToys[currentIndex], true);
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            currentIndex += 1;
        }
        next.interactable = (currentIndex + 4 < unownedToys.Count);
        prev.interactable = true;
    }

    public void PrevPage()
    {
        currentIndex -= 4;
        foreach (Transform t in storeGrid.transform)
        {
            if (currentIndex < unownedToys.Count)
            {
                t.gameObject.SetActive(true);
                t.gameObject.GetComponent<ToyInDisplay>().SetInfo(unownedToys[currentIndex], true );
            }
            else
            {
                t.gameObject.SetActive(false);
            }
            currentIndex += 1;
        }
        next.interactable = true;
        prev.interactable = (currentIndex - 4 >= 0);
    }

    public void UpdateDescription(string desc, ToyInDisplay t)
    {
        if (toy != null)
        {
            toy.Deselect();
        }
        toy = t;
        description.text = desc;

    }

    public void DisplayErrorMessage()
    {
        errorPanel.SetActive(true);
    }

    public void DisplaySuccessPanel()
    {
        successPanel.SetActive(true);
    }

    public void CloseStore()
    {
        currentIndex = 0;
        description.text = "";
        GManager.instance.PauseGame();
        this.gameObject.SetActive(false);
    }
}
