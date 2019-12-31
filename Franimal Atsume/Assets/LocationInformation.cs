using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationInformation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LocationName;
    [SerializeField] TextMeshProUGUI ToyName;
    [SerializeField] TextMeshProUGUI FranimalName;
    [SerializeField] Image ToyImage;
    [SerializeField] Image FranimalImage;

    public void LookAtInformation(Sprite toy, Sprite franimal, string toyN, string franN, SpawnLocation location)
    {
        this.gameObject.SetActive(true);
        LocationName.text = location.gameObject.name;
        ToyName.text = toyN;
        FranimalName.text = franN;
        ToyImage.sprite = toy;
        FranimalImage.sprite = franimal;
    }
}
