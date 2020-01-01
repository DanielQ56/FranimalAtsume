using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Toy", menuName = "Toy")]
public class Toy : ScriptableObject
{
    public string toy;
    public string toyDescription;
    public int cost;
    public Sprite toySprite;
    public AnimalSpecies attractedSpecies;
    public AnimalLocation placeableLocation;
    public bool hasBeenPlaced = false;
}
